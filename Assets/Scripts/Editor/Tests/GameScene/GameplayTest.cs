using NUnit.Framework;
using AltTester.AltTesterUnitySDK.Driver;
using EasyCharacterMovement;
using QaTestCace.Controller;
using QaTestCace.UI;
using System.Threading;
using System;
using UnityEngine;

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
            const int maxStackCount = 10;
            const float maxTimeCount = 10000;
            const float sleepTime = 500f;

            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var jumpButton = FindButtonByName("JumpButton");
            var rightButton = FindButtonByName("RightButton");
            var leftButton = FindButtonByName("LeftButton");
            var upButton = FindButtonByName("UpButton");
            var downButton = FindButtonByName("DownButton");

            var target = AltDriver.FindObject(By.COMPONENT, nameof(BoxController));
            var winDialog = AltDriver.FindObject(By.COMPONENT, nameof(WinDialog), enabled:false);
            var oldPlayer = player;

            int counter = 0;
            float timer = 0;
            while (winDialog.enabled != true)
            {
                Assert.Less(counter, maxStackCount);
                Assert.Less(timer, maxTimeCount);

                if (target.x > player.x)
                    rightButton.PointerDownFromObject();
                if(target.x < player.x)
                    leftButton.PointerDownFromObject();
                if (target.z > player.z)
                    upButton.PointerDownFromObject();
                if (target.z < player.z)
                    downButton.PointerDownFromObject();

                Thread.Sleep((int)sleepTime);
                rightButton.PointerUpFromObject();
                leftButton.PointerUpFromObject();
                upButton.PointerUpFromObject();
                downButton.PointerUpFromObject();

                oldPlayer = player;
                player = FindPlayer();
                target = AltDriver.FindObject(By.COMPONENT, nameof(BoxController));
                winDialog = AltDriver.FindObject(By.COMPONENT, nameof(WinDialog), enabled: false);

                if(Math.Abs(player.worldX - oldPlayer.worldX) < 0.1f && 
                    Math.Abs(player.worldZ - oldPlayer.worldZ) < 0.1f)
                {
                    jumpButton.Click();
                    counter++;
                }

                timer += sleepTime;
            }

            var finButton = AltDriver.FindObject(By.NAME, "RestartButton");
            Assert.NotNull(finButton);
            finButton.Click();
        }

        [Test]
        public void ForceFinish()
        {
            const string componentName = "UnityEngine.GameObject";
            const string methodName = "SetActive";
            const string assemblyName = "UnityEngine.CoreModule";

            AltDriver.LoadScene("GameScene");

            var winDialog = AltDriver.FindObject(By.COMPONENT, nameof(WinDialog), enabled:false);
            winDialog.CallComponentMethod<string>(componentName, methodName, assemblyName, new[] { "true" });

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