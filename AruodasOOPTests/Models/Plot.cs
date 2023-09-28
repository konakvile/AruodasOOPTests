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
       

        /* Purpose Checkbox values
         * "1" Residential land, "2" Manufacturing land, "3" Agricultural, "4" Collective garden, 
         * "5" Forestrial, "6" Factory, "7" Storage, "8" Commercial, "9" Recreational, "10" Other */
        public string[] PurposeCB { get; set; }

        // additional checkboxes can be added later on
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


        public Plot () : base() 
        { 
        }

        // testams tik del adreso dalies pildymo
        public Plot(string region, string settlement, string microdistrict, string street, string streetNo, bool showStreetNo) : base()
        {
            Region = region;
            Settlement = settlement;
            Microdisctrict = microdistrict;
            Street = street;
            StreetNo = streetNo;
            ShowStreetNo = showStreetNo;
        }

        // minimalus objekto uzpildymas (raudonuojantys laukai, e-mail neitrauktas nes buna pre-filled)
        public Plot (string region, string settlement, string area, string lotPrice, string phoNo, bool acceptTandC, string[] checkBoxes) : base()
        {
            Region = region;
            Settlement = settlement;
            Area = area;
            LotPrice = lotPrice;
            PhoNo = phoNo;
            AcceptTandC = acceptTandC;
            PurposeCB = checkBoxes;
        }
        // maksimalus objekto uzpildymas (butini ir nebutini laukai)
        public Plot(string region, string settlement, string microdistrict, string street, string streetNo, 
            bool showStreetNo, string uniqItemNo, bool showuniqItemNo, string area, string[] purposeCB, 
            string description, string upPhoto, string youtubeLink, string tour3DLink, string lotPrice, 
            string phoNo, string email, bool doNotShowEmail, bool turnOffChat, bool acceptTandC) : base()
        {
            Region = region;
            Settlement = settlement;
            Microdisctrict = microdistrict;
            Street = street;
            StreetNo = streetNo;
            ShowStreetNo = showStreetNo;
            UniqItemNo = uniqItemNo;
            ShowUniqItemNo = showuniqItemNo;
            Area = area;
            PurposeCB = purposeCB;
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
        }

        public void FillInListingMax()
        {
            Driver.Navigate().GoToUrl("https://www.aruodas.lt/ideti-skelbima/?obj=11&offer_type=1");

            SetLocation(); 
            SetUnqItemNo();
            SetShowUniqItemNo();
            SetArea();
            SetPurposeCB();
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

            // Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            // Wait.Until(ExpectedConditions.ElementExists(By.Id("district").FindElement(By.ClassName("dropdown-input-values-address"))).FindElement(By.TagName("li[" + (settlementCount + 1) + "]"))).Click();
            //*[@id="districts_461"]/li[89]

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

        public void SetUnqItemNo()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[11]/div[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[11]/div[1]/input")).SendKeys(this.UniqItemNo);
        }

        public void SetShowUniqItemNo()
        {
            if (!(this.ShowUniqItemNo))
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[11]/div[2]/div/label/span")).Click();
            }
        }
        public void SetArea()
        {
            Driver.FindElement(By.Id("fieldFAreaOverAll")).SendKeys(this.Area);
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

        public void SetDescription()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[26]/div/div[1]/textarea")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[26]/div/div[1]/textarea")).SendKeys(this.Description);
        }

        public void SetUpPhotos() 
        {
            IWebElement upload_file = Driver.FindElement(By.XPath("//*[@id=\"uploadPhotoBtn\"]/input"));

            upload_file.SendKeys("C:\\Users\\akvile.kondrotaite\\OneDrive - SERMO\\Desktop\\Test files\\Images\\JPG\\alpaca.jpg");
        }

        public void SetYoutubeLink()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[30]/span[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[30]/span[1]/input")).SendKeys(this.YoutubeLink);

        }

        public void SetTour3DLink()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[31]/span[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[31]/span[1]/input")).SendKeys(this.Tour3DLink);

        }

        public void SetLotPrice()
        {
            Driver.FindElement(By.Id("priceField")).Click();
            Driver.FindElement(By.Id("priceField")).SendKeys(this.LotPrice);

        }

        public void SetPhoNo()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[34]/span[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[34]/span[1]/input")).SendKeys(this.PhoNo);

        }

        public void SetEmail()
        {
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[35]/span[1]/input")).Click();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[35]/span[1]/input")).Clear();
            Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[35]/span[1]/input")).SendKeys(this.Email);

        }

        public void SetDoNotShowEmail()
        {
            if (this.DoNotShowEmail)
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[36]/div/div/div/label/span")).Click();
            }

        }

        public void SetTurnOffChat()
        {
            if (this.TurnOffChat)
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[37]/div/div/div/label/span")).Click();
            }
        }

        public void SetAcceptTandC()
        {
            if (this.AcceptTandC)
            {
                Driver.FindElement(By.XPath("//*[@id=\"newObjectForm\"]/ul/li[38]/span[1]/div/div/label/span")).Click();
            }
        }

    }
}
