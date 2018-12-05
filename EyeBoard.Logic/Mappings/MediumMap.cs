
using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;

namespace EyeBoard.Logic.Mappings
{
    public class MediumMap : ClassMap<Medium>
    {
        public MediumMap()
        {
            Table("Media");
            
            Id(x => x.Id).GeneratedBy.Guid();
            
            Map(x => x.State);
            Map(x => x.Created);
            Map(x => x.CreatedBy);
            Map(x => x.Modified);
            Map(x => x.ModifiedBy);

            Map(x => x.Title);
            Map(x => x.PublishUp);
            Map(x => x.PublishDown);
            Map(x => x.Ordering);
            Map(x => x.Url);

            DiscriminateSubClassesOnColumn("MediumType");

            HasManyToMany(x => x.Groups)
                .Cascade.SaveUpdate()
                .LazyLoad().Not
                .Inverse()
                .Table("GroupMedia");
        }
    }
}
