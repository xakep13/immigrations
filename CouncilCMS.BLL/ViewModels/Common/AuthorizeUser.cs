
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class AuthorizeUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }
        public string[] Premissions { get; set; }
        public bool Blocked { get; set; }
    }
}
