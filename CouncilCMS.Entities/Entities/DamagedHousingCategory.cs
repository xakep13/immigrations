using Bissoft.CouncilCMS.DAL.Entities;
using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
	public class DamagedHousingCategory : BaseCategoryEntity
	{
		public bool AllowOpenDataExport { get; set; }

		public virtual DamagedHousingCategory ParentCategory { get; set; }
		public virtual DamagedHousingCategory RelatedCategory { get; set; }
		public virtual DamagedHousingCategoryTemplate Template { get; set; }
		public virtual ICollection<DamagedHousingCategory> ChildCategories { get; set; } = new List<DamagedHousingCategory>();
		public virtual ICollection<DamagedHousingCategory> RelatedCategories { get; set; } = new List<DamagedHousingCategory>();
		public virtual ICollection<DamagedHousing> DamagedHousing { get; set; } 

		public DamagedHousingCategory()
		{
			DamagedHousing = new List<DamagedHousing>();
		}
	}
}
