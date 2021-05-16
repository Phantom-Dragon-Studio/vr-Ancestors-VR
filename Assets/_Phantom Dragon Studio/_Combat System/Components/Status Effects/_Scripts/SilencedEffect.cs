using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SilencedEffect : StatusEffectBase
{

    protected override void ApplyEffect()
    {
        myElementType = ElementType.Arcane;
        base.ApplyEffect();
        this.characterInformation.characterData.isSilenced = true;
    }


    protected override void RemoveEffect()
    {
        base.RemoveEffect();
        this.characterInformation.characterData.isSilenced = false;
    }
}
