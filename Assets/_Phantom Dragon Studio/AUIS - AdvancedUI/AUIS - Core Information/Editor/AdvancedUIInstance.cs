using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AdvancedUISkinData : ScriptableObject
{
    public class AdvancedUIInstance : Editor
    {

        static GameObject clickedObject;

        private static GameObject Create(string objectName)
        {
            GameObject instance = Instantiate(Resources.Load<GameObject>(objectName));
            instance.name = objectName;
            clickedObject = UnityEditor.Selection.activeObject as GameObject;
            if (clickedObject != null)
            {
                instance.transform.SetParent(clickedObject.transform, false);
            }
            return instance;
        }

        [MenuItem("GameObject/Phantom Dragon Studios/UI/New Button", priority = 0)]
        private static void AddButton()
        {
            Create("AdvancedUIButton");
        }
    }
}
