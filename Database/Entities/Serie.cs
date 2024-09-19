using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Serie

    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string? VideoUrl { get; set; }

        [Required]
        public string? PortadaUrl { get; set; }

        public string? Description { get; set; }

        public int? GenreId{ get; set; }

        public int? ProducerId { get; set; }

        public ICollection<genre_serie>? SecondaryGenres { get; set; }


        //Navigation Property
        public Genre? Genre { get; set; }
        public Producer? Producer { get; set; }
        


    }
}
