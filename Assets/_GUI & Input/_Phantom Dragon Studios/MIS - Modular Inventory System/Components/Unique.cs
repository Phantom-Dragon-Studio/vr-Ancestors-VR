using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unique : ItemInspectorAgent
{
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
