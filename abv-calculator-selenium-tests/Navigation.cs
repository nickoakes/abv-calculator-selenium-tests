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

        #region SetUp

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver("C:/Users/Nick/source/repos/abv-calculator-selenium-tests/abv-calculator-selenium-tests");

            _wait = new WebDriverWait(_driver, new TimeSpan(1000));

            TestHelper.SetURL(_driver);
        }

        #endregion

        [Test]
        [Retry(3)]
        public void Start_StartButtonClicked_DrinkSelectionStage()
        {
            try
            {
                TestHelper.NavigateToStage(_driver, _wait, (int)TestHelper.Stages.DrinkSelection);

                bool result = TestHelper.CheckPromptText(_driver, "What have you made?");

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [Test]
        [Retry(3)]
        public void DrinkSelectionStage_ContinueButtonClicked_BaseSugarStage()
        {
            try
            {
                TestHelper.NavigateToStage(_driver, _wait, (int)TestHelper.Stages.BaseSugar);

                bool result = TestHelper.CheckPromptText(_driver, "The default sugar content for beer is 517.7 grams per one standard 40-pint beer kit");

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [Test]
        [Retry(3)]
        public void BaseSugarStage_YesButtonClicked_AdditionalSugarStage()
        {
            try
            {
                TestHelper.NavigateToStage(_driver, _wait, (int)TestHelper.Stages.AdditionalSugar);

                bool result = TestHelper.CheckPromptText(_driver, "Have you added any additional sugar?");

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [Test]
        [Retry(3)]
        public void AdditionalSugarStage_NoButtonClicked_TotalVolumeStage()
        {
            try
            {
                TestHelper.NavigateToStage(_driver, _wait, (int)TestHelper.Stages.TotalVolume);

                bool result = TestHelper.CheckPromptText(_driver, "What's the total volume?");

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [Test]
        [Retry(3)]
        public void TotalVolumeStage_VolumeEnteredAndContinueClicked_ResultStage()
        {
            try
            {
                TestHelper.NavigateToStage(_driver, _wait, (int)TestHelper.Stages.Result);

                bool result = TestHelper.CheckPromptText(_driver, "is approximately");

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [Test]
        [Retry(3)]
        public void ResultStage_ResetClicked_StartStage()
        {
            try
            {
                TestHelper.NavigateToStage(_driver, _wait, (int)TestHelper.Stages.Result);

                TestHelper.Reset(_driver, _wait);

                bool result = TestHelper.CheckPromptText(_driver, "Use this calculator to work out the approximate A.B.V. of home-brewed drinks.");

                Assert.IsTrue(result);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [Test]
        [Retry(3)]
        public void TotalVolumeStage_BackButtonClickedRepeatedly_DrinkSelectionStage()
        {
            try
            {
                TestHelper.NavigateToStage(_driver, _wait, (int)TestHelper.Stages.TotalVolume);

                while(!TestHelper.CheckPromptText(_driver, "What have you made?"))
                {
                    TestHelper.ClickButtonToAdvance(_driver, _wait, "back");
                }

            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        #region TearDown

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }

        #endregion
    }
}
