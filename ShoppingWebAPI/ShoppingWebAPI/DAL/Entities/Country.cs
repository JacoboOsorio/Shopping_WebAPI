using System.ComponentModel.DataAnnotations;

namespace ShoppingWebAPI.DAL.Entities
{
    public class Country : Entity
    {
        [Display(Name = "Pais")] //To show the field with another name
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")] //Equals "NVARCHAR()" in SQL
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] //Equals "NOT NULL" in SQL
        public string Name { get; set; }

        [Display(Name = "Estado")]
        public ICollection<State> States { get; set; } //Significa que un pais puede tener N estados

        //Propiedad de lectura que no se mapea en la tabla de la base de datos
        //Notacion flecha (=>): Indica que es una propiedad de lectura (Reemplazo del "get")
        public int StatesNumber => States == null ? 0 : States.Count;
    }
}
