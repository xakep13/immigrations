using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.Services.Public
{
	public class CmsDamagedHousingCategoryList
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<CmsDamagedHousingCategoryListItem> Categories { get; set; }
	}
}
