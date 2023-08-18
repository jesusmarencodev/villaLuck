using VillaLuck.Datos;
using VillaLuck.Modelos;
using VillaLuck.Repositorio.IRepositorio;

namespace VillaLuck.Repositorio
{
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla>, IVillaNumeroRepositorio
    {
        private readonly AplicationDbContext _db;
        public NumeroVillaRepositorio(AplicationDbContext db):base(db)// com base pasamos el db al padre
        {
            _db = db;
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.NumeroVillas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
