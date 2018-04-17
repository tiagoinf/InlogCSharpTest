using InlogCSharpTest.Dominio.Entidades;

namespace InlogCSharpTest.Dominio.Interfaces.Repositorios
{
    public interface IVeiculoRepositorio : IRepositorioBase<Veiculo>
    {
        Veiculo BuscarPorChassi(string chassi);
    }
}
