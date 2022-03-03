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
                StreamWriter sw = new StreamWriter("c:\\5by5\\Pacientes.txt");

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
                    paciente.PossuiComorbidade = bool.Parse(values[3]);
                    paciente.PassouTriagem = bool.Parse(values[4]);


                    string[] valoresComorbidades = values[5].Split('|');
                    Comorbidade[] comorbidades = new Comorbidade[valoresComorbidades.Length-1];

                    for (int i = 0; i < valoresComorbidades.Length -1; i++)
                    {
                        comorbidades[i] = new Comorbidade(valoresComorbidades[i]);
                    }

                    paciente.Comorbidades = comorbidades;

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
