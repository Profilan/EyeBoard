using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;

namespace EyeBoard.Logic.Mappings
{
    public class PresentationMap : SubclassMap<Presentation>
    {
        public PresentationMap()
        {
            DiscriminatorValue(2);
        }
    }
}
