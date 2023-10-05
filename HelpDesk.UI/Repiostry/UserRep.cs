using HelpDesk.Common.Entities;
using HelpDesk.Common.Models;
using HelpDesk.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HelpDesk.UI.Repiostry
{
    public class UserRep
    {
        public UserRep(AppDbContext context)
        {
            _context = context;
        }
        readonly AppDbContext _context = new AppDbContext();

        public async Task<UserModel> GetUser(string name, string password)
        {
            return await _context.users.Where(x => (x.Email == name) && (x.Password == password)).Select(u => new UserModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).FirstOrDefaultAsync();
        }
        public  async Task<List<SelectListItem>> dropdownelement()
        {
          return  await _context.companies.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.CompanyId.ToString()
            }).ToListAsync();
        }

        public async Task<IEnumerable<UserModel>> get()
        {

            return await (from u in _context.users

                          select new UserModel()
                          {
                              Id = u.Id,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              Email = u.Email,
                              CompanyName = _context.companies.FirstOrDefault(m => m.CompanyId == u.CompanyId).Name,
                              Mobile = u.Mobile
                          }).ToListAsync();
        }

        public async Task<UserEditModel> AddorEdit(Guid? Id)
        {
            UserEditModel item ;

            var _companies = await dropdownelement();
            item = new UserEditModel();
            
            if (Id == Guid.Empty || Id == null) {

                item.Id = Guid.Empty;
                item.Companylist = _companies; 


            }
            else
            {
                if (Id.HasValue)
                {
                    item = _context.users.Where(u => u.Id == Id).Select(u => new UserEditModel()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName ,
                    LastName = u.LastName,
                        Mobile = u.Mobile,
                        CompanyId = u.CompanyId,
                        Email = u.Email,
                        Password = u.Password
                        ,Companylist= _companies
                    }).FirstOrDefault();
                }
            }
           
            return item;
        }

        public bool AddorEdit(Guid itemid, UserEditModel model)
        {
            try
            {
                var user = _context.users.Where(x => x.Id == itemid).FirstOrDefault();
                if (user == null)
                {
                    user = new UserDetail()
                    {
                        Id = Guid.NewGuid(),
                        CompanyId = model?.CompanyId,
                        FirstName = model?.FirstName,
                        LastName = model?.LastName,
                        Email = model?.Email,
                        Password = model?.Password,
                        IsEnabled = true,
                        Mobile = model?.Mobile
                    };
                    _context.users.Add(user);
                }
                else
                {
                    user = new UserDetail()
                    {
                        Id = user.Id,
                        CompanyId = model?.CompanyId,
                        FirstName = model?.FirstName,
                        LastName = model?.LastName,
                        Email = model?.Email,
                        Password = model?.Password,
                        IsEnabled = true,
                        Mobile = model?.Mobile
                    };
                    _context.Entry(user).State = EntityState.Modified;
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
