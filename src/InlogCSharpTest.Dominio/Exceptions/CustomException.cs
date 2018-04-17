using System;

namespace InlogCSharpTest.Dominio.Exceptions
{
    public class CustomException : Exception
    {
        public string MensagemAmigavel { get; set; }
    }
}
