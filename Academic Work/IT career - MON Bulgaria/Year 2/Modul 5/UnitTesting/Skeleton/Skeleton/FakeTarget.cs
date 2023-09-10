using Skeleton.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton {
    public class FakeTarget : ITarget {

        public void TakeAttack(int attackPoints) {

        }
        public int Health => 0;
        public int GiveExperience() {
            return 20;
        }
        public bool IsDead() {
            return true;
        }
    }
}
