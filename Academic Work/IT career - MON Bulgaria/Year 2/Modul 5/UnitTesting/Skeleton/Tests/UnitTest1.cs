using Skeleton;
using Skeleton.Interfaces;

namespace Tests {

    public class Tests {

        private const string HeroName = "Vaultie";

        [SetUp]
        public void Setup() {

        }

        [Test]
        public void HeroGainsXPAfterAttacksIfTargetDies() {
            ITarget fakeTarget = new FakeTarget();
            IWeapon fakeWeapon = new FakeWeapon(); 

            Hero hero = new(HeroName, fakeWeapon);
            hero.Attack(fakeTarget);

            Assert.AreEqual(20, hero.Experience);
        }
    }
}