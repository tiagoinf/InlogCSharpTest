using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InlogCSharpTest.Dominio.Interfaces.Repositorios;
using System.Linq.Expressions;

namespace InlogCSharpTest.Infra.Data.Repositorios
{
    public class RepositorioBase<T> : IDisposable, IRepositorioBase<T> where T : class
    {
        protected readonly InlogCSharpTestContext _context;
        protected readonly DbSet<T> _DbSet;

        public RepositorioBase(InlogCSharpTestContext context)
        {
            _context = context;
            _DbSet = context.Set<T>();
        }

        public virtual void Adicionar(T obj)
        {
            _DbSet.Add(obj);
        }

        public virtual void Alterar(T obj)
        {
            _DbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public virtual void Remover(T obj)
        {
            if (_context.Entry(obj).State == EntityState.Detached)
            {
                _DbSet.Attach(obj);
            }
            _DbSet.Remove(obj);
        }

        public virtual T BuscarPorId(int id)
        {
            return _DbSet.Find(id);
        }

        public virtual IEnumerable<T> BuscarTodos()
        {
            return _DbSet.ToList();
        }

        public virtual IEnumerable<T> BuscarTodos(Expression<Func<T, bool>> predicado)
        {
            return _DbSet.Where(predicado).ToList();
        }

        public virtual IQueryable<T> Consulta()
        {
            IQueryable<T> query = _DbSet;

            return query;
        }

        public bool Salvar()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
