using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.Interfaces {
    public interface ITarget {
        public void TakeAttack(int attackPoints);
        public int Health { get; }
        public int GiveExperience();
        public bool IsDead();
    }
}
