
namespace SpecFlow.Contrib.JsonData.SpecNUnitTests.Models
{
    public class User
    {
        public string Login { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }

        public User(string login, string firstName, string lastName)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
