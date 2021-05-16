using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Collection", menuName = "Phantom Dragon Studios/Scriptable Ability System/Ability Collection", order = 1)]
public class AbilityCollection : ScriptableObject {

    public List<_AbilityData> abilities = new List<_AbilityData>();
}
