using Profilan.SharedKernel;
using StructureMap;

namespace EyeBoard.Service.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                });
        }
    }
}
