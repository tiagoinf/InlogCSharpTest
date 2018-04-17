using System;
using System.Linq;
using InlogCSharpTest.Dominio.Entidades;
using InlogCSharpTest.Dominio.Interfaces.Repositorios;
using InlogCSharpTest.Infra.Data;
using InlogCSharpTest.Infra.Data.Repositorios;
using InlogCSharpTest.Dominio.Exceptions;

namespace InlogCSharpTest.App
{
    public class Program
    {
        private static InlogCSharpTestContext _contexto;
        private static IVeiculoRepositorio _veiculoRepositorio;

        public static void Main(string[] args)
        {
            _contexto = new InlogCSharpTestContext();
            _veiculoRepositorio = new VeiculoRepositorio(_contexto);

            Console.Title = "Gestão de Frota 1.0";

            TelaMenu();
        }

        private static void TelaMenu()
        {
            Console.WriteLine("Digite uma opção e tecle enter");
            Console.WriteLine("");
            Console.WriteLine("1: Inserir um veículo");
            Console.WriteLine("2: Editar um veículo existente");
            Console.WriteLine("3: Deletar um veículo existente");
            Console.WriteLine("4: Listar todos os veículos");
            Console.WriteLine("5: Encontrar veículo por Chassi");
            Console.WriteLine("9: Sair");

            Console.WriteLine("");
            Console.Write("Opção selecionada: ");
            var opcao = Console.ReadLine();

            int opcaoSelecionada = 0;
            while (!int.TryParse(opcao, out opcaoSelecionada))
            {
                Console.Write("Informe apenas números para a opção: ");
                opcao = Console.ReadLine();
            }

            switch (opcaoSelecionada)
            {
                case 1:
                    Console.Clear();
                    TelaInserirVeiculo();
                    break;
                case 2:
                    Console.Clear();
                    TelaEditarVeiculo();
                    break;
                case 3:
                    Console.Clear();
                    TelaExcluirVeiculo();
                    break;
                case 4:
                    Console.Clear();
                    TelaListarVeiculos();
                    break;
                case 5:
                    Console.Clear();
                    TelaPesquisarVeiculo();
                    break;
                case 9:
                    Console.Clear();
                    TelaSair();
                    break;
                default:
                    Console.WriteLine("");
                    Console.Write("Opção não encontrada. Pressone Enter para continuar");
                    Console.ReadLine();
                    Console.Clear();
                    TelaMenu();
                    break;
            }

        }

        private static void TelaInserirVeiculo()
        {
            Console.WriteLine("Inserir um Veículo");
            Console.WriteLine("");

            try
            {
                Console.Write("Informe o Chassi: ");
                string chassi = Console.ReadLine();
                Console.Write("Informe o Tipo: 1- Ônibus 2- Caminhão : ");
                string tipo = Console.ReadLine();
                int tipoSelecionado = 0;
                int.TryParse(tipo, out tipoSelecionado);
                Console.Write("Informe a cor: ");
                string cor = Console.ReadLine();

                var veiculo = new Veiculo(chassi, tipoSelecionado, cor);
                if (veiculo.PossuiNotificaoes())
                {
                    Console.WriteLine("Erros nos dados informados do veículo: ");
                    foreach (var item in veiculo.Notificacoes)
                    {
                        Console.WriteLine(string.Concat("- ", item));
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Tecle Enter para tentar novamente.");
                    Console.ReadLine();
                    Console.Clear();
                    TelaInserirVeiculo();
                }

                var veiculoExistente = _veiculoRepositorio.BuscarPorChassi(chassi);
                if (veiculoExistente != null)
                {
                    throw new CustomException() { MensagemAmigavel = "Chassi já cadastrado." };
                }

                _veiculoRepositorio.Adicionar(veiculo);
                _veiculoRepositorio.Salvar();

                Console.WriteLine("");
                Console.WriteLine("Veículo inserido com sucesso!");
                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();

            }
            catch (CustomException cex)
            {
                Console.WriteLine("Erro: " + cex.MensagemAmigavel);
                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaInserirVeiculo();
            }
            catch (Exception ex)
            {
                //Log ex
                Console.WriteLine("");
                Console.WriteLine("Houve um erro inesperado ao inserir um veículo. Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();
            }
        }

        private static void TelaEditarVeiculo()
        {
            Console.WriteLine("Editar Veículo");
            Console.WriteLine("");

            try
            {
                Console.Write("Informe o Chassi: ");
                string chassi = Console.ReadLine();

                var veiculo = _veiculoRepositorio.BuscarPorChassi(chassi);
                if(veiculo == null)
                {
                    throw new CustomException() { MensagemAmigavel = "Chassi não encontrado." };
                }

                Console.Write("Informe a cor: ");
                string cor = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(cor))
                {
                    throw new CustomException() { MensagemAmigavel = "Informe um valor para a Cor." };
                }

                veiculo.Cor = cor;

                if (veiculo.PossuiNotificaoes())
                {
                    Console.WriteLine("Erros nos dados informados do veículo: ");
                    foreach (var item in veiculo.Notificacoes)
                    {
                        Console.WriteLine(string.Concat("- ", item));
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Tecle Enter para tentar novamente.");
                    Console.ReadLine();
                    Console.Clear();
                    TelaEditarVeiculo();
                }

                _veiculoRepositorio.Alterar(veiculo);
                _veiculoRepositorio.Salvar();

                Console.WriteLine("");
                Console.WriteLine("Veículo Alterado com sucesso!");
                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();

            }
            catch (CustomException cex)
            {
                Console.WriteLine("Erro: " + cex.MensagemAmigavel);
                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaEditarVeiculo();
            }
            catch (Exception ex)
            {
                //Log ex
                Console.WriteLine("");
                Console.WriteLine("Houve um erro inesperado ao editar um veículo. Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();
            }
        }

        private static void TelaExcluirVeiculo()
        {
            Console.WriteLine("Excluir Veículo");
            Console.WriteLine("");

            try
            {
                Console.Write("Informe o Chassi: ");
                string chassi = Console.ReadLine();

                var veiculo = _veiculoRepositorio.BuscarPorChassi(chassi);
                if (veiculo == null)
                {
                    throw new CustomException() { MensagemAmigavel = "Chassi não encontrado." };
                }

                Console.Write("Confirmar exclusão? Digite S para Sim, N para Não: ");
                string confirma = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(confirma))
                {
                    throw new CustomException() { MensagemAmigavel = "Informe um valor para a Confirmação." };
                }

                if(confirma.ToLower() == "s")
                {
                    _veiculoRepositorio.Remover(veiculo);
                    _veiculoRepositorio.Salvar();

                    Console.WriteLine("");
                    Console.WriteLine("Veículo Excluído com sucesso!");
                }

                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();

            }
            catch (CustomException cex)
            {
                Console.WriteLine("Erro: " + cex.MensagemAmigavel);
                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaExcluirVeiculo();
            }
            catch (Exception ex)
            {
                //Log ex
                Console.WriteLine("");
                Console.WriteLine("Houve um erro inesperado ao excluir um veículo. Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();
            }
        }

        private static void TelaListarVeiculos()
        {
            Console.WriteLine("Lista de Veículos");
            Console.WriteLine("");

            try
            {
                var veiculos = from v in _veiculoRepositorio.Consulta() orderby v.Chassi select v;
                if (veiculos.Count() > 0)
                {
                    foreach (var item in veiculos)
                    {
                        Console.WriteLine(string.Format("Chassi: {0}, Tipo: {1}, Nro. Passageiros: {2}, Cor: {3} ", item.Chassi, item.Tipo.ToString(), item.NumeroPassageiros, item.Cor));
                    }
                }
                else
                {
                    Console.Write("Nenhum veículo encontrado.");
                }


                Console.WriteLine("");
                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();
            }
            catch (Exception ex)
            {
                //Log ex
                Console.WriteLine("");
                Console.WriteLine("Houve um erro inesperado ao listar veículos. Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();
            }
        }

        private static void TelaPesquisarVeiculo()
        {
            Console.WriteLine("Pesquisar Veículo");
            Console.WriteLine("");

            try
            {
                Console.Write("Informe o Chassi: ");
                string chassi = Console.ReadLine();

                var veiculo = _veiculoRepositorio.BuscarPorChassi(chassi);
                if (veiculo == null)
                {
                    throw new CustomException() { MensagemAmigavel = "Chassi não encontrado." };
                }

                Console.WriteLine("");
                Console.WriteLine(string.Format("Chassi: {0}, Tipo: {1}, Nro. Passageiros: {2}, Cor: {3} ", veiculo.Chassi, veiculo.Tipo.ToString(), veiculo.NumeroPassageiros, veiculo.Cor));

                Console.WriteLine("");
                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();

            }
            catch (CustomException cex)
            {
                Console.WriteLine("Erro: " + cex.MensagemAmigavel);
                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaPesquisarVeiculo();
            }
            catch (Exception ex)
            {
                //Log ex
                Console.WriteLine("");
                Console.WriteLine("Houve um erro inesperado ao pesquisar um veículo. Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();
            }
        }

        private static void TelaSair()
        {
            Console.WriteLine("Sair");
            Console.WriteLine("");

            try
            {
                Console.Write("Deseja realmente sair e fechar? Digite S para Sim, N para Não: ");
                string confirma = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(confirma))
                {
                    throw new CustomException() { MensagemAmigavel = "Informe um valor para a Confirmação." };
                }

                if (confirma.ToLower() == "s")
                {
                    Console.Clear();
                }
                else
                {
                    Console.Write("Tecle Enter para continuar.");
                    Console.ReadLine();
                    Console.Clear();
                    TelaMenu();
                }
            }
            catch (CustomException cex)
            {
                Console.WriteLine("Erro: " + cex.MensagemAmigavel);
                Console.Write("Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaSair();
            }
            catch (Exception ex)
            {
                //Log ex
                Console.WriteLine("");
                Console.WriteLine("Houve um erro inesperado ao sair. Tecle Enter para continuar.");
                Console.ReadLine();
                Console.Clear();
                TelaMenu();
            }
        }
    }
}
