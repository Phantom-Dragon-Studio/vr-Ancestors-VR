using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DummyItem : MonoBehaviour {

    [HideInInspector]
    public ItemSlotType mySlotType;

    private Image myImage;
    private string twoHandedPairItemID;

    private void Awake()
    {
        myImage = this.GetComponent<Image>();
        myImage.color = Color.grey;
        myImage.enabled = false;
    }

    // Use this for initialization
    public void InitializeDummy(string myName, Sprite twinImage)
    {
        this.name = myName + "_DummyItem";
        myImage.sprite = twinImage;
    }

    public void SetDummyItemID (string IDtoSet)
    {
        twoHandedPairItemID = IDtoSet;
    }

    
}
