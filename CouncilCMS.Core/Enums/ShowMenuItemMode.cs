using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.Core.Enums
{
    public enum ShowMenuItemMode
    {
        [LocalDescription("AllLangualges", NameResourceType = typeof(Locals))]
        AllLangualges,

        [LocalDescription("PerLanguage", NameResourceType = typeof(Locals))]
        PerLanguage
    }
}
