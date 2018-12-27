using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.Core.Enums
{
    public enum ContentType
    {       
        [LocalDescription("Ckeditor", NameResourceType = typeof(Locals))]
        Ckeditor = 0,

        [LocalDescription("Image", NameResourceType = typeof(Locals))]
        Image = 1,

        [LocalDescription("Gallery", NameResourceType = typeof(Locals))]
        Gallery = 2,

        [LocalDescription("Table", NameResourceType = typeof(Locals))]
        Table = 3,

        [LocalDescription("Video", NameResourceType = typeof(Locals))]
        Video = 4,

        [LocalDescription("Html", NameResourceType = typeof(Locals))]
        Html = 5,

        [LocalDescription("Map", NameResourceType = typeof(Locals))]
        Map = 6,

        [LocalDescription("Form", NameResourceType = typeof(Locals))]
        Form = 7,

        [LocalDescription("File", NameResourceType = typeof(Locals))]
        File = 8,

        [LocalDescription("Widget", NameResourceType = typeof(Locals))]
        Widget = 9,

        [LocalDescription("TextBlock", NameResourceType = typeof(Locals))]
        TextBlock = 10,

        [LocalDescription("Menu", NameResourceType = typeof(Locals))]
        Menu = 11,

        [LocalDescription("Accordion", NameResourceType = typeof(Locals))]
        Accordion = 12,

        [LocalDescription("Collapse", NameResourceType = typeof(Locals))]
        Collapse = 13
    }
}
