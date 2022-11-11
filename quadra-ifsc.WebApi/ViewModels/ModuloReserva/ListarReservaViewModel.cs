using System;

namespace quadra_ifsc.WebApi.ViewModels.ModuloReserva
{
    public class ListarReservaViewModel
    {
        public Guid Id { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraTermino { get; set; }
    }
}
