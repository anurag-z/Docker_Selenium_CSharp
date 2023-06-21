
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


    public class Driver
    {

    static IWebDriver _driver;
    public Driver()
    {
        intialise();


    }

    public void intialise()
    {
        if (_driver == null)
        {
            //string driverpath = "/opt/selenium/";
            //string driverexecutable = "chromedriver";

            //ChromeOptions options = new ChromeOptions();
            //options.AddArguments("headless");
            //options.AddArguments("no-sandbox");
            //options.AddArgument("--whiitelisted-ips");
            //options.AddArgument("--disable-extensions");
            //options.BinaryLocation = "/opt/google/chrome/chrome";
            //ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverpath, driverexecutable);

            //_driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(30));

          //  ChromeOptions options = new ChromeOptions();
           // options.AddArguments(resol)
             _driver = new ChromeDriver();
            _driver.Manage().Window.Size = new Size(400, 400);
          
        }


    }

    public IWebDriver getdriver()
    {

        return _driver;

    }
    public  static void  teardown()
    {
       // var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();

       

        _driver.Close();
        _driver.Quit();
        
    }


}


