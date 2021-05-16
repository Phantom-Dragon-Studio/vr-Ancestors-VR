using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New WorldObject Information", menuName = "Phantom Dragon Studios/VRCombatSystem/WorldObject Information", order = 3)]
public class _WorldObjectData : ScriptableObject
{
    [Header("Object World Info")]
    [Tooltip("Name of the object")]
    [SerializeField] public string objectName;
    [Tooltip("Summary of the object, like 'Sturdy Wall' or 'Basic Melee Fighter' ")]
    [SerializeField] public string objectDescription;
    [Tooltip("Position in the World.")]
    [SerializeField] public Vector3 objectWorldLocation;

    [Header("Dropdown Selectables")]
    [Tooltip("Factions effect who the character gets along with. Faction alliances can change during gameplay.")]
    [SerializeField] public Faction faction;
    [Tooltip("DamageType is decided by the player's current weapon type, but will be pre-defined for NPC's")]
    [SerializeField] public DamageType damageType;
    [Tooltip("ArmorType will be decided by the majority of the armour worn by the player. Pre-Defined for NPC's")]
    [SerializeField] public ArmourType armourType;
    [Tooltip("ElementalAttunmentType will determine the imbeded resistances. If Attunment Type is Water, " +
        "you'll do less damage to Light, but more damage to fire, while also taking more damage from lightning.")]
    [SerializeField] public ElementType elementalAttunmentType;

    [Header("Associated Art Work to use")]
    [Tooltip("Image to use for specific unit's health display")]
    [SerializeField] public Sprite healthBar;
    [Tooltip("Image to use for specific unit's MEF display")]
    [SerializeField] public Sprite M_E_FBar;
    [Tooltip("Image to use for specific unit's image display")]
    [SerializeField] public Sprite portrait;
    [Tooltip("The 3D model used to render the character")]
    [SerializeField] public GameObject characterModel;

    [Header("Primary & Seconadry Attributes - DETERMINED BY PRIMARY STATS")]
    [Tooltip("How much health does the character have? - DETERMINED BY STATS")]
    [SerializeField] public float maximumHealth;
    [Tooltip("Current health will be calculated by the stats and automatically regenerate over time until full. - DETERMINED BY STATS")]
    [SerializeField] public float currentHealth;
    [Tooltip("How much health is healed per tick? - DETERMINED BY STATS")]
    [SerializeField] public float healthRegenerationAmount;
    [Tooltip("How much MEF does the character have? - DETERMINED BY STATS")]
    [SerializeField] public float maximumM_E_F;
    [SerializeField] public float currentM_E_F;
    [SerializeField] public float M_E_FRegenerationAmount;
    [SerializeField] public float maximumStamina;
    [SerializeField] public float currentStamina;
    [SerializeField] public float staminaRegenerationAmount;
    [SerializeField] public float baseAttackRange;
    [SerializeField] public float baseMovementSpeed;
    [SerializeField] public float baseAttackSpeed;
    [SerializeField] public float baseDamage;
    [SerializeField] public float criticalStrikeChance;
    [SerializeField] public float dodgeChance;
    [SerializeField] public float baseDefense;
    [SerializeField] public int baseStrength;
    [SerializeField] public int baseAgility;
    [SerializeField] public int baseIntelligence;
    [SerializeField] public int baseEndurance;

    [Header("Elemental Resistance")]
    [SerializeField] public float elementalResistance;
    [SerializeField] public float fireResistance;
    [SerializeField] public float earthResistance;
    [SerializeField] public float windResistance;
    [SerializeField] public float divineResistance;
    [SerializeField] public float darkResistance;
    [SerializeField] public float arcaneResistance;
    [SerializeField] public float lightningResistance;
    [SerializeField] public float waterResistance;

    [Header("Global References")]
    [Tooltip("Is the object stationary, or can it move?")]
    [SerializeField] public bool objectIsStatic;
    [Tooltip("Is the character currently alive?")]
    [SerializeField] public bool isAlive;
    [Tooltip("Is this character currently in combat?")]
    [SerializeField] public bool isInCombat;
    [Tooltip("Does this unit's attacks automatically ignore the targets defence?")]
    [SerializeField] public bool ignoreDefense;
    [Tooltip("Is this unit a player, or NPC?")]
    [SerializeField] public bool isPlayer;
    [SerializeField] public bool isSilenced;

    [Header("Sounds")]
    [Tooltip("Sound clips played when the character moves")]
    [SerializeField] public AudioClip[] footstepSound; //Generic walking sound.
    [Tooltip("Sound clips played when the character takes damage")]
    [SerializeField] public AudioClip[] oofSound; //The sound when they get hit hard or jump or fall.
    [Tooltip("Sounds clips played when the character is given commands.")]
    [SerializeField] public AudioClip[] attackSounds; //Array of different sounds to play when they attack or use abilities.

}

public enum Faction
    {
        Neutral,
        RoyalGuardians,
        KanatiTribe,
        ElvenDisciples,
        DragonMawDestroyers,
        NordicChampions,
        UnknownAnomalies
    }

    public enum DamageType
    {
        None,
        Normal,
        Piercing,
        Heavy,
        Siege,
        Magical,
        Leader,
        Demonic
    }

    public enum ArmourType
    {
        None,
        Cloth,
        Leather,
        Chainmail,
        Plate,
        Wood,
        Stone,
        Invulnerable
    }

    public enum ElementType
    {
        None,
        Earth,
        Fire,
        Wind,
        Water,
        Lightning,
        Arcane,
        Divine,
        Dark
    }

