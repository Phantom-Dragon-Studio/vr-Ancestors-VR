using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unique : ItemInspectorAgent
{
    private string myItemID;
    private ItemInspectorAgent myAgent;

    public void Start()
    {
        myAgent = this.GetComponent<ItemInspectorAgent>();
        AssignID(myAgent);
    }

    public void AssignID (ItemInspectorAgent myAgent) {
        if (myAgent != null)
        {
            myItemID = myAgent.itemInformation.ITEMID;
        }
	}

    public bool CheckForDuplicates(ItemBaseAtributes uniqueItemToCheckFor, InventoryDatabase databaseToCheckForUniqueItem)
    {
        if (databaseToCheckForUniqueItem.WEAPONSLIST.Contains(uniqueItemToCheckFor)) return true;
        if (databaseToCheckForUniqueItem.ARMORLIST.Contains(uniqueItemToCheckFor)) return true;
        if (databaseToCheckForUniqueItem.REAGENTSLIST.Contains(uniqueItemToCheckFor)) return true;
        if (databaseToCheckForUniqueItem.QUESTITEMSLIST.Contains(uniqueItemToCheckFor)) return true;
        if (databaseToCheckForUniqueItem.CONSUMABLES.Contains(uniqueItemToCheckFor)) return true;

        else return false;
    }
}
