using NUnit.Framework;
using AltTester.AltTesterUnitySDK.Driver;
using EasyCharacterMovement;
using System.Threading;

namespace QaTestCace.Test.Game
{
    public class MovementTests
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

        [Test]
        public void CheckMoveRight()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("RightButton");

            var startPos = player.GetWorldPosition();
            button.Click();
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Greater(endPos.x, startPos.x);
        }

        [Test]
        public void CheckMoveLeft()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("LeftButton");

            var startPos = player.GetWorldPosition();
            button.Click();
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Less(endPos.x, startPos.x);
        }

        [Test]
        public void CheckMoveUp()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("UpButton");

            var startPos = player.GetWorldPosition();
            button.Click();
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Greater(endPos.z, startPos.z);
        }

        [Test]
        public void CheckMoveDown()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("DownButton");

            var startPos = player.GetWorldPosition();
            button.Click();
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Less(endPos.z, startPos.z);
        }

        [Test]
        public void CheckJump()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("JumpButton");

            // Check Jump
            var startPos = player.GetWorldPosition();
            button.Click();
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Greater(endPos.y, startPos.y);

            Thread.Sleep(1000);

            // Check Jump in Air
            player = FindPlayer();
            startPos = player.GetWorldPosition();
            button.Click();
            player = FindPlayer();
            endPos = player.GetWorldPosition();
            Assert.LessOrEqual(endPos.y, startPos.y);
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