using Covid_19.Entities;

namespace Covid_19
{
    internal class ListaPacientes
    {
        public Paciente Inicio { get; set; }
        public Paciente Fim { get; set; }
        public ListaPacientes()
        {
            Inicio = Fim = null;
        }

        public Paciente[] ObterTodos()
        {
            Paciente[] pacientes = new Paciente[Count()];
            int count = 0;

            if (EstaVazio()) return null;
            else
            {
                Paciente paciente = Inicio;
                do
                {
                    pacientes[count++] = paciente;

                    paciente = paciente.Proximo;

                } while (paciente != null);
            }

            return pacientes;
        }

        public int Count()
        {
            if (EstaVazio()) return 0;
            else
            {
                Paciente paciente = Inicio;
                int count = 0;
                do
                {
                    count++;
                    paciente = paciente.Proximo;

                } while (paciente != null);
                return count;
            }

        }
        public Paciente[] BuscaPeloCPFNome(string busca)
        {
            Paciente[] buscaPaciente = new Paciente[Count()];
            int countSearch = 0;

            if (EstaVazio()) return null;
            else
            {
                Paciente paciente = Inicio;
                do
                {
                    if (paciente.Nome.ToLower().Contains(busca.ToLower()) || paciente.CPF.Contains(busca))
                    {
                        buscaPaciente[countSearch++] = paciente;
                    }
                    paciente = paciente.Proximo;
                } while (paciente != null);
            }
            return buscaPaciente;
        }

        public Paciente ObterPeloCPF(string cpf)
        {
            Paciente paciente = Inicio;

            if (EstaVazio()) return null;
            else
            {

                while (paciente.Proximo != null)
                {

                    if (cpf == paciente.CPF)
                    {
                        return paciente;
                    }
                    paciente = paciente.Proximo;
                }
            }
            return paciente;
        }

        public Paciente Push(Paciente novoPaciente)
        {
            novoPaciente.Proximo = null;

            if (EstaVazio())
            {
                novoPaciente.Senha = 1;
                Inicio = Fim = novoPaciente;
            }
            else
            {
                novoPaciente.Senha = Fim.Senha + 1;
                novoPaciente.Anterior = Fim;
                Fim.Proximo = novoPaciente;
                Fim = novoPaciente;
            }

            return novoPaciente;
        }


        public bool Pop(string CPF)
        {
            if (EstaVazio()) return false;
            else if (CPF == Inicio.CPF)
            {
                if (Inicio.Proximo == null)
                    Fim = Inicio = null;
                else
                {
                    Inicio = Inicio.Proximo;
                    Inicio.Anterior = null;
                }
            }
            else
            {
                Paciente paciente = Inicio;
                do
                {
                    if (paciente.Proximo.CPF == CPF)
                        if (paciente.Proximo.Proximo == null)
                        {
                            Fim = paciente;
                            paciente.Proximo = null;
                        }
                        else
                        {
                            paciente.Anterior = paciente.Anterior;

                            paciente.Proximo = paciente.Proximo.Proximo;

                            paciente.Proximo.Anterior = paciente;
                            break;
                        }

                    paciente = paciente.Proximo;

                } while (paciente != null);

            }

            return true;
        }


        public bool EstaVazio()
        {
            return (Inicio == null) && (Fim == null) ? true : false;
        }
    }
}
