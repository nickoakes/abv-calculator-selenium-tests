using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace abv_calculator_selenium_tests
{
    public static class TestHelper
    {

        #region Generic

        static string URL 
        { 
            get 
            {
                return "https://nickoakes.github.io/vue-abv-calculator/";
            } 
        }

        public enum Stages
        {
            Start,
            DrinkSelection,
            BaseSugar,
            AdditionalSugar,
            TotalVolume,
            Result
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

        /// <summary>
        /// Returns a boolean value based on whether the prompt text displayed in the first visible <h2> element on the page contains the specified string
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool CheckPromptText(IWebDriver driver, string text)
        {
            return GetVisibleElementInCollection(driver.FindElements(By.TagName("h2"))).Text.Contains(text);
        }

        #endregion

        #region Navigation

        /// <summary>
        /// Clicks the currently displayed button with the specified text, and returns this button as an IWebElement
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="buttonText"></param>
        /// <returns></returns>
        public static IWebElement ClickButton(IWebDriver driver, string buttonText)
        {
            ReadOnlyCollection<IWebElement> buttons = driver.FindElements(By.ClassName("v-btn__content"));

            IWebElement buttonClicked = null;

            for(int i = 0; i < buttons.Count; i++)
            {
                if(buttons[i].Displayed && buttons[i].Text.ToLower().Contains(buttonText))
                {
                    buttons[i].Click();

                    buttonClicked = buttons[i];

                    break;
                }
            }

            return buttonClicked;
        }

        /// <summary>
        /// Clicks the currently displayed button with the specified text, and waits until this button is no longer visible
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="buttonText"></param>
        public static void ClickButtonToAdvance(IWebDriver driver, WebDriverWait wait, string buttonText)
        {
            IWebElement button = ClickButton(driver, buttonText);

            wait.Until(x => !button.Displayed);
        }

        /// <summary>
        /// Clicks 'Start' to advance to the drink selection stage
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public static void CompleteStart(IWebDriver driver, WebDriverWait wait)
        {
            ClickButtonToAdvance(driver, wait, "start");
        }

        /// <summary>
        /// Selects the specified drink choice and clicks 'Continue' to advance to the base sugar stage
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="drinkChoice"></param>
        public static void CompleteDrinkSelection(IWebDriver driver, WebDriverWait wait, string drinkChoice)
        {
            ReadOnlyCollection<IWebElement> inputs = driver.FindElements(By.CssSelector(".v-input--selection-controls__input input[type='radio']"));

            for(int i = 0; i < inputs.Count; i++)
            {
                if(inputs[i].Displayed && inputs[i].GetAttribute("value") == drinkChoice)
                {
                    inputs[i].Click();

                    break;
                }
            }

            ClickButtonToAdvance(driver, wait, "continue");
        }

        /// <summary>
        /// Clicks 'Yes' to set the base sugar mass to default, and advance to the additional sugar stage
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public static void CompleteBaseSugarWithDefault(IWebDriver driver, WebDriverWait wait)
        {
            ClickButtonToAdvance(driver, wait, "yes");
        }

        /// <summary>
        /// Clicks 'No' to set the additional sugar mass to 0, and advance to the total volume stage
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public static void CompleteAdditionalSugarWithDefault(IWebDriver driver, WebDriverWait wait)
        {
            ClickButtonToAdvance(driver, wait, "no");
        }

        /// <summary>
        /// Enters the total volume specified, and clicks 'Continue' to advance to the result stage
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="totalVolume"></param>
        public static void CompleteTotalVolume(IWebDriver driver, WebDriverWait wait, decimal totalVolume)
        {
            driver.FindElement(By.Id("input-74")).SendKeys(totalVolume.ToString());

            ClickButtonToAdvance(driver, wait, "continue");
        }

        /// <summary>
        /// Clicks the 'Reset' button to return to the start stage
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public static void Reset(IWebDriver driver, WebDriverWait wait)
        {
            ClickButtonToAdvance(driver, wait, "reset");
        }

        /// <summary>
        /// Navigates to the stage specified by the shortest route possible
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="stage"></param>
        /// <param name="drinkChoice"></param>
        /// <param name="totalVolume"></param>
        public static void NavigateToStage(IWebDriver driver, WebDriverWait wait, int stage, string drinkChoice = "Beer", decimal totalVolume = 1)
        {
            int currentStage = 0;

            while(currentStage < stage)
            {
                currentStage++;

                switch (currentStage)
                {
                    case (int)Stages.DrinkSelection:
                        CompleteStart(driver, wait);
                        break;
                    case (int)Stages.BaseSugar:
                        CompleteDrinkSelection(driver, wait, drinkChoice);
                        break;
                    case (int)Stages.AdditionalSugar:
                        CompleteBaseSugarWithDefault(driver, wait);
                        break;
                    case (int)Stages.TotalVolume:
                        CompleteAdditionalSugarWithDefault(driver, wait);
                        break;
                    case (int)Stages.Result:
                        CompleteTotalVolume(driver, wait, totalVolume);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
