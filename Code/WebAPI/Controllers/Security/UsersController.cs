using Helpers;
using Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTO;
using Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/Users")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IApplicationUserService _applicationUserService;

        private readonly INotificationService _notificationService;
        private SecurityHelper _securityHelper;
        private readonly IUserMapper _userMapper;
        private readonly IConfiguration _configuration;
        private readonly IDocumentService _documentService;
        public UsersController(IUserService UserService, IApplicationUserService ApplicationUserService, INotificationService notificationService, SecurityHelper securityHelper, IUserMapper userMapper, IConfiguration configuration,
            IDocumentService documentService)
        {
            _userService = UserService;
            _notificationService = notificationService;
            _securityHelper = securityHelper;
            _userMapper = userMapper;
            _configuration = configuration;
            _applicationUserService = ApplicationUserService;
            _documentService = documentService;
        }


        //// GET api/Users
        //public IActionResult GetUsers()
        //{
        //    ResultDTO result = new ResultDTO();
        //    List<User> users = _userService.GetAll();
        //    result.Results = users;
        //    return Ok(result);

        //}

        // GET api/Users/1/10
        [HttpGet("{PageNumber}/{PageSize}/{SortBy}/{SortDirection}")]
        public PagedResult<User> GetUsers(int PageNumber, int PageSize, string SortBy = "", string SortDirection = "")
        {
            return _userService.GetAll(PageNumber, PageSize, SortBy, SortDirection);
        }
        // GET api/Users/5
        //[HttpGet("{id}")]
        //public IActionResult GetUser(int id)
        //{
        //    ResultDTO result = new ResultDTO();
        //    User user = _userService.GetUser(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    result.Results = user;
        //    return Ok(result);
        //}

        //// PUT api/Users/5
        //[HttpPut("{id}")]
        //public IActionResult PutUser(int id, [FromBody] User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != user.UserID)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        User _user = _userService.GetUser(id);
        //        if (user.UserPassword != _user.UserPassword)
        //        {
        //            user.UserPassword = _securityHelper.Md5Encryption(user.UserPassword);
        //        }
        //        //  user.Role = null;
        //        _userService.UpdateUser(user);
        //        _userService.SaveUser();
        //    }
        //    catch (Exception ex)
        //    {
        //        Task.Run(() =>
        //        {
        //            ILogger logger = LoggerFactory.CreateLogger();
        //            logger.Error(ex);
        //        });
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode((int)HttpStatusCode.NoContent);
        //}

        //// POST api/Users
        //[HttpPost]
        //public IActionResult PostUser([FromBody] UserDTO userDTO, string lang = "ar")
        //{
        //    ResultDTO result = new ResultDTO();
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    userDTO.UserPassword = _securityHelper.Md5Encryption(userDTO.UserPassword);
        //    User user = _userMapper.MapUser(userDTO);
        //    _userService.CreateUser(user);
        //    _userService.SaveUser();
        //    ApplicationUser applicationUser = new ApplicationUser();
        //    applicationUser.UserID = user.UserID;
        //    applicationUser.ApplicationID = _securityHelper.getAppIDFromToken();
        //    _applicationUserService.CreateApplicationUser(applicationUser);
        //    _applicationUserService.SaveApplicationUser();
        //    result.Results = user;
        //    if (ConfigurationHelper.EnableNotifications)
        //    { 
        //        _notificationService.SendRegisterationNotification(user.UserID, lang);
        //    }
        //    return Ok(result);
        //}

        //// DELETE api/Users/5
        //[HttpDelete("{id}")]
        //public IActionResult DeleteUser(int id)
        //{
        //    ResultDTO result = new ResultDTO();
        //    User user = _userService.GetUser(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _userService.DeleteUser(user);
        //    _userService.SaveUser();
        //    result.Results = user;
        //    return Ok(result);
        //}

        private bool UserExists(int id)
        {
            User user = _userService.GetUser(id);
            return user != null;
        }

        // GET api/Users/CurrentUser
        [HttpGet("CurrentUser")]
        public IActionResult GetCurrentUser()
        {
            ResultDTO result = new ResultDTO();
            int id = _securityHelper.getUserIDFromToken();
            User user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            result.Results = user;
            return Ok(result);
        }

        // POST api/Users/FilteredList"
        [HttpPost("FilteredList")]
        public PagedResult<User> LoadFilteredUsers([FromBody] FilterModel<User> FilterObject)
        {
            //if no search is applied
            if (FilterObject.SearchObject == null)
            {
                FilterObject.SearchObject = new User();
            }
            return _userService.GetAll(FilterObject);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IActionResult Post([FromBody] LoginDTO loginModel)
        {
            if (ModelState.IsValid)
            {
                Models.User User = _userService.UserLogin(loginModel.UserName, _securityHelper.getAppIDFromToken());
                ApplicationUser ApplicationUser = _applicationUserService.GetApplicationUserByUserID(User.UserID);
                // default rerurn url
                string returnURL = ApplicationUser.Application.ReturnURL;
                // set return url from document if appsetting not apply application return url
                if (!ConfigurationHelper.ApplicationReturnURL && loginModel.DocumentID != 0)
                {
                    Document document = _documentService.GetDocument(loginModel.DocumentID);
                    returnURL = document?.ReturnURL;
                }

                if (User != null)
                {
                    var claims = new[]
                    {
                        new Claim("AppId", ApplicationUser.ApplicationID.ToString()),
                        new Claim("UserId", User.UserID.ToString()),
                        new Claim("DocumentId",loginModel.DocumentID.ToString()),
                        new Claim("ReturnURL", returnURL??string.Empty),
                        new Claim("CallbackURL", ApplicationUser.Application.CallbackURL),
                        new Claim("UserEmail", User.UserName)


                    };
                    int tokenExpiration;
                    int.TryParse(_configuration["TokenExpiration"], out tokenExpiration);

                    JwtSecurityToken token = new JwtSecurityToken
                    (
                        issuer: _configuration["Issuer"],
                        audience: _configuration["Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(tokenExpiration),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),
                             SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

                }
                else
                {
                    return Unauthorized();
                }


            }

            return BadRequest();
        }
        
        [HttpPost("UpdateByEmail")]
        public IActionResult UpdateByEmail([FromBody] UpdateUserDTO updateUserDTO)
        {
            int appId = _securityHelper.getAppIDFromToken();

            try
            {
                _userService.UpdateUserByEmail(appId, updateUserDTO);
            }
            catch (Exception ex)
            {
                Task.Run(() =>
                {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
            }

            return Ok();
        }
    }
}