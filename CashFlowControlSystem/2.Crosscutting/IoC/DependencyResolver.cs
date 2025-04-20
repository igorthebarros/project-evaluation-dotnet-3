using IoC.ModuleInitializers;
using Microsoft.AspNetCore.Builder;

namespace IoC
{
    public static class DependencyResolver
    {
        public static void RegisterDependencies(this WebApplicationBuilder builder)
        {
            new SharedKernelModuleInitializer().Initialize(builder);
            new InfrastructureModuleInitializer().Initialize(builder);
        }
    }
}
