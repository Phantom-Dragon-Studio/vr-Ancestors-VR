using Zenject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageApplicator : MonoBehaviour
{

    private ICalculations _calculations;
    [Inject]
    public void Construct(ICalculations calculations)
    {
        _calculations = calculations;
    }

    float damageModifierIntensity, damageToDeal, modifiedDamage, chanceToCrit = 0;
    CharacterStats targetHit, owner;
    [SerializeField] private bool magicDamage;
    [SerializeField] public ElementType damagingElementType;

    void Awake()
    {
        owner = FindObjectOfType<CharacterClass>().GetComponent<CharacterStats>();
    }

    void OnCollisionEnter(Collision collision)
    {
        AccuracyModifier hitTargetModifier = collision.gameObject.GetComponent<AccuracyModifier>();
        targetHit = collision.gameObject.transform.root.GetComponent<CharacterStats>();
        if (hitTargetModifier != null && targetHit != null)
        {
            damageModifierIntensity = hitTargetModifier.MyModifier;
            damageToDeal = GetCriticalHit(GetBaseDamage());
            DealDamage(damageToDeal);
        }
        else if (hitTargetModifier == null && targetHit != null)
        {
            damageModifierIntensity = 1;
            damageToDeal = GetCriticalHit(GetBaseDamage());
            if (targetHit != null)
            {
                DealDamage(damageToDeal);
            }
            //if IsStatic, PlaySound & Check if it's destructible (special interactable objects), 
            //Play a visual effect on the surface.
            //Play some sound depending on what we hit.
        }
        else
        {
            //Play some sound depending on what we hit.
            //Debug.Log("Nothing was hit that we can damage.");
        }
    }

    void OnCollisionExit(Collision collision)
    {

    }

    public float GetBaseDamage()
    {
        return owner.characterData.baseDamage * damageModifierIntensity;
    }

    public float GetCriticalHit(float calculatedDamage)
    {
        chanceToCrit = owner.characterData.criticalStrikeChance;
        bool wasCriticalHit = _calculations.DecideIfCriticalHit(chanceToCrit);
        if (wasCriticalHit)
        {
            modifiedDamage = calculatedDamage * damageModifierIntensity;
            return modifiedDamage;
        }
        else
        {
            return calculatedDamage;
        }
    }
    public void DealDamage(float passedInDamageValue)
    {
        if(magicDamage == true)
        {
            targetHit.TakeMagicalDamage(this.damagingElementType, targetHit.AttemptToDodgeIncomingAttack(passedInDamageValue));
        }
        else
        {
            targetHit.TakePhysicalDamage(targetHit.AttemptToDodgeIncomingAttack(passedInDamageValue));
        }
    }
}

