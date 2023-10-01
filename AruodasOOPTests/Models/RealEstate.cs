using AruodasOOPTests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AruodasOOPTests.Models
{
    internal class RealEstate
    {
        public IWebDriver Driver { get; set; }

        public WebDriverWait Wait { get; set; }

        public string Region { get; set; }
        public string Settlement { get; set; }
        public string Microdisctrict { get; set; }
        public string Street { get; set; }
        public string UniqItemNo { get; set; }
        public bool ShowUniqItemNo { get; set; }
        public string Area { get; set; }
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

        public RealEstate() {

            this.Driver = DriverClass.Driver;
            this.Wait = DriverClass.Wait;
        }

        public RealEstate(string region, string settlement, string microdistrict, string street, string uniqItemNo, bool showUniqItemNo, string area, string description, string upPhoto, string youtubeLink, string tour3DLink, string lotPrice,
            string phoNo, string email, bool doNotShowEmail, bool turnOffChat, bool acceptTandC)
        {

            this.Driver = DriverClass.Driver;
            this.Wait = DriverClass.Wait;

            Region = region;
            Settlement = settlement;
            Microdisctrict = microdistrict;
            Street = street;
            UniqItemNo = uniqItemNo;
            ShowUniqItemNo = showUniqItemNo;
            Area = area;
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

        // Tik minimalaus uzpildymo testui plot
        public RealEstate (string region, string settlement, string area, string lotPrice, string phoNo, bool acceptTandC)
        {
            this.Driver = DriverClass.Driver;
            this.Wait = DriverClass.Wait;

            Region = region;
            Settlement = settlement;
            Area = area;
            LotPrice = lotPrice;
            PhoNo = phoNo;
            AcceptTandC = acceptTandC;

        }

        // Tik minimalaus uzpildymo testui garage
        public RealEstate(string region, string settlement, string lotPrice, string phoNo, bool acceptTandC)
        {
            this.Driver = DriverClass.Driver;
            this.Wait = DriverClass.Wait;

            Region = region;
            Settlement = settlement;
            LotPrice = lotPrice;
            PhoNo = phoNo;
            AcceptTandC = acceptTandC;

        }

        // Tik adreso pildymo testams
        public RealEstate(string region, string settlement, string microdistrict, string street)
        {
            this.Driver = DriverClass.Driver;
            this.Wait = DriverClass.Wait;

            Region = region;
            Settlement = settlement;
            Microdisctrict = microdistrict;
            Street = street;

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

            Driver.FindElement(By.Id("districtTitle")).Click();

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
            }
            catch (Exception e)
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

        public virtual void SetLocation()
        {
            SetRegion();
            SetSettlement();
            try { SetMicrodistrict(); }
            catch { Console.WriteLine("Microdistrict field is hidden"); }
            SetStreet();
        }

        public void SetUnqItemNo()
        {
            Driver.FindElement(By.Name("RCNumber")).Click();
            Driver.FindElement(By.Name("RCNumber")).SendKeys(this.UniqItemNo);
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

       public void SetDescription()
        {
           Driver.FindElement(By.Name("notes_lt")).Click();
           Driver.FindElement(By.Name("notes_lt")).SendKeys(this.Description);
        }

        public void SetUpPhotos()
        {
            IWebElement upload_file = Driver.FindElement(By.XPath("//*[@id=\"uploadPhotoBtn\"]/input"));

            upload_file.SendKeys("C:\\Users\\akvile.kondrotaite\\OneDrive - SERMO\\Desktop\\Test files\\Images\\JPG\\alpaca.jpg");
        }

        public void SetYoutubeLink()
        {
            Driver.FindElement(By.Name("Video")).Click();
            Driver.FindElement(By.Name("Video")).SendKeys(this.YoutubeLink);
        }

        public void SetTour3DLink()
        {
            Driver.FindElement(By.Name("tour_3d")).Click();
            Driver.FindElement(By.Name("tour_3d")).SendKeys(this.Tour3DLink);
        }

        public void SetLotPrice()
        {
            Driver.FindElement(By.Id("priceField")).Click();
            Driver.FindElement(By.Id("priceField")).SendKeys(this.LotPrice);

        }

        public void SetPhoNo()
        {
            IList<IWebElement> elements = Driver.FindElement(By.ClassName("new-object-from")).FindElements(By.TagName("li"));
            elements[elements.Count - 7].FindElements(By.TagName("span"))[0].FindElement(By.TagName("input")).SendKeys(this.PhoNo);
        }

        public void SetEmail()
        {
            Driver.FindElement(By.Name("email")).Click();
            Driver.FindElement(By.Name("email")).Clear();
            Driver.FindElement(By.Name("email")).SendKeys(this.Email);
        }

        public void SetDoNotShowEmail()
        {
            if (this.DoNotShowEmail)
            {
                IList<IWebElement> elements = Driver.FindElement(By.ClassName("new-object-from")).FindElements(By.TagName("li"));
                elements[elements.Count - 5].FindElements(By.TagName("span"))[0].Click();
            }

        }

        // teisingas mokytojo variantas
        public void SetTurnOffChat()
        {
            if (this.TurnOffChat)
            {
                IList<IWebElement> elements = Driver.FindElement(By.ClassName("new-object-from")).FindElements(By.TagName("li"));
                elements[elements.Count - 4].FindElements(By.TagName("span"))[0].Click();
            }
        }
        
        public void SetAcceptTandC()
        {
            if (this.AcceptTandC)
            {
                IList<IWebElement> elements = Driver.FindElement(By.ClassName("new-object-from")).FindElements(By.TagName("li"));
                elements[elements.Count - 3].FindElements(By.TagName("span"))[1].Click();
            }
        }

        // variantas ta pati metoda naudoti paskutinems 3 checkboxams
        public void BottomCB(bool checkbox, int position)
        {
            if (checkbox)
            {
                IList<IWebElement> elements = Driver.FindElement(By.ClassName("new-object-from")).FindElements(By.TagName("li"));
                elements[elements.Count - position].FindElements(By.TagName("span"))[1].Click();
            }
        }

        public virtual void FillInListingMax()
        {

         SetLocation();
         SetUnqItemNo();
         SetShowUniqItemNo();
         SetArea();
         SetDescription();
         SetUpPhotos();
         SetYoutubeLink();
         SetTour3DLink();
         SetLotPrice();
         SetPhoNo();
         SetEmail();
         SetDoNotShowEmail();
         SetTurnOffChat();
         SetAcceptTandC();

        }

        public virtual void FillInListingMin()
        {
            SetRegion();
            SetSettlement();
            SetArea();
            SetLotPrice();
            SetPhoNo();
            SetAcceptTandC();

        }

    }
}
