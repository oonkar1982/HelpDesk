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
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.UI.Controllers
{
    [Authorize]
    public class AgentsController : Controller
    {
        private readonly AppDbContext _context;

       
        

        public AgentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserDetails
        public async Task<IActionResult> Index()
        {
            UserRep item=new UserRep(_context);
            var appDbContext = item.get();
            return View(await appDbContext);
        }


        // GET: UserDetails/Create
        public async Task<IActionResult> Create(Guid? id)
        {
            UserRep item = new UserRep(_context);
            

            var userDetail = await item.AddorEdit(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            ViewData["Companies"] = new SelectList(userDetail.Companylist, "Value", "Text", userDetail.CompanyId);
            return View(userDetail);
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Mobile,IsEnabled,Password,CompanyId")] UserDetail userDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userDetail.Id = Guid.NewGuid();
                    _context.Add(userDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex) { 
                Console.Write(ex); 
            }

            ViewData["Companies"] = new SelectList(_context.companies, "CompanyId", "CompanyId", userDetail.CompanyId);
            return View(userDetail);
        }

        // GET: UserDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            UserRep item = new UserRep(_context);
            if (id == null )
            {
                return NotFound();
            }
            Thread.Sleep(100);
            var userDetail = await item.AddorEdit(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            ViewData["Companies"] = new SelectList(userDetail.Companylist,  "Value", "Text", userDetail.CompanyId);

           
            return View(userDetail);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,Email,Mobile,IsEnabled,Password,CompanyId")] UserDetail userDetail)
        {
            Thread.Sleep(100);
            if (id != userDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailExists(userDetail.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.companies, "CompanyId", "CompanyId", userDetail.CompanyId);
            return View(userDetail);
        }

        // GET: UserDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var userDetail = await _context.users
                .Include(u => u.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userDetail == null)
            {
                return NotFound();
            }

            return View(userDetail);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                if (_context.users == null)
                {
                    return Problem("Entity set 'AppDbContext.users'  is null.");
                }
                var userDetail = await _context.users.FindAsync(id);
                if (userDetail != null)
                {
                    _context.users.Remove(userDetail);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message.ToString());
            }
        }

        private bool UserDetailExists(Guid id)
        {
          return (_context.users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
