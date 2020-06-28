using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Player
{
    public class WebPlayer : IPlayer, IDisposable
    {
        private const string STREAM_SOURCE = @"https://twist.moe/a/";
        private const string CHROME_PATH = @"/Users/ramelb.francisco/Documents/Jasper/learn-dotnet/Scarab/bin/";
        private IWebDriver driver;
        public WebPlayer()
        {
            // Configure ChromeOptions
            var options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");

            // create webdriver
            driver = new ChromeDriver(CHROME_PATH, options);
            driver.Manage().Window.Maximize();
        }

        /// <summary>Gets a relative URL and plays the first episode of the anime
        /// </summary>
        public void Play(string relativeUrl)
        {
            string fullUrl = STREAM_SOURCE + relativeUrl;
            // Go to the relativeUrl
            driver.Navigate().GoToUrl(fullUrl);
        }

        /// <summary>Play an anime's specific episode
        /// </summary>
        public void Play(string relativeUrl, int episodeNumber)
        {
            string fullUrl = STREAM_SOURCE + relativeUrl + "/" + episodeNumber.ToString();
            // Go to the relativeUrl
            driver.Navigate().GoToUrl(fullUrl);
        }

        // Quit the driver to prevent problems
        public void Dispose()
        {
            driver.Quit();
        }
    }
}