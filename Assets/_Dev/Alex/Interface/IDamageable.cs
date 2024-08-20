namespace Alex
{
    public interface IDamageable
    {
        void TakeDamage(float amount, IAttackable source);
    }
}