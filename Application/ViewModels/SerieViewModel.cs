using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SerieViewModel
    {

        public int Id { get; set; }

      
        public string Title { get; set; }

        public string? Description { get; set; }


        public int? GenreId { get; set; }

        public int? ProducerId { get; set; }

        public string? PortadaUrl { get; set; }

        public string? VideoUrl { get; set; }

        public List<string> Genres { get; set; } = new List<string>();

        public string Producer { get; set; }

        public string GenreName { get; set; }

    }
}
