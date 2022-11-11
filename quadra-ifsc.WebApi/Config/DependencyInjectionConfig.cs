using eAgenda.Dominio;
using Microsoft.Extensions.DependencyInjection;
using quadra_ifsc.Aplicacao.ModuloAutenticacao;
using quadra_ifsc.Aplicacao.ModuloReserva;
using quadra_ifsc.Configs;
using quadra_ifsc.Dominio.ModuloReserva;
using quadra_ifsc.Orm;
using quadra_ifsc.Orm.ModuloReserva;

namespace quadra_ifsc.WebApi.Config
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services)
        {
            services.AddSingleton((x) => new ConfiguracaoAplicacaoQuadraIfsc().ConnectionStrings);

            services.AddScoped<QuadraIfscDbContext>();

            services.AddScoped<IContextoPersistencia, QuadraIfscDbContext>();

            services.AddScoped<IRepositorioReserva, RepositorioReservaOrm>();
            services.AddTransient<ServicoReserva>();
        }
    }
}