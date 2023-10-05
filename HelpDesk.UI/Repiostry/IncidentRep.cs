using HelpDesk.Common.Entities;
using HelpDesk.Common.Helpers;
using HelpDesk.Common.Models;
using HelpDesk.UI.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HelpDesk.UI.Repiostry
{
    public class IncidentRep
    {
        
        public IncidentRep(AppDbContext context)
        {
            _context = context;
        }
        readonly AppDbContext _context= new  AppDbContext();
        
        public async Task<List<IncidentModel>> getList()
        {
           

            var list =  (from i in _context.incidents
                    join c in _context.customers on i.CustomerId equals c.CustomerId
                    select new IncidentModel()
                    {
                        IncidentId = i.IncidentId,
                        CaseOrigin = _context.map.FirstOrDefault(m => m.AttributeName == "CaseOriginCode" && m.AttributeValue == i.CaseTypeCode).DisplayValue,
                        Priority = _context.map.FirstOrDefault(m => m.AttributeName == "PriorityCode" && m.AttributeValue == i.PriorityCode).DisplayValue,
                        Status = _context.map.FirstOrDefault(m => m.AttributeName == "statuscode" && m.AttributeValue == i.StatusCode).DisplayValue,
                        TicketNumber = i.TicketNumber,
                        CreatedOn = i.CreatedOn,
                        Customer = c.Name,
                        Phone = c.Telephone1,
                        CreatedBy = _context.users.FirstOrDefault(m => m.Id == i.CreatedBy).FirstName
                    });


            return await list.ToListAsync();
        }

        public async Task<PaginatedData<IncidentModel>> PaginatedGetList(int pageIndex, int pageSize)
        {
            var list = await (from i in _context.incidents
                        join c in _context.customers on i.CustomerId equals c.CustomerId
                        select new IncidentModel()
                        {
                            IncidentId = i.IncidentId,
                            CaseOrigin = _context.map.FirstOrDefault(m => m.AttributeName == "CaseOriginCode" && m.AttributeValue == i.CaseTypeCode).DisplayValue,
                            Priority = _context.map.FirstOrDefault(m => m.AttributeName == "PriorityCode" && m.AttributeValue == i.PriorityCode).DisplayValue,
                            Status = _context.map.FirstOrDefault(m => m.AttributeName == "statuscode" && m.AttributeValue == i.StatusCode).DisplayValue,
                            TicketNumber = i.TicketNumber,
                            CreatedOn = i.CreatedOn,
                            Customer = c.Name,
                            Phone = c.Telephone1,
                            CreatedBy = _context.users.FirstOrDefault(m => m.Id == i.CreatedBy).FirstName
                        }).ToListAsync();

           
            return PaginatedData<IncidentModel>.CreateList(list, pageIndex, pageSize);
        }
        public EditIncidentModel GetIncidentById(Guid itemid)
        {
            EditIncidentModel item = new EditIncidentModel();

            if (itemid == Guid.Empty)
            {
                item = new EditIncidentModel()
                {
                    incident = new IncidentCreateModel()
                    {
                        IncidentId = new Guid()
                    },
                    Customers = _context.customers.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.CustomerId.ToString(),
                    }).ToList(),
                    droplists = _context.map.Where(x => x.ObjectTypeCode == 1).Select(x => new dropdownelement()
                    {
                        DisplayValue=x.DisplayValue,AttributeName=x.AttributeName,AttributeValue= x.AttributeValue
                    }).ToList()
                    
                };
            }
            else
            {
                item = new EditIncidentModel()
                {
                    incident = _context.incidents.Select(e => new IncidentCreateModel()
                    {
                        IncidentId = e.IncidentId,
                        CaseOriginCode = e.CaseOriginCode,CustomerId = e.CustomerId,PriorityCode= e.PriorityCode,
                        StateCode=e.StateCode,StatusCode=e.StatusCode,TicketNumber=e.TicketNumber,
                        Title=e.Title,CaseTypeCode=e.CaseTypeCode,CreatedBy=e.CreatedBy,CreatedOn=e.CreatedOn,OwnerId=e.OwnerId,description=e.description,ModifiedOn=e.ModifiedOn,
                    }).Where(e => e.IncidentId == itemid).FirstOrDefault(),
                    Customers = _context.customers.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.CustomerId.ToString(),
                    }).ToList(),
                    droplists = _context.map.Where(x => x.ObjectTypeCode == 1).Select(x => new dropdownelement()
                    {
                        DisplayValue = x.DisplayValue,
                        AttributeName = x.AttributeName,
                        AttributeValue = x.AttributeValue
                    }).ToList(),
                    Owners= _context.users.Select(x => new SelectListItem()
                    {
                        Text = x.FullName,
                        Value = x.Id.ToString(),
                    }).ToList(),

                };
            }

            return item;
        }
        public IncidentCreateModel GetCustomerId(Guid itemid)
        {
            IncidentCreateModel item = new IncidentCreateModel();

            if (itemid != Guid.Empty)
            {
                item = new IncidentCreateModel()
                {

                    
                    IncidentId = new Guid(),CustomerId = itemid,
                    
                    Customers = _context.customers.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.CustomerId.ToString(),
                    }).ToList(),
                    droplists = _context.map.Where(x => x.ObjectTypeCode == 1).Select(x => new dropdownelement()
                    {
                        DisplayValue = x.DisplayValue,
                        AttributeName = x.AttributeName,
                        AttributeValue = x.AttributeValue
                    }).ToList()
                };
            }
            

            return item;
        }
    
        public bool Save( IncidentCreateModel model )
        {
            if (model !=null)
            {
                var item = _context.incidents.Find(model.IncidentId);
                if (item == null)
                {
                    Incident incident = new Incident()
                    {
                        IncidentId = Guid.NewGuid(),
                        CustomerId = model.CustomerId,
                        Title = model.Title,
                        TicketNumber = model.TicketNumber,
                        CreatedOn = DateTime.Now,
                        CreatedBy = model.CreatedBy,
                        CaseTypeCode = model.CaseTypeCode,
                        CaseOriginCode = model.CaseOriginCode,
                        description = model.description,
                        ModifiedOn = model.ModifiedOn,
                        OwnerId = model.OwnerId

                    };
                    _context.Add(incident);
                }
                else
                {
					
					item.Title = model.Title;item.TicketNumber = model.TicketNumber;

                    if(model.CreatedOn != null) {  item.CreatedOn = DateTime.Now; }
                    item.PriorityCode= model.PriorityCode;

					item.CreatedBy = model.CreatedBy;
					item.CaseTypeCode = model.CaseTypeCode;
					item.CaseOriginCode = model.CaseOriginCode;
					item.description = model.description;
                    item.ModifiedOn = DateTime.Now;
                    item.OwnerId = model.OwnerId;
                    item.StateCode = model.StateCode;
                    _context.Entry(item).State = EntityState.Modified;
                    


				}
                _context.SaveChanges();
                return true;

            }
            return false;

        }
    }
}
