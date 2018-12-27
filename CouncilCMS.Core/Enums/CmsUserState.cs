using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.Core.Enums
{
    public enum CmsUserState
    {
        [LocalDescription("All", NameResourceType = typeof(Locals))]
        All,

        [LocalDescription("Actives", NameResourceType = typeof(Locals))]
        Active,

        [LocalDescription("Blockeds", NameResourceType = typeof(Locals))]
        Blocked,

        [LocalDescription("Deleted", NameResourceType = typeof(Locals))]
        Deleted        
    }
}
