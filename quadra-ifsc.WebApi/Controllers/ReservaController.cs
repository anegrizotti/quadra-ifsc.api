using AutoMapper;
using eAgenda.Webapi.Controllers.Compartilhado;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using quadra_ifsc.Aplicacao.ModuloReserva;
using quadra_ifsc.Dominio.ModuloReserva;
using quadra_ifsc.WebApi.ViewModels.ModuloReserva;
using System;
using System.Collections.Generic;

namespace quadra_ifsc.WebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/reservas")]
    [ApiController]
    [Authorize]
    public class ReservaController : QuadraIfscControllerBase
    {
        private readonly ServicoReserva servicoReserva;
        private readonly IMapper mapeadorReservas;

        public ReservaController(ServicoReserva servicoReserva, IMapper mapeadorReservas)
        {
            this.servicoReserva = servicoReserva;
            this.mapeadorReservas = mapeadorReservas;
        }

        [HttpGet]
        public ActionResult<List<ListarReservaViewModel>> SelecionarTodos()
        {
            var reservaResult = servicoReserva.SelecionarTodos();

            if (reservaResult.IsFailed)
                return InternalError(reservaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorReservas.Map<List<ListarReservaViewModel>>(reservaResult.Value)
            });
        }

        [HttpGet("visualizacao-completa/{id:guid}")]
        public ActionResult<ListarReservaViewModel> SelecionarReservaCompletaPorId(Guid id)
        {
            var reservaResult = servicoReserva.SelecionarPorId(id);

            if (reservaResult.IsFailed && RegistroNaoEncontrado(reservaResult))
                return NotFound(reservaResult);

            if (reservaResult.IsFailed)
                return InternalError(reservaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorReservas.Map<VisualizarReservaViewModel>(reservaResult.Value)
            });
        }

        [HttpGet("{id:guid}")]
        public ActionResult<FormsReservaViewModel> SelecionarReservaPorId(Guid id)
        {
            var reservaResult = servicoReserva.SelecionarPorId(id);

            if (reservaResult.IsFailed && RegistroNaoEncontrado(reservaResult))
                return NotFound(reservaResult);

            if (reservaResult.IsFailed)
                return InternalError(reservaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorReservas.Map<FormsReservaViewModel>(reservaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsReservaViewModel> Inserir(FormsReservaViewModel reservaVM)
        {
            var reserva = mapeadorReservas.Map<Reserva>(reservaVM);

            var reservaResult = servicoReserva.Inserir(reserva);

            if (reservaResult.IsFailed)
                return InternalError(reservaResult);

            return Ok(new
            {
                sucesso = true,
                dados = reservaVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsReservaViewModel> Editar(Guid id, FormsReservaViewModel reservaVM)
        {
            var reservaResult = servicoReserva.SelecionarPorId(id);

            if (reservaResult.IsFailed && RegistroNaoEncontrado(reservaResult))
                return NotFound(reservaResult);

            var reserva = mapeadorReservas.Map(reservaVM, reservaResult.Value);

            reservaResult = servicoReserva.Editar(reserva);

            if (reservaResult.IsFailed)
                return InternalError(reservaResult);

            return Ok(new
            {
                sucesso = true,
                dados = reservaVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var reservaResult = servicoReserva.Excluir(id);

            if (reservaResult.IsFailed && RegistroNaoEncontrado<Reserva>(reservaResult))
                return NotFound<Reserva>(reservaResult);

            if (reservaResult.IsFailed)
                return InternalError<Reserva>(reservaResult);

            return NoContent();
        }
    }
}
