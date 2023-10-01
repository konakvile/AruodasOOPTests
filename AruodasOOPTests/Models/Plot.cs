using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AruodasOOPTests.Models
{
    internal class Plot : RealEstate
    {
        
        public string StreetNo { get; set; }
        public bool ShowStreetNo { get; set; }
       
        /* Purpose Checkbox values
         * "1" Residential land, "2" Manufacturing land, "3" Agricultural, "4" Collective garden, 
         * "5" Forestrial, "6" Factory, "7" Storage, "8" Commercial, "9" Recreational, "10" Other */
        public string[] PurposeCB { get; set; }

        
        public Plot () : base() 
        { 
        }

        // testams tik del adreso dalies pildymo
        public Plot(string region, string settlement, string microdistrict, string street, 
            string streetNo, bool showStreetNo) : base(region, settlement, microdistrict, street)
        {
            StreetNo = streetNo;
            ShowStreetNo = showStreetNo;
        }

        // minimalus objekto uzpildymas (raudonuojantys laukai, e-mail neitrauktas nes buna pre-filled)
        public Plot (string region, string settlement, string area, string lotPrice, string phoNo, bool acceptTandC, string[] checkBoxes)
            : base(region, settlement, area, lotPrice, phoNo, acceptTandC)
        {
            PurposeCB = checkBoxes;
        }

        // maksimalus objekto uzpildymas (butini ir nebutini laukai)
        public Plot(string region, string settlement, string microdistrict, string street, string streetNo, 
            bool showStreetNo, string uniqItemNo, bool showuniqItemNo, string area, string[] purposeCB, 
            string description, string upPhoto, string youtubeLink, string tour3DLink, string lotPrice, 
            string phoNo, string email, bool doNotShowEmail, bool turnOffChat, bool acceptTandC) : base(region, 
                settlement, microdistrict, street, uniqItemNo, showuniqItemNo, area, description, upPhoto, 
                youtubeLink, tour3DLink, lotPrice, phoNo, email, doNotShowEmail, turnOffChat, acceptTandC)
        {
            StreetNo = streetNo;
            ShowStreetNo = showStreetNo;
            PurposeCB = purposeCB;   
        }

        /* Leaving the code for learning purposes, cannot transfer to RealEstate class
        public void FillInListingMin() // first test, will not select district
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=11&offer_type=1");
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[3]/span[1]/input[2]")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"regionDropdown\"]/li[1]/input")).SendKeys(this.Region);
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("//*[@id=\"regionDropdown\"]/li[1]/input")).SendKeys(Keys.Enter);

            Driver.FindElement(By.Id("districtTitle")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"districts_461\"]/li[1]/input")).SendKeys(this.Settlement);
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("//*[@id=\"districts_461\"]/li[1]/input")).SendKeys(Keys.Enter);

            Driver.FindElement(By.Id("fieldFAreaOverAll")).SendKeys(this.Area);

            SetPurposeCB();

            Driver.FindElement(By.Id("priceField")).SendKeys(this.LotPrice);                        
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[34]/span[1]/input")).SendKeys(this.PhoNo);
            
            if (this.AcceptTandC){
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[38]/span[1]/div/div/label/span")).Click();
            } 
        } */

        override public void FillInListingMax()
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=11&offer_type=1");

            base.FillInListingMax();
            SetPurposeCB();
        }

        override public void FillInListingMin()
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=11&offer_type=1");

            base.FillInListingMin();
            SetPurposeCB();
        }

        override public void SetLocation()
        {
           base.SetLocation();
            SetStreetNo();
            SetShowStreetNo();
        } 

        public void SetStreetNo()
        {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[7]/span[1]/input")).SendKeys(this.StreetNo);
        }

        public void SetShowStreetNo()
        {
            if (!(this.ShowStreetNo))
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[7]/div/div/label/span")).Click();
            }
        }

        public void SetPurposeCB()
        {
            for (int i = 0; i < this.PurposeCB.Length; i++)
            {
                switch (this.PurposeCB[i])
                {
                    case "1": // Residential land
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[1]/label/span")).Click();
                        break;
                    case "2": // Manufacturing land
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[2]/label/span")).Click();
                        break;
                    case "3": // Agricultural
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[3]/label/span")).Click();
                        break;
                    case "4": // Collective garden
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[4]/label/span")).Click();
                        break;
                    case "5": // Forestrial
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[5]/label/span")).Click();
                        break;
                    case "6": // Factory
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[6]/label/span")).Click();
                        break;
                    case "7": // Storage
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[7]/label/span")).Click();
                        break;
                    case "8": // Commercial
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[8]/label/span")).Click();
                        break;
                    case "9": // Recreational
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[9]/label/span")).Click();
                        break;
                    case "10": // Other
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[10]/label/span")).Click();
                        break;
                }
            }
        }
    }
}
