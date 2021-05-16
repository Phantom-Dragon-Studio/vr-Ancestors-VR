using Zenject;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsCollector : MonoBehaviour {

    [HideInInspector]
    private ItemBaseAtributes myCollectedInformation;

    private CharacterClass mainCharacter;
    public CharacterStats mainCharacterStats;
    public List<TextMeshProUGUI> attributesTextDisplay;

    private string normal, white, red, green, blue;

    public ItemBaseAtributes MyCollectedInformation { get => myCollectedInformation; set => myCollectedInformation = value; }

    public void Awake()
    {
        normal = "<#FFC800>";
        white = "<#ffffff>";
        red = "<#fb0707>";
        green = "<#23ff00>";
        blue = "<#00FFFF>";

        MyCollectedInformation = ScriptableObject.CreateInstance("ItemBaseAtributes") as ItemBaseAtributes;

        //Listens for stat changes to be finalized, then applies them to the character panel.
        //mainCharacterStats.OnPassPlayerCharacterStats += AssignInformationToCharacterStatsPanel;
    }

    //This runs in start to avoid execution order conflicts.
    void Start()
    {
        AskForInformation(MyCollectedInformation);
    }

    public void AskForInformation(ItemBaseAtributes informationContainer)
    {
        ClearInformation();
        AssignInformationToCharacterStatsPanel();
    }

    private void ClearInformation()
    {
        MyCollectedInformation.MAXHEALTH = 0;
        MyCollectedInformation.HEALTHREGEN = 0;
        MyCollectedInformation.MAX_M_E_F = 0;
        MyCollectedInformation.M_E_FREGEN = 0;
        MyCollectedInformation.STAMINA = 0;
        MyCollectedInformation.STAMINAREGEN = 0;
        MyCollectedInformation.BASEDAMAGE = 0;
        MyCollectedInformation.BASEDEFENSE = 0;
        MyCollectedInformation.STRENGTH = 0;
        MyCollectedInformation.AGILITY = 0;
        MyCollectedInformation.INTELLECT = 0;
        MyCollectedInformation.ENDURANCE = 0;
        MyCollectedInformation.elementalResistance = 0;
        MyCollectedInformation.fireResistance = 0;
        MyCollectedInformation.earthResistance = 0;
        MyCollectedInformation.waterResistance = 0;
        MyCollectedInformation.windResistance = 0;
        MyCollectedInformation.darkResistance = 0;
        MyCollectedInformation.divineResistance = 0;
        MyCollectedInformation.lightningResistance = 0;
        MyCollectedInformation.arcaneResistance = 0;
        MyCollectedInformation.ATTACKRANGE = 0;
    }

    private void AssignInformationToCharacterStatsPanel()
    {
        //0, 1, & 2 are for Health, MEF, and Stamina; which are respectivley taken care of by their own scripts.
        attributesTextDisplay[3].text = normal + "Attack: " + white + Math.Round(mainCharacterStats.characterData.baseDamage).ToString();
        attributesTextDisplay[4].text = normal + "Defense: " + white + Math.Round(mainCharacterStats.characterData.baseDefense).ToString();
        attributesTextDisplay[5].text = normal + "Strength: " + green + Mathf.Round(mainCharacterStats.characterData.baseStrength).ToString();
        attributesTextDisplay[6].text = normal + "Agility: " + green + Mathf.Round(mainCharacterStats.characterData.baseAgility).ToString();
        attributesTextDisplay[7].text = normal + "Intelligence: " + green + Mathf.Round(mainCharacterStats.characterData.baseIntelligence).ToString();
        attributesTextDisplay[8].text = normal + "Endurance: " + green + Mathf.Round(mainCharacterStats.characterData.baseEndurance).ToString();
        attributesTextDisplay[9].text = normal + "Critical Chance: " + green + Math.Round(mainCharacterStats.characterData.criticalStrikeChance, 2) + "%".ToString();
        attributesTextDisplay[10].text = normal + "Dodge Chance: " + green + Math.Round(mainCharacterStats.characterData.dodgeChance, 2) + "%".ToString();
        attributesTextDisplay[11].text = normal + "Attack Range: " + green + Math.Round(mainCharacterStats.characterData.baseAttackRange).ToString();
        attributesTextDisplay[12].text = normal + "Fire Resistance: " + blue + Math.Round(mainCharacterStats.characterData.fireResistance).ToString();
        attributesTextDisplay[13].text = normal + "Water Resistance: " + blue + Math.Round(mainCharacterStats.characterData.waterResistance).ToString();
        attributesTextDisplay[14].text = normal + "Earth Resistance: " + blue + Math.Round(mainCharacterStats.characterData.earthResistance).ToString();
        attributesTextDisplay[15].text = normal + "Wind Resistance: " + blue + Math.Round(mainCharacterStats.characterData.windResistance).ToString();
        attributesTextDisplay[16].text = normal + "Lightning Resistance: " + blue + Math.Round(mainCharacterStats.characterData.lightningResistance).ToString();
        attributesTextDisplay[17].text = normal + "Divine Resistance: " + blue + Math.Round(mainCharacterStats.characterData.divineResistance).ToString();
        attributesTextDisplay[18].text = normal + "Arcane Resistance: " + blue + Math.Round(mainCharacterStats.characterData.arcaneResistance).ToString();
        attributesTextDisplay[19].text = normal + "Darkness Resistance: " + blue + Math.Round(mainCharacterStats.characterData.darkResistance).ToString();
    }
}
