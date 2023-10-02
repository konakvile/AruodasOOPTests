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
    internal class Flat : RealEstate
    {
        
        public string HouseNo { get; set; }
        public bool ShowHouseNo { get; set; }
        public string FlatNo { get; set; }
        public bool ShowFlatNo { get; set; }
        public string RoomNo { get; set; }
        public string FlatFloorNo { get; set; }
        public string BuildingFloorNo { get; set; }
        public string BuildingYear { get; set; }
        public bool Renovation { get; set; }

        /* Equipment radio button values
        * "1" Brick, "2" Block house, "3" Monolithic, "4" Wooden house, "5" Carcass house, "6" Log house, "7" Panel, "8" Other */
        public string HouseType { get; set; } // ivedinesiu skaicius, kad butu switch
        
        /* Equipment radio button values
         * "1" Fully quipped, "2" Partially equipped, "3" Not equipped, "4" Under construction, "5" Foundation, "6" Other */
        public string Equipment { get; set; } // ivedinesiu skaicius, kad butu switch

        /* Heating system Checkbox values
         * "1" Central, "2" Electric, "3" Liquid, "4" Central thermostat, 
         * "5" Geothermal, "6" Aerothermal, "7" Gas, "8" Solid fuel, "9" Sun energy, "10" Other */
        public string[] HeatingCB { get; set; }
        public bool ExtraFeatures { get; set; }
        public string[] FeaturesDescriptionCB { get; set; }
        public string[] FeaturesAddPremisesCB { get; set; }
        public string[] FeaturesAddEquipmentCB { get; set; }
        public string[] FeaturesSecurityCB { get; set; }
        public string EnergyClass { get; set; } // ivedinesiu pavadinimus (raides), kad butu switch
        
        /* Sale type Checkbox values
         * "1" Interested in exchanging, "2" Sale by auction */
        public string[] SaleTypeCB { get; set; }


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
            bool showStreetNo, string uniqItemNo, bool showuniqItemNo, string area, string[] purposeCB, bool extraFeatures, string[] featuresCB,
            string description, string upPhoto, string youtubeLink, string tour3DLink, string lotPrice, 
            string phoNo, string email, bool doNotShowEmail, bool turnOffChat, bool acceptTandC) : base(region, 
                settlement, microdistrict, street, uniqItemNo, showuniqItemNo, area, description, upPhoto, 
                youtubeLink, tour3DLink, lotPrice, phoNo, email, doNotShowEmail, turnOffChat, acceptTandC)
        {
            StreetNo = streetNo;
            ShowStreetNo = showStreetNo;
            PurposeCB = purposeCB; 
            ExtraFeatures = extraFeatures;
            FeaturesCB = featuresCB;
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
            SetExtraFeatures();
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

        public void SetFeaturesCB()
        {
            for (int i = 0; i < this.FeaturesCB.Length; i++)
            {
                switch (this.FeaturesCB[i])
                {
                    case "1": // Electricity
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[20]/div/div[1]/label/span")).Click();
                        break;
                    case "2": // Gas
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[20]/div/div[2]/label/span")).Click();
                        break;
                    case "3": // Sewage
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[20]/div/div[3]/label/span")).Click();
                        break;
                    case "4": // Marginal land
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[20]/div/div[4]/label/span")).Click();
                        break;
                    case "5": // Near forest
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[20]/div/div[5]/label/span")).Click();
                        break;
                    case "6": // No buildings
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[20]/div/div[6]/label/span")).Click();
                        break;
                    case "7": // Geodesic measurements
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[20]/div/div[7]/label/span")).Click();
                        break;
                    case "8": // With coast
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[20]/div/div[8]/label/span")).Click();
                        break;
                    case "9": // Paved road
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[20]/div/div[9]/label/span")).Click();
                        break;
                    case "10": // Interested in exchanging 
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[22]/div/div/div/label/span")).Click();
                        break;
                    case "11": // Sale by auction
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[23]/div/div/div/label/span")).Click();
                        break;
                }
            }
        }

        public void SetExtraFeatures()
        {
            if (this.ExtraFeatures)
            {
                Driver.FindElement(By.Id("showMoreFields")).Click();
                SetFeaturesCB();
            }
        }
    }
}
