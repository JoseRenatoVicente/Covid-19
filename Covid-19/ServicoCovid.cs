using Covid_19.Entidades.Enums;
using Covid_19.Entities;
using System;

namespace Covid_19
{
    internal class ServicoCovid
    {
        public ListaPacientes listaPacientes = new ListaPacientes();

        public FilaPaciente filaPacientePreferencial = new FilaPaciente();
        public FilaPaciente filaPacienteNormal = new FilaPaciente();

        int contadorSenha;
        int contadorFilaPreferencial;
        public void ObterTodosPacientes()
        {
            EstaVazio();

            foreach (var paciente in listaPacientes.ObterTodos())
            {
                if (paciente != null) Console.WriteLine(paciente.DadosCompletosPaciente());
            }
        }
        public Paciente[] ObterPacientesPorNomeCPF()
        {
            EstaVazio();

            Console.Write("Digite o nome ou CPF do paciente para que seja localizado: ");
            Paciente[] pacientes = listaPacientes.BuscaPeloCPFNome(Console.ReadLine());

            if (pacientes[0] == null)
                Console.WriteLine("Paciente não encontrado");

            foreach (Paciente paciente in pacientes)
                if (paciente != null) Console.WriteLine(paciente.DadosCompletosPaciente());

            return pacientes;
        }

        public Paciente CadastroPaciente()
        {

            Console.WriteLine("Proximo da fila de espera. Senha: " + (++contadorSenha));

            Paciente paciente = AdicionaPacienteFila(EntradaDadosIniciaisPaciente(new Paciente()));

            Console.WriteLine(paciente.DadosMinimosPaciente());

            Console.WriteLine();
            Console.WriteLine("Paciente adicionado com sucesso");

            return paciente;
        }

        public Paciente AdicionaPacienteFila(Paciente paciente)
        {

            if (paciente.Idade >= 60)
            {
                filaPacientePreferencial.Push(paciente);
            }
            else
            {
                filaPacienteNormal.Push(paciente);
            }

            return paciente;
        }

        public void Triagem()
        {

            if (filaPacientePreferencial.EstaVazio() || contadorFilaPreferencial > 2)
            {
                Paciente paciente = filaPacienteNormal.Pop();

                Console.WriteLine("Proximo paciente");
                Console.WriteLine(paciente.DadosMinimosPaciente());

                listaPacientes.Push(EntradaDadosPaciente(paciente));
                contadorFilaPreferencial = 0;
            }
            else if (contadorFilaPreferencial < 2)
            {
                Paciente paciente = filaPacientePreferencial.Pop();

                Console.WriteLine("Proximo paciente");
                Console.WriteLine(paciente.DadosMinimosPaciente());

                listaPacientes.Push(EntradaDadosPaciente(paciente));
                contadorFilaPreferencial++;
            }

            //Chama proximo paciente
            //Adiciona suas informações adicionais
            //Verifica se é caso de emergencia e manda para internação
            // se não tem sintomas de covid apenas registra
            //se ela apresenta sintomas mas nao grave ela vai para o exame

        }

        private Paciente EntradaDadosIniciaisPaciente(Paciente paciente)
        {
            if (paciente.Nome == null || paciente.Nome == "")
            {
                Console.Write("Nome: ");
                paciente.Nome = Console.ReadLine();

                if (string.Empty == paciente.Nome)
                {
                    Console.WriteLine("O nome não pode ser vazio");
                    EntradaDadosIniciaisPaciente(paciente);
                }
            }

            if (paciente.CPF == null || paciente.CPF == "")
            {
                Console.Write("CPF: ");
                paciente.CPF = Console.ReadLine();

                if (string.Empty == paciente.CPF)
                {
                    Console.WriteLine("O CPF não pode ser vazio");
                    EntradaDadosIniciaisPaciente(paciente);
                }
            }

            if (paciente.DataNascimento == default(DateTime))
            {
                Console.Write("Data de Nascimento: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
                    paciente.DataNascimento = dataNascimento;
                else
                {
                    Console.WriteLine("A data de nascimento inválida");
                    EntradaDadosIniciaisPaciente(paciente);
                }

            }

            if (paciente.Sexo == 0)
            {
                Console.Write("Digite o número referente ao Sexo: ");
                Console.Write(@"
 1- Masculino
 2- Feminino
 3- InterSexo
");

                if (Int32.TryParse(Console.ReadLine(), out int numeroSexo))
                    if (numeroSexo >= 1 && numeroSexo <= 3)
                        paciente.Sexo = (Sexo)numeroSexo;
                    else
                    {
                        Console.WriteLine($"Formato incorreto digite um número referente ao sexo que seja válido");
                        EntradaDadosIniciaisPaciente(paciente);
                    }
            }
            Console.Clear();

            return paciente;

        }

        private Paciente EntradaDadosPaciente(Paciente paciente)
        {
            if (paciente.DiasSintomas == 0)
            {
                Console.Write("Dias sintomas: ");
                if (Int32.TryParse(Console.ReadLine(), out int dias))
                    if (dias >= 1)
                        paciente.DiasSintomas = dias;
                    else
                    {
                        Console.WriteLine("Os dias devem ser maio que 1");
                        EntradaDadosPaciente(paciente);
                    }
            }

            if (paciente.PossuiComorbidade == false)
            {
                Console.WriteLine("O paciente possui alguma comobidade? Sim/Nao");
                string comorbidade = Console.ReadLine().ToLower();
                paciente.PossuiComorbidade = comorbidade == "sim" || comorbidade == "s" ? true : false;
            }


            if (paciente.PossuiComorbidade)
            {
                Comorbidade[] comorbidades;

                Console.WriteLine("O Paciente possui quantas comorbidades");
                int contatorComobidades = int.Parse(Console.ReadLine());

                comorbidades = new Comorbidade[contatorComobidades];

                for (int i = 0; i != contatorComobidades; i++)
                {
                    comorbidades[i] = EntradaDadosComorbidades(new Comorbidade());
                }

                paciente.Comorbidades = comorbidades;
            }

            Console.Clear();

            return paciente;

        }

        private Comorbidade EntradaDadosComorbidades(Comorbidade comorbidade)
        {
            if (comorbidade.NomeComorbidade == null || comorbidade.NomeComorbidade == "")
            {
                Console.Write("Nome da Comorbidade: ");
                comorbidade.NomeComorbidade = Console.ReadLine();

                if (string.Empty == comorbidade.NomeComorbidade)
                {
                    Console.WriteLine("O nome não pode ser vazio");
                    EntradaDadosComorbidades(comorbidade);
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
            if (listaPacientes.EstaVazio())
            {
                Console.WriteLine("Nenhum paciente cadastrado no sistema");
                Program.BackMenu();
            }
        }

    }
}
