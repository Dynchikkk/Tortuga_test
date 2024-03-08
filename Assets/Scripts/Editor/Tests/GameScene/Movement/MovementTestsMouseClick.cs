using NUnit.Framework;
using AltTester.AltTesterUnitySDK.Driver;
using System.Threading;

namespace QaTestCace.Test.Game.Movement
{
    public class MovementTestsMouseClick : MovementTests
    {
        const AltKeyCode MOUSE_KEY_CODE = AltKeyCode.Mouse0;
        const float PRESS_DURATION = 0.1f;

        [Test]
        public override void CheckMoveRight()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("RightButton");

            var startPos = player.GetWorldPosition();
            AltDriver.MoveMouse(button.GetScreenPosition());
            AltDriver.PressKey(MOUSE_KEY_CODE, PRESS_DURATION);
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Greater(endPos.x, startPos.x);
        }

        [Test]
        public override void CheckMoveLeft()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("LeftButton");

            var startPos = player.GetWorldPosition();
            AltDriver.MoveMouse(button.GetScreenPosition());
            AltDriver.PressKey(MOUSE_KEY_CODE, PRESS_DURATION);
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Less(endPos.x, startPos.x);
        }

        [Test]
        public override void CheckMoveUp()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("UpButton");

            var startPos = player.GetWorldPosition();
            AltDriver.MoveMouse(button.GetScreenPosition());
            AltDriver.PressKey(MOUSE_KEY_CODE, PRESS_DURATION);
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Greater(endPos.z, startPos.z);
        }

        [Test]
        public override void CheckMoveDown()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("DownButton");

            var startPos = player.GetWorldPosition();
            AltDriver.MoveMouse(button.GetScreenPosition());
            AltDriver.PressKey(MOUSE_KEY_CODE, PRESS_DURATION);
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Less(endPos.z, startPos.z);
        }

        [Test]
        public override void CheckJump()
        {
            AltDriver.LoadScene("GameScene");

            var player = FindPlayer();
            var button = FindButtonByName("JumpButton");

            // Check Jump
            var startPos = player.GetWorldPosition();
            AltDriver.MoveMouse(button.GetScreenPosition());
            AltDriver.PressKey(MOUSE_KEY_CODE, PRESS_DURATION);
            player = FindPlayer();
            var endPos = player.GetWorldPosition();
            Assert.Greater(endPos.y, startPos.y);

            Thread.Sleep(1000);

            // Check Jump in Air
            player = FindPlayer();
            startPos = player.GetWorldPosition();
            AltDriver.MoveMouse(button.GetScreenPosition());
            AltDriver.PressKey(MOUSE_KEY_CODE, PRESS_DURATION);
            player = FindPlayer();
            endPos = player.GetWorldPosition();
            Assert.LessOrEqual(endPos.y, startPos.y);
        }
    }
}