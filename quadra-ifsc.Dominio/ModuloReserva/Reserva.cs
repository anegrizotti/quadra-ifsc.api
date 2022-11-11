using eAgenda.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quadra_ifsc.Dominio.ModuloReserva
{
    public class Reserva : EntidadeBase<Reserva>
    {
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Reserva reserva &&
                   Data == reserva.Data &&
                   HoraInicio.Equals(reserva.HoraInicio) &&
                   HoraTermino.Equals(reserva.HoraTermino);
        }

        public override void Atualizar(Reserva registro)
        {
            Id = registro.Id;
            Data = registro.Data;
            HoraInicio = registro.HoraInicio;
            HoraTermino = registro.HoraTermino;
        }

        public Reserva(DateTime data, TimeSpan horaInicio, TimeSpan horaTermino)
        {
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
        }

        public Reserva()
        {

        }
    }
}
