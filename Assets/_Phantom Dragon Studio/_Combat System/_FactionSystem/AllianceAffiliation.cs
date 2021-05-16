using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class FactionAffiliation : MonoBehaviour
{
    CharacterStats characterStats;

    void Awake()
    {
        characterStats = this.GetComponent<CharacterStats>();

        //Dictionary to track faction affiliation.
        //
    }
}
