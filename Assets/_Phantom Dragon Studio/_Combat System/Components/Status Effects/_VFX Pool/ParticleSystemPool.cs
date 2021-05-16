using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPool : MonoBehaviour
{

    [SerializeField] private VFXTable particleSystemTable;
    [SerializeField] private ParticleSystem particleSystemToSpawn;
    [SerializeField] private Dictionary<ElementType, List<ParticleSystem>> pools = new Dictionary<ElementType, List<ParticleSystem>>();

    void Awake()
    {
        pools.Add(ElementType.None, new List<ParticleSystem>());
        pools.Add(ElementType.Fire, new List<ParticleSystem>());
        pools.Add(ElementType.Wind, new List<ParticleSystem>());
        pools.Add(ElementType.Earth, new List<ParticleSystem>());
        pools.Add(ElementType.Divine, new List<ParticleSystem>());
        pools.Add(ElementType.Dark, new List<ParticleSystem>());
        pools.Add(ElementType.Arcane, new List<ParticleSystem>());
        pools.Add(ElementType.Lightning, new List<ParticleSystem>());
        pools.Add(ElementType.Water, new List<ParticleSystem>());

        InitializePools(ElementType.Fire);
        InitializePools(ElementType.Wind);
        InitializePools(ElementType.Earth);
        InitializePools(ElementType.Divine);
        InitializePools(ElementType.Dark);
        InitializePools(ElementType.Arcane);
        InitializePools(ElementType.Lightning);
        InitializePools(ElementType.Water);


    }

    private void InitializePools(ElementType triggeringElement)
    {
        for (int i = 0; i < 20; i++)
        {
            particleSystemToSpawn = SpawnTypeCheck(triggeringElement);
            ReturnVFXToPool(particleSystemToSpawn);
            pools[triggeringElement].Add(particleSystemToSpawn);
        }
    }

    public ParticleSystem GetVFX(ElementType triggeringElement)
    {
        for (int i = 0; i < pools[triggeringElement].Count; i++)
        {
            if (pools[triggeringElement][i].gameObject.activeSelf == false) //If the obj is not active, it must be available to use.
            {
                return pools[triggeringElement][i];
            }
            else if (pools[triggeringElement][i].gameObject.activeSelf == true) //Nothing eligible found, increment and try again.
            {
                continue;
            }
        }

        return SpawnTypeCheck(triggeringElement);
    }

    public void ReturnVFXToPool(ParticleSystem usedParticleEffect)
    {
        if (usedParticleEffect != null)
        {
            usedParticleEffect.Stop();
            usedParticleEffect.gameObject.transform.SetParent(this.gameObject.transform);
            usedParticleEffect.transform.position = Vector3.zero;
            usedParticleEffect.gameObject.SetActive(false);
        }
    }

    public ParticleSystem SpawnTypeCheck(ElementType triggeringElement)
    {
        switch (triggeringElement) //Made it through the entire list, still found nothing... Extend pool...
        {
            case ElementType.None:
                {
                    particleSystemToSpawn = Instantiate(particleSystemTable.stunnedEffectVFX_ParticleSystem);
                    pools[triggeringElement].Add(particleSystemToSpawn);
                    return particleSystemToSpawn;
                }
            case ElementType.Fire:
                {
                    particleSystemToSpawn = Instantiate(particleSystemTable.burningEffectVFX_ParticleSystem);
                    pools[triggeringElement].Add(particleSystemToSpawn);
                    return particleSystemToSpawn;
                }
            case ElementType.Wind:
                {
                    particleSystemToSpawn = Instantiate(particleSystemTable.genericSlowingVFX_ParticleSystem);
                    pools[triggeringElement].Add(particleSystemToSpawn);
                    return particleSystemToSpawn;
                }
            case ElementType.Earth:
                {
                    particleSystemToSpawn = Instantiate(particleSystemTable.poisonEffectVFX_ParticleSystem);
                    pools[triggeringElement].Add(particleSystemToSpawn);
                    return particleSystemToSpawn;
                }
            case ElementType.Divine:
                {
                    particleSystemToSpawn = Instantiate(particleSystemTable.attributebuffEffectVFX_ParticleSystem);
                    pools[triggeringElement].Add(particleSystemToSpawn);
                    return particleSystemToSpawn;
                }
            case ElementType.Dark:
                {
                    particleSystemToSpawn = Instantiate(particleSystemTable.timedDeathVFX_ParticleSystem);
                    pools[triggeringElement].Add(particleSystemToSpawn);
                    return particleSystemToSpawn;
                }
            case ElementType.Arcane:
                {
                    particleSystemToSpawn = Instantiate(particleSystemTable.silencedEffectVFX_ParticleSystem);
                    pools[triggeringElement].Add(particleSystemToSpawn);
                    return particleSystemToSpawn;
                }
            case ElementType.Lightning:
                {
                    particleSystemToSpawn = Instantiate(particleSystemTable.staticChargeEffectVFX_ParticleSystem);
                    pools[triggeringElement].Add(particleSystemToSpawn);
                    return particleSystemToSpawn;
                }
            case ElementType.Water:
                {
                    particleSystemToSpawn = Instantiate(particleSystemTable.frozenEffectVFX_ParticleSystem);
                    pools[triggeringElement].Add(particleSystemToSpawn);
                    return particleSystemToSpawn;
                }
        }
        return null;
    }

}
