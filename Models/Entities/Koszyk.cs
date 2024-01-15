namespace BookstoreMVC.Models.Entities
{
    public class Koszyk
    {
        public List<PozycjaKoszyka> Pozycje { get; set; }

        public Koszyk()
        {
            Pozycje = new List<PozycjaKoszyka>();
        }

        public void DodajDoKoszyka(Book book)
        {
            var istniejacaPozycja = Pozycje.FirstOrDefault(p => p.Tytul == book.Title);

            if (istniejacaPozycja != null)
            {
                istniejacaPozycja.Ilosc++;
            }
            else
            {
                Pozycje.Add(new PozycjaKoszyka(book.Title, book.Price, book.Img));
            }
        }

        public void UsunZKoszyka(int bookId)
        {
            var pozycjaDoUsuniecia = Pozycje.FirstOrDefault(p => p.Id == bookId);
            if (pozycjaDoUsuniecia != null)
            {
                Pozycje.Remove(pozycjaDoUsuniecia);
            }
        }

        public double CenaCalkowitaKoszyka
        {
            get { return Pozycje.Sum(p => p.CenaCalkowita); }
        }
    }
}
