using Covid_19.Entities;

namespace Covid_19
{
    internal class FilaPaciente
    {
        public Paciente Inicio { get; set; }
        public Paciente Fim { get; set; }

        public FilaPaciente()
        {
            Inicio = Fim = null;
        }

        public Paciente Push(Paciente novoPaciente)
        {
            if (EstaVazio())
            {
                Inicio = Fim = novoPaciente;
            }
            else
            {
                novoPaciente.Anterior = Fim;
                Fim.Proximo = novoPaciente;
                Fim = novoPaciente;
            }

            return novoPaciente;
        }

        public bool Pop()
        {
            if (EstaVazio()) return false;
            else
            {
                Inicio = Inicio.Proximo;
            }

            return true;
        }


        public bool EstaVazio()
        {
            return (Inicio == null) && (Fim == null) ? true : false;
        }
    }

}
