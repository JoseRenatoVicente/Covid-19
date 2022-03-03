namespace Covid_19.Entidades
{
    internal class Triagem
    {
        public double Pressao { get; set; }
        public int BatimentosCardiacos{ get; set; }
        public int Saturacao { get; set; }
        public double Temperatura { get; set; }
        public int DiasSintomas { get; set; }
        public bool PossuiComorbidade { get; set; }
        public bool ResultadoTesteCovid { get; set; }
        public SintomasCovid SintomasCovid { get; set; }



    }
}
