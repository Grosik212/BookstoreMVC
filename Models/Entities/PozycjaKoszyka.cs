namespace BookstoreMVC.Models.Entities
{
    public class PozycjaKoszyka
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public double Cena { get; set; }
        public int Ilosc { get; set; }

        public string Img { get; set; }

        public double CenaCalkowita
        {
            get { return Cena * Ilosc; }
        }

        public PozycjaKoszyka(string tytul, double cena, string img, int ilosc = 1)
        {
            Tytul = tytul;
            Cena = cena;
            Ilosc = ilosc;
            Img = img; 
        }
    }

}
