using Zenject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions: IConditions, IInitializable {

    private IGameManager _gameManager;
    [Inject]
    public void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Initialize()
    {
        InitializeElementalAttunmentTable();
    }

    private List<ElementType> fireWeaknesses = new List<ElementType>();
    private List<ElementType> earthWeaknesses = new List<ElementType>();
    private List<ElementType> windWeaknesses = new List<ElementType>();
    private List<ElementType> divineWeaknesses = new List<ElementType>();
    private List<ElementType> darkWeaknesses = new List<ElementType>();
    private List<ElementType> arcaneWeaknesses = new List<ElementType>();
    private List<ElementType> lightningWeaknesses = new List<ElementType>();
    private List<ElementType> waterWeaknesses = new List<ElementType>();

    private List<ElementType> fireStrengths = new List<ElementType>();
    private List<ElementType> earthStrengths = new List<ElementType>();
    private List<ElementType> windStrengths = new List<ElementType>();
    private List<ElementType> divineStrengths = new List<ElementType>();
    private List<ElementType> darkStrengths = new List<ElementType>();
    private List<ElementType> arcaneStrengths = new List<ElementType>();
    private List<ElementType> lightningStrengths = new List<ElementType>();
    private List<ElementType> waterStrengths = new List<ElementType>();

    public void InitializeElementalAttunmentTable()
    {
        //Weaknesses
        fireWeaknesses.Add(ElementType.Water);
        fireWeaknesses.Add(ElementType.Wind);

        earthWeaknesses.Add(ElementType.Fire);
        earthWeaknesses.Add(ElementType.Divine);

        windWeaknesses.Add(ElementType.Water);
        windWeaknesses.Add(ElementType.Arcane);

        divineWeaknesses.Add(ElementType.Earth);
        divineWeaknesses.Add(ElementType.Fire);

        darkWeaknesses.Add(ElementType.Divine);
        darkWeaknesses.Add(ElementType.Lightning);

        arcaneWeaknesses.Add(ElementType.Dark);

        lightningWeaknesses.Add(ElementType.Arcane);
        lightningWeaknesses.Add(ElementType.Divine);

        waterWeaknesses.Add(ElementType.Lightning);
        waterWeaknesses.Add(ElementType.Dark);

        //Strengths
        fireStrengths.Add(ElementType.Divine);
        fireStrengths.Add(ElementType.Earth);

        earthStrengths.Add(ElementType.Wind);
        earthStrengths.Add(ElementType.Lightning);

        windStrengths.Add(ElementType.Fire);
        windStrengths.Add(ElementType.Divine);

        divineStrengths.Add(ElementType.Dark);
        divineStrengths.Add(ElementType.Lightning);

        darkStrengths.Add(ElementType.Arcane);
        darkStrengths.Add(ElementType.Water);

        arcaneStrengths.Add(ElementType.Lightning);
        arcaneStrengths.Add(ElementType.Wind);


        lightningStrengths.Add(ElementType.Water);
        lightningStrengths.Add(ElementType.Dark);

        waterStrengths.Add(ElementType.Fire);
        waterStrengths.Add(ElementType.Wind);

    }


    #region TARGETING CHECKS
    //Target Is Attackable?
    public bool TargetIsAttackable(_WorldObjectData targetInformation)
    {
        if (targetInformation.armourType != ArmourType.Invulnerable)
        {
            Debug.Log("Conditions: Target is attackable");
            return true;
        }
        else if (targetInformation.armourType == ArmourType.Invulnerable)
        {
            Debug.Log("Conditions: Target is not invulnerable.");
            return false;
        }
        Debug.Log("Conditions: TargetIsAttackable says target isa not attackable, but is not invulnerable either...");
        return false;
    }

    //Target Is Still Alive?
    public bool TargetIsAlive(_WorldObjectData targetInformation)
    {
        if (targetInformation.isAlive != true)
        {
            Debug.Log("Conditions: Target alive.");
            return true;
        }
        else if (targetInformation.isAlive == false)
        {
            Debug.Log("Conditions: Target not alive.");
            return false;
        }
        Debug.Log("Conditions: TargetIsAlive could not determine if target was alive or dead.");
        return false;
    }

    //Within Range?
    public bool TargetWithinBaseAttackRange(_WorldObjectData attackerInformation, _WorldObjectData targetInformation)
    {
        float distanceToTarget = Vector3.Distance(attackerInformation.objectWorldLocation, targetInformation.objectWorldLocation);
        if (attackerInformation.baseAttackRange >= distanceToTarget)
        {
            Debug.Log("Conditions: Target within attack range");
            return true;
        }
        Debug.Log("Conditions: Target is not wihthin attack range");
        return false;
    }
    #endregion

    #region ELEMENTAL RESISTANCE CHECKS
    public float ElementalStrengthAndWeaknessComparison(ElementType elementDamageType, _WorldObjectData targetedCharacter)
    {
        //Debug.Log("Conditions - Checking Elemental Resistance of target" + targetedCharacter.objectName);
        switch (elementDamageType)
        {
            case ElementType.Fire:
                {
                    return targetedCharacter.fireResistance;
                }
            case ElementType.Earth:
                {
                    return targetedCharacter.earthResistance;
                }
            case ElementType.Wind:
                {
                    return targetedCharacter.windResistance;
                }
            case ElementType.Divine:
                {
                    return targetedCharacter.divineResistance;
                }
            case ElementType.Dark:
                {
                    return targetedCharacter.darkResistance;
                }
            case ElementType.Arcane:
                {
                    return targetedCharacter.arcaneResistance;
                }
            case ElementType.Lightning:
                {
                    return targetedCharacter.lightningResistance;
                }
            case ElementType.Water:
                {
                    return targetedCharacter.waterResistance;
                }
            default:
                {
                    Debug.LogWarning("No Elemental Resistance Found For Assigned Damage Type");
                    break;
                }
        }
        Debug.LogError("Elemental Comparison Failed!");
        return -1;
    }

    public float ElementalAttunmentAdjustment(ElementType elementDamageType, _WorldObjectData targetedCharacter)
    {
        switch (elementDamageType)
        {
            case ElementType.Fire:
                {
                    if (fireWeaknesses.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentDamageReductionRatio;
                    }
                    else if (fireStrengths.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentStrengthToDamageRatio;
                    }
                    else return 1;
                }
            case ElementType.Earth:
                {
                    if (earthWeaknesses.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentDamageReductionRatio;
                    }
                    else if (earthStrengths.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentStrengthToDamageRatio;
                    }
                    else return 1;
                }
            case ElementType.Wind:
                {
                    if (windWeaknesses.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentDamageReductionRatio;
                    }
                    else if (windStrengths.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentStrengthToDamageRatio;
                    }
                    else return 1;
                }
            case ElementType.Divine:
                {
                    if (divineWeaknesses.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentDamageReductionRatio;
                    }
                    else if (divineStrengths.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentStrengthToDamageRatio;
                    }
                    else return 1;
                }
            case ElementType.Dark:
                {
                    if (darkWeaknesses.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentDamageReductionRatio;
                    }
                    else if (darkStrengths.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentStrengthToDamageRatio;
                    }
                    else return 1;
                }
            case ElementType.Arcane:
                {
                    if (arcaneWeaknesses.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentDamageReductionRatio;
                    }
                    else if (arcaneStrengths.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentStrengthToDamageRatio;
                    }
                    else return 1;
                }
            case ElementType.Lightning:
                {
                    if (lightningWeaknesses.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentDamageReductionRatio;
                    }
                    else if (lightningStrengths.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentStrengthToDamageRatio;
                    }
                    else return -1;
                }
            case ElementType.Water:
                {
                    if (waterWeaknesses.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentDamageReductionRatio;
                    }
                    else if (waterStrengths.Contains(targetedCharacter.elementalAttunmentType))
                    {
                        return _gameManager.GameSettings.elementalAttunmentStrengthToDamageRatio;
                    }
                    else return 1;
                }
        }

        Debug.LogWarning("No Elemental Attunment Found!");
        return 1;

    }
    #endregion
}
