using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ItemInspectorAgent : MonoBehaviour
{
    public ItemBaseAtributes itemInformation;                                                      //Gives us access to the item attributes data.
    private Vector2 combineOffset = new Vector2();                                          //X and Y Offset from cursor
    private GameObject placeHolder = null;                                                  //Placeholder GameObject.
    private Image imageComp;
    private InventoryManager inventoryManager;
    [HideInInspector] public Transform ParentItemPanelBeforeDraggingStarted = null;         //Parent object of item currently being dragged.
    [HideInInspector] public Transform postDragParentItemPanel = null;                      //Parent object after dragging started
    [HideInInspector] public Transform placeholderParent = null;
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

    //Creates a dummy gameobject called placeholder in order to represent where an item will be placed in the inventory.
    private void PushSorting()
    {
        placeHolder = new GameObject("Temp Placeholder");                                    //Create new placeholder.
        placeholderParent = ParentItemPanelBeforeDraggingStarted;                            //Set variable values to match eachother for now.
        placeHolder.transform.SetParent(ParentItemPanelBeforeDraggingStarted);               //Set the placeHolder parent to my parent.
        LayoutElement layoutElement = placeHolder.AddComponent<LayoutElement>();             //Add a LayoutElement to the placeholder dummy object.
        layoutElement.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;  //Set the Height to match the high of the currently dragged item height.
        layoutElement.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;    //Do the same thing to the width that we just did with the height.
        layoutElement.flexibleHeight = 0;                                                    //Do not allow height changes.
        layoutElement.flexibleWidth = 0;                                                     //Do not allow width changes.
        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());             //Set the placeholder's place in the index to the same place in the index as the currently dragged item
        UpdatePushSortingPlaceholder();
    }

    //Responsible for updating the placeholder's current position in the inventory.
    private void UpdatePushSortingPlaceholder()
    {
        if (placeHolder.transform.parent != placeholderParent)
            placeHolder.transform.SetParent(placeholderParent);

        int newSiblingIndex = placeholderParent.childCount;
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.y > placeholderParent.GetChild(i).position.y && this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;


                if (placeHolder.transform.GetSiblingIndex() > newSiblingIndex && placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;
                break;
            }
        }
        placeHolder.transform.SetSiblingIndex(newSiblingIndex);
    }
}