using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Table("Categories");

            Id(x => x.Id).GeneratedBy.Guid();

            Map(x => x.State);
            Map(x => x.Created);
            Map(x => x.CreatedBy);
            Map(x => x.Modified);
            Map(x => x.ModifiedBy);

            Map(x => x.Title);
            Map(x => x.Model);
        }
    }
}
