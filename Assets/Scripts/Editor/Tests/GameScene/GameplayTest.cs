using NUnit.Framework;
using AltTester.AltTesterUnitySDK.Driver;
using EasyCharacterMovement;
using QaTestCace.Controller;
using QaTestCace.UI;
using System.Threading;

namespace QaTestCace.Test.Game
{
    public class GameplayTest
    {
        public AltDriver AltDriver {  get; private set; }

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

        [Test()]
        public void AutoFinish()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var jumpButton = FindButtonByName("JumpButton");
            var rightButton = FindButtonByName("RightButton");
            var leftButton = FindButtonByName("LeftButton");
            var upButton = FindButtonByName("UpButton");
            var downButton = FindButtonByName("DownButton");

            var target = AltDriver.FindObject(By.COMPONENT, nameof(BoxController));
            var winDialog = AltDriver.FindObject(By.COMPONENT, nameof(WinDialog), enabled:false);
            int index = 0;
            while (winDialog.enabled != true)
            {
                if (target.x > player.x)
                    rightButton.Click();
                else if(target.x < player.x)
                    leftButton.Click();
                else if (target.z > player.z)
                    upButton.Click();
                else if (target.z < player.z)
                    downButton.Click();

                if(index % 10 == 0)
                    jumpButton.Click();

                player = FindPlayer();
                target = AltDriver.FindObject(By.COMPONENT, nameof(BoxController));
                winDialog = AltDriver.FindObject(By.COMPONENT, nameof(WinDialog), enabled: false);
                index++;
            }

            var finButton = AltDriver.FindObject(By.NAME, "RestartButton");
            Assert.NotNull(finButton);
            finButton.Click();
        }

        [Test]
        public void ForceFinish()
        {
            AltDriver.LoadScene("GameScene");

            var winDialog = AltDriver.FindObject(By.COMPONENT, nameof(WinDialog), enabled:false);
            winDialog.CallComponentMethod<string>("UnityEngine.GameObject", "SetActive", "UnityEngine.CoreModule", new[] { "true" });

            Thread.Sleep(100);

            var finButton = AltDriver.FindObject(By.NAME, "RestartButton", enabled:false);
            Assert.NotNull(finButton);
            finButton.Click();
        }

        private AltObject FindPlayer()
        {
            var player = AltDriver.FindObject(By.COMPONENT, nameof(CharacterMovement));
            Assert.NotNull(player);
            return player;
        }

        private AltObject FindButtonByName(string name)
        {
            var button = AltDriver.FindObject(By.NAME, name);
            Assert.NotNull(button);
            return button;
        }
    }
}