using HelpDesk.Common.Models;
using HelpDesk.UI.Data;
using HelpDesk.UI.Models;
using HelpDesk.UI.Repiostry;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace HelpDesk.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserRep _userRep;
        
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
		{
			_logger = logger; _context = context; _userRep = new UserRep(_context);
        }

        [Authorize]
        public IActionResult Index()
		{
            var cookie = User.Claims.FirstOrDefault(
             c => c.Type == ClaimTypes.Sid)?.Value; 

            Console.WriteLine(cookie);
            Charts();

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {


            var user = await _userRep.GetUser(email, password);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName ?? string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email ?? string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString() ?? string.Empty));


            

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);          
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public void Charts()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("NXP", 14));
            dataPoints.Add(new DataPoint("Infineon", 10));
            dataPoints.Add(new DataPoint("Renesas", 9));
            dataPoints.Add(new DataPoint("STMicroelectronics", 8));
            dataPoints.Add(new DataPoint("Texas Instruments", 7));
            dataPoints.Add(new DataPoint("Bosch", 5));
            dataPoints.Add(new DataPoint("ON Semiconductor", 4));
            dataPoints.Add(new DataPoint("Toshiba", 3));
            dataPoints.Add(new DataPoint("Micron", 3));
            dataPoints.Add(new DataPoint("Osram", 2));
            dataPoints.Add(new DataPoint("Others", 35));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

           
        }
    }
}