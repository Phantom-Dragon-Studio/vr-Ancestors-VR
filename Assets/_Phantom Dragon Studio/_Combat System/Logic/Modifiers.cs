using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierType
{
    HealthMultiplier,
    ManaOverTimeMultiplier,
    DamageOverTimeMultiplier,
    DamageBoostMultiplier,
    ManaBoostMultiplier,
    DefenseMultiplier,
    AttackSpeed,
    MovementSpeed,
}

public class Modifiers {

    public float percentageModifier = 0;

    // Use this for initialization
    void Awake () {
		
	}
}
