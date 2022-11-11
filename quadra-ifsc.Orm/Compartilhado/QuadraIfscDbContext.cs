using eAgenda.Dominio;
using eAgenda.Dominio.ModuloAutenticacao;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using quadra_ifsc.Configs;
using Serilog;
using System;
using System.Linq;
using System.Reflection;

namespace quadra_ifsc.Orm
{
    public class QuadraIfscDbContext :
        IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>, IContextoPersistencia
    {
        private string connectionString;

        public QuadraIfscDbContext(ConnectionStrings connectionStrings)
        {
            this.connectionString = connectionStrings.SqlServer;
        }

        public void GravarDados()
        {
            SaveChanges();
        }

        public void DesfazerAlteracoes()
        {
            var registrosAlterados = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList();

            foreach (var registro in registrosAlterados)
            {
                switch (registro.State)
                {
                    case EntityState.Added:
                        registro.State = EntityState.Detached;
                        break;

                    case EntityState.Deleted:
                        registro.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        registro.State = EntityState.Unchanged;
                        registro.CurrentValues.SetValues(registro.OriginalValues);
                        break;

                    default:
                        break;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);

            ILoggerFactory loggerFactory = LoggerFactory.Create((x) =>
            {
                x.AddSerilog(Log.Logger);//instalar o pacote Serilog.Extensions.Logging
            });

            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Type tipo = typeof(QuadraIfscDbContext);

            Assembly dllComConfiguracoesOrm = tipo.Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(dllComConfiguracoesOrm);

            base.OnModelCreating(modelBuilder);
        }
    }
}
