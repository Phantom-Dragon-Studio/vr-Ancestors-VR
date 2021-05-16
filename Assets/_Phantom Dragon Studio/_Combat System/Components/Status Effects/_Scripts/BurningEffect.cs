using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BurningEffect : StatusEffectBase
{
    public float initialBurnDamage = 50;

    private float tickableBurnDamage = 10;

    protected override void ApplyEffect()
    {
        myElementType = ElementType.Fire;
        base.ApplyEffect();
        this.characterInformation.TakeMagicalDamage(ElementType.Fire, initialBurnDamage);
    }

    protected override void TickActions()
    {
        base.TickActions();
        this.characterInformation.TakeMagicalDamage(ElementType.Fire, tickableBurnDamage);
    }
}
