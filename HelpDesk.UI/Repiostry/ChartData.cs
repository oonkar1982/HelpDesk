using HelpDesk.Common.Models;
using HelpDesk.UI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HelpDesk.UI.Repiostry
{
	public class ChartData
	{
		
		public ChartData(AppDbContext context)
		{
			_context = context;
		}
		readonly AppDbContext _context = new AppDbContext();
		public List<DataPoint> GetChartData() {

			//get status record
			 var result = _context.incidents
			.GroupBy(i => i.StateCode)
				.Select(g => new { Mon = g.Key, Count = g.Count() })
			.ToList();

			return result.Select(i => new DataPoint(i.Mon.ToString(), i.Count)).ToList<DataPoint>();
		} 
	}
}
