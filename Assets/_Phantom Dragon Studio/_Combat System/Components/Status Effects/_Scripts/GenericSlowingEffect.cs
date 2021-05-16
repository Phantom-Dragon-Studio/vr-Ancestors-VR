using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenericSlowingEffect : StatusEffectBase
{
    float slowingRate = 0.5f;

    protected override void ApplyEffect()
    {
        this.myElementType = ElementType.Wind;
        base.ApplyEffect();
    }

    //TODO ---- Slow movement speed, attack speed, and animation speed.
}
