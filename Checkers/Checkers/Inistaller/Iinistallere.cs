using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkers.Inistaller
{
    interface Iinistallere
    {
        void InistallerServer(IServiceCollection services, IConfiguration config);
    }
}
