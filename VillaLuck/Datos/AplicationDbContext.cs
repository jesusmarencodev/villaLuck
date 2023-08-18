using Microsoft.EntityFrameworkCore;
using VillaLuck.Modelos;

namespace VillaLuck.Datos
{
    public class AplicationDbContext:DbContext
    {
        public DbSet<Villa> Villas { get; set; }
        public DbSet<NumeroVilla> NumeroVillas { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Nombre = "Villa 1",
                    Detalle = "Detalle Villa 1",
                    ImagenUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 200,
                    Amenidad="",
                    Tarifa = 200,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                },
                new Villa()
                {
                    Id = 2,
                    Nombre = "Villa 2",
                    Detalle = "Detalle Villa 2",
                    ImagenUrl = "",
                    Ocupantes = 7,
                    MetrosCuadrados = 300,
                    Amenidad = "",
                    Tarifa = 300,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                }
            );
        }
    }
}
