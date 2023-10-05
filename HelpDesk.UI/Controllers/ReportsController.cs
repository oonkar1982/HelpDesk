using HelpDesk.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;

namespace HelpDesk.UI.Controllers
{
	public class ReportsController : Controller
	{
		private readonly AppDbContext _context;




		public ReportsController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{

			var list = (from i in _context.incidents
						join c in _context.customers on i.CustomerId equals c.CustomerId
						select new { i.Title, i.TicketNumber, c.Name }).ToList();


			using (var fs = new FileStream("Reports\\Untitled.rdl", FileMode.Open, FileAccess.Read))
			{
				LocalReport report = new LocalReport();
				report.LoadReportDefinition(fs);
				report.DataSources.Add(new Microsoft.Reporting.NETCore.ReportDataSource(name: "DataSet1", list));
				//var parameters = new[] { new Microsoft.Reporting.NETCore.ReportParameter("Title", "Invoice 4/2020") };
				//report.SetParameters(parameters);
				byte[] pdf = report.Render("PDF");
				return File(pdf, contentType: "application/pdf");
			}


		}

	}

	
}
