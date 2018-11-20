using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ItemInspectorAgent : MonoBehaviour
{
    public ItemBaseAtributes itemInformation;                                                                                                   
    private Image imageComp;
    [HideInInspector] public  InventoryManager inventoryManager;
    [HideInInspector] public ItemPanel parentItemPanel = null;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        imageComp = this.GetComponent<Image>();
        imageComp.sprite = itemInformation.ITEMICON;
    }

    public ItemPanel GetCurrentItemPanel()
    {
        parentItemPanel = this.transform.parent.GetComponent<ItemPanel>();
        return parentItemPanel;
    }
}