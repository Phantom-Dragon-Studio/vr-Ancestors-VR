using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueClass : CharacterClass
{
    protected override void ApplySecondaryGearBonuses()
    {
        _characterStats.characterData.baseAttackSpeed += _characterStats.characterData.baseAgility
            * _gameManager.GameSettings.attackSpeedToAgilityRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.baseMovementSpeed += _characterStats.characterData.baseAgility
            * _gameManager.GameSettings.movementSpeedToAgilityRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.criticalStrikeChance += (_characterStats.characterData.baseAgility + _characterStats.characterData.baseStrength / 2)
             * _gameManager.GameSettings.criticalChanceToAttributeRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.dodgeChance += _characterStats.characterData.baseAgility / 0.5f
            * _gameManager.GameSettings.dodgeChanceToAttributeRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.baseDamage += _characterStats.characterData.baseStrength + _characterStats.characterData.baseAgility
            * 0.75f * _gameManager.GameSettings.damageToAttributeRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.baseDefense += _characterStats.characterData.baseEndurance
            * _gameManager.GameSettings.defenseToAttributeRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.healthRegenerationAmount += _characterStats.characterData.baseStrength / 4
            * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.maximumHealth += (_characterStats.characterData.baseEndurance * 4)
            * _gameManager.GameSettings.healthToAttributeRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.M_E_FRegenerationAmount += _characterStats.characterData.baseAgility / 4
            * _gameManager.GameSettings.M_E_FRegenerationToAttributeRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.maximumM_E_F += _characterStats.characterData.baseAgility
            * _gameManager.GameSettings.M_E_FToAttributeRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.staminaRegenerationAmount += _characterStats.characterData.baseAgility
            * _gameManager.GameSettings.staminaRegenerationToAttributeRatio
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.maximumStamina += _characterStats.characterData.baseAgility
            * _gameManager.GameSettings.staminaToAttributeRatio
                * _gameManager.GameSettings.difficultyModifier;

        base.ApplySecondaryGearBonuses();
    }


}
