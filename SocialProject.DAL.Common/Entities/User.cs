namespace SocialProject.DAL.Common.Entities
{
    public class User : Entity<long>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string City { get; set; }

        public string SecurityStamp { get; set; }
    }
}