using Covid_19.Entities;
using System;
using System.IO;

namespace Covid_19
{
    internal class CovidRepositorio
    {
        ListaPacientes listaPacientes = new ListaPacientes();

        public void WriteFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter("c:\\5by5\\Pacientes.csv");

                Paciente[] pacientes = listaPacientes.ObterTodos();

                foreach (var paciente in pacientes)
                {
                    sw.WriteLine(paciente.ConverterParaCSV());
                }

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void ReadFile()
        {
            try
            {
                StreamReader sr = new StreamReader("c:\\5by5\\Pacientes.txt");

                int count = 0;
                string line = sr.ReadLine();

                while (line != null)
                {
                    Paciente paciente = new Paciente();
                    string[] values = line.Split(';');

                    paciente.Nome = values[0];
                    paciente.CPF = values[1];
                    paciente.DataNascimento = DateTime.Parse(values[2]);
                    paciente.Idade = int.Parse(values[3]);
                    paciente.PossuiComorbidade = bool.Parse(values[4]);
                    paciente.PassouTriagem = bool.Parse(values[5]);

                    paciente.Proximo = null;

                    listaPacientes.Push(paciente);

                    count++;
                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
