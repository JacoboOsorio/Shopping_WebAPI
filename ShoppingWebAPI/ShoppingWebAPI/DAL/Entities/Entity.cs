using System.ComponentModel.DataAnnotations;

namespace ShoppingWebAPI.DAL.Entities
{
    public class Entity
    {
        //Data annotation: Kind of labels that i give
        //to my properties or fields
        [Key] //Primary key
        //Guid: a hash that changes randomly and
        //it's not self-incrementing
        public Guid Id { get; set; }

        //?: Means that properties are nullable
        //(There's no problem if i don't send any
        //value or parameter)
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
