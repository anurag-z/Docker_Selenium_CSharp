using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Reflection;

namespace Docker_Dummy.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        Driver _driver=new Driver();

        //public CalculatorStepDefinitions(ScenarioContext scenariocontext) : base(scenariocontext)
        //{
        //  //  _scenarioContext=scenariocontext;
        //}



        //private readonly ScenarioContext _scenarioContext;

        //public CalculatorStepDefinitions(ScenarioContext scenarioContext)
        //{
        //    _scenarioContext = scenarioContext;
        //}

        [Given(@"I Navigate to URL")]
        public void GivenINavigateToURL()
        {
            _driver.getdriver().Navigate().GoToUrl("https://www.google.com");
        //    _scenarioContext["driver"] =_driver.getdriver();
            Thread.Sleep(10000);
            Hooks.getlog().Info("Navigate to" + _driver.getdriver().Url);
       //     _scenarioContext["asas"] = "as";
            //Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        [Given(@"I Search for youtube")]
        public void GivenISearchForYoutube()
        {
         //   _driver.getdriver().FindElement(By.XPath("//button[contains(@aria-label,'thanks')]")).Click();
            _driver.getdriver().FindElement(By.XPath("//*[@aria-label=\"Search\"]")).SendKeys("Anurag");
            Thread.Sleep(3000);
            _driver.getdriver().FindElement(By.XPath("//*[@aria-label='Google Search']")).Click();

            Thread.Sleep(30000);
            Assert.IsTrue(_driver.getdriver().Title.Contains("Anuragqq"));
            Hooks.getlog().Info("Expected value :Anurag");
            Hooks.getlog().Info("Actual value :"+_driver.getdriver().Title);
            //Hooks.getlog().Logger.Log();
        }

        

    }
}