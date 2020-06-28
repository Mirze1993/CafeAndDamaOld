using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebApiTutorial.Inistaller
{
    interface IInistaller
    {
        void Inistaller(IServiceCollection services, IConfiguration configuration);
    }
}
