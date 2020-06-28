using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTutorial.Inistaller
{
    public static class InistallerExtension
    {
        public static void Inistaller(this IServiceCollection services, IConfiguration configuration)
        {
            var ınistallers= typeof(Startup).Assembly.ExportedTypes.Where(x =>
            typeof(IInistaller).IsAssignableFrom(x)&&x.IsClass&&!x.IsAbstract&&!x.IsInterface
            ).Select(x=>Activator.CreateInstance(x)).Cast<IInistaller>().ToList();

            ınistallers.ForEach(x => x.Inistaller(services, configuration));
        }
    }
}
