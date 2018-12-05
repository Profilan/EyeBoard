using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;

namespace EyeBoard.Logic.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("Roles");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);
        }
    }
}
