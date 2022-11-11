using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using quadra_ifsc.Dominio.ModuloReserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quadra_ifsc.Orm.ModuloReserva
{
    public class MapeadorReservaOrm : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("TBReserva");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.HoraInicio).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.HoraTermino).HasColumnType("bigint").IsRequired();

            builder.HasOne(x => x.Usuario)
               .WithMany()
               .IsRequired(false)
               .HasForeignKey(x => x.UsuarioId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
