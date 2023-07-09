namespace Code.Scripts.Framework.Health
{
    public interface IDamageTaker
    {
        public void TakeDamage(IDamageApplier damageApplier);
    }
}