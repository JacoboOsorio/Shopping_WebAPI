using System.ComponentModel.DataAnnotations;

namespace ShoppingWebAPI.Entities
{
    public class Country : Entity
    {
        [Display(Name = "Pais")] //To show the field with another name
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")] //Equals "NVARCHAR()" in SQL
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] //Equals "NOT NULL" in SQL
        public string Name { get; set; }
    }
}
