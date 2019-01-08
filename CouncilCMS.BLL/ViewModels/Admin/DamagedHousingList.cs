using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
	public class DamagedHousingList
	{
		public int Page { get; set; }
		public string Query { get; set; }
		public string DateRange { get; set; }
		public int DateType { get; set; }
		public int? CategoryId { get; set; }
		public string SortBy { get; set; }
		public int SortDirection { get; set; }
		public int ItemState { get; set; }
		public List<SelectListItem> Categories { get; set; }

		public int? User { get; set; }
		public int UserAction { get; set; }
		public List<SelectListItem> Users { get; set; }
	}
}
