using Moq;
using Skeleton.Interfaces;

namespace Tests {
    public class Tests {

        private Mock<ITarget> _fakeTarget;
        private Mock<IWeapon> _fakeWeapon;
        private Hero _hero;

        [SetUp]
        public void Setup() {
            _fakeTarget = new();
            _fakeWeapon = new();
        }

        [Test]
        public void Test1() {
            _fakeTarget.Setup(x => x.Health).Returns(0);
            _fakeTarget.Setup(x => x.GiveExperience()).Returns(20);
            _fakeTarget.Setup(x => x.IsDead()).Returns(true);
            _hero = new("Vaultie", _fakeWeapon.Object);
            _hero.Attack(_fakeTarget.Object);
            Assert.AreEqual(20, _hero.Experience);

        }
    }
}