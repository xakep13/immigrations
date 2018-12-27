using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.Core.Enums
{
    public enum CmsItemState
    {
        [LocalDescription("All", NameResourceType = typeof(Locals))]
        All,

        [LocalDescription("Published", NameResourceType = typeof(Locals))]
        Published,

        [LocalDescription("NotPublished", NameResourceType = typeof(Locals))]
        NotPublished,

        [LocalDescription("Deleted", NameResourceType = typeof(Locals))]
        Deleted        
    }
}
