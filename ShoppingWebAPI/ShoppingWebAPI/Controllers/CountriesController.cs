using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebAPI.DAL;
using ShoppingWebAPI.DAL.Entities;

namespace ShoppingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public CountriesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await _context.Countries.ToListAsync(); //SELECT * FROM COUNTRIES

            if(countries == null) return NotFound();

            return countries;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Country>> GetCountryById(Guid? id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id); //SELECT * FROM COUNTRIES WHERE ID = X

            if (country == null) return NotFound();

            return Ok(country);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountry(Country country)
        {
            try
            {
                country.Id = Guid.NewGuid();
                country.CreatedDate = DateTime.Now;

                _context.Countries.Add(country);
                await _context.SaveChangesAsync(); //Aqui es donde se hace el "UPDATE"
            }
            catch(DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate")) return Conflict(String.Format("{0} ya existe", country.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(country);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]
        public async Task<ActionResult> EditCountry(Guid? id, Country country)
        {
            try
            {
                if (id != country.Id) return NotFound("Country not found");
                
                country.ModifiedDate = DateTime.Now;

                _context.Countries.Update(country);
                await _context.SaveChangesAsync();  //Aqui es donde se hace el "UPDATE"
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate")) return Conflict(String.Format("{0} ya existe", country.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(country);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteCountry(Guid? id)
        {
            if (_context.Countries == null) return Problem("Entity set 'DataBaseContext.Countries' is null.");

            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);

            if (country == null) return NotFound("Country not found");

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync(); //Hace las veces del delete en SQL

            return Ok(String.Format("El pais {0} fue eliminado!", country.Name));
        }
    }
}
