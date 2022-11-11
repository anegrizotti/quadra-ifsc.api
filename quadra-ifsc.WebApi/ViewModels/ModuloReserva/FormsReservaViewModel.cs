using System;
using System.ComponentModel.DataAnnotations;

namespace quadra_ifsc.WebApi.ViewModels.ModuloReserva
{
    public class FormsReservaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public TimeSpan HoraTermino { get; set; }
    }
}
