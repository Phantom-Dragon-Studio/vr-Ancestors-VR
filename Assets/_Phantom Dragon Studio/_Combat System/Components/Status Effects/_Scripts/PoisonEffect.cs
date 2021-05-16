using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoisonEffect : StatusEffectBase
{
    public float initialPoisonDamage;

    private float tickablePoisonDamage;

    protected override void ApplyEffect()
    {
        myElementType = ElementType.Earth;
        base.ApplyEffect();
        this.characterInformation.TakeMagicalDamage(ElementType.Earth, initialPoisonDamage);
    }

    protected override void TickActions()
    {
        this.characterInformation.TakeMagicalDamage(ElementType.Earth, tickablePoisonDamage);
    }
}
