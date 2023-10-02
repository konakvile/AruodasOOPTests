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
    internal class Garage : RealEstate
    {
        public bool GarageOrParkingSpace { get; set; } // input values true - garage or false - parking space
        public string StreetNo { get; set; }
        public bool ShowStreetNo { get; set; }
        
        /*Type input values
         * GARAGE 1 - Stone, 2 - Iron, 3 - Underground, 4 - Multistory, 5 - Other
         * PARKING SPACE 6 - Underground parking, 7 - Parking lot, 8 - Multistory car park, 9 - Other 
         * // gal other bus galima ta pati skaiciu naudoti, priklausys nuo XPath ar pan.
         */
        public int Type { get; set; }
        public int CarCapacity { get; set; }
        public bool ExtraFeatures { get; set; }
        public string[] FeaturesCB { get; set; }
        
        public Garage()
        { 
        }

        // minimalus objekto uzpildymas (raudonuojantys laukai, e-mail neitrauktas nes buna pre-filled)
        public Garage(string region, string settlement, string area, bool garageOrParkingSpace, int type, int carCapacity, string lotPrice, string phoNo, bool acceptTandC) : base(region, 
            settlement, area, lotPrice, phoNo, acceptTandC)
        { 
            GarageOrParkingSpace = garageOrParkingSpace;
            Type = type;
            CarCapacity = carCapacity; 
        }

        // maksimalus objekto uzpildymas (butini ir nebutini laukai)
        public Garage(string region, string settlement, string microdistrict, string street, bool garageOrParkingSpace, string streetNo, 
            bool showStreetNo, string uniqItemNo, bool showuniqItemNo, string area, int type, int carCapacity, bool extraFeatures, string[] featuresCB,
            string description, string upPhoto, string youtubeLink, string tour3DLink, string lotPrice, 
            string phoNo, string email, bool doNotShowEmail, bool turnOffChat, bool acceptTandC) : base(region, settlement, microdistrict, street, uniqItemNo, 
                showuniqItemNo, area, description, upPhoto, youtubeLink, tour3DLink, lotPrice, phoNo, email, doNotShowEmail, turnOffChat, acceptTandC)
        {
            GarageOrParkingSpace = garageOrParkingSpace;
            StreetNo = streetNo;
            ShowStreetNo = showStreetNo;
            Type = type;
            CarCapacity = carCapacity;
            ExtraFeatures = extraFeatures;
            FeaturesCB = featuresCB;
        }

        override public void FillInListingMin()
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=13&offer_type=1");
            base.FillInListingMin();
            
            SetType();
            SetCarCapacity();
        }

        override public void FillInListingMax()
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=13&offer_type=1");
            base.FillInListingMax();

            SetGarageOrParkingSpace();
            SetType();
            SetCarCapacity();
            SetExtraFeatures();
        }

        override public void SetLocation()
        {
            base.SetLocation();
            
            SetStreetNo();
            SetShowStreetNo();

        } 

        public void SetGarageOrParkingSpace()
        {
            if (this.GarageOrParkingSpace)
            {
                Driver.FindElement(By.XPath("//*[@id=\"parking_checkbox\"]/div/label/span")).Click(); // garage
            }
            else {
                Driver.FindElement(By.XPath("//*[@id=\"whole_building_checkbox\"]/div/label/span")).Click(); // parking space
            }          
        }

        public void SetStreetNo()
        {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[8]/span[1]/input")).SendKeys(this.StreetNo);
        }

        public void SetShowStreetNo()
        {
            if (!(this.ShowStreetNo))
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[8]/div/div/label/span")).Click();
            }
        }
 
        public void SetTypeGarage()
        {
            switch (this.Type)
            {
                case 1: // Stone
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[1]")).Click();
                    break;
                case 2: // Iron
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[2]")).Click();
                    break;
                case 3: // Underground
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[3]")).Click();
                    break;
                case 4: // Multistory
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[4]")).Click();
                    break;
                case 5: // Other
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[16]/div/div[5]")).Click();
                    break;
            }
        }

        public void SetTypeParkingSpace()
        {
            switch (this.Type)
            {
                case 6: // Underground parking
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[17]/div/div[1]")).Click();
                    break;
                case 7: // Parking lot
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[17]/div/div[2]")).Click();
                    break;
                case 8: // Multistory car park
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[17]/div/div[3]")).Click();
                    break;
                case 9: // Other
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[17]/div/div[4]")).Click();
                    break;
            }

        }

        public void SetType()
        {
            if (this.GarageOrParkingSpace)
            {
                SetTypeGarage(); // garage
            }
            else
            {
                SetTypeParkingSpace(); // parking space
            }
        }


        public void SetCarCapacity()
        {
            switch (this.CarCapacity)
            {
                case 1: 
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[18]/div/div[1]")).Click();
                    break;
                case 2: 
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[18]/div/div[2]")).Click();
                    break;
                case 3: 
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[18]/div/div[3]")).Click();
                    break;
                case 4: 
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[18]/div/div[4]")).Click();
                    break;
                default: // Other
                    Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[18]/div/span/input")).SendKeys(this.CarCapacity + "");
                    break;
            }
        }

        public void SetFeaturesCBGarage()
        {
            for (int i = 0; i < this.FeaturesCB.Length; i++)
            {
                switch (this.FeaturesCB[i])
                {
                    case "1": // Security
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[22]/div/div[1]/label/span")).Click();
                        break;
                    case "2": // Automatic gates
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[22]/div/div[2]/label/span")).Click();
                        break;
                    case "3": // Pit
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[22]/div/div[3]/label/span")).Click();
                        break;
                    case "4": // Basement
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[22]/div/div[4]/label/span")).Click();
                        break;
                    case "5": // Water
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[22]/div/div[5]/label/span")).Click();
                        break;
                    case "6": // Heating
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[22]/div/div[6]/label/span")).Click();
                        break;
                    case "7": // Interested in exchanging
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[26]/div/div/div/label/span")).Click();
                        break;
                    case "8": // Sale by auction
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[27]/div/div/div/label/span")).Click();
                        break;
                }
            }
        }

        public void SetFeaturesCBParkingSpace()
        {
            for (int i = 0; i < this.FeaturesCB.Length; i++)
            {
                switch (this.FeaturesCB[i])
                {
                    case "1": // Security
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[24]/div/div[1]/label/span")).Click();
                        break;
                    case "2": // Automatic gates
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[24]/div/div[2]/label/span")).Click();
                        break;
                    case "3": // Heating
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[24]/div/div[3]/label/span")).Click();
                        break;
                    case "4": // Lock
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[24]/div/div[4]/label/span")).Click();
                        break;
                    case "5": // Fenced
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[24]/div/div[5]/label/span")).Click();
                        break;
                    case "6": // Under the roof
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[24]/div/div[6]/label/span")).Click();
                        break;
                    case "7": // Storeroom
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[24]/div/div[7]/label/span")).Click();
                        break;
                    case "8": // Interested in exchanging
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[26]/div/div/div/label/span")).Click();
                        break;
                    case "9": // Sale by auction
                        Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[27]/div/div/div/label/span")).Click();
                        break;
                }
            }
        }

        public void SetExtraFeatures()
        {

            if (this.ExtraFeatures)
            {
                if (this.GarageOrParkingSpace)
                {
                    Driver.FindElement(By.Id("showMoreFields")).Click();
                    SetFeaturesCBGarage();
                }
                else 
                {
                    Driver.FindElement(By.Id("showMoreFields")).Click();
                    SetFeaturesCBParkingSpace();
                }
            }
        }

       
    }
}
