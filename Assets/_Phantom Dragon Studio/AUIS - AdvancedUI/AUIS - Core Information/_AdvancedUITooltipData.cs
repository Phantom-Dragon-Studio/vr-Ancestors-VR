using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CreateAssetMenu(fileName = "New UI Tooltip Data Container", menuName = "Phantom Dragon Studios/Advanced UI System/Tooltip Data Container", order = 2)]
public class _AdvancedUITooltipData : ScriptableObject
{
    public string buttonName;
    public string regularUIButtonTooltip;

}