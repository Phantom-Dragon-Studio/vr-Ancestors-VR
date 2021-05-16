using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[Serializable]
public class InventoryDatabase
{
    [SerializeField] public static Dictionary<int, ItemBaseAtributes> globalDB = new Dictionary<int, ItemBaseAtributes>();

    [SerializeField] private List<ItemBaseAtributes> weapons = new List<ItemBaseAtributes>();
    [SerializeField] private List<ItemBaseAtributes> armor = new List<ItemBaseAtributes>();
    [SerializeField] private List<ItemBaseAtributes> consumables = new List<ItemBaseAtributes>();
    [SerializeField] private List<ItemBaseAtributes> questItems = new List<ItemBaseAtributes>();
    [SerializeField] private List<ItemBaseAtributes> reagents = new List<ItemBaseAtributes>();

    public List<ItemBaseAtributes> WEAPONSLIST
    {
        get { return this.weapons; }
    }

    public List<ItemBaseAtributes> ARMORLIST
    {
        get { return this.armor; }
    }

    public List<ItemBaseAtributes> CONSUMABLES
    {
        get { return this.consumables; }
    }

    public List<ItemBaseAtributes> QUESTITEMSLIST
    {
        get { return this.questItems; }
    }

    public List<ItemBaseAtributes> REAGENTSLIST
    {
        get { return this.reagents; }
    }

    public Dictionary<int, ItemBaseAtributes> GLOBALDATABASE
    {
        get { return globalDB; }
    }

    public InventoryDatabase()
    {
        this.ClearInventory();
    }

    public void ClearInventory()
    {
        this.weapons.Clear();
        this.armor.Clear();
        this.consumables.Clear();
        this.questItems.Clear();
        this.reagents.Clear();
    }

    public void AddItem(ItemBaseAtributes item)
    {
        switch (item.ITEMCATEGORY)
        {
            case ItemCategory.ARMOR:
                {
                    this.armor.Add(item);
                    break;
                }
            case ItemCategory.WEAPONS:
                {
                    this.weapons.Add(item);
                    break;
                }
            case ItemCategory.CONSUMABLES:
                {
                    this.consumables.Add(item);
                    break;
                }
            case ItemCategory.QUESTITEMS:
                {
                    this.questItems.Add(item);
                    break;
                }
            case ItemCategory.REAGENTS:
                {
                    this.reagents.Add(item);
                    break;
                }
        }
    }

    public void RemoveItem(ItemBaseAtributes item)
    {
        switch (item.ITEMCATEGORY)
        {
            case ItemCategory.ARMOR:
                {
                    this.armor.Remove(item);
                    break;
                }
            case ItemCategory.WEAPONS:
                {
                    this.weapons.Remove(item);
                    break;
                }
            case ItemCategory.CONSUMABLES:
                {
                    this.consumables.Remove(item);
                    break;
                }
            case ItemCategory.QUESTITEMS:
                {
                    this.questItems.Remove(item);
                    break;
                }
            case ItemCategory.REAGENTS:
                {
                    this.reagents.Remove(item);
                    break;
                }
        }
    }


}
