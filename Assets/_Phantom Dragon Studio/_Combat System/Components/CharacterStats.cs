using Zenject;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterStats : MonoBehaviour
{
    public _WorldObjectData characterData;

    public Action<float, float> PlayerHealthValueChanged = delegate { };
    public Action<float, float> PlayerM_E_FValueChanged = delegate { };
    public Action<float, float> PlayerStaminaValueChanged = delegate { };

    private IGameManager _gameManager;
    private ICalculations _calculations;
    private StatsCollector _playerStatsCollector;

    [Inject]
    public void Construct(IGameManager gameManager, ICalculations calculations, StatsCollector playerStatsCollector)
    {
        _gameManager = gameManager;
        _calculations = calculations;
        _playerStatsCollector = playerStatsCollector;
    }

    public float AttemptToDodgeIncomingAttack(float damage)
    {
        if (this.characterData.isPlayer == true)
        {
            characterData.dodgeChance /= _gameManager.GameSettings.difficultyModifier;
        }

        bool attackDodgeSuccessful = _calculations.DecideIfAttackIsDodged(characterData.dodgeChance);

        if (attackDodgeSuccessful)
        {
            damage = 0;
            return damage;
        }
        else return damage;
    }

    public void TakePhysicalDamage(float damage)
    {
        this.characterData.currentHealth -= _calculations.NPCDealStandardDamageMinusDefense
                                                (this.characterData, 5, damage);
        if (this.characterData.currentHealth <= 0)
        {
            //Play Death Animation.
            //Add it to an object pool to be used by the Spawning function and reset the values to default.
        }
        //Update Display
        if (PlayerHealthValueChanged != null)
        {
            PlayerHealthValueChanged(this.characterData.currentHealth, this.characterData.maximumHealth);
        }
    }

    public void TakeMagicalDamage(ElementType elementType, float damage)
    {
        this.characterData.currentHealth -= _calculations.PlayerDealMagicDamageMinusResistance
                                                (this.characterData, 5, damage, elementType);


        if (this.characterData.currentHealth <= 0)
        {
            //TODO Play Death Animation.
            //TODO Add it to an object pool to be used by the Spawning function and reset the values to default.
        }

        //Update Display
        if (PlayerHealthValueChanged != null)
        {
            PlayerHealthValueChanged(this.characterData.currentHealth, this.characterData.maximumHealth);
        }
    }

    public void RestoreHealth(float healthToRestore)
    {
        this.characterData.currentHealth += healthToRestore;
        if (this.characterData.currentHealth > this.characterData.maximumHealth)
        {
            this.characterData.currentHealth = this.characterData.maximumHealth;
        }

        //Update Display
        if (PlayerHealthValueChanged != null)
        {
            PlayerHealthValueChanged(this.characterData.currentHealth, this.characterData.maximumHealth);
        }
    }

    public void RestoreM_E_F(float M_E_FToRestore)
    {
        this.characterData.currentM_E_F += M_E_FToRestore;
        if (this.characterData.currentM_E_F > this.characterData.maximumM_E_F)
        {
            this.characterData.currentM_E_F = this.characterData.maximumM_E_F;
        }

        //Update Display
        if (PlayerM_E_FValueChanged != null)
        {
            PlayerM_E_FValueChanged(this.characterData.currentM_E_F, this.characterData.maximumM_E_F);
        }
    }

    public void RestoreStamina(float staminaToRestore)
    {
        this.characterData.currentStamina += staminaToRestore;
        if (this.characterData.currentStamina > this.characterData.maximumStamina)
        {
            this.characterData.currentStamina = this.characterData.maximumStamina;
        }
        if (PlayerStaminaValueChanged != null)
        {
            PlayerStaminaValueChanged(this.characterData.currentStamina, this.characterData.maximumStamina);
        }
    }
}