using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculations : ICalculations {

    IGameManager _gameManager;
    IConditions _conditions;
    

    public Calculations(IGameManager gameManager, IConditions conditions)
    {
        _gameManager = gameManager;
        _conditions = conditions;
    }

    //Takes in.............
    //
    //Time
    //Modifiers
    //Distance
    //Damage Type -     Normal, Piercing, Heavy, Siege, Magical, Leader, Demonic
    //Damage Amount
    //Heal Amount
    //Defense Type
    //Elements
    //Movement Speed
    //Attack Speed

    //Puts out.................

    //Velocity
    //TotalDamage
    //TotalHealing
    //FinalAttackSpeed
    //FinalMovementSpeed
    //StatusAilments

    #region Player ~ Standard Damage Handling
    public float PlayerDealDamageIgnoringDefense(_WorldObjectData targetInformation, float damage, float randomRange, float velocity)
    {

        float randomizedDamage = Random.Range(damage - randomRange, damage + randomRange) * (velocity / 2);
        if (randomizedDamage < 0)
        {
            return 0;
        }
        else
        {
            //Debug.Log("Calculations: Player - Dealing Standard Damage " + randomizedDamage);
            return randomizedDamage;
        }
    }

    public float PlayerDealDamageMinusDefense(_WorldObjectData targetInformation, float damage, float randomRange, float velocity)
    {

        float randomizedDamage = Random.Range(damage - randomRange, damage + randomRange)
                                    * (velocity / 2) / _gameManager.GameSettings.defenseReductionRatio;
        if (randomizedDamage < 0)
        {
            return 0;
        }
        else
        {
            //Debug.Log("Calculations: Player - Dealing Standard Damage " + randomizedDamage);
            return randomizedDamage;
        }
    }

    public bool PlayerBlock(_WorldObjectData targetInformation, float damage)
    {
        //Stuff To Do?
        return true;
    }
    #endregion

    #region Player ~ Magic Damage Handling
    public float PlayerDealMagicDamageMinusResistance (_WorldObjectData targetInformation, float randomRange, float damage, ElementType damagingElementType)
    {
        float randomizedDamage = Random.Range(damage - randomRange, damage + randomRange);
        randomizedDamage -= _conditions.ElementalStrengthAndWeaknessComparison(damagingElementType, targetInformation);
        randomizedDamage *= _conditions.ElementalAttunmentAdjustment(damagingElementType, targetInformation);
        //Debug.Log("Magic damage delt to " + targetInformation.objectName + " resulted in a total of " + randomizedDamage);
        if (randomizedDamage < 0)
        {
            return 0;
        }
        else
        {
            //Debug.Log("Calculations: Player - Dealing Magic Damage Minus Resistance & ElementalAttunment Resistance Modifier");
            return randomizedDamage;
        }
    }
    #endregion

    #region NPC - Standard Damage Handling
    public float NPCDealDamageIgnoringDefense(_WorldObjectData targetInformation, float randomRange, float damage)
    {

        float recalculatedDamage = Random.Range(damage - randomRange, damage + randomRange);
        if (recalculatedDamage < 0)
        {
            return 0;
        }
        else
        {
            //Debug.Log("Calculations: Player - Dealing Standard Damage " + recalculatedDamage);
            return recalculatedDamage;
        }
    }

    public float NPCDealStandardDamageMinusDefense(_WorldObjectData targetInformation, float randomRange, float damage)
    {
        float recalculatedDamage = Random.Range(damage - randomRange, damage + randomRange);
        recalculatedDamage -= (targetInformation.baseDefense / _gameManager.GameSettings.defenseReductionRatio);
        if (recalculatedDamage <= 0)
        {
            //Debug.Log("Damage returned 0, must have dodged the attack.");
            return 0;
        }
        else
        {
            //Debug.Log("Calculations: Player - Dealing Standard Damage " + recalculatedDamage);
            return recalculatedDamage;
        }
    }
    #endregion

    #region Random Chance Statistics
    public bool DecideIfAttackIsDodged(float chanceToDodge)
    {
        float randomDodgeChance = Random.Range(0, 100);
        if (chanceToDodge >= randomDodgeChance)
        {
            //Debug.Log("DODGED!");
            return true;
        }
        else return false;
    }

    public bool DecideIfCriticalHit(float criticalHitChance)
    {
        float randomCritChance = Random.Range(0, 100);
        if (criticalHitChance >= randomCritChance)
        {
            //Debug.Log("Critical Hit");
            return true;
        }
        else
        {
            Debug.Log("Regular Hit");
            return false;
        }
    }
    #endregion
}
