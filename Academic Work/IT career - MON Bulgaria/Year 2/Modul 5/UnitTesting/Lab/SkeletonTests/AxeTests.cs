namespace SkeletonTests {
    public class AxeTests {

        private const int dummyHP = 10;
        private const int dummyXP = 10;
        private const int axeATKPoints = 10;
        private const int axeDURPoints = 2;
        private const int lightATKPoints = 1;
        Dummy dummy;
        Axe axe;

        [SetUp]
        public void Setup() {
            dummy = new(dummyHP, dummyXP);
            axe = new(axeATKPoints, axeDURPoints);
        }

        [Test]
        public void AxeLosesDurabilityAfterAttack() { 
            axe.Attack(dummy);
            Assert.AreEqual(axeDURPoints - 1, axe.DurabilityPoints, "Axe durability isn't reducing after attack.");
        }

        [Test]
        public void BrokenAxeCannotAttack() { 
            axe.Attack(dummy);

            var ex = Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));  
        }
    }
}