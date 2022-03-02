using System;

namespace Covid_19.Entities
{
    internal class Paciente
    {

        public int Senha { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }

        public bool PossuiComorbidade { get; set; }
        public bool PassouTriagem { get; set; }

        public Comorbidade[] Comorbidades { get; set; }

        public Paciente Proximo { get; set; }
        public Paciente Anterior { get; set; }

        public Paciente()
        {

        }

        public string DadosPaciente()
        {
            string comorbidades = "";
            foreach (var comorbidade in Comorbidades)
                comorbidades += comorbidade.ConverterParaCSV();

            return $@"
                      Senha: {Senha}
                      Nome: {Nome}
                      CPF: {CPF}
                      Data de Nascimento: {DataNascimento}
                      Idade: {Idade}
                      Possui Comorbidades ?: {(PossuiComorbidade ? "Sim" : "Não")}
                      Comorbidades: {"sem registros" ?? comorbidades }";
        }

        public string ConverterParaCSV()
        {
            string comorbidadesCSV = "";
            if (Comorbidades != null)
                foreach (var comorbidade in Comorbidades)
                    comorbidadesCSV += comorbidade.ConverterParaCSV();

            return $"{Nome};{CPF};{DataNascimento};{Idade};{PossuiComorbidade};[{comorbidadesCSV}];";
        }
    }
}
