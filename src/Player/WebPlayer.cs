using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;

namespace Player
{
    public class WebPlayer : IPlayer, IDisposable
    {
        private const string STREAM_SOURCE = @"https://twist.moe/a/";
        private readonly string CHROME_PATH = Environment.GetEnvironmentVariable("CHROMEDRIVER_PATH");
        private readonly WebDriverWait wait;
        private IWebDriver driver;
        public WebPlayer()
        {
            // Configure ChromeOptions
            var options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");

            // create webdriver
            driver = new ChromeDriver(CHROME_PATH, options);
            driver.Manage().Window.Maximize(); 

            // Configure explicit wait for UI interactions
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        /// <summary>
        /// Changes the anime being played to the one whose relative URL is passed
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <param name="episode"></param>
        public void ViewAnime(string relativeUrl, int episode = 1)
        {
            string fullUrl = STREAM_SOURCE + relativeUrl + "/" + episode;
            // Go to the relativeUrl
            driver.Navigate().GoToUrl(fullUrl);
            // ToggleFullScreen();
        }

        /// <summary>
        /// Presses the Play button
        /// </summary>
        public void Play()
        {
            // Click Video to toggle play status
            IWebElement videoElement = driver.FindElement(By.CssSelector("video"));
            videoElement.Click();
        }

        /// <summary>
        /// Toggle fullscreen status of player
        /// </summary>
        public void ToggleFullScreen()
        {
            // Double Click Video to Toggle Fullscreen
            IWebElement videoElement = driver.FindElement(By.CssSelector("video"));
            new Actions(driver).DoubleClick(videoElement).Perform();
        }

        /// <summary>
        ///  Pause the WebPlayer
        /// </summary>
        public void Pause()
        {
            driver.FindElement(By.CssSelector(".play-pause-btn")).Click();
        }

        // Quit the driver to prevent problems
        public void Dispose()
        {
            driver.Close();
            driver.Quit();
        }
    }
}