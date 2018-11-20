using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class TooltipHandler : MonoBehaviour {

    #region VARIABLES AND INITIALIZATION
    //Private Variables
    [HideInInspector]
    public TooltipWindow tooltipWindow;
    private string hoverTargetName, hoverTargetDescription;
    private float hoverTargetMinRange, hoverTargetMaxRange, hoverTargetDuration, hoverTargetCooldown, hoverTargetDamage, 
        hoverTargetDefense, hoverTargetAttackSpeed;
    private TargetType? hoverTargetActionType;
    private Faction? hoverTargetFaction;
    private Sprite hoverTargetImage;

    private int hoverTargetCost, hoverTargetLevel,
        hoverTargetStrength, hoverTargetAgility, hoverTargetIntelligence, hoverTargetEndurance, hoverTargetNumericModifier;

    private bool? hoverTargetIsStatic;
    private bool showWindow = true; //Change to false later after testing.
    private float timeSinceLastTooltipDisplay;
    private TextMeshProUGUI[] tooltipFields;
    private TextMeshProUGUI selectedField, factionDisplay, titleDisplay, levelDisplay, rangeDisplay, targetTypeDisplay,
        costDisplay, durationDisplay, isStaticObjectDisplay, cooldownDisplay, descriptionDisplay, hoverTargetDamageDisplay, hoverTargetDefenseDisplay, hoverTargetAttackSpeedDisplay, hoverTargetStrengthDisplay, hoverTargetAgilityDisplay,
        hoverTargetIntelligenceDisplay, hoverTargetEnduranceDisplay;

    //Public Variables
    public Vector3 offset;
    public float timeTillFadeIfNoNewTooltip;
    public List<TextMeshProUGUI> WINDOWFIELDS;
    public Image IMAGEFIELD;

    //Initialization
    void Awake () {
        tooltipWindow = FindObjectOfType<TooltipWindow>();
        tooltipWindow.transform.localPosition = Vector3.zero;
        tooltipWindow.transform.position = tooltipWindow.transform.position + offset;
        WINDOWFIELDS = new List<TextMeshProUGUI>();
        AddAndSortWindowFields();
    }


    public void AddAndSortWindowFields()
    {
        tooltipFields = tooltipWindow.transform.GetChild(0).GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < tooltipFields.Length; i++)
        {
            selectedField = tooltipFields[i];
            tooltipFields[i].transform.SetSiblingIndex(i);
            WINDOWFIELDS.Add(selectedField);
            //WINDOWFIELDS.Sort((tooltipFields, selectedField) => 
            //    tooltipFields.transform.GetSiblingIndex().CompareTo(WINDOWFIELDS.IndexOf(WINDOWFIELDS[i])));
        }
    }

    //Only used to detect when to show tooltip based onm player actions.
    void Update()
    {
        if (tooltipWindow != null && showWindow == true)
        {
            //Show window when specific hand pose is done.
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                HideTooltipWindow();
            }
        }
    }
    #endregion

    #region DECISION LOGIC
    //Information to display when hovering over an ability.
    public void AssignTooltipData(_AbilityData abilityInfoToRead)
    {
        ClearTooltipWindow();
        hoverTargetActionType = abilityInfoToRead.abilityTargetType;
        hoverTargetName = abilityInfoToRead.abilityName;
        hoverTargetDescription = abilityInfoToRead.abilityTooltip;
        hoverTargetMinRange = abilityInfoToRead.abilityMinRange;
        hoverTargetMaxRange = abilityInfoToRead.abilityMaxRange;
        hoverTargetDuration = abilityInfoToRead.abilityDuration;
        hoverTargetCost = abilityInfoToRead.abilityCost;
        hoverTargetCooldown = abilityInfoToRead.abilityCooldown;

        UpdateTooltipDisplayInfo();
        showWindow = true;
        ShowTooltipWindow();
    }
    //Information to display when hovering over an item.
    public void AssignTooltipData(ItemBaseAtributes itemInfoToRead)
    {
        ClearTooltipWindow();
        hoverTargetName = itemInfoToRead.ITEMNAME;
        hoverTargetDescription = itemInfoToRead.ITEMDESCRIPTION;
        hoverTargetDuration = itemInfoToRead.DURATION;
        hoverTargetCost = itemInfoToRead.ITEMVALUE;
        hoverTargetDamage = itemInfoToRead.BASEDAMAGE;
        hoverTargetDefense = itemInfoToRead.BASEDEFENSE;
        hoverTargetAttackSpeed = itemInfoToRead.ATTACKSPEED;
        hoverTargetStrength = itemInfoToRead.STRENGTH;
        hoverTargetAgility = itemInfoToRead.AGILITY;
        hoverTargetIntelligence = itemInfoToRead.INTELLECT;
        hoverTargetEndurance = itemInfoToRead.ENDURANCE;

        UpdateTooltipDisplayInfo();
        showWindow = true;
        ShowTooltipWindow();
    }

    //Information to display when hovering over a regular UI button.
    public void AssignTooltipData(AdvancedUIButton UIButtonInfoToRead)
    {
        ClearTooltipWindow();
        hoverTargetName = UIButtonInfoToRead.myTooltipData.buttonName;
        hoverTargetDescription = UIButtonInfoToRead.myTooltipData.regularUIButtonTooltip;

        UpdateTooltipDisplayInfo();
        showWindow = true;
        ShowTooltipWindow();
    }
    //Information to display when hovering over a NPC or Player in-game.
    public void AssignTooltipData(WorldObjectTooltipAgent inWorldObjectToRead)
    {
        ClearTooltipWindow();
        hoverTargetName = inWorldObjectToRead.objectData.objectName;
        hoverTargetImage = inWorldObjectToRead.objectData.objectHealthBar;
        hoverTargetFaction = inWorldObjectToRead.objectData.objectFaction;
        hoverTargetLevel = inWorldObjectToRead.objectData.objectLevel;
        hoverTargetDescription = inWorldObjectToRead.objectData.objectDescription;
        hoverTargetIsStatic = inWorldObjectToRead.objectData.objectIsStatic;

        UpdateTooltipDisplayInfo();
        showWindow = true;
        ShowTooltipWindow();
    }
    #endregion

    #region ASSIGNING INFORMATION INTO THE WINDOW
    private void UpdateTooltipDisplayInfo()
    {
        if (hoverTargetImage != null)
        {
            IMAGEFIELD.sprite = hoverTargetImage;
            IMAGEFIELD.gameObject.SetActive(true);
        }
        else IMAGEFIELD.gameObject.SetActive(false);

        factionDisplay = WINDOWFIELDS[0].GetComponent<TextMeshProUGUI>(); //Faction
        if (hoverTargetFaction != null)
        {
            factionDisplay.text = "<color=#ffffffff>" + hoverTargetFaction.ToString();
            factionDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[0].gameObject.SetActive(false);

        titleDisplay = WINDOWFIELDS[1].GetComponent<TextMeshProUGUI>(); //Name
        if (titleDisplay != null)
        {
            titleDisplay.text = "<color=#6699ff>" + hoverTargetName;
            titleDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[1].gameObject.SetActive(false);

        levelDisplay = WINDOWFIELDS[2].GetComponent<TextMeshProUGUI>(); //Target's Level
        if (hoverTargetLevel != -1)
        {
            levelDisplay.text = "<color=#FFD700> Level: <color=#ffffffff>" + hoverTargetLevel.ToString();
            levelDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[2].gameObject.SetActive(false);

        rangeDisplay = WINDOWFIELDS[3].GetComponent<TextMeshProUGUI>(); //Range
        if (hoverTargetMaxRange != -1 && hoverTargetMinRange != -1)
        {
            rangeDisplay.text = "<color=#00ff00ff>Min Range: <color=#ffffffff>" + hoverTargetMinRange.ToString() + 
                "m" + "\n" + "<color=#008000ff>Max Range: <color=#ffffffff>" + hoverTargetMaxRange + "m";
            rangeDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[3].gameObject.SetActive(false);

        targetTypeDisplay = WINDOWFIELDS[4].GetComponent<TextMeshProUGUI>(); //Target Type
        if (hoverTargetActionType != null)
        {
            targetTypeDisplay.text = "<color=#ffff00ff>Ability Type: <color=#ffffffff>" + hoverTargetActionType.ToString();
            targetTypeDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[4].gameObject.SetActive(false);

        costDisplay = WINDOWFIELDS[5].GetComponent<TextMeshProUGUI>(); //Cost
        if (hoverTargetCost != -1)
        {
            costDisplay.text = "<color=#00ffffff>Cost: <color=#ffffffff>" + hoverTargetCost.ToString();
            costDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[5].gameObject.SetActive(false);

        durationDisplay = WINDOWFIELDS[6].GetComponent<TextMeshProUGUI>(); //Buff / Debuff Duration
        if (hoverTargetDuration != -1)
        {
            durationDisplay.text = "<color=#ff00ff> Duration: <color=#ffffffff>" + hoverTargetDuration.ToString() + "s";
            durationDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[6].gameObject.SetActive(false);

        isStaticObjectDisplay = WINDOWFIELDS[7].GetComponent<TextMeshProUGUI>(); //Mobile or Static target?
        if (hoverTargetIsStatic != null)
        {
            if (isStaticObjectDisplay == true)
            {
                isStaticObjectDisplay.text = "<color=#ffa500ff>" + "Stationary Object";
            }
            else if (isStaticObjectDisplay == false)
            {
                isStaticObjectDisplay.text = "<color=#ff0000ff>" + "Mobile Target";
            }
            isStaticObjectDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[7].gameObject.SetActive(false);

        cooldownDisplay = WINDOWFIELDS[8].GetComponent<TextMeshProUGUI>(); //Cooldown Length
        if (hoverTargetCooldown != -1)
        {
            cooldownDisplay.text = "<color=#008080ff>" + "Cooldown: <color=#ffffffff>" + hoverTargetCooldown.ToString() + "s";
            cooldownDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[8].gameObject.SetActive(false);

        //Item Fields
        hoverTargetDamageDisplay = WINDOWFIELDS[9].GetComponent<TextMeshProUGUI>(); //Item Damage
        if (hoverTargetDamage != 0)
        {
            hoverTargetDamageDisplay.text = "<color=#FFD700> Damage: <color=#ffffffff>" + hoverTargetDamage.ToString();
            hoverTargetDamageDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[9].gameObject.SetActive(false);

        hoverTargetDefenseDisplay = WINDOWFIELDS[10].GetComponent<TextMeshProUGUI>(); //Item Defense
        if (hoverTargetDefense != 0)
        {
            hoverTargetDefenseDisplay.text = "<color=#FFD700> Defense: <color=#ffffffff>" + hoverTargetDefense.ToString();
            hoverTargetDefenseDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[10].gameObject.SetActive(false);

        hoverTargetAttackSpeedDisplay = WINDOWFIELDS[11].GetComponent<TextMeshProUGUI>(); //Item Defense
        if (hoverTargetAttackSpeed != 0)
        {
            hoverTargetAttackSpeedDisplay.text = "<color=#FFD700> Attack Speed: <color=#ffffffff>" + hoverTargetAttackSpeed.ToString();
            hoverTargetAttackSpeedDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[11].gameObject.SetActive(false);

        hoverTargetStrengthDisplay = WINDOWFIELDS[12].GetComponent<TextMeshProUGUI>(); //Item Strength
        if (hoverTargetStrength != 0)
        {
            hoverTargetStrengthDisplay.text = "<color=#527a7a> Strength: <color=#00b300>" + hoverTargetStrength.ToString();
            hoverTargetStrengthDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[12].gameObject.SetActive(false);

        hoverTargetAgilityDisplay = WINDOWFIELDS[13].GetComponent<TextMeshProUGUI>(); //Item Agility
        if (hoverTargetAgility != 0)
        {
            hoverTargetAgilityDisplay.text = "<color=#527a7a> Agility: <color=#00b300>" + hoverTargetAgility.ToString();
            hoverTargetAgilityDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[13].gameObject.SetActive(false);

        hoverTargetIntelligenceDisplay = WINDOWFIELDS[14].GetComponent<TextMeshProUGUI>(); //Item Intelligence
        if (hoverTargetIntelligence != 0)
        {
            hoverTargetIntelligenceDisplay.text = "<color=#527a7a> Intelligence: <color=#00b300>" + hoverTargetIntelligence.ToString();
            hoverTargetIntelligenceDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[14].gameObject.SetActive(false);

        hoverTargetEnduranceDisplay = WINDOWFIELDS[15].GetComponent<TextMeshProUGUI>(); //Item Intelligence
        if (hoverTargetEndurance != 0)
        {
            hoverTargetEnduranceDisplay.text = "<color=#527a7a> Endurance: <color=#00b300>" + hoverTargetEndurance.ToString();
            hoverTargetEnduranceDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[15].gameObject.SetActive(false);

        descriptionDisplay = WINDOWFIELDS[16].GetComponent<TextMeshProUGUI>(); //Description
        if (hoverTargetDescription != null)
        {
            descriptionDisplay.text = "<color=#808080ff>" + "Description: <color=#ffffffff>" + hoverTargetDescription;
            descriptionDisplay.gameObject.SetActive(true);
        }
        else WINDOWFIELDS[16].gameObject.SetActive(false);
    }
    #endregion

    #region WINDOW MANAGEMENT

    public void ClearTooltipWindow()
    {
        hoverTargetImage = null;
        hoverTargetName = null;
        hoverTargetDescription = null;
        hoverTargetMinRange = -1;
        hoverTargetMaxRange = -1;
        hoverTargetDuration = -1;
        hoverTargetCooldown = -1;
        hoverTargetActionType = null;
        hoverTargetFaction = null;
        hoverTargetImage = null;
        hoverTargetCost = -1;
        hoverTargetLevel = -1;
        hoverTargetIsStatic = null;
        hoverTargetDamage = 0;
        hoverTargetDefense = 0;
        hoverTargetAttackSpeed = 0;
        hoverTargetStrength = 0;
        hoverTargetAgility = 0;
        hoverTargetIntelligence = 0;
        hoverTargetEndurance = 0;
        hoverTargetNumericModifier = -1;
    }

    public void ShowTooltipWindow()
    {
        tooltipWindow.gameObject.SetActive(true);
    }

    public void HideTooltipWindow()
    {
        ClearTooltipWindow();
    }
    #endregion
}