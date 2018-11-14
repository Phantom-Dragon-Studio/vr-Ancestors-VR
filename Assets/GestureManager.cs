using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour {

    GestureRecognition gestureManager = new GestureRecognition();
    int swipeRight, swipeLeft;

	//Initialization
	void Awake () {
        swipeRight = gestureManager.createGesture("SwipeLeft");
        swipeLeft = gestureManager.createGesture("SwipeRight");
    }

}
