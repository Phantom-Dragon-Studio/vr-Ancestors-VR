using Zenject;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class ItemPanel : MonoBehaviour
{
    private StatsCollector _myStatCollector;

    [Inject]
    public void Construct(StatsCollector myStatCollector)
    {
        _myStatCollector = myStatCollector;
    }


    [SerializeField] public ItemSlotType ContainerType;
    public bool heldItemsApplyToCharacterStatBonuses;
    public int maximumCapacity = 1;

    //[HideInInspector]
    public int currentCapacity = 0;

    private ItemBaseAtributes myChildInformation;


    public bool CheckAvailableSpace()
    {
        if (currentCapacity < maximumCapacity)
        {
            Debug.Log(this.gameObject.name + " has free space available.");
            return true;
        }
        else
        {
            Debug.Log(this.gameObject.name + " does NOT have free space available.");
            return false;
        }
    }

    public ItemSlotType GetContainerType()
    {
        return this.ContainerType;
    }

    public void GetChildren(StatsPanelSignal passedItemInformation)
    {
        Debug.Log("Getting Children Stats....");
        Debug.Log(this.name);
        if (this.heldItemsApplyToCharacterStatBonuses == true)
        {
            for (int i = 0; this.transform.childCount - 1 >= i; i++)
                if (this.transform.childCount != 0)
                {
                    myChildInformation = GetComponentInChildren<ItemInspectorAgent>().itemInformation;
                    Debug.Log("Child: " + myChildInformation);
                    AssignCollectedGearInformationToCharacterStatsCollector(passedItemInformation.GearStatBonus, myChildInformation);
                }
        }
    }

    public void AssignCollectedGearInformationToCharacterStatsCollector
        (ItemBaseAtributes itemInformationToAssign, ItemBaseAtributes itemToReadFrom)
    {
        itemInformationToAssign.STRENGTH += itemToReadFrom.STRENGTH;
        itemInformationToAssign.AGILITY += itemToReadFrom.AGILITY;
        itemInformationToAssign.INTELLECT += itemToReadFrom.INTELLECT;
        itemInformationToAssign.ENDURANCE += itemToReadFrom.ENDURANCE;
        itemInformationToAssign.elementalResistance += itemToReadFrom.elementalResistance;
        itemInformationToAssign.fireResistance += itemToReadFrom.fireResistance;
        itemInformationToAssign.earthResistance += itemToReadFrom.earthResistance;
        itemInformationToAssign.windResistance += itemToReadFrom.windResistance;
        itemInformationToAssign.divineResistance += itemToReadFrom.divineResistance;
        itemInformationToAssign.darkResistance += itemToReadFrom.darkResistance;
        itemInformationToAssign.arcaneResistance += itemToReadFrom.arcaneResistance;
        itemInformationToAssign.lightningResistance += itemToReadFrom.lightningResistance;
        itemInformationToAssign.waterResistance += itemToReadFrom.waterResistance;
        if (itemToReadFrom.ITEMCATEGORY == ItemCategory.WEAPONS && itemToReadFrom.isTwoHanded == true)
        {
            itemInformationToAssign.ATTACKRANGE = itemToReadFrom.ATTACKRANGE;
        }

        if (itemToReadFrom.ITEMCATEGORY == ItemCategory.WEAPONS)
        {
            itemInformationToAssign.BASEDAMAGE += itemToReadFrom.BASEDAMAGE;
        }

        if (itemToReadFrom.ITEMCATEGORY == ItemCategory.ARMOR)
        {
            itemInformationToAssign.BASEDEFENSE += itemToReadFrom.BASEDEFENSE;
        }

        if (this.ContainerType == ItemSlotType.TRINKET)
        {
            itemInformationToAssign.BASEDAMAGE += itemToReadFrom.BASEDAMAGE;
            itemInformationToAssign.BASEDEFENSE += itemToReadFrom.BASEDEFENSE;
        }


    }
}