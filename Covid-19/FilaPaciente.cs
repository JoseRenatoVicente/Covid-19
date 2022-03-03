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

        public void Push(Paciente novoPaciente)
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
        }

        public Paciente Pop()
        {
            Paciente antigoPaciente = Inicio;
            if (EstaVazio()) return null;
            else if (Inicio.Proximo == null)
                Fim = Inicio = null;
            else
            {

                Inicio = Inicio.Proximo;
            }
            antigoPaciente.Proximo = null;
            return antigoPaciente;
        }

        public bool EstaVazio()
        {
            return (Inicio == null) && (Fim == null) ? true : false;
        }
    }

}
