
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Collections.Generic;


namespace Data.Configuration
{
    public class ActionTypeConfiguration : IEntityTypeConfiguration<ActionType>
    {
 

        public void Configure(EntityTypeBuilder<ActionType> builder)
        {
            builder.Property(a => a.ActionTypeName).HasMaxLength(255);
            builder.ToTable("action_types");
            List<ActionType> ActionTypes = new List<ActionType>();

            ActionTypes.Add(new ActionType
            {
                ActionTypeID = 1,
                ActionTypeName = "Viewed"
           
            });
            ActionTypes.Add(new ActionType
            {
                ActionTypeID = 2,
                ActionTypeName = "Signed"
             
            });

            builder.HasData(ActionTypes.ToArray());
        }
    }
}
