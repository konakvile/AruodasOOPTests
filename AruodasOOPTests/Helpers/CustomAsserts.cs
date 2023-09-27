using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AruodasOOPTests.Helpers
{
    internal class CustomAsserts
    {
        public static IWebDriver Driver;
        public static void AssertEqualsId(string idtofind, string expected)
        {
            if (Driver is null)
            {
                Driver = DriverClass.Driver;
            }

            string actual = "";
            try
            {
                actual = Driver.FindElement(By.Id(idtofind)).Text;
            }
            catch
            {
                Assert.Fail("searched element not found");
            }
            Assert.AreEqual(expected, actual);
        }

        public static void AssertEqualsXPath(string xpathtofind, string expected)
        {
            if (Driver is null)
            {
                Driver = DriverClass.Driver;
            }
            string actual = "";
            try
            {
                actual = Driver.FindElement(By.XPath(xpathtofind)).Text;
            }
            catch (Exception ex)
            {
                Assert.Fail("searched element not found");
            }
            Console.WriteLine("!" + actual + "!");
            Assert.AreEqual(expected, actual);
        }

        public static void AssertEqualsClassName(string classNameToFind, string expected)
        {
            if (Driver is null)
            {
                Driver = DriverClass.Driver;
            }
            string actual = "";
            try
            {
                actual = Driver.FindElement(By.ClassName(classNameToFind)).Text;
            }
            catch (Exception ex)
            {
                Assert.Fail("searched element not found");
            }
            Assert.AreEqual(expected, actual);
        }
    }
}
