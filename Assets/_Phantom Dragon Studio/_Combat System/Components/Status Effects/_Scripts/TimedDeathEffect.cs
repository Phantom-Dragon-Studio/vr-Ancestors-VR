using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimedDeathEffect : StatusEffectBase
{

    protected override void ApplyEffect()
    {
        durationPerStack = 30f;
        myElementType = ElementType.Dark;
        base.ApplyEffect();
    }

    protected override void TickActions()
    {
        //This could be changed later to pool summoned units as well.
        Destroy(this.gameObject);
    }
}
