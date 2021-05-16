using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AbilityEditor : EditorWindow {


    public string abilityCreationFolder = "Assets/_Phantom Dragon Studios/SAS - Scriptable Ability System/SAS - Abilities/";
    public int abilityID;
    public string abilityName;
    public string abilityToolTip;
    public int abilityCost;
    public int abilityEarnedLevel;
    public TargetType? abilityTargetType;
    public float abilityMinRange;
    public float abilityMaxRange;
    public float abilityCooldown;
    public float abilityDuration;
    public bool abilityRequiresTalent;
    public TalentNode abilityRequiredTalentNode;
    public int abilityCurrentLevel;
    public int abilityMaxLevel;
    public float abilityLevelMultiplier;
    public Sprite abilityThumbnail;

    private _AbilityData newAbility;

    private SerializedProperty _AbilityCreationFolderProperty, _abilityIDProperty, _abilityNameProperty, _abilityToolTipProperty, _abilityCostProperty, _abilityEarnedLevelProperty, _abilityTargetTypeProperty, _abilityMinRangeProperty,
        _abilityMaxRangeProperty, _abilityCooldownProperty, _abilityDurationProperty, _abilityRequiresTalentProperty, _requiredTalentNode, _abilityCurrentLevel, _abilityMaxLevel, _abilityEffectMultiplier, _abilityThumbnail;



    [MenuItem("Phantom Dragon Studios/Ability System/Ability Editor")]
    public static void ShowWindow()
    {

      GetWindow<AbilityEditor>("Ability Editor");
    }

    //Window Code
    void OnGUI()
    {
        GUILayout.Label("Create custom abilities using this tool. Once saved a ScriptableObject asset containing all ability info will be created.");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.Label("NOTE: To keep abilityID organized, Use, 0-99 for AGILITY HERO, " +"\n " + "100-199 for the WARRIOR HERO, and 200-299 for the WIZARD HERO.");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        SerializedObject serializedObject = new SerializedObject(this);
        _AbilityCreationFolderProperty = serializedObject.FindProperty("abilityCreationFolder");
        _abilityIDProperty = serializedObject.FindProperty("abilityID");
        _abilityNameProperty = serializedObject.FindProperty("abilityName");
        _abilityToolTipProperty = serializedObject.FindProperty("abilityToolTip");
        _abilityCostProperty = serializedObject.FindProperty("abilityCost");
        _abilityEarnedLevelProperty = serializedObject.FindProperty("abilityEarnedLevel");
        _abilityTargetTypeProperty = serializedObject.FindProperty("abilityTargetType");
        _abilityMinRangeProperty = serializedObject.FindProperty("abilityMinRange");
        _abilityMaxRangeProperty = serializedObject.FindProperty("abilityMaxRange");
        _abilityCooldownProperty = serializedObject.FindProperty("abilityCooldown");
        _abilityDurationProperty = serializedObject.FindProperty("abilityDuration");
        _abilityRequiresTalentProperty = serializedObject.FindProperty("abilityRequiresTalent");
        _requiredTalentNode = serializedObject.FindProperty("abilityRequiredTalentNode");
        _abilityCurrentLevel = serializedObject.FindProperty("abilityCurrentLevel");
        _abilityMaxLevel = serializedObject.FindProperty("abilityMaxLevel");
        _abilityEffectMultiplier = serializedObject.FindProperty("abilityLevelMultiplier");
        _abilityThumbnail= serializedObject.FindProperty("abilityThumbnail");

        EditorGUILayout.PropertyField(_AbilityCreationFolderProperty, true);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_abilityIDProperty, true);
        EditorGUILayout.PropertyField(_abilityNameProperty, true);
        EditorGUILayout.PropertyField(_abilityToolTipProperty, true);
        EditorGUILayout.PropertyField(_abilityCostProperty, true);
        EditorGUILayout.PropertyField(_abilityEarnedLevelProperty, true);
        EditorGUILayout.PropertyField(_abilityTargetTypeProperty);
        EditorGUILayout.PropertyField(_abilityMinRangeProperty, true);
        EditorGUILayout.PropertyField(_abilityMaxRangeProperty, true);
        EditorGUILayout.PropertyField(_abilityCooldownProperty, true);
        EditorGUILayout.PropertyField(_abilityDurationProperty, true);
        EditorGUILayout.PropertyField(_abilityRequiresTalentProperty, true);
        EditorGUILayout.PropertyField(_abilityCurrentLevel, true);
        EditorGUILayout.PropertyField(_abilityMaxLevel, true);
        EditorGUILayout.PropertyField(_abilityEffectMultiplier, true);
        EditorGUILayout.PropertyField(_abilityThumbnail, true);

        if (_abilityRequiresTalentProperty.boolValue == true)
        {
            EditorGUILayout.PropertyField(_requiredTalentNode, true);
        }

        serializedObject.ApplyModifiedProperties();


        if (GUILayout.Button("Create/Update Item Asset"))
        {
            newAbility = CreateNewAbility(abilityCreationFolder, _abilityNameProperty.stringValue);
            UpdateNewItemFields(newAbility);
        }

        if (GUILayout.Button("Reset Fields"))
        {
            Reset();
        }
    }

    private void Reset()
    {
        abilityID = 0000;
        abilityName = "";
        abilityToolTip = "";
        abilityCost = 0;
        abilityEarnedLevel = 0;
        abilityTargetType = TargetType.Self;
        abilityMinRange = 0;
        abilityMaxRange = 0;
        abilityCooldown = 0;
        abilityDuration = 0;
        abilityRequiresTalent = false;
        abilityRequiredTalentNode = null;
        abilityCurrentLevel = 0;
        abilityMaxLevel = 0;
        abilityLevelMultiplier = 0;
        abilityThumbnail = null;
    }


    public void UpdateNewItemFields(_AbilityData newAbility)
    {
        newAbility.abilityID = _abilityIDProperty.intValue;
        newAbility.abilityName = _abilityNameProperty.stringValue;
        newAbility.abilityTooltip = _abilityToolTipProperty.stringValue;
        newAbility.abilityCost = _abilityCostProperty.intValue;
        newAbility.abilityEarnedLevel = _abilityEarnedLevelProperty.intValue;
        newAbility.abilityMaxRange = _abilityMinRangeProperty.floatValue;
        newAbility.abilityMaxRange = _abilityMaxRangeProperty.floatValue;
        newAbility.abilityCooldown = _abilityCooldownProperty.floatValue;
        newAbility.abilityDuration = _abilityDurationProperty.floatValue;
        newAbility.abilityRequiresTalent = _abilityRequiresTalentProperty.boolValue;
        newAbility.abilityRequiredTalentNode = _requiredTalentNode.objectReferenceValue as TalentNode;
        newAbility.abilityCurrentLevel = _abilityCurrentLevel.intValue;
        newAbility.abilityMaxLevel = _abilityMaxLevel.intValue;
        newAbility.abilityLevelMultiplier = _abilityEffectMultiplier.floatValue;
        newAbility.abilityThumbnail = _abilityThumbnail.objectReferenceValue as Sprite;
        int temp = _abilityTargetTypeProperty.enumValueIndex;
        
        switch (temp)
        {
            case 0:
                newAbility.abilityTargetType = TargetType.Self;
                break;
            case 1:
                newAbility.abilityTargetType = TargetType.AoE;
                break;
            case 2:
                newAbility.abilityTargetType = TargetType.SingleTarget;
                break;
            case 3:
                newAbility.abilityTargetType = TargetType.MultiTarget;
                break;


            default:
                newAbility.abilityTargetType = TargetType.Self;
                break;
        }
    }

    public static _AbilityData CreateNewAbility(string path, string name)
    {
        var temp = CreateInstance("AbilityInfo");
        temp.name = name;
        AssetDatabase.CreateAsset(temp, path + name.ToString() +".asset");
        AssetDatabase.SaveAssets();
        Selection.SetActiveObjectWithContext(temp, temp);
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = temp;
        return temp as _AbilityData;
    }

}