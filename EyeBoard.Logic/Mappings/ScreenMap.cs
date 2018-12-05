using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;

namespace EyeBoard.Logic.Mappings
{
    public class ScreenMap : ClassMap<Screen>
    {
        public ScreenMap()
        {
            Table("Screens");

            Id(x => x.Id).GeneratedBy.Guid();

            Map(x => x.State);
            Map(x => x.Created);
            Map(x => x.CreatedBy);
            Map(x => x.Modified);
            Map(x => x.ModifiedBy);

            Map(x => x.Title);
            Map(x => x.Location);
            Map(x => x.HostName);

            References(x => x.Group).Column("GroupId").LazyLoad().Not.Cascade.SaveUpdate();

        }
    }
}
