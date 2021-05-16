using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Different Equipment Slot Types
public enum ItemSlotType
{
    HEAD = 0,
    NECK = 1,
    SHOULDERS = 2,
    MAINHANDOFFENSE = 3,
    TORSO = 4,
    OFFHANDOFFENSE = 5,
    MAINHANDPROTECT = 6,
    OFFHANDPROTECT = 7,
    BELT = 8,
    LEGGINGS = 9,
    BOOTS = 10,
    TRINKET = 11,
    INVENTORY = 12,
    QUICKBAR = 13,
    VAULT = 14
}

public enum ItemCategory
{
    ALL,
    WEAPONS,
    ARMOR,
    CONSUMABLES,
    QUESTITEMS,
    REAGENTS
}

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Phantom Dragon Studios/Modular Inventory System/Item", order = 2)]
public class ItemBaseAtributes : ScriptableObject
{
    [Header("Item Information")]
    [SerializeField] private ItemSlotType equippableItemSlot;
    [SerializeField] private ItemCategory itemCategory;
    [SerializeField] private ItemRarity itemRarity;
    [SerializeField] private Sprite itemIcon = null;
    [SerializeField] private string itemID = "I0000";
    [SerializeField] private string itemName = "";
    [SerializeField] private string itemDescription = "";
    [SerializeField] private int itemValue = 0;
    [SerializeField] private GameObject gameModel = null;
    [Header("Primary Stats")]
    [SerializeField] private int strengthBonus = 0;
    [SerializeField] private int agilityBonus = 0;
    [SerializeField] private int intellectBonus = 0;
    [SerializeField] private int enduranceBonus = 0;
    [Header("Seconadry Stats")]
    [SerializeField] private float maximumHealthBonus = 0;
    [SerializeField] private float healthRegenerationBonus = 0;
    [SerializeField] private float maximumM_E_FBonus = 0;
    [SerializeField] private float M_E_FRegenerationBonus = 0;
    [SerializeField] private float maximumStaminaBonus = 0;
    [SerializeField] private float staminaRegenerationBonus = 0;
    [SerializeField] private float weaponAttackRange = 0;
    [SerializeField] private float movementSpeedBonus = 0;
    [SerializeField] private float damageBonus = 0;
    [SerializeField] private float criticalChancebonus = 0;
    [SerializeField] private float dodgeChanceBonus = 0;
    [SerializeField] private float defenseBonus = 0;
    [Header("Elemental Resistance")]
    [SerializeField] public float elementalResistance = 0;
    [SerializeField] public float fireResistance = 0;
    [SerializeField] public float earthResistance = 0;
    [SerializeField] public float windResistance = 0;
    [SerializeField] public float divineResistance = 0;
    [SerializeField] public float darkResistance = 0;
    [SerializeField] public float arcaneResistance = 0;
    [SerializeField] public float lightningResistance = 0;
    [SerializeField] public float waterResistance = 0;

    [SerializeField] public bool isUnique = false;
    [SerializeField] public bool isTwoHanded = false;

    public ItemSlotType EQUIPPABLEITEMSLOT
    {

        get { return this.equippableItemSlot; }
        set { this.equippableItemSlot = value; }
    }

    public ItemCategory ITEMCATEGORY
    {

        get { return this.itemCategory; }
        set { this.itemCategory = value; }
    }

    public ItemRarity ITEMRARITY
    {

        get { return this.itemRarity; }
        set { this.itemRarity = value; }
    }

    public string ITEMNAME
    {

        get { return this.itemName; }
        set { this.itemName = value; }
    }

    public string ITEMDESCRIPTION
    {

        get { return this.itemDescription; }
        set { this.itemDescription = value; }
    }

    public Sprite ITEMICON
    {

        get { return this.itemIcon; }
        set { this.itemIcon = value; }
    }

    public GameObject ITEMPREFAB
    {

        get { return this.gameModel; }
        set { this.gameModel = value; }
    }

    public int ITEMVALUE
    {

        get { return this.itemValue; }
        set { this.itemValue = value; }
    }

    public string ITEMID
    {

        get { return this.itemID; }
        set { this.itemID = value; }
    }

    public float ATTACKRANGE
    {

        get { return this.weaponAttackRange; }
        set { this.weaponAttackRange = value; }
    }

    public int STRENGTH
    {

        get { return this.strengthBonus; }
        set { this.strengthBonus = value; }
    }

    public int AGILITY
    {

        get { return this.agilityBonus; }
        set { this.agilityBonus = value; }
    }

    public int INTELLECT
    {

        get { return this.intellectBonus; }
        set { this.intellectBonus = value; }
    }

    public int ENDURANCE
    {

        get { return this.enduranceBonus; }
        set { this.enduranceBonus = value; }
    }

    public float MAXHEALTH
    {

        get { return this.maximumHealthBonus; }
        set { this.maximumHealthBonus = value; }
    }

    public float HEALTHREGEN
    {

        get { return this.healthRegenerationBonus; }
        set { this.healthRegenerationBonus = value; }
    }

    public float MAX_M_E_F
    {

        get { return this.maximumM_E_FBonus; }
        set { this.maximumM_E_FBonus = value; }
    }

    public float M_E_FREGEN
    {

        get { return this.M_E_FRegenerationBonus; }
        set { this.M_E_FRegenerationBonus = value; }
    }

    public float STAMINA
    {

        get { return this.maximumStaminaBonus; }
        set { this.maximumStaminaBonus = value; }
    }

    public float STAMINAREGEN
    {

        get { return this.staminaRegenerationBonus; }
        set { this.staminaRegenerationBonus = value; }
    }

    public float MOVEMENTSPEED
    {

        get { return this.movementSpeedBonus; }
        set { this.movementSpeedBonus = value; }
    }

    public float BASEDAMAGE
    {

        get { return this.damageBonus; }
        set { this.damageBonus = value; }
    }

    public float CRITICALCHANCE
    {

        get { return this.criticalChancebonus; }
        set { this.criticalChancebonus = value; }
    }

    public float DODGECHANCE
    {

        get { return this.dodgeChanceBonus; }
        set { this.dodgeChanceBonus = value; }
    }

    public float BASEDEFENSE
    {

        get { return this.defenseBonus; }
        set { this.defenseBonus = value; }
    }
}
