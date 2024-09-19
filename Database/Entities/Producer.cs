using Database.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Producer : BseBasicEntity
    {

        //Navigation Property
        public ICollection<Serie>? Series { get; set;}
        
    }
}
