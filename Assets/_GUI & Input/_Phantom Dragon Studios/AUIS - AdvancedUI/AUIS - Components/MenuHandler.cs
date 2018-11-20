using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MenuHandler : MonoBehaviour {

    GameObject GUIWindows;
	// Use this for initialization
	void Start () {
        GUIWindows = FindObjectOfType<TooltipHandler>().gameObject;
	}

    //Can be removed, only in place for convenience to see skin update in editor.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GUIWindows.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GUIWindows.SetActive(false);
        }
    }


    public void Awake()
    {
        if(XRDevice.isPresent)
        {
            var myDeviceName = XRDevice.model;


        }
    }
}

