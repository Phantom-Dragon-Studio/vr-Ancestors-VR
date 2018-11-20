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
    [SerializeField] private ItemSlotType equippableItemSlot;
    [SerializeField] private ItemCategory itemCategory;
    [SerializeField] private ItemRarity itemRarity;

    [SerializeField] private Sprite itemIcon = null;

    [SerializeField] private GameObject itemPrefab = null;

    [SerializeField] private string itemID = "I0000";
    [SerializeField] private string itemName = "";
    [SerializeField] private string itemDescription = "";

    [SerializeField] private int itemValue = 0;

    [SerializeField] private float baseDamage = 0;
    [SerializeField] private float baseDefense = 0;
    [SerializeField] private float attackSpeed = 0;

    [SerializeField] private int strength = 0;
    [SerializeField] private int agility = 0;
    [SerializeField] private int intellect = 0;
    [SerializeField] private int endurance = 0;

    [SerializeField] private float numericModifer = 0;
    [SerializeField] private float effectDuration = 0;

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

        get { return this.itemPrefab; }
        set { this.itemPrefab = value; }
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

    public float BASEDAMAGE
    {

        get { return this.baseDamage; }
        set { this.baseDamage = value; }
    }

    public float BASEDEFENSE
    {

        get { return this.baseDefense; }
        set { this.baseDefense = value; }
    }

    public float ATTACKSPEED
    {

        get { return this.attackSpeed; }
        set { this.attackSpeed = value; }
    }

    public int STRENGTH
    {

        get { return this.strength; }
        set { this.strength = value; }
    }

    public int AGILITY
    {

        get { return this.agility; }
        set { this.agility = value; }
    }

    public int INTELLECT
    {

        get { return this.intellect; }
        set { this.intellect = value; }
    }

    public int ENDURANCE
    {

        get { return this.endurance; }
        set { this.endurance = value; }
    }

    public float MODIFIER
    {

        get { return this.numericModifer; }
        set { this.numericModifer = value; }
    }

    public float DURATION
    {

        get { return this.effectDuration; }
        set { this.effectDuration = value; }
    }

    public void CopyInventoryItem(ItemBaseAtributes item)
    {
        this.EQUIPPABLEITEMSLOT = item.EQUIPPABLEITEMSLOT;
        this.ITEMCATEGORY = item.ITEMCATEGORY;
        this.ITEMRARITY = item.ITEMRARITY;
        this.ITEMDESCRIPTION = item.ITEMDESCRIPTION;
        this.ITEMICON = item.ITEMICON;
        this.ITEMNAME = item.ITEMNAME;
        this.ITEMPREFAB = item.ITEMPREFAB;
        this.ITEMVALUE = item.ITEMVALUE;
        this.BASEDAMAGE = item.BASEDAMAGE;
        this.BASEDEFENSE = item.BASEDEFENSE;
        this.ATTACKSPEED = item.ATTACKSPEED;
        this.STRENGTH = item.STRENGTH;
        this.AGILITY = item.AGILITY;
        this.INTELLECT = item.INTELLECT;
        this.ENDURANCE = item.ENDURANCE;
        this.MODIFIER = item.MODIFIER;
        this.DURATION = item.DURATION;
        this.ITEMID = item.ITEMID;
        this.isUnique = item.isUnique;
        this.isTwoHanded = item.isTwoHanded;
    }
}
