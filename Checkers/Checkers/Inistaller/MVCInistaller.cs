using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkers.Inistaller
{
    public class MVCInistaller : Iinistallere
    {
        public void InistallerServer(IServiceCollection services, IConfiguration config)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}
