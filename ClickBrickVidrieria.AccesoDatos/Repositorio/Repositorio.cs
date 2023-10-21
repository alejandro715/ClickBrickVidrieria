using System;
using System.Collections.Generic;
using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ClickBrickVidrieria.AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;

namespace ClickBrickVidrieria.AccesoDatos.Repositorio
{
    public class Repositorio<T> : iRepositorio<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;

        public Repositorio(ApplicationDbContext db)
        {

            _db = db;
            this.dbset = db.Set<T>();

        }
        public async Task Agregar(T entidad)
        {
            await dbset.AddAsync(entidad); //insertar en tabla
        }

        public async Task<T> Obtener(int Id)
        {
            return await dbset.FindAsync(Id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
        IQueryable<T> query = dbset;

        if(filtro != null)
            {

                query = query.Where(filtro);

            }
        if(incluirPropiedades != null)
            {

                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //emeplo categoria.marca
                }

            }
        if(orderBy !=null)
            {
                query = orderBy(query);

            }
        if(!isTracking)
            {

                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {

            IQueryable<T> query = dbset;

            if (filtro != null)
            {

                query = query.Where(filtro);

            }
            if (incluirPropiedades != null)
            {

                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //emeplo categoria.marca
                }

            }

            if (!isTracking)
            {

                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public void Remover(T entidad)
        {

            dbset.Remove(entidad);

        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbset.RemoveRange(entidad);
        }
    }
}
