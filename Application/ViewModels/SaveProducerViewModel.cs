using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SaveProducerViewModel
    {

        public int? Id { get; set; }

        [Required(ErrorMessage = "Debe Colocar el Nombre de la Productora")]
        public string Name { get; set; }




    }
}
