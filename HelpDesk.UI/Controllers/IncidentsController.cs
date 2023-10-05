using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Common.Entities;
using HelpDesk.UI.Data;
using HelpDesk.UI.Repiostry;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using HelpDesk.Common.Models;

namespace HelpDesk.UI.Controllers
{
    [Authorize]
    public class IncidentsController : Controller
    {
        private readonly AppDbContext _context;

        public IncidentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
       
        // GET: Incidents
        public async Task<IActionResult> Index(int pageIndex = 1)
        {


            int pageSize = 5;
            IncidentRep rep = new IncidentRep(_context);
            if (pageIndex < 0 || pageSize < 0)
                return BadRequest();

            var data = await rep.PaginatedGetList(pageIndex, pageSize);
            return View(data);
        }

        // GET: Incidents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.incidents == null)
            {
                return NotFound();
            }

            var incident = await _context.incidents
                .Include(i => i.Customer)
                .Include(i => i.Owner)
                .FirstOrDefaultAsync(m => m.IncidentId == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // GET: Incidents/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.customers, "CustomerId","Name" );
            ViewData["OwnerId"] = new SelectList(_context.users,  "Id", "FirstName");
            ViewData["StateCode"] = new SelectList(_context.map.Where(x=>x.AttributeName== "statecode"), "AttributeValue", "DisplayValue");
            ViewData["StatusCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "statuscode"), "AttributeValue","DisplayValue" );
            ViewData["CaseTypeCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "casetypecode"), "AttributeValue", "DisplayValue");
            ViewData["CaseOriginCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "CaseOriginCode"), "AttributeValue", "DisplayValue");
            ViewData["PriorityCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "PriorityCode"), "AttributeValue", "DisplayValue"); 
            return View();
        }

        // POST: Incidents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncidentId,CaseOriginCode,CaseTypeCode,Title,CreatedOn,TicketNumber,PriorityCode,ModifiedOn,CreatedBy,StateCode,StatusCode,CustomerId,OwnerId,OwnerIdType,ActivitiesComplete,ExistingCase")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                incident.IncidentId = Guid.NewGuid();
                _context.Add(incident);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.customers, "CustomerId", "CustomerId", incident.CustomerId);
            ViewData["OwnerId"] = new SelectList(_context.users, "Id", "Id",incident.OwnerId);
            ViewData["StateCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "statecode"), "AttributeValue", "DisplayValue",incident.StateCode);
            ViewData["StatusCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "statuscode"), "AttributeValue", "DisplayValue",incident.StateCode);
            ViewData["CaseTypeCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "casetypecode"), "AttributeValue", "DisplayValue",incident.CaseTypeCode);
            
            return View(incident);
        }

        // GET: Incidents/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (  id !=Guid.Empty!)
            {             
                
                    IncidentRep rep = new IncidentRep(_context);
                    var model =  rep.GetIncidentById(id);
                    ViewData["CustomerId"] = model.Customers;
                    ViewData["OwnerId"] = model.Owners;

                    GetDrpDown();
                    return View(model.incident);
            }
          
            return NotFound();
           
        }
        public void GetDrpDown()
        {

            ViewData["StateCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "statecode"), "AttributeValue", "DisplayValue");
            ViewData["StatusCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "statuscode"), "AttributeValue", "DisplayValue");
            ViewData["CaseTypeCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "casetypecode"), "AttributeValue", "DisplayValue");
            ViewData["CaseOriginCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "CaseOriginCode"), "AttributeValue", "DisplayValue");
            ViewData["PriorityCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "PriorityCode"), "AttributeValue", "DisplayValue");
        }
        // POST: Incidents/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, IncidentCreateModel incident)
        { 
            if (id != incident.IncidentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(incident.CreatedBy == null) {
                        incident.CreatedBy = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
					}
					if (incident.OwnerId == null)
					{
						incident.OwnerId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
					}


					IncidentRep rep = new(_context);
                    bool save = rep.Save(incident);
                    if(save)
                    {
                       

                    }

					
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentExists(incident.IncidentId))
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
            ViewData["CustomerId"] = new SelectList(_context.customers, "CustomerId", "CustomerId", incident.CustomerId);
            ViewData["OwnerId"] = new SelectList(_context.users, "Id", "Id", incident.OwnerId);
            ViewData["StateCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "statecode"), "AttributeValue", "DisplayValue", incident.StateCode);
            ViewData["StatusCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "statuscode"), "AttributeValue", "DisplayValue", incident.StateCode);
            ViewData["CaseTypeCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "casetypecode"), "AttributeValue", "DisplayValue", incident.CaseTypeCode);
            return View(incident);
        }

        // GET: Incidents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.incidents == null)
            {
                return NotFound();
            }

            var incident = await _context.incidents
                .Include(i => i.Customer)
                .Include(i => i.Owner)
                .FirstOrDefaultAsync(m => m.IncidentId == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // POST: Incidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.incidents == null)
            {
                return Problem("Entity set 'AppDbContext.incidents'  is null.");
            }
            var incident = await _context.incidents.FindAsync(id);
            if (incident != null)
            {
                _context.incidents.Remove(incident);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidentExists(Guid id)
        {
          return (_context.incidents?.Any(e => e.IncidentId == id)).GetValueOrDefault();
        }
    }
}
