using Covid_19.Entidades;
using Covid_19.Entidades.Enums;
using System;

namespace Covid_19.Entities
{
    internal class Paciente
    {
        public int Senha { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        //public Sexo Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAlta { get; set; }
        public StatusPaciente StatusPaciente { get; set; }
        public bool TesteCovidPositivo { get; set; }
        public Triagem Triagem { get; set; }
        public Comorbidade[] Comorbidades { get; set; }

        public int Idade => (DateTime.Now - DataNascimento).Days / 365;
        public bool Preferencial => Idade >= 60;

        public Paciente Proximo { get; set; }
        public Paciente Anterior { get; set; }

        public Paciente()
        {
            Triagem = new Triagem();
        }

        public Paciente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Triagem = new Triagem();
        }

        public Paciente(string nome, string cpf, DateTime dataNascimento, int diasSintomas, bool possuiComorbidade)
        {
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Triagem.DiasSintomas = diasSintomas;
            Triagem.PossuiComorbidade = possuiComorbidade;
            Triagem = new Triagem();
        }

        public bool PossuiNecessidadeInternacao()
        {
            if (Triagem.Saturacao < 90) return true;

            return false;
        }

        public string DadosCompletosPaciente()
        {
            string comorbidades = "";
            if (Comorbidades != null)
            foreach (var comorbidade in Comorbidades)
                comorbidades += comorbidade.DadosComorbidade();

            return $@"
                      Nome: {Nome}
                      CPF: {CPF}
                      Data de Nascimento: {DataNascimento.ToString("dd/MM/yyyy")}
                      Idade: {Idade}
                      Status Paciente?: {StatusPaciente}
                      Comorbidades: { comorbidades ?? "sem registros"}
                      Possui Comorbidades?: {(Triagem.PossuiComorbidade ? "Sim" : "Não")}
                      {Triagem.DadosTriagem()}";
        }

        public string DadosMinimosPaciente()
        {
            return $@"
                      Senha: {Senha}
                      Nome: {Nome}
                      CPF: {CPF}
                      Data de Nascimento: {DataNascimento.ToString("dd/MM/yyyy")}
                      Idade: {Idade} anos
                      {(Preferencial ? "Fila preferencial!" : "Fila normal!")}";
        }

        public string ConverterParaCSV()
        {
            return $"{Nome};{CPF};{DataNascimento};{Triagem.DiasSintomas};{Triagem.PossuiComorbidade}";
        }
    }
}
