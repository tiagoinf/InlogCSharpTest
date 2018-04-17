using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlogCSharpTest.Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        public int Id { get; set; }

        protected IList<string> _Notificacoes;

        public bool PossuiNotificaoes()
        {
            return (_Notificacoes != null && _Notificacoes.Count > 0);
        }

        public IList<string> Notificacoes
        {
           get { return _Notificacoes; }
        }
    }
}
