using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using System.ComponentModel.DataAnnotations;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsPersonReception
    {

        public int PersonId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valRequired")]
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valRequired")]
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valRequired")]
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string Mobile { get; set; }

        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valEmail")]
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valRequired")]
        [StringLength(2000, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string Text { get; set; }

        public string IpAddress { get; set; }
    }
}
