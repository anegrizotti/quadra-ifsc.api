using eAgenda.Aplicacao;
using eAgenda.Dominio;
using FluentResults;
using quadra_ifsc.Dominio.ModuloReserva;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quadra_ifsc.Aplicacao.ModuloReserva
{
    public class ServicoReserva : ServicoBase<Reserva, ValidadorReserva>
    {
        private IRepositorioReserva repositorioReserva;
        private IContextoPersistencia contextoPersistencia;

        public ServicoReserva(IRepositorioReserva repositorioReserva,
                             IContextoPersistencia contexto)
        {
            this.repositorioReserva = repositorioReserva;
            this.contextoPersistencia = contexto;
        }

        public Result<Reserva> Inserir(Reserva reserva)
        {
            Log.Logger.Debug("Tentando inserir reserva... {@r}", reserva);

            Result resultado = Validar(reserva);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioReserva.Inserir(reserva);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Reserva {ReservaId} inserida com sucesso", reserva.Id);

                return Result.Ok(reserva);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar inserir a reserva";

                Log.Logger.Error(ex, msgErro + " {ReservaId} ", reserva.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Reserva> Editar(Reserva reserva)
        {
            Log.Logger.Debug("Tentando editar reserva... {@r}", reserva);

            var resultado = Validar(reserva);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioReserva.Editar(reserva);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Reserva {ReservaId} editada com sucesso", reserva.Id);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar editar a reserva";

                Log.Logger.Error(ex, msgErro + " {ReservaId}", reserva.Id);

                return Result.Fail(msgErro);
            }

            return Result.Ok(reserva);
        }

        public Result Excluir(Guid id)
        {
            var reservaResult = SelecionarPorId(id);

            if (reservaResult.IsSuccess)
                return Excluir(reservaResult.Value);

            return Result.Fail(reservaResult.Errors);
        }

        public Result Excluir(Reserva reserva)
        {
            Log.Logger.Debug("Tentando excluir reserva... {@r}", reserva);

            try
            {
                repositorioReserva.Excluir(reserva);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Reserva {ReservaId} editada com sucesso", reserva.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar excluir a reserva";

                Log.Logger.Error(ex, msgErro + " {ReservaId}", reserva.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Reserva>> SelecionarTodos()
        {
            Log.Logger.Debug("Tentando selecionar reservas...");

            try
            {
                var reservas = repositorioReserva.SelecionarTodos();

                Log.Logger.Information("Reservas selecionadas com sucesso");

                return Result.Ok(reservas);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todas as reservas";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        public Result<Reserva> SelecionarPorId(Guid id)
        {
            Log.Logger.Debug("Tentando selecionar reserva {ReservaId}...", id);

            try
            {
                var reserva = repositorioReserva.SelecionarPorId(id);

                if (reserva == null)
                {
                    Log.Logger.Warning("Reserva {ReservaId} não encontrada", id);

                    return Result.Fail("Reserva não encontrada");
                }

                Log.Logger.Information("Reserva {ReservaId} selecionada com sucesso", id);

                return Result.Ok(reserva);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar a reserva";

                Log.Logger.Error(ex, msgErro + " {ReservaId}", id);

                return Result.Fail(msgErro);
            }
        }


    }
}
