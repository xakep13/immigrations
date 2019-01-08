using Bissoft.CouncilCMS.Core.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
	public class DamagedHousingEdit
	{
		[Required]
		public int Id { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string TitleUk { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string TitleRu { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string TitleEn { get; set; }

		public string DescriptionRu { get; set; }
		public string DescriptionUk { get; set; }
		public string DescriptionEn { get; set; }

		public List<string> Images { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string Email { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string Adress { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string FullName { get; set; }

		public string CreateDate { get; set; }
		public string PublishDate { get; set; }
		public string EventDate { get; set; }
		public string EditedDate { get; set; }

		public bool Published { get; set; }
		public bool AllowComments { get; set; }
		public bool ShowPublihDate { get; set; }
		public bool ShowEditDate { get; set; }

		public List<CheckedListItem> DamagedHousingCategories { get; set; }
		public List<ContentRowItem> ContentRows { get; set; }


		#region Meta

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string MetaTitleUk { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string MetaTitleRu { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string MetaTitleEn { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string MetaKeywordsUk { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string MetaKeywordsRu { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string MetaKeywordsEn { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string MetaDescriptionUk { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string MetaDescriptionRu { get; set; }

		[StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
		public string MetaDescriptionEn { get; set; }

		#endregion
	}
}
