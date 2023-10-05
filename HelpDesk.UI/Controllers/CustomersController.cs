using HelpDesk.Common.Entities;
using HelpDesk.Common.Models;
using HelpDesk.UI.Data;
using HelpDesk.UI.Repiostry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace HelpDesk.UI.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private  AppDbContext _context;

         readonly CustomerRep _customerRep;
        public CustomersController(AppDbContext context)
        {
            _context = context;
            
        }
        public   void GetDrpDown()
        {
           
            ViewData["StateCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "statecode"), "AttributeValue", "DisplayValue");
            ViewData["StatusCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "statuscode"), "AttributeValue", "DisplayValue");
            ViewData["CaseTypeCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "casetypecode"), "AttributeValue", "DisplayValue");
            ViewData["CaseOriginCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "CaseOriginCode"), "AttributeValue", "DisplayValue");
            ViewData["PriorityCode"] = new SelectList(_context.map.Where(x => x.AttributeName == "PriorityCode"), "AttributeValue", "DisplayValue");
        }
        public async Task< IActionResult> Index()
        {
            CustomerRep rep = new CustomerRep(_context);



            var model = await rep.get();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCustomerModel customer)
        {
            if (ModelState.IsValid)
            {
            }
            return View(customer);


        }

        [HttpGet]
        public  async Task<IActionResult> Edit(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                bool isGuid = Guid.TryParse(id, out Guid customerId);
                if (isGuid && customerId != Guid.Empty)
                {

                    ViewData["Contacts"]=  new SelectList(_context.contacts, "ContactId", "FullName");

                    var model = await _context.customers.Select(e => new CustomerModel()
                    {
                        Id = customerId,
                        editCustomerModel = new EditCustomerModel()
                        {
                            CustomerId = e.CustomerId,
                            Name = e.Name,
                            PrimaryContactId = e.PrimaryContactId,
                            EmailAddress1 = e.EmailAddress1,
                            EmailAddress2 = e.EmailAddress2,
                            Telephone1 = e.Telephone1,
                            Telephone2 = e.Telephone2,
                            AccountNumber = e.AccountNumber,
                            OwnerId = e.OwnerId,
                        },
                        Incidentlist = e.Incidents.Select(i => new IncidentModel()
                       {
                            IncidentId = i.IncidentId,
                            Customer = i.Customer.Name,
                            Title = i.Title
                       }).ToList()

                    }).Where(e => e.Id == customerId).FirstOrDefaultAsync();
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(string customerid, EditCustomerModel model)
        {
            CustomerRep rep = new CustomerRep(_context);
            if (ModelState.IsValid)
            {
                model.ModifiedBy = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
                bool IsSaved = rep.AddorEdit(Guid.Parse(customerid), model);
                return RedirectToAction(nameof(Index));
            }

           return View(model);
        }

        [HttpGet]
      public IActionResult CreateIncidentByCustomerId(string customerId)
        {
            if (!String.IsNullOrWhiteSpace(customerId))
            {
                bool isGuid = Guid.TryParse(customerId, out Guid _customerId);
                if (isGuid && _customerId != Guid.Empty)
                {
                    IncidentRep rep= new IncidentRep(_context);
                    var model = rep.GetCustomerId(_customerId);
                    ViewData["CustomerId"] = model.Customers;
                    GetDrpDown();
                    return View( model);
                }
            }
            return NotFound();

        }
        public IActionResult CreateIncidentByCustomerId(IncidentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedOn = DateTime.Now;
                model.OwnerId = Guid.Parse("55DD5AD7-6475-40C2-844D-EEC2068F0B0A");
                model.CreatedBy= Guid.Parse("55DD5AD7-6475-40C2-844D-EEC2068F0B0A");
                IncidentRep rep = new IncidentRep(_context);
                bool r = rep.Save(model);
                if (r)
                {
                    TempData["SuccessMessage"] = "Save Successful";
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }



   }
}
