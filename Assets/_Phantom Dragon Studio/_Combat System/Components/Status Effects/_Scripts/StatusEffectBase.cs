using Zenject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffectBase : MonoBehaviour
{
    private IGameManager _gameManager;
    [Inject]
    public void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    [SerializeField] public float durationPerStack = 5;
    [SerializeField] public int currentStackCount = 1;
    [SerializeField] public int maximumStackCount = 5;
    [SerializeField] public ElementType myElementType;

    [SerializeField] protected ParticleSystemPool VFX_Pool;
    [SerializeField] protected ParticleSystem VFX;

    [HideInInspector]
    protected CharacterStats characterInformation;

    [SerializeField] protected WaitForSeconds coroutineTimer;

    protected virtual void Awake()
    {
        characterInformation = this.GetComponent<CharacterStats>();
        coroutineTimer = new WaitForSeconds(this.durationPerStack);
        VFX_Pool = FindObjectOfType<ParticleSystemPool>();

        ApplyEffect();
    }

    protected virtual void ApplyEffect()
    {
        this.VFX = VFX_Pool.GetVFX(this.myElementType);
        this.ResetVFX();
        this.IncrementStackCount(1);
        this.TickCoroutineStart();
    }


    protected void IncrementStackCount(int numberofStacks)
    {
        if(currentStackCount != maximumStackCount)
        {
            this.currentStackCount += numberofStacks;
        }
    }

    protected void TickCoroutineStart()
    {
        this.StartCoroutine(this.TickCoroutine());
    }

    private IEnumerator TickCoroutine()
    {
        while (_gameManager.IsGamePaused == false)
        {
            yield return coroutineTimer;
            this.TickActions();
        }
    }

    protected virtual void TickActions()
    {
        this.DecrementStackCount(1);
    }

    protected void DecrementStackCount(int numberofStacks)
    {
        currentStackCount -= numberofStacks;
        if (this.currentStackCount <= 0)
        {
            StopCoroutine(this.TickCoroutine());
            this.RemoveEffect();
        }
    }

    protected virtual void RemoveEffect()
    {
        VFX_Pool.ReturnVFXToPool(this.VFX);
        Destroy(this);
    }

    protected virtual void ResetVFX()
    {
        Debug.Log("Reseting VFX....");
        if(VFX != null)
        {
            this.VFX.gameObject.SetActive(true);
            this.VFX.gameObject.transform.SetParent(this.transform);
            this.VFX.gameObject.transform.position = this.transform.position;
            this.VFX.Play();
        }
    }
}
