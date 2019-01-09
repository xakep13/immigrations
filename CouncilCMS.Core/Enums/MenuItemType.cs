using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.Core.Enums
{
    public enum MenuItemType
    {
        [LocalDescription("Page", NameResourceType = typeof(Locals))]
        Page = 0,

        [LocalDescription("Person", NameResourceType = typeof(Locals))]
        Person = 1,

        [LocalDescription("ArticleCategory", NameResourceType = typeof(Locals))]
        ArticleCategory = 2,

        [LocalDescription("PersonCategory", NameResourceType = typeof(Locals))]
        PersonCategory = 3,

		[LocalDescription("Url", NameResourceType = typeof(Locals))]
		Url = 4,

		[LocalDescription("File", NameResourceType = typeof(Locals))]
		File = 5,

		[LocalDescription("NoLink", NameResourceType = typeof(Locals))]
		None = 6,

		[LocalDescription("EnterpriseCategory", NameResourceType = typeof(Locals))]
        EnterpriseCategory = 7,

        [LocalDescription("DocumentCategory", NameResourceType = typeof(Locals))]
        DocumentCategory = 8,

		[LocalDescription("Article", NameResourceType = typeof(Locals))]
		Article = 9,

		[LocalDescription("Enterprise", NameResourceType = typeof(Locals))]
		Enterprise = 10,

		[LocalDescription("Document", NameResourceType = typeof(Locals))]
		Document = 11,

		[LocalDescription("CouncilSession", NameResourceType = typeof(Locals))]
        CouncilSession = 12,

		[LocalDescription("DamagedHousing", NameResourceType = typeof(Locals))]
		DamagedHousing = 13,

		[LocalDescription("DamagedHousingCategory", NameResourceType = typeof(Locals))]
		DamagedHousingCategory = 14,
	}
}
