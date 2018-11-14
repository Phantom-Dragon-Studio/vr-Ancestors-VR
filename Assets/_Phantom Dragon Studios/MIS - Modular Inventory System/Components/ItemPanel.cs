using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] public ItemSlotType ContainerType;
    //[HideInInspector]
    public int currentCapacity = 0;
    public int maximumCapacity = 1;

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
}