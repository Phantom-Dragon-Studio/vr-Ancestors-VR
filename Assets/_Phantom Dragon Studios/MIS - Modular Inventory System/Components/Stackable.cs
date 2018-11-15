using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stackable : ItemInspectorAgent
{
    //[HideInInspector]
    public int currentStackSize = 1;
    [Tooltip("How many of this item can fit in a single item slot.")]
    public int maximumStackSize = 1;

    private ItemPanel parentPanel;
    private GameObject stackCountGameObject;
    private TextMeshProUGUI stackCountText;

    void Awake()
    {
        stackCountText = GetComponentInChildren<TextMeshProUGUI>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        InitalizeStackableItem(this);
        UpdateStackDisplay(this);
    }

    public TextMeshProUGUI InitalizeStackableItem(Stackable thisStackableItem)
    {
        stackCountGameObject = Instantiate(inventoryManager.StackCount, this.gameObject.transform, false) as GameObject;
        stackCountGameObject.transform.position = Vector2.zero;
        parentPanel = this.GetCurrentItemPanel();
        return stackCountText;
    }

    //Responsible for updating the text GUI
    public void UpdateStackDisplay(Stackable thisStackableItem)
    {
        thisStackableItem.GetComponentInChildren<TextMeshProUGUI>().text = currentStackSize.ToString();
    }

    //Remove item from the stack when one is used, lost, sold, etc. If it's the only one left in the stack, remove that stack.
    public void DecreaseStackCount()
    {
        if (this.currentStackSize >= 1)
        {
            currentStackSize--;
        }
        if (this.currentStackSize < 1)
        {
            inventoryManager.RemoveItem(this, this.GetCurrentItemPanel());
            Destroy(this.gameObject);
        }
        UpdateStackDisplay(this);
    }

    //Add item to the stack when a new one is aquired.
    public void IncreaseStackCount()
    {
        if (this.currentStackSize >= 1 && this.currentStackSize < this.maximumStackSize)
        {
            currentStackSize++;
        }
        else if (this.currentStackSize >= this.maximumStackSize)
        {
            if (parentPanel.CheckAvailableSpace())
            {
                inventoryManager.CreateNewItem(this.itemInformation, this.GetCurrentItemPanel());
            }
        }
        UpdateStackDisplay(this);
    }

    public void SplitStack()
    {
        if (parentPanel.CheckAvailableSpace())
        {
            inventoryManager.SplitStackableItem(this, this.GetCurrentItemPanel());
            UpdateStackDisplay(this);
        }
    }

    public void DecideHowToSplitItemStack(Stackable triggeringItem)
    {
        if (triggeringItem.currentStackSize == 1)
        {
            Debug.Log("Unable to split " + triggeringItem.name + ". That slot only contains 1 item.");
            return;
        }

        //Split Stack In Half.
        if (triggeringItem.currentStackSize % 2 == 0)                                                                           
        {
            ExecuteSplitting(triggeringItem);
        }

        //Split Stack In Half, leaving any extras in the original stack.
        else if (triggeringItem.currentStackSize % 2 != 0)
        {
            ExecuteSplitting (triggeringItem);
            triggeringItem.currentStackSize++;
        }
    }

    private void ExecuteSplitting(Stackable triggeringItem)
    {
        GameObject secondStack = inventoryManager.CreateNewItem(this.itemInformation, this.GetCurrentItemPanel());
        Stackable secondStackStackableData = secondStack.GetComponent<Stackable>();
        ItemInspectorAgent secondStackitemData = secondStack.GetComponent<ItemInspectorAgent>();
        secondStackitemData.itemInformation.CopyInventoryItem(triggeringItem.itemInformation);
        triggeringItem.currentStackSize = triggeringItem.currentStackSize / 2;
        secondStackStackableData.currentStackSize = triggeringItem.currentStackSize;
        inventoryManager.playerEntireItemCollection.AddItem(secondStackitemData.itemInformation);
    }

}
