using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;

namespace EyeBoard.Logic.Mappings
{
    public class ConversionTaskMap : ClassMap<ConversionTask>
    {
        public ConversionTaskMap()
        {
            Table("Tasks");

            Id(x => x.Id).GeneratedBy.Guid();

            Map(x => x.InputFile);
            Map(x => x.OutputFile);
            Map(x => x.OriginalFile);
            Map(x => x.TaskType).CustomType<TaskType>();
            Map(x => x.Created);
            Map(x => x.Active);
        }
    }
}
