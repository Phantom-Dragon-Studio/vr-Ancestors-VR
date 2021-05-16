using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This effect will apply an electricalCharge to the target, when the electricalCharge count is eqivalent to the maxCVharge count
//The unity will cause a staticDischarge. Which will cause some kind of electrical explosion, causing a chain lightning.
//Note: It would be cool to see this effect to check for contact with other units, and if other unit is statically charged,
//Then combine the effects on 1 unit, or add a stack to each unit.
[System.Serializable]
public class StaticChargedEffect : StatusEffectBase
{
    public float initialShockDamage;

    protected override void ApplyEffect()
    {
        myElementType = ElementType.Lightning;
        base.ApplyEffect();
        //If currentStack == maxStack then cause staticDischarge and deal damage + play visualEffect.
    }
}
