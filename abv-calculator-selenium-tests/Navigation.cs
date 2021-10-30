using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace abv_calculator_selenium_tests
{
    [TestFixture]
    public class Navigation
    {
        IWebDriver _driver;
        WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver("C:/Users/Nick/source/repos/abv-calculator-selenium-tests/abv-calculator-selenium-tests");

            _wait = new WebDriverWait(_driver, new TimeSpan(1000));

            TestHelper.SetURL(_driver);
        }

        [Test]
        public void Start_StartButtonClicked_DrinkSelectionStage()
        {
            TestHelper.CompleteStart(_driver, _wait);

            bool result = TestHelper.GetVisibleElementInCollection(_driver.FindElements(By.TagName("h2"))).Text == "What have you made?";

            Assert.IsTrue(result);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}
