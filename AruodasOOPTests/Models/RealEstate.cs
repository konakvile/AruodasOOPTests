using AruodasOOPTests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
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
        public string StreetNo { get; set; }
        public bool ShowStreetNo { get; set; }
        public string UniqItemNo { get; set; }
        public bool ShowUniqItemNo { get; set; }
        public string Area { get; set; }

        public RealEstate() {

            this.Driver = DriverClass.Driver;
            this.Wait = DriverClass.Wait;
        }
        public RealEstate()
        {

            this.Driver = DriverClass.Driver;
            this.Wait = DriverClass.Wait;
        }

    }
}
