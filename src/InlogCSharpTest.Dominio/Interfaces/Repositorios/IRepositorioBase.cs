using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InlogCSharpTest.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        void Adicionar(T obj);

        void Alterar(T obj);

        void Remover(T obj);

        T BuscarPorId(int id);

        IEnumerable<T> BuscarTodos();

        IEnumerable<T> BuscarTodos(Expression<Func<T, bool>> predicado);

        IQueryable<T> Consulta();

        bool Salvar();

        void Dispose();
    }
}
