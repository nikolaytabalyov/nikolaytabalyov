using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.Interfaces {
    public interface IWeapon {

        public int AttackPoints { get; }
        public int DurabilityPoints { get; }
        public void Attack(ITarget target);

    }
}
