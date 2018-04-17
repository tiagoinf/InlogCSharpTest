using System.Linq.Expressions;
using System.Linq;
using InlogCSharpTest.Dominio.Entidades;
using InlogCSharpTest.Dominio.Exceptions;
using InlogCSharpTest.Dominio.Interfaces.Repositorios;

namespace InlogCSharpTest.Infra.Data.Repositorios
{
    public class VeiculoRepositorio : RepositorioBase<Veiculo>, IVeiculoRepositorio
    {
        public VeiculoRepositorio(InlogCSharpTestContext context) 
            : base(context)
        {
            
        }

        public Veiculo BuscarPorChassi(string chassi)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(chassi))
                {
                    throw new CustomException() { MensagemAmigavel = "O parâmetro chassi não pode ser vazio" };
                }

                var veiculo = _context.Veiculos.FirstOrDefault(s => s.Chassi == chassi);
                return veiculo;
                   
            }
            catch { throw; }
        }
    }
}
