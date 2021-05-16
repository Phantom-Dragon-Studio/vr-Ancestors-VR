using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipWindow : MonoBehaviour
{

    public static TooltipWindow windowHandler;
    [HideInInspector]
    public Transform windowParent;

    private void Awake()
    {
        if (windowHandler != null)
        {
            Destroy(this);
        }
        else if (windowHandler == null)
        {
            windowHandler = this;
        }
        windowParent = this.transform.parent;
    }

}
