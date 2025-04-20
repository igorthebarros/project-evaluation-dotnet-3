using Microsoft.AspNetCore.Builder;

namespace IoC.Interfaces
{
    public interface IModuleInitializer
    {
        void Initialize(WebApplicationBuilder builder);
    }
}
