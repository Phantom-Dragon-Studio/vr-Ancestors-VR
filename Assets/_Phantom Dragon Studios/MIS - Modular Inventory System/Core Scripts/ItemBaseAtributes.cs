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
    QUICKBAR = 13
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

[Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Phantom Dragon Studios/Modular Inventory System/Item", order = 2)]
public class ItemBaseAtributes : ScriptableObject
{
    [SerializeField] private ItemSlotType equippableItemSlot;
    [SerializeField] private ItemCategory itemCategory;
    [SerializeField] private Sprite itemIcon = null;
    [SerializeField] private GameObject itemPrefab = null;
    [SerializeField] private string itemID = "I0000";
    [SerializeField] private string itemName = "";
    [SerializeField] private string itemDescription = "";
    [SerializeField] private float itemValue = 0f;
    [SerializeField] private float baseDamage = 0;
    [SerializeField] private float baseDefence = 0;
    [SerializeField] private float attackSpeed = 0;
    [SerializeField] private float strength = 0;
    [SerializeField] private float agility = 0;
    [SerializeField] private float intellect = 0;
    [SerializeField] private float endurance = 0;
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

    public float ITEMVALUE
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

    public float BASEDEFENCE
    {

        get { return this.baseDefence; }
        set { this.baseDefence = value; }
    }

    public float ATTACKSPEED
    {

        get { return this.attackSpeed; }
        set { this.attackSpeed = value; }
    }

    public float STRENGTH
    {

        get { return this.strength; }
        set { this.strength = value; }
    }

    public float AGILITY
    {

        get { return this.agility; }
        set { this.agility = value; }
    }

    public float INTELLECT
    {

        get { return this.intellect; }
        set { this.intellect = value; }
    }

    public float ENDURANCE
    {

        get { return this.endurance; }
        set { this.endurance = value; }
    }

    public void CopyInventoryItem(ItemBaseAtributes item)
    {
        this.EQUIPPABLEITEMSLOT = item.EQUIPPABLEITEMSLOT;
        this.ITEMCATEGORY = item.ITEMCATEGORY;
        this.ITEMDESCRIPTION = item.ITEMDESCRIPTION;
        this.ITEMICON = item.ITEMICON;
        this.ITEMNAME = item.ITEMNAME;
        this.ITEMPREFAB = item.ITEMPREFAB;
        this.ITEMVALUE = item.ITEMVALUE;
        this.BASEDAMAGE = item.BASEDAMAGE;
        this.BASEDEFENCE = item.BASEDEFENCE;
        this.ATTACKSPEED = item.ATTACKSPEED;
        this.STRENGTH = item.STRENGTH;
        this.AGILITY = item.AGILITY;
        this.INTELLECT = item.INTELLECT;
        this.ENDURANCE = item.ENDURANCE;
        this.ITEMID = item.ITEMID;
        this.isUnique = item.isUnique;
        this.isTwoHanded = item.isTwoHanded;
    }
}
