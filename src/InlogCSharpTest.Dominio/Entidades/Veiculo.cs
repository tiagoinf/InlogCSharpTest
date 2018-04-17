using System;
using System.Collections.Generic;
using InlogCSharpTest.Dominio.Enuns;

namespace InlogCSharpTest.Dominio.Entidades
{
    public class Veiculo: EntidadeBase
    {
        private Veiculo()
        {

        }

        public Veiculo(string chassi, int tipo, string cor)
        {
            _Notificacoes = new List<string>();

            if(string.IsNullOrWhiteSpace(chassi))
            {
                _Notificacoes.Add("chassi não pode ser nulo ou vazio");
            }
            
            if (string.IsNullOrWhiteSpace(cor))
            {
                _Notificacoes.Add("cor não pode ser nulo ou vazio");
            }

            if (!string.IsNullOrWhiteSpace(chassi) && chassi.Length > 17)
            {
                _Notificacoes.Add("chassi não pode ser maior que 17 caracteres");
            }

            if (!string.IsNullOrWhiteSpace(cor) && cor.Length > 17)
            {
                _Notificacoes.Add("cor não pode ser maior que 50 caracteres");
            }

            VeiculoTipo tipoAux;
            Enum.TryParse(tipo.ToString(), out tipoAux);

            if (tipoAux != VeiculoTipo.Caminhao && tipoAux != VeiculoTipo.Onibus)
            {
                _Notificacoes.Add("tipo inválido");
            }

            Chassi = chassi;
            Tipo = tipoAux;
            Cor = cor;

        }

        public string Chassi { get; set; }

        public VeiculoTipo Tipo { get; set; }

        public SByte NumeroPassageiros
        {
            get
            {
                switch (Tipo)
                {
                    case VeiculoTipo.Onibus:
                        return 42;
                    case VeiculoTipo.Caminhao:
                        return 2;
                    default:
                        break;
                }
                return 0;
            }
        }

        public string Cor { get; set; }

    }
}
