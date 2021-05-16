using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class AllianceAffiliation : MonoBehaviour
{
    CharacterStats characterStats;

    void Awake()
    {
        characterStats = this.GetComponent<CharacterStats>();

    }
}
