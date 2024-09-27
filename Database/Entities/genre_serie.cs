

namespace Database.Entities
{
    public class genre_serie
    {
       
        public int SerieId { get; set; }
        public Serie? Serie { get; set; }

        public int GenreId { get; set; }
        public Genre? Genre { get; set; }

   

    }
}
