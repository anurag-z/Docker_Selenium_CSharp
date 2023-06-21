using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using log4net;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Allure.Commons;
using System.ComponentModel.DataAnnotations;
using LivingDoc.Dtos;
using OpenQA.Selenium;

[Binding]
public class Hooks : Driver

{
    protected ScenarioContext _scenarioContext;
    protected static string Timestamp_;
    protected static string filename;
    private readonly Driver _driver;
    private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public Hooks(ScenarioContext scenariocontext)
    {

        _scenarioContext = scenariocontext;
           _driver = new Driver();
    }
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        //Set default working directory for NUnit to store allure results
        Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


         Timestamp_=DateTime.Now.ToString("hhmmss");

    }
    [BeforeScenario]
    public void BF()
    {
        logs();
        log.Info("Logging has started...");
        log.Info("Time stamp"+Timestamp_);


    }

    [AfterScenario]
    public void AF()
    {
        try
        {
            Attaching_Logs();
            if (_scenarioContext.TestError != null)
            {
                var screenshot = ((ITakesScreenshot)_driver.getdriver()).GetScreenshot();
                var filename = _scenarioContext.ScenarioInfo.Title + DateTime.Now.ToString("yyyy-MM-dd") + ".png";
                var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Screenshot\\" + filename;
                var filepath = Path.GetDirectoryName(@"..\..\..\Screenshot\");
                screenshot.SaveAsFile(dir, ScreenshotImageFormat.Png);
                AllureLifecycle.Instance.AddAttachment(filename, "image/png", dir);
                screenshot.SaveAsFile(filepath, ScreenshotImageFormat.Png);
            }
        }
        catch (Exception e)
        { 
        }

    }
    [AfterTestRun]
    public static  void AfterTestRun()
    {
       

        Driver.teardown();
    }
    public static ILog getlog()
    {
        return log;
    }

    public void logs()
    {

         filename = _scenarioContext.ScenarioInfo.Title + Timestamp_;


        Hierarchy LogHire = (Hierarchy)log4net.LogManager.GetRepository();

        // var logger = LogHire.GetLogger(_scenarioContext.ScenarioInfo.Title);

        // var repository = LoggerManager.CreateRepository(_scenarioContext.ScenarioInfo.Title);

        PatternLayout playout = new PatternLayout();

        playout.ConversionPattern = " % date [%thread] %-5level %logger - %message%newline";

        playout.ActivateOptions();



        RollingFileAppender rFileAppender = new RollingFileAppender();

        rFileAppender.AppendToFile = false;

        //    rFileAppender.File = @".\allure-results\Logs\" + _scenarioContext.ScenarioInfo.Title + "";

        rFileAppender.File = @".\allure-results\Logs\";
        rFileAppender.Layout = playout;

        rFileAppender.MaxSizeRollBackups = 5;

        rFileAppender.MaximumFileSize = "1GB";

        rFileAppender.RollingStyle = RollingFileAppender.RollingMode.Size;

        rFileAppender.StaticLogFileName = true;

        rFileAppender.ActivateOptions();

        LogHire.Root.AddAppender(rFileAppender);

        MemoryAppender memAppender = new MemoryAppender();

        memAppender.ActivateOptions();

        LogHire.Root.AddAppender(memAppender);

        LogHire.Root.Level = Level.Info;

        LogHire.Configured = true;

        log = LogManager.GetLogger(filename);



        foreach (var appender in log.Logger.Repository.GetAppenders())

        {

            try

            {



                string? filePath = Path.GetDirectoryName(((log4net.Appender.FileAppender)appender).File);

                var fileName = Path.Combine(filePath, filename);

                ((log4net.Appender.FileAppender)appender).File = fileName + ".log";

                ((log4net.Appender.FileAppender)appender).ActivateOptions();

                _scenarioContext["LogFileFullPath"] = fileName + ".log";

                _scenarioContext["LogFolderPath"] = filePath;



            }

            catch (Exception e)

            {

                log.Error(e.Message);



            }

        }
    }

    public void Attaching_Logs()

    {




        var tempLogFolder = (string)_scenarioContext["LogFolderPath"] + "\\Temp";

        Directory.CreateDirectory(tempLogFolder);

        var tempLogFile = Path.Combine(tempLogFolder, filename + ".log");

        File.Copy((string)_scenarioContext["LogFileFullPath"], tempLogFile);

        AllureLifecycle.Instance.AddAttachment(tempLogFile, filename);

        File.Delete(tempLogFile);





    }
}

