using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace abv_calculator_selenium_tests
{
    [TestFixture]
    public class UnitTest1
    {
        IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver("{ABSOLUTE_PATH_TO_CHROME_DRIVER");
        }

        [Test]
        public void Test()
        {
            _driver.Url = "https://nickoakes.github.io/vue-abv-calculator/";
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}
