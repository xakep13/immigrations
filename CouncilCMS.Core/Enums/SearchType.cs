using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.Core.Enums
{
    public enum CmsSearchType
    {
        [LocalDescription("Page", NameResourceType = typeof(Locals))]
        Page,

        [LocalDescription("Person", NameResourceType = typeof(Locals))]
        Person,

        [LocalDescription("Article", NameResourceType = typeof(Locals))]
        Article,

        [LocalDescription("Enterprise", NameResourceType = typeof(Locals))]
        Enterprise
    }
}
