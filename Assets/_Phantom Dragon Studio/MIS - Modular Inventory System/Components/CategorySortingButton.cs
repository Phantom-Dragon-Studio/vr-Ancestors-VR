using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CategorySortingButton : MonoBehaviour {

    public ItemCategory categoryToSort;

    private Button thisButton;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        thisButton = this.GetComponent<Button>();
        thisButton.onClick.AddListener(delegate { inventoryManager.TabSorting(categoryToSort); });
    }
}
