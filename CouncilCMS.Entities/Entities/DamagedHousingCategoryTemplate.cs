using Bissoft.CouncilCMS.DAL.Entities;
using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
	public class DamagedHousingCategoryTemplate : BaseCategoryTemplate
	{
		public virtual ICollection<DamagedHousingCategory> Categories { get; set; } = new List<DamagedHousingCategory>();
	}
}
