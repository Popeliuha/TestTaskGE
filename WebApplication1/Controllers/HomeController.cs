using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Database;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string ReadText(string text)
        {
            try
            {
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://google.com");
                IWebElement txtSearch = driver.FindElement(By.Name("q"));
                txtSearch.SendKeys(text + Keys.Enter);

                string searchResults = "Результаты поиска"; //if it fails from your browser, replace content of this string to "Search results" in your language
                string firstResultXPath = $"//h1[text()='{searchResults}']/following-sibling::div/div[1]";
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                IWebElement firstResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(firstResultXPath)));
                IWebElement firstResultName = firstResult.FindElement(By.XPath("//h3"));
                string firstResultsText = firstResultName.Text;
                logger.Info(firstResultsText);
                driver.Close();
                using (var context = new TestContext())
                {
                    var entity = new TestEntitie { SavedText = firstResultsText};
                    context.TestEntitiesDB.Add(entity);
                    context.SaveChanges();
                }
                return firstResultsText;
            }
            catch(Exception e)
            {
                logger.Error(e.Message);
                logger.Error(e.StackTrace);
                return e.Message;
            }
        }
    }
}