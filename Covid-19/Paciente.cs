using System;

namespace Covid_19
{
    internal class Paciente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }

        public bool PossuiComorbidade { get; set; }
        public Comorbidade[] Comorbidades { get; set; }

        public Paciente Proximo { get; set; }

        public Paciente()
        {

        }
    }
}
