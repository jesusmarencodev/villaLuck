using VillaLuck.Datos;
using VillaLuck.Modelos;
using VillaLuck.Repositorio.IRepositorio;

namespace VillaLuck.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly AplicationDbContext _db;
        public VillaRepositorio(AplicationDbContext db):base(db)// com base pasamos el db al padre
        {
            _db = db;
        }

        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
