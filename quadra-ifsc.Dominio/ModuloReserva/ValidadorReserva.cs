using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quadra_ifsc.Dominio.ModuloReserva
{
    public class ValidadorReserva : AbstractValidator<Reserva>
    {
        public ValidadorReserva()
        {
            RuleFor(x => x.Data)
                .NotNull().NotEmpty();

            RuleFor(x => x.HoraInicio)
                .NotNull().NotEmpty();

            RuleFor(x => x.HoraTermino)
                .NotNull().NotEmpty();
        }
    }
}
