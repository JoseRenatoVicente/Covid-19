using Covid_19.Entidades.Enums;
using System;

namespace Covid_19.Entities
{
    internal class Paciente
    {
        public int Senha { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public Sexo Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public int DiasSintomas { get; set; }
        public bool PossuiComorbidade { get; set; }
        public bool PassouTriagem { get; set; }

        public int Idade => (DateTime.Now - DataNascimento).Days / 365;
        public bool Preferencial => (DateTime.Now - DataNascimento).Days / 365 >= 60 ?
                     true : false;

        public Comorbidade[] Comorbidades { get; set; }

        public Paciente Proximo { get; set; }
        public Paciente Anterior { get; set; }

        public Paciente()
        {

        }

        public Paciente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
        }

        public Paciente(string nome, string cpf, Sexo sexo, DateTime dataNascimento, int diasSintomas, bool possuiComorbidade, bool passouTriagem)
        {
            Nome = nome;
            CPF = cpf;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            DiasSintomas = diasSintomas;
            PossuiComorbidade = possuiComorbidade;
            PassouTriagem = passouTriagem;
        }

        public string DadosCompletosPaciente()
        {
            string comorbidades = "";
            if (Comorbidades != null)
                foreach (var comorbidade in Comorbidades)
                    comorbidades += comorbidade.ConverterParaCSV();

            return $@"
                      Senha: {Senha}
                      Nome: {Nome}
                      CPF: {CPF}
                      Data de Nascimento: {DataNascimento.ToString("dd/MM/yyyy")}
                      Sexo: {Sexo}
                      Idade: {Idade} anos
                      Possui Comorbidades?: {(PossuiComorbidade ? "Sim" : "Não")}
                      Comorbidades: {"sem registros" ?? comorbidades }";
        }

        public string DadosMinimosPaciente()
        {

            return $@"
                      Senha: {Senha}
                      Nome: {Nome}
                      CPF: {CPF}
                      Data de Nascimento: {DataNascimento.ToString("dd/MM/yyyy")}
                      Sexo: {Sexo}
                      Idade: {Idade} anos
                      {(Preferencial ? "Fila preferencial!" : "Fila normal!")}";
        }

        public string ConverterParaCSV()
        {
            return $"{Nome};{CPF};{Sexo};{DataNascimento};{DiasSintomas};{PossuiComorbidade};{PassouTriagem}";
        }
    }
}
