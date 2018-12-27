using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Public
{
    public class RecordingAtReceptionModel
    {
        [Required(ErrorMessage = "поле 'ім'я' є обов'язковим")]
        public string name { get; set; }

        [Required(ErrorMessage = "поле 'телефон' є обов'язковим")]
        [RegularExpression("^[0-9]{10}$",
            ErrorMessage ="поле 'телефон' повинно складатися з 10 цифр")]
        public string phone { get; set; }

        [Required(ErrorMessage = "поле 'електронна адреса' є обов'язковим")]
        [RegularExpression("^[0-9a-zA-Z._-]+@[0-9a-zA-Z._-]+\\.[a-z]{2,4}$",
            ErrorMessage="поле 'електронна адреса' повинно відповідати шаблону 'example@mail.com'")]
        public string email { get; set; }

        [Required(ErrorMessage = "поле 'коментар' є обов'язковим")]
        public string comment { get; set; }
    }
}
