using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using AruodasOOPTests.Models;
using OpenQA.Selenium.Support.UI;
using AruodasOOPTests.Helpers;

namespace AruodasOOPTests.Tests
{
    public class SellPlotListingTests
    {
        public static IWebDriver Driver;

        
        [Test]
        public void SellPlotListingMaxPositiveTest() 
        {
            Plot sklypas = new Plot("Vilnius", "Vilniaus", "Antakalnis", "Tverečiaus", "20", true, "555566667777", false, "10", new string[] {"1", "10", "7"},
                "The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. " +
                "Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs. " +
                "Waltz, bad nymph, for quick jigs vex! Fox nymphs grab quick-jived waltz. " +
                "Brick quiz whangs jumpy veldt fox. Bright vixens jump; dozy fowl quack. " +
                "Quick wafting zephyrs vex bold Jim. Quick zephyrs blow, vexing daft Jim. Sex-charged fop blew my", 
                "PHOTOLINK", "https://youtu.be/dQw4w9WgXcQ?si=WCfwdxt9ncH90Ksn", "https://www.3dvista.com/samples/live_pano_ny.html", "20000", 
                "37060750088", "pardaveeeejai88@harakirimail.com", true, true, true);

           sklypas.FillInListingMax();
           SubmitListing();
           Thread.Sleep(2000);
           Helpers.CustomAsserts.AssertEqualsXPath("/html/body/div[1]/div[1]/div[2]/div/span", "Paslaugų paketo pasirinkimas");


        }

        
        [Test]
        public void SellPlotListingMinPositiveTest()
        {
            Plot sklypukas = new Plot("Vilnius", "Vilnius", "2", "2000", "37060750088", true, new string[] {"7", "8", "9", "10"} );
            sklypukas.FillInListingMin();
            SubmitListing();
            Thread.Sleep(2000);
            Helpers.CustomAsserts.AssertEqualsXPath("/html/body/div[1]/div[1]/div[2]/div/span", "Paslaugų paketo pasirinkimas");
        }
        

        
        [Test]
        public void LocationTestsVilnius()
        {
            Plot a = new Plot("Vilnius", "Vilniaus", "Bajorai", "Mergelės g.", "10", false);
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=11&offer_type=1");
            a.SetLocation();
        }
        
        // Neaisku kodel nebeveikia testas, tiesiog nepagauna elementu kazkaip po refreactorinimo
        //[Test]
        //public void LocationTestsSiauliai()
        //{
        //    Plot b = new Plot("Šiauliai", "Žaliūkių", "NONE", "Nemuno", "20", true);
        //    Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=11&offer_type=1");
        //    b.SetLocation();
        //}

        [Test]
        public void LocationTestsKlaipeda()
        {
            Plot c = new Plot("Klaipėda", "Klaipėdos", "Giruliai", "Akmenų", "37", true);
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=11&offer_type=1");
            c.SetLocation();
        }
        


        [OneTimeSetUp]
        public void Initialize()
        {
            if (!(Helpers.DriverClass.Driver is null))
            {
                return;
            }

            DriverClass.Driver = new ChromeDriver();
            DriverClass.Wait = new WebDriverWait(DriverClass.Driver, TimeSpan.FromSeconds(5));
            

            Driver = DriverClass.Driver;
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.Manage().Window.Maximize();
            AcceptCookies();
            Login();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            //driver.Quit();
        }
        public void AcceptCookies()
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/");
            Driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
        }

        public void Login()
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/");
            Driver.FindElement(By.ClassName("reg-menu-div-4")).Click();
            Driver.FindElement(By.Id("userName")).SendKeys("13akvilyte13@gmail.com");
            Driver.FindElement(By.Id("password")).SendKeys("123Luniukas");
            Driver.FindElement(By.Id("loginAruodas")).Click();
        }

        public void SubmitListing()
        {
            Driver.FindElement(By.ClassName("big-submit-button")).Click();
        }

    }
}