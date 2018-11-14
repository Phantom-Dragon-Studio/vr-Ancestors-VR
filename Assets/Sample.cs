/*
 * Advaced Gesture Recognition - Unity Plug-In
 * 
 * Copyright (c) 2018 MARUI-PlugIn (inc.)
 * This software is free to use for non-commercial purposes.
 * You may use this software in part or in full for any project
 * that does not pursue financial gain, including free software 
 * and projectes completed for evaluation or educational purposes only.
 * Any use for commercial purposes is prohibited.
 * You may not sell or rent any software that includes
 * this software in part or in full, either in it's original form
 * or in altered form.
 * If you wish to use this software in a commercial application,
 * please contact us at contact@marui-plugin.com to obtain
 * a commercial license.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
 * PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY 
 * OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Oculus;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField] private Text hud_text;
    [SerializeField] private TextMesh left_hand_text;
    [SerializeField] private TextMesh right_hand_text;

    // The gesture recognition object:
    // You can have as many of these as you want simultaneously.
    private static GestureRecognition gr = new GestureRecognition();
    
    // The controller with which the user is currently gesturing (if any):
	private OVRInput.Controller  active_controller = OVRInput.Controller.None; 

    // The game object associated with the currently active controller (if any):
    private GameObject           active_controller_object = null; 

    // ID of the gesture currently being recorded,
    // or: -1 if not currently recording a new gesture,
    // or: -2 if the AI is currently trying to learn to identify gestures
    // or: -3 if the AI has recently finished learning to identify gestures
	private static int recording_gesture = -1; 

    // Last reported recognition performance (during training).
    // 0 = 0% correctly recognized, 1 = 100% correctly recognized.
    private static double last_performance_report = 0; 

    // Temporary storage for objects to display the gesture stroke.
    List<string> stroke = new List<string>(); 

    // Temporary counter variable when creating objects for the stroke display:
    int stroke_index = 0; 

    // List of Objects created with gestures:
    List<GameObject> created_objects = new List<GameObject>(); 

    // Initialization:
    void Start ()
    {
        // Load the default set of gestures.
        if (gr.loadFromFile("Assets/GestureRecognition/sample_gestures.dat") == 0)
        {
            Debug.Log("Failed to load sample gesture database file");
        }
        // Set the welcome message.
        hud_text.text = "Welcome to MARUI Gesture Plug-in!\n"
                      + "Press the trigger to draw a gesture. Available gestures:\n"
                      + "- a circle/ring (creates a cylinder)\n"
                      + "- swipe left/right (rotate object)\n"
                      + "- cross/'x' (delete object)\n"
                      + "or: press 'A'/'X' button to create new gesture.";
        left_hand_text.text = "";
        right_hand_text.text = "";
    }
    

    // Update:
    void Update()
    {
        // If recording_gesture is -3, that means that the AI has recently finished learning a new gesture.
        if (recording_gesture == -3) {
            // Show "finished" message.
            double performance = gr.recognitionScore();
            hud_text.text = "Training finished!\n(Final recognition performance = " + (performance * 100.0) + "%)\nFeel free to use your new gesture.";
            // Set recording_gesture to -1 to indicate normal operation (learning finished).
            recording_gesture = -1;
        }
        // If recording_gesture is -2, that means that the AI is currently learning a new gesture.
        if (recording_gesture == -2) {
            // Show "please wait" message
            hud_text.text = "...training...\n(Current recognition performance = " + (last_performance_report * 100.0) + "%)\nPress the 'B' or 'Y' button to cancel training.";
            // In this mode, the user may press the "B/Y" button (button 2) to cancel the learning process.
            if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch) || OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch)) {
                // Button pressed: stop the learning process.
				gr.stopTraining();
			}
			return;
		}
        // Else: if we arrive here, we're not in training/learning mode,
        // so the user can draw gestures.

        // If recording_gesture is -1, we're currently not recording a new gesture.
        if (recording_gesture == -1) {
            // In this mode, the user can press button A/X to create a new gesture
            if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch) || OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch)) {
                recording_gesture = gr.createGesture("custom gesture " + (gr.numberOfGestures() + 1));
                // from now on: recording a new gesture
                hud_text.text = "Learning a new gesture (custom gesture " + (recording_gesture-3) + "):\nPlease perform the gesture 25 times.\n(0 / 25)";
            }
        }

        // If the user is not yet dragging (pressing the trigger) on either controller, he hasn't started a gesture yet.
        if (active_controller == OVRInput.Controller.None) {
            // If the user presses either controller's trigger, we start a new gesture.
			if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0.8) {
                // Right controller trigger pressed.
                active_controller = OVRInput.Controller.RTouch;
                active_controller_object = GameObject.Find("RightHandAnchor");
            } else if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0.8) {
                // Left controller trigger pressed.
                active_controller = OVRInput.Controller.LTouch;
                active_controller_object = GameObject.Find("LeftHandAnchor");
            } else {
                // If we arrive here, the user is pressing neither controller's trigger:
                // nothing to do.
                return;
            }
            // If we arrive here: either trigger was pressed, so we start the gesture.
            GameObject hmd = GameObject.Find("CenterEyeAnchor"); // alternative: Camera.main.gameObject
            Vector3 hmd_p = hmd.transform.localPosition;
            Quaternion hmd_q = hmd.transform.localRotation;
            gr.startStroke(hmd_p, hmd_q, recording_gesture);
        }

        // If we arrive here, the user is currently dragging with one of the controllers.
        // Check if the user is still dragging or if he let go of the trigger button.
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, active_controller) > 0.3) {
            // The user is still dragging with the controller: continue the gesture.
            Vector3 p = active_controller_object.transform.localPosition; // alternative: OVRInput.GetLocalControllerPosition(active_controller);
            Quaternion q = active_controller_object.transform.localRotation; // alternative: OVRInput.GetLocalControllerRotation(active_controller);
            gr.contdStroke(p, q);
            // Show the stroke by instatiating new objects
            GameObject cube = Instantiate(GameObject.Find("StrokePoint"));
            cube.name = "stroke_" + stroke_index++;
            cube.transform.localPosition = p;
            cube.transform.localRotation = q;
            cube.transform.localScale    = new Vector3(0.005f, 0.005f, 0.005f);
            stroke.Add(cube.name);
            return;
        }
        // else: if we arrive here, the user let go of the trigger, ending a gesture.
        active_controller = OVRInput.Controller.None;
        // Delete the objectes that we used to display the gesture.
        foreach (string cube in stroke) {
            Destroy(GameObject.Find(cube));
            stroke_index = 0;
        }
        
        Vector3 pos = Vector3.zero; // This will receive the position where the gesture was performed.
        double scale = 0; // This will receive the scale at which the gesture was performed.
        Vector3 dir0 = Vector3.zero; // This will receive the primary direction in which the gesture was performed (greatest expansion).
        Vector3 dir1 = Vector3.zero; // This will receive the secondary direction of the gesture.
        Vector3 dir2 = Vector3.zero; // This will receive the minor direction of the gesture (direction of smallest expansion).
        int gesture_id = gr.endStroke(ref pos, ref scale, ref dir0, ref dir1, ref dir2);

        // If we are currently recording samples for a custom gesture, check if we have recorded enough samples yet.
        if (recording_gesture >= 0) {
            // Currently recording samples for a custom gesture - check how many we have recorded so far.
            int num_samples = gr.getGestureNumberOfSamples(recording_gesture);
            if (num_samples < 25) {
                // Not enough samples recorded yet.
                hud_text.text = "Learning a new gesture (custom gesture " + (recording_gesture - 3) + "):\nPlease perform the gesture 25 times.\n(" + num_samples + " / 25)";
            } else {
                // Enough samples recorded. Start the learning process.
                hud_text.text = "Learning gestures - please wait...\n(press B button to stop the learning process)";
                // Set up the call-backs to receive information about the learning process.
                gr.setTrainingUpdateCallback(trainingUpdateCallback);
                gr.setTrainingFinishCallback(trainingFinishCallback);
                gr.startTraining();
                // Set recording_gesture to -2 to indicate that we're currently in learning mode.
                recording_gesture = -2;
            }
            return;
        }
        // else: if we arrive here, we're not recording new sampled for custom gestures,
        // but instead have identified a new gesture.
        // Perform the action associated with that gesture.
        
        if (gesture_id < 0) { 
            // Error trying to identify any gesture
            hud_text.text = "Failed to identify gesture.";
        } else if (gesture_id == 0) {
            // "loop"-gesture: create cylinder
            hud_text.text = "Identified a CIRCLE/LOOP gesture!";
            GameObject cylinder = Instantiate(GameObject.Find("Cylinder"));
            cylinder.transform.localPosition = pos;
            cylinder.transform.localRotation = Quaternion.FromToRotation(new Vector3(0,1,0), dir2);
            cylinder.transform.localScale = new Vector3((float)scale*2, (float)scale, (float)scale*2);
            created_objects.Add(cylinder);
        } else if (gesture_id == 1) {
            // "swipe left"-gesture: rotate left
            hud_text.text = "Identified a SWIPE LEFT gesture!";
            GameObject closest_object = getClosestObject(pos);
            if (closest_object != null) {
                closest_object.transform.Rotate(new Vector3(0, 1, 0), (float)scale*400, Space.World);
            }
        } else if (gesture_id == 2) { 
            // "swipe right"-gesture: rotate right
            hud_text.text = "Identified a SWIPE RIGHT gesture!";
            GameObject closest_object = getClosestObject(pos);
            if (closest_object != null) {
                closest_object.transform.Rotate(new Vector3(0, 1, 0), -(float)scale*400, Space.World);
            }
        } else if (gesture_id == 3) {
            // "shake" or "scrap" gesture: delete closest object
            hud_text.text = "Identified a CROSS/'X' gesture!";
            GameObject closest_object = getClosestObject(pos);
            if (closest_object != null) {
                Destroy(closest_object);
                created_objects.Remove(closest_object);
            }
        } else {
            // Other ID: one of the user-registered gestures:
            hud_text.text = " identified custom registered gesture " + (gesture_id - 3);
        }
    }

    // Callback function to be called by the gesture recognition plug-in during the learning process.
    public static void trainingUpdateCallback(double performance)
    {
        // Update the performance indicator with the latest estimate.
        last_performance_report = performance;
    }

    // Callback function to be called by the gesture recognition plug-in when the learning process was finished.
    public static void trainingFinishCallback(double performance)
    {
        // Update the performance indicator with the latest estimate.
        last_performance_report = performance;
        // Signal that training was finished.
        recording_gesture = -3;
        // Save the data to file. 
        gr.saveToFile("my_gestures.dat");
    }

    // Helper function to find a GameObject in the world based on it's position.
    private GameObject getClosestObject(Vector3 pos)
    {
        GameObject closest_object = null;
        foreach (GameObject o in created_objects)
        {
            if (closest_object == null || (o.transform.localPosition - pos).magnitude < (closest_object.transform.localPosition - pos).magnitude)
            {
                closest_object = o;
            }
        }
        return closest_object;
    }
}
