using eAgenda.Dominio;
using Microsoft.EntityFrameworkCore;
using quadra_ifsc.Dominio.ModuloReserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quadra_ifsc.Orm.ModuloReserva
{
    public class RepositorioReservaOrm : IRepositorioReserva
    {
        private DbSet<Reserva> reservas;
        private readonly QuadraIfscDbContext dbContext;

        public RepositorioReservaOrm(IContextoPersistencia contextoPersistencia)
        {
            dbContext = (QuadraIfscDbContext)contextoPersistencia;
            reservas = dbContext.Set<Reserva>();
        }

        public void Inserir(Reserva novoRegistro)
        {
            reservas.Add(novoRegistro);
        }

        public void Editar(Reserva registro)
        {
            reservas.Update(registro);
        }

        public void Excluir(Reserva registro)
        {
            reservas.Remove(registro);
        }

        public Reserva SelecionarPorId(Guid id)
        {
            return reservas
                .SingleOrDefault(x => x.Id == id);
        }

        public List<Reserva> SelecionarTodos()
        {
            return reservas.ToList();
        }
    }
}
