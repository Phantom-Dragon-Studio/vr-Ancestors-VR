using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoHanded : ItemInspectorAgent
{
    GameObject mirrorObject;

    public void InitializeTwoHanders(ItemInspectorAgent passedItemData, ItemBaseAtributes passedItemInformation)
    {
        this.itemInformation = passedItemData.itemInformation;
        mirrorObject = Instantiate(Resources.Load("_DummyItem"), this.gameObject.transform.parent, false) as GameObject;
        DummyItem myDummy = mirrorObject.GetComponent<DummyItem>();
        Debug.Log(passedItemData.itemInformation);
        myDummy.InitializeDummy(this.itemInformation.ITEMNAME, this.itemInformation.ITEMICON);
    }

    public void UpdateTwinLocation(DummyItem dummyItem, ItemPanel triggeringPanel)
    {
        //Move to correct place & parent.
    }

    public void WeaponEquipped(DummyItem dummyItem, ItemPanel triggeringPanel)
    {
        //Tell Dummy Stuff
    }

    public void WeaponUnequipped(DummyItem dummyItem, ItemPanel triggeringPanel)
    {
        //Tell Dummy Stuff
    }


}