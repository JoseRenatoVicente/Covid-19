using Covid_19.Entidades.Enums;
using Covid_19.Entities;
using System;

namespace Covid_19
{
    internal class ServicoCovid
    {
        public ListaPacientes listaPacientes = new ListaPacientes();

        public FilaPaciente filaPacientePreferencial = new FilaPaciente("FilaPacientePreferencial.csv");
        public FilaPaciente filaPacienteNormal = new FilaPaciente("FilaPacienteNormal.csv");
        public FilaPaciente filaPacienteInternados = new FilaPaciente("FilaPacienteInternados.csv");

        public Leitos configuracaoSistema = new Leitos();

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

            if (pacientes == null)
                Console.WriteLine("Paciente não encontrado");
            else
            {
                foreach (Paciente paciente in pacientes)
                    if (paciente != null) Console.WriteLine(paciente.DadosCompletosPaciente());
            }
            return pacientes;
        }

        public void ListaEspera()
        {
            foreach (var paciente in filaPacienteInternados.ObterTodos())
            {
                if (paciente != null) Console.WriteLine(paciente.DadosCompletosPaciente());
            }


        }

        public Paciente CadastroPaciente()
        {
            Paciente paciente = new Paciente();

            Console.WriteLine("Proximo da fila de espera. Senha: " + (++contadorSenha));

            Console.Write("Infome o CPF: ");
            string cpf = Console.ReadLine();

            if (string.Empty == cpf)
            {
                Console.WriteLine("O CPF não pode ser vazio");
                CadastroPaciente();
            }
            else
            {
                Paciente resultadoPaciente = listaPacientes.ObterPeloCPF(cpf);

                if (resultadoPaciente == null)
                {
                    paciente = AdicionaPacienteFila(EntradaDadosIniciaisPaciente(new Paciente() { CPF = cpf }));

                    Console.WriteLine(paciente.DadosMinimosPaciente());

                    Console.WriteLine();
                    Console.WriteLine("Paciente adicionado com sucesso");
                }
                else
                {
                    Console.WriteLine(resultadoPaciente.DadosCompletosPaciente());
                    paciente = AdicionaPacienteFila(resultadoPaciente);

                    Console.WriteLine();
                    Console.WriteLine("Paciente adicionado com sucesso");
                }
            }

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

            EstaVazio();

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
            Console.Clear();

            return paciente;

        }

        private Paciente EntradaDadosPaciente(Paciente paciente)
        {

            if (paciente.Triagem.DiasSintomas == 0)
            {
                Console.Write("Dias sintomas: ");
                if (Int32.TryParse(Console.ReadLine(), out int dias))
                    if (dias >= 1)
                        paciente.Triagem.DiasSintomas = dias;
                    else
                    {
                        Console.WriteLine("Os dias devem ser maio que 1");
                        EntradaDadosPaciente(paciente);
                    }
            }

            if (paciente.Triagem.DiasSintomas < 3)
            {
                Console.WriteLine("O paciente deve voltar após o terceiro dia para realizar o exame de covid");
                return paciente;
            }

            if (paciente.Triagem.Pressao == 0)
            {
                Console.Write("Pressão: ");
                if (double.TryParse(Console.ReadLine(), out double pressao))
                    if (pressao >= 1)
                        paciente.Triagem.Pressao = pressao;
                    else
                    {
                        Console.WriteLine("A pressão tem que ser maior que 1");
                        EntradaDadosPaciente(paciente);
                    }
            }

            if (paciente.Triagem.BatimentosCardiacos == 0)
            {
                Console.Write("Batimentos Cardíacos: ");
                if (Int32.TryParse(Console.ReadLine(), out int batimentosCardiacos))
                    if (batimentosCardiacos >= 1)
                        paciente.Triagem.BatimentosCardiacos = batimentosCardiacos;
                    else
                    {
                        Console.WriteLine("Os Batimentos Cardíacos devem ser maior que 1");
                        EntradaDadosPaciente(paciente);
                    }
            }

            if (paciente.Triagem.Saturacao == 0)
            {
                Console.Write("Saturação: ");
                if (Int32.TryParse(Console.ReadLine(), out int saturacao))
                    if (saturacao >= 1)
                        paciente.Triagem.Saturacao = saturacao;
                    else
                    {
                        Console.WriteLine("A saturação deve ser maior que 1");
                        EntradaDadosPaciente(paciente);
                    }
            }

            if (paciente.Triagem.Temperatura == 0)
            {
                Console.Write("Temperatura: ");
                if (double.TryParse(Console.ReadLine(), out double temperatura))
                    if (temperatura >= 1)
                        paciente.Triagem.Temperatura = temperatura;
                    else
                    {
                        Console.WriteLine("A saturação deve ser maior que 1");
                        EntradaDadosPaciente(paciente);
                    }
            }

            if (paciente.Triagem.PossuiComorbidade == false)
            {
                Console.WriteLine("O paciente possui alguma comobidade? Sim/Nao");
                string comorbidade = Console.ReadLine().ToLower();
                paciente.Triagem.PossuiComorbidade = comorbidade == "sim" || comorbidade == "s" ? true : false;
            }


            if (paciente.Triagem.PossuiComorbidade)
            {
                Console.Write("Quantas: ");
                if (Int32.TryParse(Console.ReadLine(), out int quantidade))
                    if (quantidade >= 1)
                        paciente.Triagem.Saturacao = quantidade;
                    else
                    {
                        Console.WriteLine("A quantidade deve ser maior que 1");
                        EntradaDadosPaciente(paciente);
                    }

                Comorbidade[] comorbidades;

                comorbidades = new Comorbidade[quantidade];

                for (int i = 0; i != quantidade; i++)
                {
                    comorbidades[i] = EntradaDadosComorbidades(new Comorbidade());
                }

                paciente.Comorbidades = comorbidades;
            }

            Console.WriteLine("Sintomas do COVID:");

            Console.WriteLine("Perda paladar? Sim/Nao");
            string perdaPaladar = Console.ReadLine().ToLower();
            paciente.Triagem.SintomasCovid.PerdaPaladar = perdaPaladar == "sim" || perdaPaladar == "s" ? true : false;



            if (paciente.PossuiNecessidadeInternacao())
            {
                Console.WriteLine("O paciente deve ser internado!");
                if (!configuracaoSistema.PossuiVaga())
                {
                    Console.WriteLine("Não temos vaga, o paciente foi adicionado a lista de internação");
                    filaPacienteInternados.Push(paciente);
                }
                else
                {
                    Console.WriteLine("Temos vaga");
                    configuracaoSistema.LeitosOcupados++;
                    paciente.EstaInternado = true;
                }
                return paciente;
            }

            paciente.PassouTriagem = true;

            return paciente;
        }

        private Comorbidade EntradaDadosComorbidades(Comorbidade comorbidade)
        {
            if (comorbidade.NomeComorbidade == null)
            {
                Console.Write("Nome: ");
                comorbidade.NomeComorbidade = Console.ReadLine();

                if (string.Empty == comorbidade.NomeComorbidade)
                {
                    Console.WriteLine("O nome não pode ser vazio");
                }

            }
            return comorbidade;
        }



        public Paciente DarAltaPaciente(string cpf)
        {
            return new Paciente();
        }

        public Paciente EditarPaciente()
        {
            return new Paciente();
        }

        public void ConfiguracoesSistema()
        {
            Console.Write("Qual a quantidade de leitos atualmente?: ");
            if (int.TryParse(Console.ReadLine(), out int quantidadeLeitos))
                if (quantidadeLeitos >= 1)
                    configuracaoSistema.TotalLeitos = quantidadeLeitos;
                else
                {
                    Console.WriteLine("Número de leitos inválido");
                    ConfiguracoesSistema();
                }

        }

        public void EstaVazio()
        {
            if (filaPacienteNormal.EstaVazio() && filaPacientePreferencial.EstaVazio())
            {
                Console.WriteLine("Nenhum paciente cadastrado no sistema");
                Program.BackMenu();
            }
        }

    }
}
