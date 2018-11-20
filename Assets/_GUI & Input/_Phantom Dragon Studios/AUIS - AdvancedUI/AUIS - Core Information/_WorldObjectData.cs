using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    None,
    RoyalGuardians,
    KanatiTribe,
    ElvenDisciples,
    DragonMawDestroyers,
    NordicChampions,
    UnknownAnomalies
}

[CreateAssetMenu(fileName = "New WorldObject Data Container", menuName = "Phantom Dragon Studios/Advanced UI System/WorldObject Data Container", order = 3)]
public class _WorldObjectData : ScriptableObject {

    public Transform            objectWorldLocation;
    public string               objectName;
    public Faction              objectFaction;
    public Sprite               objectHealthBar;
    public string               objectDescription;
    public int                  objectLevel;
    public float                objectDistance;
    public bool                 objectIsStatic;
}
