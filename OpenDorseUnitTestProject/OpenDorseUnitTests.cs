using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace OpenDorseCareersTests
{
    /// <summary>
    /// QA AUTOMATION ENGINEER Developer: Tina Bean
    /// Summary description for MyOpenDorseCareersTests
    /// These two tests are to navigate to the careers page and to find specific job posting 
    /// and click on the job post link.
    /// </summary>
    [TestClass]
    public class MyOpenDorseCareersTests  
    {
        public TestContext testContextInstance;
        private IWebDriver driver;
        private string appURL;
        public int iTests;
        public int iExecuted;
        public Microsoft.Office.Interop.Outlook.Application OutlookApp;

        public MyOpenDorseCareersTests()
        {


        }

            

        [TestMethod]
        [TestCategory("Chrome")]
        public void NavigateToOpenDorseCareers() // Navigate to OpenDorse Career Page  
        {

            driver.Navigate().GoToUrl(appURL + "/");
            Thread.Sleep(5000);

            driver.FindElement(By.LinkText("Careers")).Click();

            Thread.Sleep(5000);

           
            if (driver.FindElement(By.TagName("title")).GetAttribute("innerText").Trim().Equals("BambooHR"))

            {

                Console.WriteLine(driver.Title.ToString() + " is the correct title. Currently on Careers Page.");

            }
            else
            {
                ResultsLogFileManager.ResultsLog(TestContext, "My Result");
                Console.WriteLine(driver.Title.ToString() + " is the incorrect title");
                Assert.Fail();
            }

        }


        [TestMethod]
        [TestCategory("Chrome")]
        public void OpenDorseClickOnJobPost() // Find Specific Job Posting 
        {

            driver.Navigate().GoToUrl(appURL + "/");

            Thread.Sleep(5000); 

            driver.FindElement(By.LinkText("Careers")).Click();

            Thread.Sleep(5000);

            if (driver.FindElement(By.TagName("title")).GetAttribute("innerText").Trim().Equals("BambooHR"))

            {

                Console.WriteLine(driver.Title.ToString() + " is the correct title. Currently on Careers Page.");

            }
            else
            {
                ResultsLogFileManager.ResultsLog(TestContext, "My Result");
                Console.WriteLine(driver.Title.ToString() + " is the incorrect title");
                Assert.Fail();
            }

            IList<IWebElement> allLinks = driver.FindElements(By.TagName("a"));
            foreach (IWebElement link in allLinks)
            {
                
                if (link.Text.Trim().Equals("QA Engineer"))
                {
                    link.Click();
                    break;
                }
            }

            Thread.Sleep(7000);
            
            if (driver.FindElement(By.TagName("h2")).GetAttribute("innerText").Equals("QA Engineer"))

            {

                Console.WriteLine(driver.Title.ToString() + " is the correct title");

            }
            else
            {
                ResultsLogFileManager.ResultsLog(TestContext, "My Result");
                Console.WriteLine(driver.Title.ToString() + " is the incorrect title");
                Assert.Fail();
            }

        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestInitialize()]
        public void SetupTest()
        {
            
            appURL = "https://opendorse.com/";
     
            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }


       

        public class ResultsLogFileManager // Log Results in Text File locate in this dir C:\Users\USERNAME\source\repos\OpenDorseUnitTestProject\TestResults
        {
            public static void ResultsLog(TestContext testContext, string myResultString)
            {
                string path = testContext.TestLogsDir.ToString() + "\\" + "MyResult.txt";

                // Create a file to write to.
                if (!File.Exists(path))
                {
                    using (TextWriter sw = File.CreateText(path))
                    {
                        Thread.Sleep(2000);
                        sw.WriteLine(" ");
                        Thread.Sleep(2000);
                        sw.WriteLine("*****");
                        Thread.Sleep(2000);
                        sw.WriteLine(" ");
                        Thread.Sleep(2000);
                        sw.WriteLine(testContext.TestName.ToString());
                        Thread.Sleep(2000);
                        sw.WriteLine(testContext.TestLogsDir.ToString());
                        Thread.Sleep(2000);
                        sw.WriteLine(testContext.TestDir.ToString());
                    }

                }
                else
                    using (StreamWriter fs = new StreamWriter(path, true))
                    {
                        fs.Write(myResultString);
                    }
            }
        }




    }
}