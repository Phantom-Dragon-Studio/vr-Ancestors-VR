using Zenject;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public abstract class CharacterClass : MonoBehaviour
{
    protected CharacterStats _characterStats;
    protected StatsCollector _statsWindowCollector;
    protected IGameManager _gameManager;

    protected AttributeEffect[] attributeBuffs;

    [Header("Experience")]
    [Tooltip("The character's level is automatically determined by the amount of totalExperience")]
    [SerializeField] public int characterLevel = 1;
    [Tooltip("This is how much EXP the character currently has. If higher than 0 the unit will spawn with a higher level.")]
    [SerializeField] public float totalExperience;

    [Inject]
    public void Construct(IGameManager gameManager, StatsCollector statsWindowCollector)
    {
        _gameManager = gameManager;
        _statsWindowCollector = statsWindowCollector;
    }

    public virtual void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
    }

    public virtual void ClearStats()
    {
        _characterStats.characterData.baseStrength = 0;
        _characterStats.characterData.baseAgility = 0;
        _characterStats.characterData.baseIntelligence = 0;
        _characterStats.characterData.baseEndurance = 0;

        _characterStats.characterData.baseAttackSpeed = 0;
        _characterStats.characterData.baseMovementSpeed = 0;
        _characterStats.characterData.criticalStrikeChance = 0;
        _characterStats.characterData.dodgeChance = 0;
        _characterStats.characterData.baseDamage = 0;
        _characterStats.characterData.baseDefense = 0;
        _characterStats.characterData.healthRegenerationAmount = 0;
        _characterStats.characterData.maximumHealth = 1;
        _characterStats.characterData.M_E_FRegenerationAmount = 0;
        _characterStats.characterData.maximumM_E_F = 1;
        _characterStats.characterData.staminaRegenerationAmount = 0;
        _characterStats.characterData.maximumStamina = 1;

        _characterStats.characterData.elementalResistance = 0;
        _characterStats.characterData.fireResistance = 0;
        _characterStats.characterData.earthResistance = 0;
        _characterStats.characterData.windResistance = 0;
        _characterStats.characterData.divineResistance = 0;
        _characterStats.characterData.darkResistance = 0;
        _characterStats.characterData.arcaneResistance = 0;
        _characterStats.characterData.lightningResistance = 0;
        _characterStats.characterData.waterResistance = 0;

    }

    public virtual void CalculatePrimaryAttributes()
    {
        Debug.Log("Calculating Primary");
        _statsWindowCollector.AskForInformation(_statsWindowCollector.MyCollectedInformation);

        //Apply base level Primary Attribute bonuses and apply Gear Primary Stat Bonuses.
        _characterStats.characterData.baseStrength += characterLevel 
            + _statsWindowCollector.MyCollectedInformation.STRENGTH;

        _characterStats.characterData.baseAgility += characterLevel
        + _statsWindowCollector.MyCollectedInformation.AGILITY;

        _characterStats.characterData.baseIntelligence += characterLevel
        + _statsWindowCollector.MyCollectedInformation.INTELLECT;

        _characterStats.characterData.baseEndurance += characterLevel
        + _statsWindowCollector.MyCollectedInformation.ENDURANCE;

        ApplySecondaryGearBonuses();
    }

    protected virtual void ApplySecondaryGearBonuses()
    {
        //Apply Gear Secondary Stat Bonuses.
        _characterStats.characterData.maximumHealth += _statsWindowCollector.MyCollectedInformation.MAXHEALTH;
        _characterStats.characterData.healthRegenerationAmount += _statsWindowCollector.MyCollectedInformation.HEALTHREGEN;
        _characterStats.characterData.maximumM_E_F += _statsWindowCollector.MyCollectedInformation.MAX_M_E_F;
        _characterStats.characterData.M_E_FRegenerationAmount += _statsWindowCollector.MyCollectedInformation.M_E_FREGEN;
        _characterStats.characterData.maximumStamina += _statsWindowCollector.MyCollectedInformation.STAMINA;
        _characterStats.characterData.staminaRegenerationAmount += _statsWindowCollector.MyCollectedInformation.STAMINAREGEN;
        _characterStats.characterData.baseAttackRange = _statsWindowCollector.MyCollectedInformation.ATTACKRANGE;
        _characterStats.characterData.baseMovementSpeed += _statsWindowCollector.MyCollectedInformation.MOVEMENTSPEED;
        _characterStats.characterData.baseDamage += _statsWindowCollector.MyCollectedInformation.BASEDAMAGE;
        _characterStats.characterData.criticalStrikeChance += _statsWindowCollector.MyCollectedInformation.CRITICALCHANCE;
        _characterStats.characterData.dodgeChance += _statsWindowCollector.MyCollectedInformation.DODGECHANCE;
        _characterStats.characterData.baseDefense += _statsWindowCollector.MyCollectedInformation.BASEDEFENSE;

        CalculateElementalResistances();
    }

    protected virtual void CalculateElementalResistances()
    {
        Debug.Log("Calculating Secondary");

        //ELEMENT RESISTANCE CALCULATIONS
        _characterStats.characterData.elementalResistance  += _statsWindowCollector.MyCollectedInformation.elementalResistance
            + _statsWindowCollector.MyCollectedInformation.elementalResistance
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.fireResistance += _characterStats.characterData.elementalResistance
            + _statsWindowCollector.MyCollectedInformation.fireResistance
                * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.earthResistance += _characterStats.characterData.elementalResistance
            + _statsWindowCollector.MyCollectedInformation.earthResistance
            * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.windResistance += _characterStats.characterData.elementalResistance
            + _statsWindowCollector.MyCollectedInformation.windResistance
            * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.divineResistance += _characterStats.characterData.elementalResistance
            + _statsWindowCollector.MyCollectedInformation.divineResistance
            * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.darkResistance += _characterStats.characterData.elementalResistance
            + _statsWindowCollector.MyCollectedInformation.darkResistance
            * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.arcaneResistance += _characterStats.characterData.elementalResistance
            + _statsWindowCollector.MyCollectedInformation.arcaneResistance
            * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.lightningResistance += _characterStats.characterData.elementalResistance
            + _statsWindowCollector.MyCollectedInformation.lightningResistance
            * _gameManager.GameSettings.difficultyModifier;

        _characterStats.characterData.waterResistance += _characterStats.characterData.elementalResistance
            + _statsWindowCollector.MyCollectedInformation.waterResistance
            * _gameManager.GameSettings.difficultyModifier;

        //Call a function that sends an event to the Character Stats panel that has ALL Bonuses attached.
        SendOutFinalPlayerInformation();
    }

    public void SendOutFinalPlayerInformation()
    {
        //OnPassPlayerCharacterStats();
    }
}
