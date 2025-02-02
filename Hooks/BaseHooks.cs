using TrippleZero.Common;
using TrippleZero.StepDefinitions;
using TrippleZero.Utils;

namespace TrippleZero.Hooks
{
    [Binding]
    internal class BaseHooks : TestBase
    {
        private readonly ScenarioContext _scenarioContext;
        private ITestOutputHelper _output;
        private static readonly string _browserType = EnvironmentManager.GetOrThrow("BrowserType");
        public BaseHooks(ScenarioContext scenarioContext, ITestOutputHelper output) : base(_browserType)
        {
            _scenarioContext = scenarioContext;
            _output = output;
        }



        [BeforeScenario("@StandardUserAuthenticated")]
        public async Task LoginWithValidUser()
        {
            LoginStepDefinitions loginSteps = new LoginStepDefinitions(_scenarioContext, _output);
            await loginSteps.GivenINavigateToTheLoginPage();
            await loginSteps.WhenIEnterValidUsernameAndPassword();
            await loginSteps.WhenIClickTheLoginButton();
        }
    }
}
