using System;

namespace Covid_19
{
    internal class Program
    {
        static ServicoCovid service = new ServicoCovid();
        static CovidRepositorio covid = new CovidRepositorio();
        static void Main(string[] args)
        {
            covid.ReadFile();
            covid.WriteFile();
            Menu();
        }

        public static void Menu()
        {

            Console.WriteLine(@"

                                1) Cadastro de um Paciente
                                2) Buscar Paciente
                                3) Fila de pacientes
                                ------------------------------
                                0) - Sair
");

            string option = Console.ReadLine();

            switch (option)
            {
                case "0": Environment.Exit(0); break;

                case "1":
                    Console.Clear();
                    service.CadastroPaciente();
                    BackMenu();
                    break;

                case "2":
                    Console.Clear();
                    service.ObterPacientesPorNomeCPF();
                    BackMenu();
                    break;

                case "3":
                    Console.Clear();
                    service.ObterTodosPacientes();
                    BackMenu();
                    break;

                default:
                    Console.WriteLine("Opção inválida! ");
                    BackMenu();
                    break;
            }

        }

        public static void BackMenu()
        {
            Console.WriteLine("\n Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }
    }
}
