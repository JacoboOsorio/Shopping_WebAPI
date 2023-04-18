﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebAPI.DAL;
using ShoppingWebAPI.DAL.Entities;

namespace ShoppingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public StatesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetStatesById/{id}")]
        public async Task<ActionResult<IEnumerable<Country>>> GetStatesById(Guid? stateId)
        {
            var state = await _context.States.FirstOrDefaultAsync(s => s.Id == stateId);

            if(state == null) return NotFound();

            return Ok(state);
        }

        [HttpGet, ActionName("Get")]
        [Route("GetStatesByCountryId")]
        public async Task<ActionResult<IEnumerable<State>>> GetStatesByCountryId(Guid? countryId)
        {
            var states = await _context.States.Where(c => c.CountryId == countryId).ToListAsync();

            if (states == null) return NotFound();

            return Ok(states);
        }

        [HttpPost, ActionName("Create")]
        [Route("CreateState")]
        public async Task<ActionResult> CreateState(State state, Guid countryId)
        {
            try
            {
                state.Id = Guid.NewGuid();
                state.CreatedDate = DateTime.Now;
                state.CountryId = countryId;
                state.Country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == countryId);
                state.ModifiedDate = null;
                state.Cities = new List<City>();

                _context.States.Add(state);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate")) return Conflict(String.Format("{0} ya existe en {1}", state.Name, state.Country.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(state);
        }

        [HttpPut, ActionName("Edit")]
        [Route("EditState/{id}")]
        public async Task<ActionResult> EditState(Guid stateId, State state)
        {
            try
            {
                if (stateId != state.Id) return NotFound("State not found");
                
                state.ModifiedDate = DateTime.Now;

                _context.States.Update(state);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate")) return Conflict(String.Format("{0} ya existe en {1}", state.Name, state.Country.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(state);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("DeleteState/{id}")]
        public async Task<ActionResult> DeleteState(Guid? stateId)
        {
            if (_context.States == null) return Problem("Entity set 'DataBaseContext.States' is null.");

            var state = await _context.States.FirstOrDefaultAsync(c => c.Id == stateId);

            if (state == null) return NotFound("State not found");

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return Ok(String.Format("El estado {0} fue eliminado!", state.Name));
        }
    }
}
