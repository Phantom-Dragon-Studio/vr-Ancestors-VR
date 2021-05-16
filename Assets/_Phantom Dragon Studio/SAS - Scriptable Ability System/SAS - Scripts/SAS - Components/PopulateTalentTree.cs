using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateTalentTree : MonoBehaviour {
    public List<_AbilityData> talentTreeNodes;
    public int availableSpendingPoints;

    private AbilityCollection collectionOfAbilities;
    private TalentNode selectedNode;
    private TalentNode[] nodesInCollection;

    //Initialization
    void Awake() {
        availableSpendingPoints = 17;
        nodesInCollection = GetComponentsInChildren<TalentNode>();
        talentTreeNodes = new List<_AbilityData>();

        //Itterate through collection of TalentNodes in TalentTree and add them (in ascending order) to the TalentTree's dictionary for organization and accessibility.
        for (int i = 0; i < nodesInCollection.Length; i++)
        {
            selectedNode = nodesInCollection[i];
            if (selectedNode.nodeInfo != null) talentTreeNodes.Add(selectedNode.nodeInfo);             
            talentTreeNodes.Sort((talentTreeNodes, selectedNode) => talentTreeNodes.abilityID.CompareTo(selectedNode.abilityID));
        }


        ////TEST BLOCK FOR PRINTING ASSIGNED ABILITIES.
        //for (int k = 0; k < talentTreeNodes.Count; k++)
        //{
        //    Debug.Log(talentTreeNodes[k].abilityName);
        //}
        ////TEST BLOCK FOR PRINTING ASSIGNED ABILITIES.
    }
}
