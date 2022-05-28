using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BestCompanyWebApp.Data;
using BestCompanyWebApp.Models;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using System.Collections;

namespace BestCompanyWebApp.Controllers
{
    public class ActionModelsController : Controller
    {
        private readonly BestCompanyWebAppContext _context;

        public ActionModelsController(BestCompanyWebAppContext context)
        {
            _context = context;
        }

        // GET: ActionModels
        public async Task<IActionResult> Index()
        {
            var bestCompanyWebAppContext = _context.ActionModels.Include(a => a.Employee);
            return View(await bestCompanyWebAppContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult GetAll(DataSourceLoadOptions loadOptions)
        {
            var ticketTemplates = _context.ActionModels.ToList();
            return Json(DataSourceLoader.Load(ticketTemplates, loadOptions));
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new ActionModel();
            var _values = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, _values);

            if (!TryValidateModel(model))
                return BadRequest();

            var result = _context.ActionModels.Add(model);
            _context.SaveChanges();

            return Json(result.Entity.ActionId);
        }

        // GET: ActionModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ActionModels == null)
            {
                return NotFound();
            }

            var actionModel = await _context.ActionModels.FindAsync(id);
            if (actionModel == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employee>(), "EmployeeId", "EmployeeName", actionModel.EmployeeId);
            return View(actionModel);
        }

        // POST: ActionModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActionId,ActionType,EmployeeId,ActionDate")] ActionModel actionModel)
        {
            if (id != actionModel.ActionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActionModelExists(actionModel.ActionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employee>(), "EmployeeId", "EmployeeId", actionModel.EmployeeId);
            return View(actionModel);
        }

        // GET: ActionModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.ActionModels == null)
            {
                return Problem("Entity set 'BestCompanyWebAppContext.ActionModel'  is null.");
            }
            var actionModel = await _context.ActionModels.FindAsync(id);
            if (actionModel != null)
            {
                _context.ActionModels.Remove(actionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActionModelExists(int id)
        {
          return (_context.ActionModels?.Any(e => e.ActionId == id)).GetValueOrDefault();
        }

        private void PopulateModel(ActionModel model, IDictionary values)
        {
            string ACTIONTYPE = nameof(ActionModel.ActionType);
            string EMPLOYEEID = nameof(ActionModel.EmployeeId);
            string ACTIONDATE = nameof(ActionModel.ActionDate);

            if (values.Contains(ACTIONTYPE))
            {
                model.ActionType = Convert.ToString(values[ACTIONTYPE]);
            }

            if (values.Contains(EMPLOYEEID))
            {
                model.EmployeeId = Convert.ToInt32(values[EMPLOYEEID]);
            }

            if (values.Contains(ACTIONDATE))
            {
                model.ActionDate = Convert.ToDateTime(values[ACTIONDATE]);
            }
        }
    }
}
