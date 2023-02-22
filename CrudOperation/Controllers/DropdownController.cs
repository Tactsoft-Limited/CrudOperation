using CrudOperation.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperation.Controllers
{
    public class DropdownController : Controller
    {
        private readonly AppDbContext _context;

        public DropdownController(AppDbContext context)
        {
            _context = context;
           
        }

        public JsonResult GetStatesByCountryId(int countryId)
        {
            List<State> statesList = new List<State>();
            statesList = (from state in _context.States
                          where state.CountryId == countryId
                          select state).ToList();

            return Json(statesList);

        }

        public JsonResult GetCitiesByStateId(int stateId)
        {
            List<City> citiesList = new List<City>();

            citiesList = (from city in _context.Cities
                          where city.StateId == stateId
                          select city).ToList();

            return Json(citiesList);
        }

    }
}
