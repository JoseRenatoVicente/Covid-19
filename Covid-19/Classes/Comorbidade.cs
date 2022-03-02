namespace Covid_19.Entities
{
    internal class Comorbidade
    {
        public string NomeComorbidade { get; set; }

        public string ConverterParaCSV()
        {
            return $"{NomeComorbidade};";
        }
    }
}
