using System;

namespace quadra_ifsc.WebApi.ViewModels.ModuloReserva
{
    public class VisualizarReservaViewModel
    {
        public Guid Id { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraTermino { get; set; }
    }
}
