using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FrozenSlowingEffect : StatusEffectBase
{
    protected float initialFreezeDamage = 50;

    protected override void ApplyEffect()
    {
        myElementType = ElementType.Water;
        base.ApplyEffect();
        this.characterInformation.TakeMagicalDamage(ElementType.Water, initialFreezeDamage);

        //TODO ---- Slow movement speed, attack speed, and animation speed.
    }
}
