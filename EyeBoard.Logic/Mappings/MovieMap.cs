using EyeBoard.Logic.Models;
using FluentNHibernate.Mapping;

namespace EyeBoard.Logic.Mappings
{
    public class MovieMap : SubclassMap<Movie>
    {
        public MovieMap()
        {
            DiscriminatorValue(1);
        }
    }
}
