using Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Application.ViewModels
{
    public class SaveSerieViewModel
    {

             public int Id { get; set; }

             [Required(ErrorMessage = "Debe Colocar el Titulo de la Serie")]

             public string Title { get; set; }


             [Required(ErrorMessage = "Debe Colocar la Descripcion de la Serie")]
             public string? Description { get; set; }

             [Required(ErrorMessage = "Debe Agregar El Genero Principal de la Serie")]
             public int? GenreId { get; set; }


            [Required(ErrorMessage = "Debe Agregar la Productora de la Serie")]
            public int? ProducerId { get; set; }


            [Required(ErrorMessage = "Debe Colocar el URl de la Portada")]
            public string? PortadaUrl { get; set; }


            [Required(ErrorMessage = "Debe Colocar el Url  del Video de la Serie")]
            public string? VideoUrl { get; set; }



            [Required(ErrorMessage = "Debe Colocar al Menos un Genero Secundario")]
            public List<int>? SecondaryGenresIds { get; set; } = new List<int>();
           



    }
}

