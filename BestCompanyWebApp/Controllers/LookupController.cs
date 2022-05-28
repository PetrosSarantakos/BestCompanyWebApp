using BestCompanyWebApp.Data;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BestCompanyWebApp.Controllers
{
    public class LookupController : Controller
    {

        private readonly BestCompanyWebAppContext _context;

        public LookupController(BestCompanyWebAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult EmployeeLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Employees
                         select new
                         {
                             Value = i.EmployeeId,
                             Text = i.EmployeeName
                         };

            return Json(DataSourceLoader.Load(lookup, loadOptions));
        }

    }
}
