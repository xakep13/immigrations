using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.Core.Enums
{
    public enum Gender
    {
        [LocalDescription("Male", NameResourceType = typeof(Locals))]
        Male,

        [LocalDescription("Female", NameResourceType = typeof(Locals))]
        Female
    }
}
