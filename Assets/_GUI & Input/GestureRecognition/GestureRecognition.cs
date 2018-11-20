/*
 * GestureRecognition - VR gesture recognition library plug-in for Unity.
 * Copyright (c) 2018 MARUI-PlugIn (inc.)
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
 * 
 * # HOW TO USE:
 * 
 * (1) Place the GestureRecognition64.dll file in the /Assets/Plugins/ folder in your unity project
 * and add the GestureRecognition.cs file to your project scripts. 
 * 
 * 
 * (2) Create a new Gesture recognition object and register the gestures that you want to identify later.
 * <code>
 * GestureRecognition gr = new GestureRecognition();
 * int myFirstGesture = gr.createGesture("my first gesture");
 * int mySecondGesture = gr.createGesture("my second gesture");
 * </code>
 * 
 * 
 * (3) Record a number of samples for each gesture by calling startStroke(), contdStroke() and endStroke()
 * for your registered gestures, each time inputting the headset and controller transformation.
 * <code>
 * Vector3 hmd_p = Camera.main.gameObject.transform.position;
 * Quaternion hmd_q = Camera.main.gameObject.transform.rotation;
 * gr.startStroke(hmd_p, hmd_q, myFirstGesture);
 * 
 * // repeat the following while performing the gesture with your controller:
 * Vector3 p = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
 * Quaternion q = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
 * gr.contdStroke(p,q);
 * // ^ repead while performing the gesture with your controller.
 * 
 * gr.endStroke();
 * </code>
 * Repeat this multiple times for each gesture you want to identify.
 * We recommend recording at least 20 samples for each gesture.
 * 
 * 
 * (4) Start the training process by calling startTraining().
 * You can optionally register callback functions to receive updates on the learning progress
 * by calling setTrainingUpdateCallback() and setTrainingFinishCallback().
 * <code>
 * gr.setMaxTrainingTime(10000); // Set training time to 10 seconds.
 * gr.startTraining();
 * </code>
 * You can stop the training process by calling stopTraining().
 * After training, you can check the gesture identification performance by calling recognitionScore()
 * (a value of 1 means 100% correct recognition).
 * 
 * 
 * (5) Now you can identify new gestures performed by the user in the same way
 * as you were recording samples:
 * <code>
 * Vector3 hmd_p = Camera.main.gameObject.transform.position;
 * Quaternion hmd_q = Camera.main.gameObject.transform.rotation;
 * gr.startStroke(hmd_p, hmd_q);
 * 
 * // repeat the following while performing the gesture with your controller:
 * Vector3 p = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
 * Quaternion q = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
 * gr.contdStroke(p,q);
 * // ^ repead while performing the gesture with your controller.
 * 
 * int identifiedGesture = gr.endStroke();
 * if (identifiedGesture == myFirstGesture) {
 *     // ...
 * }
 * </code>
 * 
 * 
 * (6) Now you can save and load the neural network.
 * <code>
 * gr.saveToFile("C:/myGestures.dat");
 * // ...
 * gr.loadFromFile("C:/myGestures.dat");
 * </code>
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;
using System.Text;

public class GestureRecognition {

    // ------------------------------------------------------------
    public delegate IntPtr MetadataCreatorFunction();
    public delegate void TrainingCallbackFunction(double performace);

    [DllImport("GestureRecognition64", EntryPoint = "GR_create", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GR_create(); //!< Create new instance.
    [DllImport("GestureRecognition64", EntryPoint = "GR_delete", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GR_delete(IntPtr gro); //!< Delete instance.
    [DllImport("GestureRecognition64", EntryPoint = "GR_startStroke", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_startStroke(IntPtr gro, double[] hmd_p, double[] hmd_q, int record_as_sample); //!< Start new stroke.
    [DllImport("GestureRecognition64", EntryPoint = "GR_startStrokeM", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_startStrokeM(IntPtr gro, double[,] hmd, int record_as_sample); //!< Start new stroke.
    [DllImport("GestureRecognition64", EntryPoint = "GR_contdStroke", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_contdStroke(IntPtr gro, double[] p, double[] q); //!< Continue stroke data input.
    [DllImport("GestureRecognition64", EntryPoint = "GR_contdStrokeM", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_contdStrokeM(IntPtr gro, double[,] m); //!< Continue stroke data input.
    [DllImport("GestureRecognition64", EntryPoint = "GR_endStroke", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_endStroke(IntPtr gro, double[] pos, double[] scale, double[] dir0, double[] dir1, double[] dir2); //!< End the stroke and identify the gesture.
    [DllImport("GestureRecognition64", EntryPoint = "GR_numberOfGestures", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_numberOfGestures(IntPtr gro); //!< Get the number of gestures currently recorded in the system.
    [DllImport("GestureRecognition64", EntryPoint = "GR_deleteGesture", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_deleteGesture(IntPtr gro, int index); //!< Delete the recorded gesture with the specified index.
    [DllImport("GestureRecognition64", EntryPoint = "GR_deleteAllGestures", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GR_deleteAllGestures(IntPtr gro); //!< Delete recorded gestures.
    [DllImport("GestureRecognition64", EntryPoint = "GR_createGesture", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_createGesture(IntPtr gro, string name, IntPtr metadata); //!< Create new gesture.
    [DllImport("GestureRecognition64", EntryPoint = "GR_recognitionScore", CallingConvention = CallingConvention.Cdecl)]
    public static extern double GR_recognitionScore(IntPtr gro); //!< Get the gesture recognition score of the current neural network (0~1).
    // [DllImport("GestureRecognition64", EntryPoint = "GR_getGestureName", CallingConvention = CallingConvention.Cdecl)]
    // public static extern string GR_getGestureName(IntPtr gro, int index); //!< Get the name of a registered gesture.
    [DllImport("GestureRecognition64", EntryPoint = "GR_getGestureNameLength", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_getGestureNameLength(IntPtr gro, int index); //!< Get the length of the name of a registered gesture.
    [DllImport("GestureRecognition64", EntryPoint = "GR_copyGestureName", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_copyGestureName(IntPtr gro, int index, StringBuilder buf, int buflen); //!< Copy the name of a registered gesture to a buffer.
    [DllImport("GestureRecognition64", EntryPoint = "GR_getGestureMetadata", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GR_getGestureMetadata(IntPtr gro, int index); //!< Get the command of a registered gesture.
    [DllImport("GestureRecognition64", EntryPoint = "GR_getGestureNumberOfSamples", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_getGestureNumberOfSamples(IntPtr gro, int index); //!< Get the number of recorded samples of a registered gesture.
    [DllImport("GestureRecognition64", EntryPoint = "GR_setGestureName", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_setGestureName(IntPtr gro, int index, string name); //!< Set the name of a registered gesture.
    [DllImport("GestureRecognition64", EntryPoint = "GR_setGestureMetadata", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_setGestureMetadata(IntPtr gro, int index, IntPtr metadata); //!< Set the command of a registered gesture.
    [DllImport("GestureRecognition64", EntryPoint = "GR_saveToFile", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_saveToFile(IntPtr gro, string path); //!< Save the neural network and recorded training data to file.
    [DllImport("GestureRecognition64", EntryPoint = "GR_loadFromFile", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_loadFromFile(IntPtr gro, string path, MetadataCreatorFunction createMetadata); //!< Load the neural network and recorded training data from file.
    [DllImport("GestureRecognition64", EntryPoint = "GR_loadFromBuffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_loadFromBuffer(IntPtr gro, string buffer, MetadataCreatorFunction createMetadata); //!< Load the neural network and recorded training data buffer.
    [DllImport("GestureRecognition64", EntryPoint = "GR_startTraining", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_startTraining(IntPtr gro); //!< Start train the Neural Network based on the the currently collected data.
    [DllImport("GestureRecognition64", EntryPoint = "GR_isTraining", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_isTraining(IntPtr gro); //!< Whether the Neural Network is currently training.
    [DllImport("GestureRecognition64", EntryPoint = "GR_stopTraining", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GR_stopTraining(IntPtr gro); //!< Stop the training process (last best result will be used).
    [DllImport("GestureRecognition64", EntryPoint = "GR_getMaxTrainingTime", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GR_getMaxTrainingTime(IntPtr gro); //!< Get maximum training time in ms.
    [DllImport("GestureRecognition64", EntryPoint = "GR_setMaxTrainingTime", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GR_setMaxTrainingTime(IntPtr gro, int t); //!< Set maximum training time in ms.

    [DllImport("GestureRecognition64", EntryPoint = "GR_setTrainingUpdateCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GR_setTrainingUpdateCallback(IntPtr gro, TrainingCallbackFunction cbf); //!< Set callback function to be called during training.
    [DllImport("GestureRecognition64", EntryPoint = "GR_setTrainingFinishCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GR_setTrainingFinishCallback(IntPtr gro, TrainingCallbackFunction cbf); //!< Set callback function to be called when training is finished.


    private IntPtr m_gro;


    //                                                          ________________________________
    //_________________________________________________________/     GestureRecognition()
    /// <summary>
    /// Constructor.
    /// </summary>
    public GestureRecognition()
    {
        m_gro = GR_create();
    }
    //                                                          ________________________________
    //_________________________________________________________/   ~GestureRecognition()
    /// <summary>
    /// Destructor.
    /// </summary>
    ~GestureRecognition()
    {
        if (m_gro.ToInt64() != 0)
        {
            GR_delete(m_gro);
        }
    }
    //                                                          ________________________________
    //_________________________________________________________/         startStroke()
    /// <summary>
    /// Start a new gesture (stroke) performance.
    /// If record_as_sample is set to a gesture ID, the following gesture performance will be recorded as a
    /// sample of that gesture (sample-recording-mode).
    /// If record_as_sample is not set, the gesture recognition library will attempt to identify the
    /// gesture based on previously recorded samples (gesture-identification-mode).
    /// Note that you must first record samples and train the neural network (by calling startTraining())
    /// before new and unknown gestures can be identified.
    /// </summary>
    /// <param name="hmd">Current transformation of the VR headset / HMD.
    /// This must be a double[4,4] column-major matrix (where the translational component is in the m[3][*] sub-array).</param>
    /// <param name="record_as_sample">[OPTIONAL] Set this to the ID of a gesture to start recording a sample for that gesture.</param>
    /// <returns>
    /// Null if an error occurred, else non-null.
    /// </returns>
    public int startStroke(double[,] hmd, int record_as_sample=-1)
    {
        return GR_startStrokeM(m_gro, hmd, record_as_sample);
    }
    //                                                          ________________________________
    //_________________________________________________________/         startStroke()
    /// <summary>
    /// Start a new gesture (stroke) performance.
    /// If record_as_sample is set to a gesture ID, the following gesture performance will be recorded as a
    /// sample of that gesture (sample-recording-mode).
    /// If record_as_sample is not set, the gesture recognition library will attempt to identify the
    /// gesture based on previously recorded samples (gesture-identification-mode).
    /// Note that you must first record samples and train the neural network (by calling startTraining())
    /// before new and unknown gestures can be identified.
    /// </summary>
    /// <param name="hmd_p">The current position of the VR headset (users POV).</param>
    /// <param name="hmd_q">The current rotation of the VR headset (users POV).</param>
    /// <param name="record_as_sample">[OPTIONAL] Set this to the ID of a gesture to start recording a sample for that gesture.</param>
    /// <returns>
    /// Null if an error occurred, else non-null.
    /// </returns>
    public int startStroke(Vector3 hmd_p, Quaternion hmd_q, int record_as_sample = -1)
    {
        double[] p = new double[3] { hmd_p.x, hmd_p.y, hmd_p.z};
        double[] q = new double[4] { hmd_q.x, hmd_q.y, hmd_q.z, hmd_q.w };
        return GR_startStroke(m_gro, p, q, record_as_sample);
    }
    //                                                          ________________________________
    //_________________________________________________________/         startStroke()
    /// <summary>
    /// Start a new gesture (stroke) performance.
    /// If record_as_sample is set to a gesture ID, the following gesture performance will be recorded as a
    /// sample of that gesture (sample-recording-mode).
    /// If record_as_sample is not set, the gesture recognition library will attempt to identify the
    /// gesture based on previously recorded samples (gesture-identification-mode).
    /// Note that you must first record samples and train the neural network (by calling startTraining())
    /// before new and unknown gestures can be identified.
    /// </summary>
    /// <param name="hmd_p">The current position of the VR headset (users POV). This must be a double[3] array.</param>
    /// <param name="hmd_q">The current rotation of the VR headset (users POV). This must be a double[4] array.</param>
    /// <param name="record_as_sample">[OPTIONAL] Set this to the ID of a gesture to start recording a sample for that gesture.</param>
    /// <returns>
    /// Null if an error occurred, else non-null.
    /// </returns>
    public int startStroke(double[] hmd_p, double[] hmd_q, int record_as_sample = -1)
    {
        return GR_startStroke(m_gro, hmd_p, hmd_q, record_as_sample);
    }
    //                                                          ________________________________
    //_________________________________________________________/         contdStroke()
    /// <summary>
    /// Continue performing a gesture.
    /// </summary>
    /// <param name="m">Transformation of the controller with which the gesture is performed.
    /// This must be a double[4,4] column-major matrix (where the translational component is in the m[3][*] sub-array).</param>
    /// <returns>
    /// Null if an error occurred, else non-null.
    /// </returns>
    public int contdStroke(double[,] m)
    {
        return GR_contdStrokeM(m_gro, m);
    }
    //                                                          ________________________________
    //_________________________________________________________/         contdStroke()
    /// <summary>
    /// Continue performing a gesture.
    /// </summary>
    /// <param name="p">Position of the controller with which the gesture is performed.</param>
    /// <param name="q">Rotation of the controller with which the gesture is performed.</param>
    /// <returns>
    /// Null if an error occurred, else non-null.
    /// </returns>
    public int contdStroke(Vector3 p, Quaternion q)
    {
        double[] _p = new double[3] { p.x, p.y, p.z };
        double[] _q = new double[4] { q.x, q.y, q.z, q.w };
        return GR_contdStroke(m_gro, _p, _q);
    }
    //                                                          ________________________________
    //_________________________________________________________/          contdStroke()
    /// <summary>
    /// Continue performing a gesture.
    /// </summary>
    /// <param name="p">Position of the controller with which the gesture is performed. This must be a double[3] array.</param>
    /// <param name="q">Rotation of the controller with which the gesture is performed. This must be a double[4] array.</param>
    /// <returns>
    /// Null if an error occurred, else non-null.
    /// </returns>
    public int contdStroke(double[] p, double[] q)
    {
        return GR_contdStroke(m_gro, p, q);
    }
    //                                                          ________________________________
    //_________________________________________________________/          endStroke()
    /// <summary>
    /// End a gesture (stroke).
    /// If the gesture was started as recording a new sample (sample-recording-mode), then
    /// the gesture will be added to the reference examples for this gesture.
    /// If the gesture (stroke) was started in identification mode, the gesture recognition
    /// library will attempt to identify the gesture.
    /// </summary>
    /// <param name="pos">[OUT] The position where the gesture was performed.</param>
    /// <param name="scale">[OUT] The scale at which the gesture was performed.</param>
    /// <param name="dir0">[OUT] The primary direction in which the gesture was performed (ie. widest direction).</param>
    /// <param name="dir1">[OUT] The secondary direction in which the gesture was performed.</param>
    /// <param name="dir2">[OUT] The minor direction in which the gesture was performed (ie. narrowest direction).</param>
    /// <returns>
    /// The ID of the gesture identified.
    /// </returns>
    public int endStroke(ref Vector3 pos, ref double scale, ref Vector3 dir0, ref Vector3 dir1, ref Vector3 dir2)
    {
        double[] _pos = new double[3];
        double[] _scale = new double[1];
        double[] _dir0 = new double[3];
        double[] _dir1 = new double[3];
        double[] _dir2 = new double[3];
        int ret = GR_endStroke(m_gro, _pos, _scale, _dir0, _dir1, _dir2);
        pos.x = (float)_pos[0];
        pos.y = (float)_pos[1];
        pos.z = (float)_pos[2];
        scale = _scale[0];
        dir0.x = (float)_dir0[0];
        dir0.y = (float)_dir0[1];
        dir0.z = (float)_dir0[2];
        dir1.x = (float)_dir1[0];
        dir1.y = (float)_dir1[1];
        dir1.z = (float)_dir1[2];
        dir2.x = (float)_dir2[0];
        dir2.y = (float)_dir2[1];
        dir2.z = (float)_dir2[2];
        return ret;
    }
    //                                                          ________________________________
    //_________________________________________________________/          endStroke()
    /// <summary>
    /// End a gesture (stroke).
    /// If the gesture was started as recording a new sample (sample-recording-mode), then
    /// the gesture will be added to the reference examples for this gesture.
    /// If the gesture (stroke) was started in identification mode, the gesture recognition
    /// library will attempt to identify the gesture.
    /// </summary>
    /// <param name="pos">[OUT] The position where the gesture was performed. This must be a double[3] array.</param>
    /// <param name="scale">[OUT] The scale at which the gesture was performed. This must be a double[1] array.</param>
    /// <param name="dir0">[OUT] The primary direction in which the gesture was performed (ie. widest direction). This must be a double[3] array.</param>
    /// <param name="dir1">[OUT] The secondary direction in which the gesture was performed. This must be a double[3] array.</param>
    /// <param name="dir2">[OUT] The minor direction in which the gesture was performed (ie. narrowest direction). This must be a double[3] array.</param>
    /// <returns>
    /// The ID of the gesture identified.
    /// </returns>
    public int endStroke(ref double[] pos, ref double[] scale, ref double[] dir0, ref double[] dir1, ref double[] dir2)
    {
        return GR_endStroke(m_gro, pos, scale, dir0, dir1, dir2);
    }
    //                                                          ________________________________
    //_________________________________________________________/          endStroke()
    /// <summary>
    /// End a gesture (stroke).
    /// If the gesture was started as recording a new sample (sample-recording-mode), then
    /// the gesture will be added to the reference examples for this gesture.
    /// If the gesture (stroke) was started in identification mode, the gesture recognition
    /// library will attempt to identify the gesture.
    /// </summary>
    /// <returns>
    /// The ID of the gesture identified.
    /// </returns>
    public int endStroke()
    {
        return GR_endStroke(m_gro, null, null, null, null, null);
    }
    //                                                          ________________________________
    //_________________________________________________________/      numberOfGestures()
    /// <summary>
    /// Query the number of currently registered gestures.
    /// </summary>
    /// <returns>
    /// The number of gestures currently registered in the library.
    /// </returns>
    public int numberOfGestures()
    {
        return GR_numberOfGestures(m_gro);
    }
    //                                                          ________________________________
    //_________________________________________________________/         deleteGesture()
    /// <summary>
    /// Delete currently registered gesture.
    /// </summary>
    /// <param name="index">ID of the gesture to delete.</param>
    /// <returns>
    /// Null on failure, non-null on success.
    /// </returns>
    public int deleteGesture(int index)
    {
        return GR_deleteGesture(m_gro, index);
    }
    //                                                          ________________________________
    //_________________________________________________________/      deleteAllGestures()
    /// <summary>
    /// Delete all currently registered gestures.
    /// </summary>
    public void deleteAllGestures()
    {
        GR_deleteAllGestures(m_gro);
    }
    //                                                          ________________________________
    //_________________________________________________________/      createGesture()
    /// <summary>
    /// Create a new gesture.
    /// </summary>
    /// <param name="name">Name of the new gesture.</param>
    /// <returns>
    /// ID of the new gesture.
    /// </returns>
    public int createGesture(string name)
    {
        return GR_createGesture(m_gro, name, IntPtr.Zero);
    }
    //                                                          ________________________________
    //_________________________________________________________/      recognitionScore()
    /// <summary>
    /// Get the current recognition performance
    /// (probabliity to recognize the correct gesture: a value between 0 and 1 where 0 is 0%
    /// and 1 is 100% correct recognition rate).
    /// </summary>
    /// <returns>
    /// The current recognition performance
    /// (probabliity to recognize the correct gesture: a value between 0 and 1 where 0 is 0%
    /// and 1 is 100% correct recognition rate).
    /// </returns>
    public double recognitionScore()
    {
        return GR_recognitionScore(m_gro);
    }
    //                                                          ________________________________
    //_________________________________________________________/      getGestureName()
    /// <summary>
    /// Get the name associated to a gesture. 
    /// </summary>
    /// <param name="index">ID of the gesture to query.</param>
    /// <returns>
    /// Name of the gesture. On failure, an empty string is returned.
    /// </returns>
    public string getGestureName(int index)
    {
        int strlen = GR_getGestureNameLength(m_gro, index);
        if (strlen <= 0)
        {
            return "";
        }
        StringBuilder sb = new StringBuilder(strlen+1);
        GR_copyGestureName(m_gro, index, sb, sb.Capacity);
        return sb.ToString();
    }
    //                                                          ________________________________
    //_________________________________________________________/   getGestureNumberOfSamples()
    /// <summary>
    /// Query the number of samples recorded for one gesture.
    /// </summary>
    /// <param name="index">ID of the gesture to query.</param>
    /// <returns>
    /// Number of samples recorded for this gesture or -1 if the gesture does not exist.
    /// </returns>
    public int getGestureNumberOfSamples(int index)
    {
        return GR_getGestureNumberOfSamples(m_gro, index);
    }
    //                                                          ________________________________
    //_________________________________________________________/       setGestureName()
    /// <summary>
    /// Set the name associated with a gesture.
    /// </summary>
    /// <param name="index">ID of the gesture whose name to set.</param>
    /// <param name="name">The new name of the gesture.</param>
    /// <returns>
    /// Null if setting the name failed, non-null if setting the name was successful.
    /// </returns>
    public int setGestureName(int index, string name)
    {
        return GR_setGestureName(m_gro, index, name);
    }
    //                                                          ________________________________
    //_________________________________________________________/      saveToFile()
    /// <summary>
    /// Save the current neural network to a file.
    /// </summary>
    /// <param name="path">File system path and filename where to save the file.</param>
    /// <returns>
    /// Null if saving failed, non-null if saving was successful.
    /// </returns>
    public int saveToFile(string path)
    {
        return GR_saveToFile(m_gro, path);
    }
    //                                                          ________________________________
    //_________________________________________________________/    loadFromFile()
    /// <summary>
    /// Load a previously saved gesture recognition neural network from a file.
    /// </summary>
    /// <param name="path">File system path and filename from where to load.</param>
    /// <returns>
    /// Null if loading failed, non-null if loading was successful.
    /// </returns>
    public int loadFromFile(string path)
    {
        return GR_loadFromFile(m_gro, path, null);
    }
    //                                                          ________________________________
    //_________________________________________________________/     loadFromBuffer()
    /// <summary>
    /// Load a previously saved gesture recognition neural network from a string buffer.
    /// </summary>
    /// <param name="buffer">The string buffer containing the neural network.</param>
    /// <returns>
    /// Null if loading failed, non-null if loading was successful.
    /// </returns>
    public int loadFromBuffer(string buffer)
    {
        return GR_loadFromBuffer(m_gro, buffer, null);
    }
    //                                                          ________________________________
    //_________________________________________________________/      startTraining()
    /// <summary>
    /// Start the learning process.
    /// After calling this function the gesture recognition library will attempt to learn
    /// patterns in the previously recorded sample strokes in order to find commonalities
    /// and identify future gestures.
    /// </summary>
    /// <returns>
    /// Null if starting the learning process failed, non-null if the learning process
    /// was successfully started.
    /// </returns>
    public int startTraining()
    {
        return GR_startTraining(m_gro);
    }
    //                                                          ________________________________
    //_________________________________________________________/       isTraining()
    /// <summary>
    /// Query whether the gesture recognition library is currently trying to learn
    /// gesture identification.
    /// </summary>
    /// <returns>
    /// Null if the gesture recognition library is NOT currently in the learning process,
    /// non-null if it is learning.
    /// </returns>
    public int isTraining()
    {
        return GR_isTraining(m_gro);
    }
    //                                                          ________________________________
    //_________________________________________________________/       stopTraining()
    /// <summary>
    /// Stop the learning process.
    /// Due to the asynchronous nature of the learning process ("learning in background")
    /// it may take a moment before the training in the background is actually stopped.
    /// </summary>
    public void stopTraining()
    {
        GR_stopTraining(m_gro);
    }
    //                                                          ________________________________
    //_________________________________________________________/     getMaxTrainingTime()
    /// <summary>
    /// Get the maximum time used for the learning process (in milliseconds).
    /// </summary>
    /// <returns>
    /// Maximum time to spend learning (in milliseconds).
    /// </returns>
    public int getMaxTrainingTime()
    {
        return GR_getMaxTrainingTime(m_gro);
    }
    //                                                          ________________________________
    //_________________________________________________________/     setMaxTrainingTime()
    /// <summary>
    /// Set the maximum time used for the learning process (in milliseconds).
    /// </summary>
    /// <param name="t">Maximum time to spend learning (in milliseconds).</param>
    public void setMaxTrainingTime(int t)
    {
        GR_setMaxTrainingTime(m_gro, t);
    }
    //                                                          ________________________________
    //_________________________________________________________/   setTrainingUpdateCallback()
    /// <summary>
    /// Set a function to be called during the training process.
    /// The function will be called whenever the gesture recognition library was able to
    /// increase its performance.
    /// The callback function will receive the current recognition performance
    /// (probabliity to recognize the correct gesture: a value between 0 and 1 where 0 is 0%
    /// and 1 is 100% correct recognition rate).
    /// </summary>
    /// <param name="cbf">The function to call when the recognition performance could be increased.</param>
    public void setTrainingUpdateCallback(TrainingCallbackFunction cbf)
    {
        GR_setTrainingUpdateCallback(m_gro, cbf);
    }
    //                                                          ________________________________
    //_________________________________________________________/   setTrainingFinishCallback()
    /// <summary>
    /// Set a callback function to be called when the learning process finishes.
    /// The callback function will receive the final recognition performance
    /// (probabliity to recognize the correct gesture: a value between 0 and 1 where 0 is 0%
    /// and 1 is 100% correct recognition rate).
    /// </summary>
    /// <param name="cbf">The function to call when the learning process is finished.</param>
    public void setTrainingFinishCallback(TrainingCallbackFunction cbf)
    {
        GR_setTrainingFinishCallback(m_gro, cbf);
    }

}
