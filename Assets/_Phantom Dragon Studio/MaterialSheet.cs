using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material Sheet", menuName = "Material Sheet", order = 0)]
public class MaterialSheet : ScriptableObject
{
    public Material loadingScreenMaterial;
    public Material nightMaterial;
    public Material morningMaterial;
    public Material dayMaterial;
    public Material rainingDayMaterial;
    public Material rainingNightMaterial;

}
