using Microsoft.EntityFrameworkCore.Design;
using quadra_ifsc.Configs;

namespace quadra_ifsc.Orm
{
    public class QuadraIfscDbContextFactory : IDesignTimeDbContextFactory<QuadraIfscDbContext>
    {
        public QuadraIfscDbContext CreateDbContext(string[] args)
        {
            var config = new ConfiguracaoAplicacaoQuadraIfsc();

            return new QuadraIfscDbContext(config.ConnectionStrings);
        }
    }
}
