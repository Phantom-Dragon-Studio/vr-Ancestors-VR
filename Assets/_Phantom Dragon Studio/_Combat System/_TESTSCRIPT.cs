using Zenject;
using System;
using UnityEngine;

public class _TESTSCRIPT : MonoBehaviour
{
    public CharacterClass mainCharacter;
    IExperienceCalculator _expC;

    [Inject]
    public void Construct(IExperienceCalculator expC)
    {
        _expC = expC;
    }

    // Update is called once per frame
    void Update()
    { 
        //Force HP bar to update for player
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mainCharacter.ClearStats();
            _expC.UpdateEarnedExperience(10000f, this.mainCharacter);
        }

        //Deal Standard Damage To a Target
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var temp3 = mainCharacter.gameObject.AddComponent<StaticChargedEffect>();
            temp3.durationPerStack = 5f;
            temp3.currentStackCount = 2;
            temp3.maximumStackCount = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var temp3 = mainCharacter.gameObject.AddComponent<AttributeEffect>();
            temp3.attributeEffected = AttibuteEffectBonus.Agility;
            temp3.effectAmount = 100;
            temp3.durationPerStack = 5f;
            temp3.currentStackCount = 2;
            temp3.maximumStackCount = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            var temp3 = mainCharacter.gameObject.AddComponent<BurningEffect>();
            temp3.myElementType = ElementType.Fire;
            temp3.durationPerStack = 10f;
            temp3.currentStackCount = 5;
            temp3.maximumStackCount = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            var temp2 = mainCharacter.gameObject.AddComponent<GenericSlowingEffect>();
            temp2.durationPerStack = 5f;
            temp2.currentStackCount = 2;
            temp2.maximumStackCount = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            var temp3 = mainCharacter.gameObject.AddComponent<StunnedEffect>();
            temp3.durationPerStack = 5f;
            temp3.currentStackCount = 2;
            temp3.maximumStackCount = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            var temp3 = mainCharacter.gameObject.AddComponent<FrozenSlowingEffect>();
            temp3.durationPerStack = 5f;
            temp3.currentStackCount = 2;
            temp3.maximumStackCount = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            var temp3 = mainCharacter.gameObject.AddComponent<PoisonEffect>();
            temp3.durationPerStack = 5f;
            temp3.currentStackCount = 2;
            temp3.maximumStackCount = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            var temp3 = mainCharacter.gameObject.AddComponent<TimedDeathEffect>();
            temp3.durationPerStack = 5f;
            temp3.currentStackCount = 2;
            temp3.maximumStackCount = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            var temp3 = mainCharacter.gameObject.AddComponent<SilencedEffect>();
            temp3.durationPerStack = 5f;
            temp3.currentStackCount = 2;
            temp3.maximumStackCount = 5;
        }

    }
}
