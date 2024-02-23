namespace Bloggie.Web.Models.ViewModels {
    public class UserViewModel {
        //prop de hien thi danh sach user 
        public List<User> Users { get; set; }

        //prop de bind vao form tao moi user
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AdminRoleCheckbox { get; set; }
    }
}