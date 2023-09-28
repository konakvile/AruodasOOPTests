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
        public string Region { get; set; }
        public string Settlement { get; set; }
        public string Microdisctrict { get; set; }
        public string Street { get; set; }
        public bool GarageOrParkingSpace { get; set; } // input values true - garage or false - parking space
        public string StreetNo { get; set; }
        public bool ShowStreetNo { get; set; }
        public string UniqItemNo { get; set; }
        public bool ShowUniqItemNo { get; set; }
        public string Area { get; set; }
        
        /*Type input values
         * GARAGE 1 - Stone, 2 - Iron, 3 - Underground, 4 - Multistory, 5 - Other
         * PARKING SPACE 6 - Underground parking, 7 - Parking lot, 8 - Multistory car park, 9 - Other 
         * // gal other bus galima ta pati skaiciu naudoti, priklausys nuo XPath ar pan.
         */
        public int Type { get; set; }
        public int CarCapacity { get; set; }
        public string Description { get; set; }
        public string UpPhotos { get; set; } 
        public string YoutubeLink { get; set; }
        public string Tour3DLink { get; set; }
        public string LotPrice { get; set; }
        public string PhoNo { get; set; }
        public string Email { get; set; }
        public bool DoNotShowEmail { get; set; }
        public bool TurnOffChat { get; set; }
        public bool AcceptTandC { get; set; }


        public Garage() : base() 
        { 
        }

        // minimalus objekto uzpildymas (raudonuojantys laukai, e-mail neitrauktas nes buna pre-filled)
        public Garage(string region, string settlement, bool garageOrParkingSpace, int type, int carCapacity, string lotPrice, string phoNo, bool acceptTandC) : base()
        {
            Region = region;
            Settlement = settlement;
            GarageOrParkingSpace = garageOrParkingSpace;
            Type = type;
            CarCapacity = carCapacity;
            LotPrice = lotPrice;
            PhoNo = phoNo;
            AcceptTandC = acceptTandC;
            
        }
        // maksimalus objekto uzpildymas (butini ir nebutini laukai)
        public Garage(string region, string settlement, string microdistrict, string street, bool garageOrParkingSpace, string streetNo, 
            bool showStreetNo, string uniqItemNo, bool showuniqItemNo, string area, int type, int carCapacity,
            string description, string upPhoto, string youtubeLink, string tour3DLink, string lotPrice, 
            string phoNo, string email, bool doNotShowEmail, bool turnOffChat, bool acceptTandC) : base()
        {
            Region = region;
            Settlement = settlement;
            Microdisctrict = microdistrict;
            Street = street;
            GarageOrParkingSpace = garageOrParkingSpace;
            StreetNo = streetNo;
            ShowStreetNo = showStreetNo;
            UniqItemNo = uniqItemNo;
            ShowUniqItemNo = showuniqItemNo;
            Area = area;
            Type = type;
            CarCapacity = carCapacity;
            Description = description;
            UpPhotos = upPhoto;
            YoutubeLink = youtubeLink;
            Tour3DLink = tour3DLink;
            LotPrice = lotPrice;
            PhoNo = phoNo;
            Email = email;
            DoNotShowEmail = doNotShowEmail;
            TurnOffChat = turnOffChat;
            AcceptTandC = acceptTandC;
        }

        public void FillInListingMin()
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=13&offer_type=1");
            SetRegion();
            SetSettlement();
            SetType();
            SetCarCapacity();
            SetLotPrice();
            SetPhoNo();
            SetDoNotShowEmail();
            SetTurnOffChat();
            SetAcceptTandC();

        }

        public void FillInListingMax()
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=13&offer_type=1");

            SetLocation();
            SetGarageOrParkingSpace();
            SetUnqItemNo();
            SetShowUniqItemNo();
            SetArea();
            SetType();
            SetCarCapacity();
            SetDescription();
            SetUpPhotos(); //should consider an array too for multiple photos
            SetYoutubeLink();
            SetTour3DLink();
            SetLotPrice();
            SetPhoNo();
            SetEmail();
            SetDoNotShowEmail();
            SetTurnOffChat();
            SetAcceptTandC();

        }

        public void SetLocation()
        {
            SetRegion();
            SetSettlement();
            try { SetMicrodistrict(); }
            catch { Console.WriteLine("Microdistrict field is hidden"); }
            SetStreet();
            SetStreetNo();
            SetShowStreetNo();

        } 

        public void SetRegion()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[3]/span[1]/input[2]")).Click();

            int regionCount = Driver.FindElement(By.Id("regionDropdown")).FindElements(By.TagName("li")).Count;
            Console.WriteLine("SUSKAICIUOTOS SAVIVALDYBES: " + regionCount);

            Driver.FindElement(By.XPath("//*[@id=\"regionDropdown\"]/li[1]/input")).SendKeys(this.Region);

            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

            Wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"regionDropdown\"]/li[" + (regionCount + 1) + "]"))).Click();
            
        }

        public void SetSettlement()
        {

            Driver.FindElement(By.XPath("//*[@id=\"district\"]")).Click();
           
            IList<IWebElement> lis = Driver.FindElements(By.ClassName("dropdown-input-values-address"))[1].FindElements(By.TagName("li"));
            Console.WriteLine("SUSKAICIUOTOS GYVENVIETES: " + lis.Count);

            if (lis.Count > 14)
            {

                Driver.FindElement(By.Id("district")).FindElement(By.ClassName("dropdown-input-values-address")).FindElement(By.TagName("input")).SendKeys(this.Settlement);
                Thread.Sleep(2000);
                Driver.FindElement(By.Id("district")).FindElement(By.ClassName("dropdown-input-values-address")).FindElement(By.TagName("input")).SendKeys(Keys.Enter);
            }
            else 
            {
                for (int i = 0; i < lis.Count; i++)
                {
                    if (lis[i].Text.Contains(this.Settlement))
                    {
                        lis[i].Click();
                        break;
                    }
                }
            }
        }

        public void SetMicrodistrict()
        {
            Driver.FindElement(By.Id("quartalTitle")).Click();

            IList<IWebElement> microLis = Driver.FindElements(By.ClassName("dropdown-input-values-address"))[2].FindElements(By.TagName("li"));
            Console.WriteLine("SUSKAICIUOTI MIKRORAJONAI: " + microLis.Count);

            if (microLis.Count > 14)
            {
            
                Driver.FindElement(By.Id("quartalField")).FindElement(By.ClassName("dropdown-input-values-address")).FindElement(By.TagName("input")).SendKeys(this.Microdisctrict);
                Thread.Sleep(2000);
                Driver.FindElement(By.Id("quartalField")).FindElement(By.ClassName("dropdown-input-values-address")).FindElement(By.TagName("input")).SendKeys(Keys.Enter);
            }
            else
            {
                for (int i = 0; i < microLis.Count; i++)
                {
                    if (microLis[i].Text.Contains(this.Microdisctrict))
                    {
                        microLis[i].Click();
                        break;
                    }
                }
            }

        }

        public void SetStreet()
        {
            Driver.FindElement(By.XPath("//*[@id=\"streetField\"]/span[1]")).Click();
            IList<IWebElement> streetLis;
            try
            {
                streetLis = Driver.FindElements(By.ClassName("dropdown-input-values-address"))[3].FindElements(By.TagName("li"));
            }catch(Exception e)
            {
                streetLis = Driver.FindElements(By.ClassName("dropdown-input-values-address"))[2].FindElements(By.TagName("li"));
            }
            Console.WriteLine("SUSKAICIUOTOS GATVES: " + streetLis.Count);

            if (streetLis.Count > 14)
            {
                Driver.FindElement(By.Id("streetField")).FindElement(By.ClassName("dropdown-input-values-address")).FindElement(By.TagName("input")).SendKeys(this.Street);
                Thread.Sleep(2000);
                Driver.FindElement(By.Id("streetField")).FindElement(By.ClassName("dropdown-input-values-address")).FindElement(By.TagName("input")).SendKeys(Keys.Enter);
            }
            else
            {
                for (int i = 0; i < streetLis.Count; i++)
                {
                    if (streetLis[i].Text.Contains(this.Street))
                    {
                        streetLis[i].Click();
                        break;
                    }
                }
            }

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

        public void SetUnqItemNo()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[12]/div[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[12]/div[1]/input")).SendKeys(this.UniqItemNo);
        }

        public void SetShowUniqItemNo()
        {
            if (!(this.ShowUniqItemNo))
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[12]/div[2]/div/label/span")).Click();
            }
        }
        public void SetArea()
        {
            Driver.FindElement(By.Id("fieldFAreaOverAll")).SendKeys(this.Area);
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

        public void SetDescription()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[30]/div/div[1]/textarea")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[30]/div/div[1]/textarea")).SendKeys(this.Description);
        }

        public void SetUpPhotos() 
        {
            IWebElement upload_file = Driver.FindElement(By.XPath("//*[@id=\"uploadPhotoBtn\"]/input"));

            upload_file.SendKeys("C:\\Users\\akvile.kondrotaite\\OneDrive - SERMO\\Desktop\\Test files\\Images\\JPG\\alpaca.jpg");
        }

        public void SetYoutubeLink()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[34]/span[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[34]/span[1]/input")).SendKeys(this.YoutubeLink);

        }

        public void SetTour3DLink()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[35]/span[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[35]/span[1]/input")).SendKeys(this.Tour3DLink);

        }

        public void SetLotPrice()
        {
            Driver.FindElement(By.Id("priceField")).Click();
            Driver.FindElement(By.Id("priceField")).SendKeys(this.LotPrice);

        }

        public void SetPhoNo()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[38]/span[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[38]/span[1]/input")).SendKeys(this.PhoNo);

        }

        public void SetEmail()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[39]/span[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[39]/span[1]/input")).Clear();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[39]/span[1]/input")).SendKeys(this.Email);

        }

        public void SetDoNotShowEmail()
        {
            if (this.DoNotShowEmail)
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[40]/div/div/div/label/span")).Click();
            }

        }

        public void SetTurnOffChat()
        {
            if (this.TurnOffChat)
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[41]/div/div/div/label/span")).Click();
            }
        }

        public void SetAcceptTandC()
        {
            if (this.AcceptTandC)
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[42]/span[1]/div/div/label/span")).Click();
            }
        }

    }
}
