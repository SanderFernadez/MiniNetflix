
using Database.Common;
using System.ComponentModel.DataAnnotations;


namespace Database.Entities
{
    public class Genre: BseBasicEntity
    {
        //Navigation Property
        public ICollection<genre_serie>? Series { get; set; }



    }
}
