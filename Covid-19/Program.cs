﻿using Covid_19.Entities;
using System;

namespace Covid_19
{
    internal class Program
    {
        static ServicoCovid service = new ServicoCovid();
        static CovidRepositorio covid = new CovidRepositorio();
        static void Main(string[] args)
        {
            service.AdicionaPacienteFila(new Paciente("Jose", "1234", DateTime.Parse("22/04/2004")));
            service.AdicionaPacienteFila(new Paciente("Joao", "2345", DateTime.Parse("23/06/2002")));

            service.AdicionaPacienteFila(new Paciente("Jose pre", "3456", DateTime.Parse("22/04/1900")));
            service.AdicionaPacienteFila(new Paciente("Jose pre", "4567", DateTime.Parse("22/04/1900")));

            Menu();
        }

        public static void Menu()
        {

            Console.WriteLine(@"

                                1) Cadastro de um Paciente
                                2) Buscar Paciente
                                3) Triagem
                                4) Mudar configurações do sistema
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
                    service.Triagem();
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
