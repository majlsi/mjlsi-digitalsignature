using System;
using System.IO;
using System.Text;
using Data;
using Data.Infrastructure;
using Helpers;
using Loggers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebHostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRazorPages(); 

            //configure Security Helper
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<SecurityHelper, SecurityHelper>();
            services.AddTransient<CodeGenerationHelper, CodeGenerationHelper>();
            services.AddTransient<PdfHelper, PdfHelper>();
            services.AddTransient<ControlsHelper, ControlsHelper>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(jwtBearerOptions =>
                    {
                        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateActor = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Issuer"],
                            ValidAudience = Configuration["Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]))
                        };
                    });

            services.AddControllers()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            //Add Cross Origins Policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            //to be used in migration only
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<DBEntities>( // replace "YourDbContext" with the class name of your DbContext
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            //General
            services.AddScoped<DbContext, DBEntities>();
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
 
            RazorBootStrapper.Init(WebHostEnvironment);
            ConfigureService.RegisterRepositories(services);
            ConfigureService.RegisterServices(services);
            ConfigureService.RegisterMappers(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, DbContext dbContext)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {


                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/html";

                    await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                    await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    // Use exceptionHandlerPathFeature to process the exception (for example, 
                    // logging), but do NOT expose sensitive error information directly to 
                    // the client.
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(exceptionHandlerPathFeature?.Error);
                    if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                    {
                        await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
                    }

                    await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                    await context.Response.WriteAsync("</body></html>\r\n");
                    await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                });
            });
            bool stopHTTPConfiguration = false;
            bool.TryParse(Configuration["StopHTTPConfiguration"], out stopHTTPConfiguration);
            if (!stopHTTPConfiguration)
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
            //get auto migration configuration
            bool IsAutoMigration = false;
            string IsAutoMigrationConfig = Configuration["IsAutoMigration"];
            if (!string.IsNullOrEmpty(IsAutoMigrationConfig))
            {
                IsAutoMigration = Convert.ToBoolean(IsAutoMigrationConfig);
            }
            if (IsAutoMigration)
            {
                dbContext.Database.Migrate();
            }

            ConfigurationHelper.Configure(serviceProvider.GetService<IConfiguration>());
            HostEnvironmentHelper.Configure(serviceProvider.GetService<IHostEnvironment>());

        }
    }
}
