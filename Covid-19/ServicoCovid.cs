using Covid_19.Entities;
using System;

namespace Covid_19
{
    internal class ServicoCovid
    {
        ListaPacientes filaInicial = new ListaPacientes();

        public void ObterTodosPacientes()
        {
            EstaVazio();

            foreach (var paciente in filaInicial.PegarFila())
            {
                if (paciente != null) Console.WriteLine(paciente.DadosPaciente());
            }
        }

        public Paciente[] ObterPacientesPorNomeCPF()
        {
            EstaVazio();

            Console.Write("Digite o nome ou CPF do paciente para que seja localizado: ");
            Paciente[] pacientes = filaInicial.BuscaPeloCPFNome(Console.ReadLine());

            if (pacientes[0] == null)
                Console.WriteLine("Paciente não encontrado");

            foreach (Paciente paciente in pacientes)
                if (paciente != null) Console.WriteLine(paciente.DadosPaciente());

            return pacientes;
        }

        public Paciente CadastroPaciente()
        {
            Console.WriteLine(filaInicial.Push(EntradaDadosPaciente(new Paciente())).DadosPaciente());

            Console.WriteLine();
            Console.WriteLine("Paciente adicionado com sucesso");
            return new Paciente();
        }

        private Paciente EntradaDadosPaciente(Paciente paciente)
        {
            if (paciente.Nome == null || paciente.Nome == "")
            {
                Console.Write("Nome: ");
                paciente.Nome = Console.ReadLine();

                if (string.Empty == paciente.Nome)
                {
                    Console.WriteLine("O nome não pode ser vazio");
                    EntradaDadosPaciente(paciente);
                }
            }

            if (paciente.CPF == null || paciente.CPF == "")
            {
                Console.Write("CPF: ");
                paciente.CPF = Console.ReadLine();

                if (string.Empty == paciente.CPF)
                {
                    Console.WriteLine("O CPF não pode ser vazio");
                    EntradaDadosPaciente(paciente);
                }
            }

            if (paciente.DataNascimento == default(DateTime))
            {
                Console.Write("Data de Nascimento: ");
                paciente.DataNascimento = DateTime.Parse(Console.ReadLine());

                if (paciente.DataNascimento == default(DateTime))
                {
                    Console.WriteLine("A data de nascimento não pode estar vazia");
                    EntradaDadosPaciente(paciente);
                }

                paciente.Idade = (DateTime.Now - paciente.DataNascimento).Days / 365;
            }

            Comorbidade[] comorbidades;

            Console.WriteLine("O Paciente possui quantas comorbidades");
            int contatorComobidades = int.Parse(Console.ReadLine());

            comorbidades = new Comorbidade[contatorComobidades];

            for (int i = 0; i != contatorComobidades; i++)
            {
                comorbidades[i] = EntradaDadosPacientes(new Comorbidade());
            }

            paciente.Comorbidades = comorbidades;

            Console.Clear();

            return paciente;

        }

        private Comorbidade EntradaDadosPacientes(Comorbidade comorbidade)
        {
            if (comorbidade.NomeComorbidade == null || comorbidade.NomeComorbidade == "")
            {
                Console.Write("Nome da Comorbidade: ");
                comorbidade.NomeComorbidade = Console.ReadLine();

                if (string.Empty == comorbidade.NomeComorbidade)
                {
                    Console.WriteLine("O nome não pode ser vazio");
                    EntradaDadosPacientes(comorbidade);
                }
            }

            return comorbidade;
        }

        public Paciente EditarPaciente()
        {
            return new Paciente();
        }

        public void EstaVazio()
        {
            if (filaInicial.EstaVazio())
            {
                Console.WriteLine("Nenhum paciente cadastrado no sistema");
                Program.BackMenu();
            }
        }

    }
}
