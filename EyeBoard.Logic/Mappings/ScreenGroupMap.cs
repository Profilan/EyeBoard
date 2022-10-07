using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;

namespace EyeBoard.Logic.Mappings
{
    public class ScreenGroupMap : ClassMap<ScreenGroup>
    {
        public ScreenGroupMap()
        {
            Table("ScreenGroups");

            Id(x => x.Id).GeneratedBy.Guid();

            Map(x => x.State);
            Map(x => x.Created);
            Map(x => x.CreatedBy);
            Map(x => x.Modified);
            Map(x => x.ModifiedBy);

            Map(x => x.Title);
            Map(x => x.Theme).Nullable();

            HasMany(x => x.Screens)
                .Cascade.SaveUpdate()
                .KeyColumn("GroupId")
                .LazyLoad().Not
                .Inverse();

            HasManyToMany(x => x.Media)
                .Cascade.SaveUpdate()
                .LazyLoad().Not
                .Table("GroupMedia");

            HasManyToMany(x => x.Notifications)
                .Cascade.SaveUpdate()
                .LazyLoad().Not
                .Table("GroupNotifications");
        }
    }
}
