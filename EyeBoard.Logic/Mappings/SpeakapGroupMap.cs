using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Mappings
{
    public class SpeakapGroupMap : ClassMap<SpeakapGroup>
    {
        public SpeakapGroupMap()
        {
            Table("SpeakapGroups");

            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Enabled);
        }
    }
}
