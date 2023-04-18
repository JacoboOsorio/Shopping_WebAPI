using System.ComponentModel.DataAnnotations;

namespace ShoppingWebAPI.DAL.Entities
{
    public class State : Entity
    {
        [Display(Name = "Estado")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Pais")]
        public Country Country { get; set; }

        [Display(Name = "ID Pais")]
        public Guid CountryId { get; set; }

        [Display(Name = "Ciudades")]
        public ICollection<City> Cities { get; set; }

        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
