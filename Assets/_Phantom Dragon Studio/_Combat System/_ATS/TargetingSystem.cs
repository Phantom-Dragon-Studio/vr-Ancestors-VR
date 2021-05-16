using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem {

    //SelfTarget - No targeting Necessary, automatically targets the caster.
    //SingleTarget - 1 Attacker, 1 Defender. Used for NPC Attacks & Player Spells
    //Multi-Target - Gets the next target within range of its last target. (Think Chain lighting)
    //AoE - Gets everything withing range of caster either friendlies or enemies. (Think Expolosions)
}
