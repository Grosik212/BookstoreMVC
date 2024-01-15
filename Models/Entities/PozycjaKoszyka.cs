namespace BookstoreMVC.Models.Entities
{
    public class PozycjaKoszyka
    {
        public string Tytul { get; set; }
        public double Cena { get; set; }
        public int Ilosc { get; set; }

        public string Img { get; set; }

        public double CenaCalkowita
        {
            get { return Cena * Ilosc; }
        }

        public PozycjaKoszyka(string tytul, double cena, int ilosc = 1, string img = null)
        {
            Tytul = tytul;
            Cena = cena;
            Ilosc = ilosc;
            Img = img; 
        }
    }

}
