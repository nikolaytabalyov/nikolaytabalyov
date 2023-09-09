namespace SkeletonTests {
    public class DummyTests {

        private const int dummyHP = 10;
        private const int dummyXP = 10;
        private const int axeATKPoints = 10;
        private const int axeDURPoints = 10;
        private const int lightATKPoints = 1;
        Dummy dummy;
        Axe axe;

        [SetUp]
        public void Setup() {
            dummy = new(dummyHP, dummyXP);
            axe = new(axeATKPoints, axeDURPoints);
        }

        [Test]
        public void DummyLosesHPWhenAttacked() {
            int startHP = dummy.Health;
            dummy.TakeAttack(lightATKPoints);
            Assert.AreEqual(startHP - lightATKPoints, dummy.Health, "Dummy isn't taking damage!");
        }

        [Test]
        public void DeadDummyThrowsExceptionWhenAttacked() {
            axe.Attack(dummy);
            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
        }

        [Test]
        public void DeadDummyGivesXP() {
            axe.Attack(dummy);
            Assert.AreEqual(dummyXP, dummy.GiveExperience());
        }

        [Test]
        public void AliveDummyDoesNotGiveXP() {
            var ex = Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());
            Assert.AreEqual(ex.Message, "Target is not dead.");
        }

    }
}