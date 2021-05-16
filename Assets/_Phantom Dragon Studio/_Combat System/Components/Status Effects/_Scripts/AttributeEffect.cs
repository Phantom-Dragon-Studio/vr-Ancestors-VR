using System.Collections;
using System;
using UnityEngine;

//New refactoring should result in an order of the following to apply attributes.
//Character.ClearStats
//ApplyEffect
//Character.CalculatePrimaryStats (This function chain reaction triggers... 
//stats for Primary, Secondary, Elemental, and Gear stat bonuses then relays it back to the Stats panel,
//while calculating values automatically..

public enum AttibuteEffectBonus
{
    Strength,
    Agility,
    Intelligence,
    Endurance,
    HealthBase,
    HealthRegen,
    MEFBase,
    MEFRegen,
    Stamina,
    StamRegen,
    CriticalChance,
    DodgeChance,
    ElementalResistance,
    FireResistance,
    EarthResistance,
    WindResistance,
    DivineResistance,
    DarkResistance,
    ArcaneResistance,
    LightningResistance,
    WaterResistance,
}

public class AttributeEffect : StatusEffectBase
{
    [SerializeField] public AttibuteEffectBonus attributeEffected;
    [SerializeField] public int effectAmount;
    [SerializeField] private CharacterClass characterClass;


    override protected void ApplyEffect()
    {
        myElementType = ElementType.Divine;
        characterClass = this.characterInformation.GetComponent<CharacterClass>();
        characterClass.ClearStats();
        ApplyAttributeBuff(this.effectAmount);
        characterClass.CalculatePrimaryAttributes();
        base.ApplyEffect();
    }

    protected override void RemoveEffect()
    {
        characterClass.ClearStats();
        ApplyAttributeBuff(-this.effectAmount);
        characterClass.CalculatePrimaryAttributes();
        base.RemoveEffect();
    }

    public void ApplyAttributeBuff(int amountToAdjustBy)
    {
        Debug.Log("Applying Effect");
        switch (attributeEffected)
        {
            case AttibuteEffectBonus.Strength:
                {
                    this.characterInformation.characterData.baseStrength += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.Agility:
                {
                    this.characterInformation.characterData.baseAgility += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.Intelligence:
                {
                    this.characterInformation.characterData.baseIntelligence += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.Endurance:
                {
                    this.characterInformation.characterData.baseEndurance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.HealthBase:
                {
                    this.characterInformation.characterData.maximumHealth += amountToAdjustBy;
                    if (amountToAdjustBy >= 0)
                    {
                        this.characterInformation.characterData.currentHealth += amountToAdjustBy;
                    }
                    break;
                }
            case AttibuteEffectBonus.HealthRegen:
                {
                    this.characterInformation.characterData.healthRegenerationAmount += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.MEFBase:
                {
                    this.characterInformation.characterData.maximumM_E_F += amountToAdjustBy;
                    if (amountToAdjustBy >= 0)
                    {
                        this.characterInformation.characterData.currentM_E_F += amountToAdjustBy;
                    }
                    break;
                }
            case AttibuteEffectBonus.MEFRegen:
                {
                    this.characterInformation.characterData.M_E_FRegenerationAmount += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.Stamina:
                {
                    this.characterInformation.characterData.maximumStamina += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.StamRegen:
                {
                    this.characterInformation.characterData.staminaRegenerationAmount += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.CriticalChance:
                {
                    this.characterInformation.characterData.criticalStrikeChance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.DodgeChance:
                {
                    this.characterInformation.characterData.criticalStrikeChance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.ElementalResistance:
                {
                    this.characterInformation.characterData.elementalResistance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.FireResistance:
                {
                    this.characterInformation.characterData.fireResistance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.EarthResistance:
                {
                    this.characterInformation.characterData.earthResistance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.WindResistance:
                {
                    this.characterInformation.characterData.windResistance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.DivineResistance:
                {
                    this.characterInformation.characterData.divineResistance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.DarkResistance:
                {
                    this.characterInformation.characterData.darkResistance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.ArcaneResistance:
                {
                    this.characterInformation.characterData.arcaneResistance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.LightningResistance:
                {
                    this.characterInformation.characterData.lightningResistance += amountToAdjustBy;
                    break;
                }
            case AttibuteEffectBonus.WaterResistance:
                {
                    this.characterInformation.characterData.waterResistance += amountToAdjustBy;
                    break;
                }
        }
    }
}
