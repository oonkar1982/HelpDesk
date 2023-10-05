using HelpDesk.Common.Entities;
using HelpDesk.Common.Models;
using HelpDesk.UI.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace HelpDesk.UI.Repiostry
{
    public class CustomerRep
    {
        public CustomerRep(AppDbContext context)
        {
            _context = context;
        }
        readonly AppDbContext _context = new AppDbContext();



        public async Task<IEnumerable<CustomerView>> get()
        {

            return await (from u in _context.customers

                          select new CustomerView()
                          {
                              CustomerId = u.CustomerId,
                              AccountNumber = u.AccountNumber,
                              Telephone = u.Telephone1,
                              PrimaryContact = _context.contacts.FirstOrDefault(m => m.ContactId == u.PrimaryContactId).FullName ?? string.Empty,
                              Name = u.Name,
                              Email = u.EmailAddress1 ?? u.EmailAddress2


                          }).ToListAsync();
        }

        //public async Task<UserEditModel> AddorEdit(Guid? Id)
        //{
        //    UserEditModel item;

        //    var _companies = await dropdownelement();
        //    item = new UserEditModel();

        //    if (Id == Guid.Empty || Id == null)
        //    {

        //        item.Id = Guid.Empty;
        //        item.Companylist = _companies;


        //    }
        //    else
        //    {
        //        if (Id.HasValue)
        //        {
        //            item = _context.users.Where(u => u.Id == Id).Select(u => new UserEditModel()
        //            {
        //                Id = u.Id,
        //                FirstName = u.FirstName,
        //                LastName = u.LastName,
        //                Mobile = u.Mobile,
        //                CompanyId = u.CompanyId,
        //                Email = u.Email,
        //                Password = u.Password
        //                ,
        //                Companylist = _companies
        //            }).FirstOrDefault();
        //        }
        //    }

        //    return item;
        //}

        public bool AddorEdit(Guid itemid, EditCustomerModel model)
        {
            try
            {
                var item = _context.customers.Where(x => x.CustomerId == itemid).FirstOrDefault();

                if (item == null)
                {
                    var _item = new Customer()
                    {CustomerId= Guid.NewGuid(),
                      Name=model.Name,
                      AccountNumber= model.AccountNumber,Description=model.Description,Telephone1 = model.Telephone1,Telephone2 = model.Telephone2,
                      EmailAddress1= model.EmailAddress1,EmailAddress2 = model.EmailAddress2,ModifiedOn=DateTime.Now,ModifiedBy=model.ModifiedBy,
                        WebSiteUrl = model.WebSiteUrl,PrimaryContactId=model.PrimaryContactId,
                    };
                    _context.customers.Add(_item);
                }
                else
                {
                                                            
                    item.Name = model.Name;
                    item.AccountNumber = model.AccountNumber;
                    item.Description = model.Description;
                    item.Telephone1 = model.Telephone1;
                    item.Telephone2 = model.Telephone2;
                    item.EmailAddress1 = model.EmailAddress1;
                    item.EmailAddress2 = model.EmailAddress2;
                    item.ModifiedOn = DateTime.Now;
                    item.ModifiedBy = model.ModifiedBy; item.WebSiteUrl = model.WebSiteUrl;
                    item.PrimaryContactId = model.PrimaryContactId;

                    _context.Entry(item).State = EntityState.Modified;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }
    }
}

