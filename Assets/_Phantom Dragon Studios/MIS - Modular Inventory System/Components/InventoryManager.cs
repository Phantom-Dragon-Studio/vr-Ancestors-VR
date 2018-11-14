using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region VARIABLES
    public GameObject ItemTemplate;
    public GameObject StackCount;
    public GameObject DummyItem;

    private string itemID;
    private GameObject newItem, finalNewItem;
    private bool continueProcess = false;
    private Unique newUniqueComponent;
    private bool doesThisUniqueAlreadyExist = false;
    private ItemInspectorAgent finalNewItemInformation;
    public InventoryDatabase playerEntireItemCollection;
    #endregion

    public GameObject CreateNewItem(ItemBaseAtributes newItemAttributes, ItemPanel targetedPanel)
    {
        if(targetedPanel.CheckAvailableSpace() == true)
        {
            finalNewItemInformation = AddItem(newItemAttributes, targetedPanel);
            if (finalNewItemInformation != null)
            {
                finalNewItem = finalNewItemInformation.gameObject;
                finalNewItem.transform.SetParent(targetedPanel.transform);
                playerEntireItemCollection.AddItem(finalNewItemInformation.itemInformation);
                return finalNewItem;
            }
            else return null;
        }
        else
        {
            Debug.Log("Inventory full, unable to add new item.");
            return null;
        }
    }

    public ItemInspectorAgent AddItem(ItemBaseAtributes newItemInformation, ItemPanel targetedPanel)
    {
        itemID = newItemInformation.ITEMID;
        newItem = Instantiate(ItemTemplate, this.gameObject.transform, false) as GameObject;
        Image newItemImage = newItem.GetComponent<Image>();
        newItemImage.sprite = newItemInformation.ITEMICON;
        newItem.name = newItemInformation.ITEMNAME;

        Debug.Log("Inventory is adding the item: " + newItem.gameObject.name);
        if (newItemInformation.ITEMCATEGORY == ItemCategory.CONSUMABLES || newItemInformation.ITEMCATEGORY == ItemCategory.REAGENTS)
        {
            StackableCheck(newItemInformation, targetedPanel);
        }
        else if (newItemInformation.ITEMCATEGORY == ItemCategory.WEAPONS)
        {
            if (newItemInformation.isTwoHanded)
            {
                UniqueCheck(newItemInformation);
                if (newItem != null)
                {
                    Debug.Log("Creating TwoHander....");
                    TwoHanded myNewComponent = newItem.AddComponent<TwoHanded>();
                    myNewComponent.itemInformation = newItemInformation;
                    myNewComponent.InitializeTwoHanders(myNewComponent, myNewComponent.itemInformation);
                }
            }
            else
            {
                UniqueCheck(newItemInformation);
                if (newItem != null)
                {
                    Debug.Log("Creating OneHander....");
                    ItemInspectorAgent newItemAgent = newItem.AddComponent<ItemInspectorAgent>();
                    newItemAgent.itemInformation = newItemInformation;
                }
            }
        }
        else //Regular item without special restrictions.
        {
            Debug.Log("Running default item case.");
            UniqueCheck(newItemInformation);
            if (newItem != null && newItem.GetComponent<ItemInspectorAgent>() == null)
            {
                ItemInspectorAgent newItemAgent = newItem.AddComponent<ItemInspectorAgent>();
                newItemAgent.itemInformation = newItemInformation;
            }
        }

        //Final check to make sure we're still creating the item.
        if (newItem != null)
        {
            finalNewItemInformation = newItem.GetComponent<ItemInspectorAgent>();
            finalNewItemInformation.itemInformation.CopyInventoryItem(newItemInformation);
            newItem.transform.SetParent(targetedPanel.transform);
        }
        return finalNewItemInformation;
    }

    public void RemoveItem(ItemInspectorAgent triggeringItem, ItemPanel targetedPanel)
    {
        Debug.Log("Inventory is removing item: " + triggeringItem.name);
        DestroyImmediate(triggeringItem.gameObject);
        playerEntireItemCollection.RemoveItem(finalNewItemInformation.itemInformation);
        

    }

    public void UniqueCheck(ItemBaseAtributes newItemToCheck)
    {
        Debug.Log("Checking if item is unique.");
        if (newItemToCheck.isUnique == true)
        {
            newUniqueComponent = newItem.AddComponent<Unique>();
            newUniqueComponent.itemInformation = newItemToCheck;
            doesThisUniqueAlreadyExist = newUniqueComponent.CheckForDuplicates(newUniqueComponent.itemInformation, playerEntireItemCollection);
            if (doesThisUniqueAlreadyExist == true)
            {
                DestroyImmediate(newItem);
                Debug.Log("Unique Item already exists in your item collection. " +
                            "Don't forget to check your encampment if you're having trouble locating the item.");
            }
            else
            {
                Debug.Log("No duplicates found, adding unique component.");
                newUniqueComponent.itemInformation = newItemToCheck;
                Debug.Log(newUniqueComponent.itemInformation);
            }
        }
        else
        {
            Debug.Log("Item is not unique, adding to inventory.");
        }
    }

    public GameObject StackableCheck(ItemBaseAtributes newItemInformation, ItemPanel targetedPanel)
    {
        if (newItem.GetComponent<Stackable>() == false)
        {
            var myNewComponent = newItem.AddComponent<Stackable>();
            if (CheckForPartialStack(myNewComponent, newItemInformation, targetedPanel) != null)
            {
                myNewComponent.currentStackSize = 1;
                myNewComponent.maximumStackSize = 20;
                return myNewComponent.gameObject;
            }
            else return null;
        }
        else
        {
            Debug.Log("Checking for a partial stack returned null.");
            return null;
        }
    }

    public void SplitStackableItem(Stackable triggeringStackable, ItemPanel targetedPanel)
    {
        Debug.Log("Inventory is splitting item stack: " + triggeringStackable.name);
        if (targetedPanel.CheckAvailableSpace() == true)
        {
            triggeringStackable.DecideHowToSplitItemStack(triggeringStackable);
        }
        else { }
        Debug.Log("Panel does not have enough space to split the item into a new stack.");

    }

    private GameObject CheckForPartialStack(Stackable stackableItemToCheckFor, ItemBaseAtributes itemInformationToCheckFor, ItemPanel targetedPanel)
    {
        Debug.Log("Checking for partials....");
        bool partialStackFound = false;
        continueProcess = true;
        Stackable[] allStackableItems = targetedPanel.GetComponentsInChildren<Stackable>();
        for (int i = 0; i < allStackableItems.Length; i++)
        {
            if (continueProcess)
            {
                if (itemID == allStackableItems[i].itemInformation.ITEMID)
                {
                    if (allStackableItems[i].currentStackSize < allStackableItems[i].maximumStackSize)
                    {
                        Debug.Log("A partial stack of this item with a matching itemID of " + itemID + " was found. Adding new item to the stack.");
                        partialStackFound = true;
                        allStackableItems[i].currentStackSize++;
                        allStackableItems[i].UpdateStackDisplay(allStackableItems[i]);
                        continueProcess = false;
                        DestroyImmediate(newItem);
                        return null;
                    }
                }
            }
        }
        if (!partialStackFound)
        {
            Debug.Log("Partial stack not found.");
            if (targetedPanel.CheckAvailableSpace() == true)
            {
                Debug.Log("Creating new STACKABLE...");
                stackableItemToCheckFor.itemInformation = itemInformationToCheckFor;
                return newItem = stackableItemToCheckFor.gameObject;
            }
            else if (targetedPanel.CheckAvailableSpace() == false)
            {
                Debug.Log("Item Panel is full, unable to add item.");
                return null;
            }
        }
        Debug.Log("Return null from add item....?");
        return null;
    }

    public void ManualSorting(ItemInspectorAgent triggeringItem, ItemPanel targetedPanel)
    {
        Debug.Log("Inventory is manually sorting item: " + triggeringItem.name);

    }

    public void TabSorting(ItemCategory categoryToShow)
    {
        Debug.Log("Inventory is tab sorting items by category: ");
    }
}
