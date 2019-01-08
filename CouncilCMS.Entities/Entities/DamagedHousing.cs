using Bissoft.CouncilCMS.DAL.Entities;
using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
	public class DamagedHousing : BaseAuditionEntity
	{

		public string Email { get; set; }
		public string Adress { get; set; }
		public string FullName { get; set; }

		public string UrlNameRu { get; set; }
		public string UrlNameUk { get; set; }
		public string UrlNameEn { get; set; }

		public string DescriptionUk { get; set; }
		public string DescriptionRu { get; set; }
		public string DescriptionEn { get; set; }

		public string KeywordsUk { get; set; }
		public string KeywordsRu { get; set; }
		public string KeywordsEn { get; set; }

		public string MetaTitleUk { get; set; }
		public string MetaTitleRu { get; set; }
		public string MetaTitleEn { get; set; }

		public string MetaDescriptionUk { get; set; }
		public string MetaDescriptionRu { get; set; }
		public string MetaDescriptionEn { get; set; }

		public string MetaKeywordsUk { get; set; }
		public string MetaKeywordsRu { get; set; }
		public string MetaKeywordsEn { get; set; }

		public bool ShowPublihDate { get; set; }
		public bool ShowEditDate { get; set; }
		public bool AllowComments { get; set; }

		

		public DateTime? EventDate { get; set; }
		public DateTime? EditedDate { get; set; }

		public bool Saved { get; set; }
		public int ViewCount { get; set; }

		public List<string> Images { get; set; }
		public string ImageSource { get; set; }

		public virtual List<ContentRow> ContentRows { get; set; }
		public virtual List<DamagedHousingCategory> Categories { get; set; }
		public virtual List<DamagedHousingCategory> LevelOfDamage { get; set; }
	}
}
