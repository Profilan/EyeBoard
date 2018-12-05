using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;

namespace EyeBoard.Logic.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.State);
            Map(x => x.Created);
            Map(x => x.CreatedBy);
            Map(x => x.Modified);
            Map(x => x.ModifiedBy);

            Map(x => x.UserName);
            Map(x => x.DisplayName);
            Map(x => x.HashedPassword).Column("Password");
            Map(x => x.Enabled);

            HasManyToMany(x => x.Roles)
                .Cascade.SaveUpdate()
                .LazyLoad().Not
                .Table("UserRoles");
        }
    }
}
