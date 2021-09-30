using Snapshooter.NUnit;
using TechTalk.SpecFlow;
using SpecFlow.Contrib.JsonData.SpecNUnitTests.Models;

namespace SpecFlow.Contrib.JsonData.SpecNUnitTests.Steps
{
    [Binding]
    public class UsersStepDefinitions
    {
        User _user;

        [Given(@"I got user (.*), (.*), (.*)")]
        public void GivenIGotUser(string login, string firstName, string lastName)
        {
            _user = new User(login, firstName, lastName);
        }

        [Then(@"user should match to snapshoot")]
        public void ThenUserShouldMatchToSnapshoot()
        {
            _user.MatchSnapshot($"{_user.Login}_{_user.FirstName}_{_user.LastName}");
        }
    }
}
