using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebAPI.DAL;
using ShoppingWebAPI.DAL.Entities;

namespace ShoppingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public CitiesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _context.Cities.ToListAsync();

            if(cities == null) return NotFound();

            return cities;
        }

        [HttpGet, ActionName("GetCity")]
        [Route("GetCityById/{id}")]
        public async Task<ActionResult<City>> GetCityById(Guid? id)
        {
            var city = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);

            if (city == null) return NotFound();

            return Ok(city);
        }

        [HttpGet, ActionName("Get")]
        [Route("GetCitiesByStateId")]
        public async Task<ActionResult<IEnumerable<City>>> GetCitiesByStateId(Guid? stateId)
        {
            var cities = await _context.Cities.Where(c => c.StateId == stateId).ToListAsync();

            if (cities == null) return NotFound();

            return Ok(cities);
        }

        [HttpPost, ActionName("CreateCity")]
        [Route("CreateCity")]
        public async Task<ActionResult> CreateCity(City city)
        {
            try
            {
                city.Id = Guid.NewGuid();
                city.CreatedDate = DateTime.Now;

                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate")) return Conflict(String.Format("{0} ya existe", city.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(city);
        }

        [HttpPut, ActionName("EditCity")]
        [Route("EditCity/{id}")]
        public async Task<ActionResult> EditCity(Guid? id, City city)
        {
            try
            {
                if (id != city.Id) return NotFound("City not found");
                
                city.ModifiedDate = DateTime.Now;

                _context.Cities.Update(city);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate")) return Conflict(String.Format("{0} ya existe", city.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(city);
        }

        [HttpDelete, ActionName("DeleteCity")]
        [Route("DeleteCity/{id}")]
        public async Task<ActionResult> DeleteCity(Guid? id)
        {
            if (_context.Cities == null) return Problem("Entity set 'DataBaseContext.Cities' is null.");

            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);

            if (city == null) return NotFound("City not found");

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return Ok(String.Format("La ciudad {0} fue eliminado!", city.Name));
        }
    }
}
