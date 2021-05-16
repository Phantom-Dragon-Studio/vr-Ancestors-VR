using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New VFXTable", menuName = "Phantom Dragon Studios/VRCombatSystem/ParticleSystemTable", order = 3)]

[System.Serializable]
public class VFXTable : ScriptableObject
{
    [SerializeField] public ParticleSystem burningEffectVFX_ParticleSystem;
    [SerializeField] public ParticleSystem frozenEffectVFX_ParticleSystem;
    [SerializeField] public ParticleSystem poisonEffectVFX_ParticleSystem;
    [SerializeField] public ParticleSystem genericSlowingVFX_ParticleSystem;
    [SerializeField] public ParticleSystem stunnedEffectVFX_ParticleSystem;
    [SerializeField] public ParticleSystem staticChargeEffectVFX_ParticleSystem;
    [SerializeField] public ParticleSystem timedDeathVFX_ParticleSystem;
    [SerializeField] public ParticleSystem silencedEffectVFX_ParticleSystem;
    [SerializeField] public ParticleSystem attributebuffEffectVFX_ParticleSystem;

}
