using NUnit.Framework;
using AltTester.AltTesterUnitySDK.Driver;

namespace QaTestCace.Test.Lobby
{
    public class LoadTests
    {   
        public AltDriver AltDriver { get; private set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            AltDriver = new AltDriver();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            AltDriver.Stop();
        }

        [Test]
        public void StartLobby()
        {
            AltDriver.LoadScene("StartScene");
            AltDriver.WaitForCurrentSceneToBe("StartScene");
        }

        [Test]
        public void StartGame()
        {
            AltDriver.LoadScene("StartScene");
            AltDriver.WaitForCurrentSceneToBe("StartScene");

            var gameplayButton = AltDriver.FindObject(By.NAME, "Button");
            Assert.NotNull(gameplayButton);
            gameplayButton.Click();
            AltDriver.WaitForCurrentSceneToBe("GameScene");
        }
    }
}