using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
	public class DamagedHousingListItem
	{
		public int Id { get; set; }
		public string TitleUk { get; set; }
		public string TitleRu { get; set; }
		public string TitleEn { get; set; }
		public IEnumerable<string> CategoriesUk { get; set; }
		public IEnumerable<string> CategoriesRu { get; set; }
		public IEnumerable<string> CategoriesEn { get; set; }
		public IEnumerable<string> LevelOfDamageUk { get; set; }
		public IEnumerable<string> LevelOfDamageRu { get; set; }
		public IEnumerable<string> LevelOfDamageEn { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? PublishDate { get; set; }
		public DateTime? EditedDate { get; set; }
		public DateTime? LastEditDate { get; set; }
		public String CreateUser { get; set; }
		public String LastEditUser { get; set; }
		public int ViewCount { get; set; }
		public bool Deleted { get; set; }
		public bool Published { get; set; }
	}
}
