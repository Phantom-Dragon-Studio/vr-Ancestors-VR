using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public enum TargetType
{
    Self,
    AoE,
    SingleTarget,
    MultiTarget
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Ability", menuName = "Phantom Dragon Studios/Scriptable Ability System/Ability", order = 2)]
public class _AbilityData : ScriptableObject
{
    public int abilityID;
    public string abilityName;
    public string abilityTooltip;
    public int abilityCost;
    public int abilityEarnedLevel;
    public TargetType abilityTargetType;
    public float abilityMinRange;
    public float abilityMaxRange;
    public float abilityCooldown;
    public float abilityDuration;
    public bool abilityRequiresTalent;
    public TalentNode abilityRequiredTalentNode;
    public int abilityCurrentLevel;
    public int abilityMaxLevel;
    public float abilityLevelMultiplier;
    public Sprite abilityThumbnail;



    public _AbilityData(int _abilityID, string _abilityName, string _abilityToolTip, int _abilityCost, int _abilityEarnedLevel, TargetType _abilityTargetType, float _abilityMinRange, float _abilityMaxRange, float _abilityCooldown,
        float _abilityDuration, bool _abilityRequiresTalent, TalentNode _abilityRequiredTalentNode, int _abilityCurrentLevel, int _abiilityMaxLevel, float _abilityLevelMultiplier, Sprite _abilityThumbnail)
    {
        abilityID = _abilityID;
        abilityName = _abilityName;
        abilityTooltip = _abilityToolTip;
        abilityCost = _abilityCost;
        abilityEarnedLevel = _abilityEarnedLevel;
        abilityTargetType = _abilityTargetType;
        abilityMaxRange = _abilityMinRange;
        abilityMaxRange = _abilityMaxRange;
        abilityCooldown = _abilityCooldown;
        abilityDuration = _abilityDuration;
        abilityRequiresTalent = _abilityRequiresTalent;
        abilityRequiredTalentNode = _abilityRequiredTalentNode;
        abilityCurrentLevel = _abilityCurrentLevel;
        abilityMaxLevel = _abiilityMaxLevel;
        abilityLevelMultiplier  = _abilityLevelMultiplier;
        abilityThumbnail = _abilityThumbnail;
    }

    public int GetCurrentLevel
    {
        get { return abilityCurrentLevel; }
    }
}




/*****************************************************************************************************************************
 * Things to add to SO:
 * 
 * 
 * 
 * 
 * 
 * 
 * *************************************************************************************************************************/