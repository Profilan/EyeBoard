using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Mappings
{
    public class TaskMap : ClassMap<EyeBoard.Logic.Models.Task>
    {
        public TaskMap()
        {
            Table("Tasks");

            Id(x => x.Id).GeneratedBy.Guid();

            Map(x => x.InputFile);
            Map(x => x.OutputFile);
            Map(x => x.OriginalFile);
            Map(x => x.TaskType).CustomType<TaskType>();
            Map(x => x.Active);
        }
    }
}
