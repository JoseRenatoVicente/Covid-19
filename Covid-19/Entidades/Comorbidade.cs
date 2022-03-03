namespace Covid_19.Entities
{
    internal class Comorbidade
    {
        public string CPF { get; set; }
        public string NomeComorbidade { get; set; }

        public Comorbidade()
        {

        }
        public Comorbidade(string nomeComorbidade)
        {
            NomeComorbidade = nomeComorbidade;
        }

        public string ConverterParaCSV()
        {
            return $"{CPF};{NomeComorbidade}";
        }
    }
}
