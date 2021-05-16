using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy = 0,       //For Noobs
    Medium = 1,     //Average Players
    Hard = 2,       //Default Difficulty
    Insane = 3,     //Real Gamers
    IWHBYD = 4      //People with a death wish - Unlocks hidden dialogue.
}

/// <summary>
/// Difficulty effects many factors, Below will be a collection and their access points for the number of effects 
/// difficulty has on the game.
/// 
/// Experience Earned - Bonus Experience for harder difficulties, 
///                     but harder difficulties require more experience to level up - Game Settings
/// Player Stats - _WorldObjectData
/// Number of enemies - Precedural Spawning System
/// Special Abilities of some enemies (Especially bosses) - Unknown Location
/// What cutscenes and dialog gets displayed. - Unknown Locaton
/// How much reputation factions take to max out - Faction Manager?
/// Item Cost - _ItemInformation
/// ##########################################################################################



[System.Serializable]
[CreateAssetMenu(fileName = "New Game Settings Configuration", menuName = "Phantom Dragon Studios/Game Settings", order = 0)]
public class GameSettings : ScriptableObject
{
    [Header("Difficulty & Leveling Factors")]
    [HideInInspector]
    public float levelModifier;                             //Set by difficulty selected by the player.
    [HideInInspector]
    public float difficultyModifier;                        //Set by difficulty selected by the player.
    public Difficulty difficulty;                           //Difficulties to select from.
    public float baseExperiencePerLevel;                    //Adjust here for balancing.
    [Header("Random Chance Factors")]
    public float dodgeChanceToAttributeRatio;
    public float criticalChanceToAttributeRatio;
    [Header("Mana, Energy, Focus")]
    public float M_E_FToAttributeRatio;
    public float M_E_FRegenerationToAttributeRatio;
    public float M_E_FRegenerationInterval;
    [Header("Health")]
    public float healthToAttributeRatio;
    public float healthRegenerationToAttributeRatio;
    public float healthRegenerationInterval;
    [Header("Stamina")]
    public float staminaToAttributeRatio;
    public float staminaRegenerationToAttributeRatio;
    public float staminaRegenerationInterval;
    [Header("Out-of-Combat Factors")]
    public float outOfCombatRegenerationMultiplier;
    [Header("Damage Modifers")]
    public float defenseToAttributeRatio;
    public float damageToAttributeRatio;
    public float defenseReductionRatio;
    public float movementSpeedToAgilityRatio;
    public float attackSpeedToAgilityRatio;
    public float elementalAttunmentStrengthToDamageRatio;


    [HideInInspector]
    public float elementalAttunmentDamageReductionRatio;


    public void GameSetup()
    {
        switch (difficulty)
        {
            case (Difficulty.Easy):
                {
                    levelModifier = 1.5f;
                    difficultyModifier = 1.25f;
                    elementalAttunmentDamageReductionRatio = 1;
                    break;
                }
            case (Difficulty.Medium):
                {
                    levelModifier = 2f;
                    difficultyModifier = 1f;
                    elementalAttunmentDamageReductionRatio = 0.9f;
                    break;
                }
            case (Difficulty.Hard):
                {
                    levelModifier = 3f;
                    difficultyModifier = 0.75f;
                    elementalAttunmentDamageReductionRatio = 0.8f;
                    break;
                }
            case (Difficulty.Insane):
                {
                    levelModifier = 3.5f;
                    difficultyModifier = 0.50f;
                    elementalAttunmentDamageReductionRatio = 0.7f;
                    break;
                }
            case (Difficulty.IWHBYD):
                {
                    levelModifier = 4f;
                    difficultyModifier = 0.25f;
                    elementalAttunmentDamageReductionRatio = 0.5f;
                    break;
                }
        }

    }
}
