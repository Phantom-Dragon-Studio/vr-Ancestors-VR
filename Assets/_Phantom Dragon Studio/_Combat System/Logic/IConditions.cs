public interface IConditions
{
    bool TargetIsAttackable(_WorldObjectData targetInformation);
    bool TargetIsAlive(_WorldObjectData targetInformation);
    bool TargetWithinBaseAttackRange(_WorldObjectData attackerInformation, _WorldObjectData targetInformation);
    float ElementalStrengthAndWeaknessComparison(ElementType elementDamageType, _WorldObjectData targetedCharacter);
    float ElementalAttunmentAdjustment(ElementType elementDamageType, _WorldObjectData targetedCharacter);
}
