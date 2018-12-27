using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.Core.Enums
{
    public enum VoteResult
    {
        [LocalDescription("Accepted", NameResourceType = typeof(Locals))]
        Accepted = 1,

        [LocalDescription("Declined", NameResourceType = typeof(Locals))]
        Declined = 2,

        //[LocalDescription("Transfered", NameResourceType = typeof(Locals))]
        //Transfered = 3    
    }
}
