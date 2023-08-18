

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaLuck.Datos;
using VillaLuck.Repositorio.IRepositorio;

namespace VillaLuck.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly AplicationDbContext _db;
        //se usa para transformar a entidades
        internal DbSet<T> dbSet;

        public Repositorio(AplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();//aqui transformacmos la sentidades
        }

        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool traked = true)
        {
            IQueryable<T> query = dbSet;
            if (!traked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query.ToListAsync();
        }

        public async Task Remover(T endidad)
        {
           dbSet.Remove(endidad);
            await Grabar();
        }
    }
}
