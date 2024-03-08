using AltTester.AltTesterUnitySDK.Driver;
using EasyCharacterMovement;
using NUnit.Framework;

namespace QaTestCace.Test.Game.Movement
{
    public abstract class MovementTests
    {
        public AltDriver AltDriver { get; protected set; }

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

        public abstract void CheckMoveRight();
        public abstract void CheckMoveLeft();
        public abstract void CheckMoveUp();
        public abstract void CheckMoveDown();
        public abstract void CheckJump();

        protected AltObject FindPlayer()
        {
            var player = AltDriver.FindObject(By.COMPONENT, nameof(CharacterMovement));
            Assert.NotNull(player);
            return player;
        }

        protected AltObject FindButtonByName(string name)
        {
            var button = AltDriver.FindObject(By.NAME, name);
            Assert.NotNull(button);
            return button;
        }
    }
}
