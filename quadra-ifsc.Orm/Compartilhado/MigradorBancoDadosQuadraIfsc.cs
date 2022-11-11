using Microsoft.EntityFrameworkCore;
using quadra_ifsc.Configs;
using System.Linq;

namespace quadra_ifsc.Orm
{
    public static class MigradorBancoDadosQuadraIfsc
    {
        public static void AtualizarBancoDados()
        {
            var config = new ConfiguracaoAplicacaoQuadraIfsc();

            var db = new QuadraIfscDbContext(config.ConnectionStrings);

            var migracoesPendentes = db.Database.GetPendingMigrations();

            if (migracoesPendentes.Count() > 0)
                db.Database.Migrate();
        }
    }
}
