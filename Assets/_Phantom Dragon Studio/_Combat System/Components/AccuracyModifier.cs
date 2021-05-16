using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageSeverity
{
    Reduced,
    Normal,
    Increased,
    Critical
}


public class AccuracyModifier : MonoBehaviour {

    public DamageSeverity damageSeverity;
    public float accuracyDamageModifier;

	// Use this for initialization
	void Start () {
        switch (damageSeverity)
        {
            case DamageSeverity.Reduced:
                {
                    accuracyDamageModifier = 0.5f;
                    break;
                }
            case DamageSeverity.Normal:
                {
                    accuracyDamageModifier = 1f;
                    break;
                }
            case DamageSeverity.Increased:
                {
                    accuracyDamageModifier = 1.25f;
                    break;
                }
            case DamageSeverity.Critical:
                {
                    accuracyDamageModifier = 1.50f;
                    break;
                }
        }
	}

    public float MyModifier
    {
        get { return accuracyDamageModifier; }
    }
}
