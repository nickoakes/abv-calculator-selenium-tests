using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace abv_calculator_selenium_tests
{
    public static class TestHelper
    {
        static string URL 
        { 
            get 
            {
                return "https://nickoakes.github.io/vue-abv-calculator/";
            } 
        }

        /// <summary>
        /// Sets the driver URL
        /// </summary>
        /// <param name="driver"></param>
        public static void SetURL(IWebDriver driver)
        {
            driver.Url = URL;
        }

        /// <summary>
        /// Returns the first element in a collection whose 'Displayed' property is true
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static IWebElement GetVisibleElementInCollection(ReadOnlyCollection<IWebElement> elements)
        {
            IWebElement element = null;

            for(int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Displayed)
                {
                    element = elements[i];

                    break;
                }
            }

            return element;
        }

        #region Navigation

        /// <summary>
        /// Clicks 'Start' to advance to the drink selection stage
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public static void CompleteStart(IWebDriver driver, WebDriverWait wait)
        {
            IWebElement startButton = driver.FindElement(By.CssSelector("button.v-btn"));

            startButton.Click();

            wait.Until(x => !startButton.Displayed);
        }

        #endregion
    }
}
