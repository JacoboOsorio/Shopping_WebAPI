using System.ComponentModel.DataAnnotations;

namespace ShoppingWebAPI.DAL.Entities
{
    public class Category : Entity
    {
        [Display(Name = "Categoria")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? Description { get; set; }
    }
}
