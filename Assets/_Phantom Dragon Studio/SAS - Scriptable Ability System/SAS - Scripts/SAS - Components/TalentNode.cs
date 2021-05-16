using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class TalentNode : MonoBehaviour {



    //[HideInInspector]
    [HideInInspector]
    private PopulateTalentTree talentTree;
    public _AbilityData nodeInfo;


    private GameObject nodeGameObject;
    private TextMeshProUGUI talentLevelDisplayText;
    private Vector2 itemImageDimensionms = new Vector2();
    private Image myImage;

    // Use this for initialization
    void Start () {
        nodeGameObject = this.gameObject;
        talentLevelDisplayText = this.nodeGameObject.GetComponentInChildren<TextMeshProUGUI>();
        talentTree = this.nodeGameObject.GetComponentInParent<PopulateTalentTree>();
        myImage = this.nodeGameObject.GetComponent<Image>();
        myImage.sprite = this.nodeInfo.abilityThumbnail;
        this.InitializeTalentLevelDisplay();
        this.UpdateTalentLevelDisplay(talentLevelDisplayText);
    }

    public void CheckTriggerType()
    {
        if (talentTree.availableSpendingPoints >= 1)
        {
        this.ValidateNodeChange(1);
        } else if (talentTree.availableSpendingPoints != 0)
        {
        this.ValidateNodeChange(-1);
        }
        else
        {
            Debug.Log("Insufficient Talent Points Avaikable.");
        }
    }

    public void ValidateNodeChange(int pointToSpend)
    {
        //If we don't have enough points, just stop and display message to inform player.
        if (pointToSpend != 0 == true)//If we have points to spend
        {
            Debug.Log("Applying available talent poiont to " + this.nodeInfo.name);
            if (pointToSpend == -1)
            {
                if (nodeInfo.abilityCurrentLevel > 0)
                {
                    this.DecreaseTalentLevel();
                }
            }
            else if (pointToSpend >= 1)
            {
                if (nodeInfo.abilityCurrentLevel < this.nodeInfo.abilityMaxLevel)
                {
                    this.IncreaseTalentLevel();
                }
            }
        }
        else if (pointToSpend == 0 == false)
        {
            Debug.Log("Insufficient spendable talent points available.");
        }
    }

    public void InitializeTalentLevelDisplay()
    {
        float w = GetComponent<RectTransform>().rect.width;
        float h = GetComponent<RectTransform>().rect.height;
        itemImageDimensionms.x = w;
        itemImageDimensionms.y = h;
        this.nodeGameObject.transform.position = this.transform.position;
    }

    //Responsible for updating the text GUI
    public void UpdateTalentLevelDisplay(TextMeshProUGUI currentTextToUpdate)
    {
        currentTextToUpdate.text = this.nodeInfo.abilityCurrentLevel.ToString();
    }

    //Remove item from the stack when one is used, lost, sold, etc. If it's the only one left in the stack, remove that stack.
    public void DecreaseTalentLevel()
    {
        if (this.nodeInfo.abilityCurrentLevel <= 0)
        {
            Debug.Log("Cannot reduce current talent level, current level is less than 1.");
        } else if (this.nodeInfo.abilityCurrentLevel > 0)
        {
            this.nodeInfo.abilityCurrentLevel--;
            talentTree.availableSpendingPoints++;
        }
        this.UpdateTalentLevelDisplay(talentLevelDisplayText);
    }

    //Add item to the stack when a new one is aquired.
    public void IncreaseTalentLevel()
    {
        if (nodeInfo.abilityCurrentLevel == nodeInfo.abilityMaxLevel)
        {
            Debug.Log("Cannot increase current talent level, current level is already at the maximum level.");
        }
        else if (nodeInfo.abilityCurrentLevel < nodeInfo.abilityMaxLevel)
        {
            this.nodeInfo.abilityCurrentLevel++;
            talentTree.availableSpendingPoints--;
        }
        this.UpdateTalentLevelDisplay(talentLevelDisplayText);
    }
}
