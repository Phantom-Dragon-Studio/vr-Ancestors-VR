public interface ICalculations
{
    float PlayerDealDamageIgnoringDefense(_WorldObjectData targetInformation, float damage, float randomRange, float velocity);

    float PlayerDealDamageMinusDefense(_WorldObjectData targetInformation, float damage, float randomRange, float velocity);

    float PlayerDealMagicDamageMinusResistance(_WorldObjectData targetInformation, float randomRange, float damage, ElementType damagingElementType);

    float NPCDealDamageIgnoringDefense(_WorldObjectData targetInformation, float randomRange, float damage);

    float NPCDealStandardDamageMinusDefense(_WorldObjectData targetInformation, float randomRange, float damage);

    bool DecideIfAttackIsDodged(float chanceToDodge);

    bool DecideIfCriticalHit(float criticalHitChance);
}
