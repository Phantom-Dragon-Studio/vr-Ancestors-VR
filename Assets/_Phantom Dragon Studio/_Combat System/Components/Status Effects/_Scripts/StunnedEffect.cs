using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StunnedEffect : StatusEffectBase
{
    public float initialStunDamage = 25;
    private CharacterController characterController;

    protected override void ApplyEffect()
    {
        myElementType = ElementType.None;
        characterController = this.GetComponent<CharacterController>();

        if (characterController == null)
        {
            DecrementStackCount(this.currentStackCount); 
        }
        else 
        {
            this.characterInformation.TakeMagicalDamage(ElementType.None, initialStunDamage);

            characterController.enabled = false;

            base.ApplyEffect();
        }
    }

    protected override void RemoveEffect()
    {
        characterController.enabled = true;
        base.RemoveEffect();
    }
}
