using AlexTools.Extensions;
using System.Collections.Generic;

namespace Alex
{
    public interface IAttackable
    {
        bool CanAttack { get; set; }
        float Damage { get; set; }
        ITargetable Target { get; set; }

        void Attack() { }

        void Attack(IDamageable target)
        {
            if (CanAttack) 
                target.TakeDamage(Damage, this);
        }

        void Attack(IEnumerable<IDamageable> targets) => targets.ForEach(x => Attack(x));
        void Attack(params IDamageable[] targets) => Attack(targets as IEnumerable<IDamageable>);
    }
}