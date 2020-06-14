using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkers.Inistaller
{
    public static class InistallerExtension
    {
        public static void  InistallerConfig(this IServiceCollection s, IConfiguration config)
        {
            var t = typeof(Startup).Assembly.GetExportedTypes().Where(
               x => typeof(Iinistallere).IsAssignableFrom(x) && x.IsClass
               ).Select(Activator.CreateInstance).Cast<Iinistallere>().ToList();

            t.ForEach(x => x.InistallerServer(s, config));
        }
    }
}
