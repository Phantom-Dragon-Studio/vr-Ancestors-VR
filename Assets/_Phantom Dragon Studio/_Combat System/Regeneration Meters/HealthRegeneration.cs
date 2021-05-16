using Zenject;
using System.Collections;
using UnityEngine;

public class HealthRegeneration : MonoBehaviour
{
    IGameManager _gameManager;

    [Inject]
    public void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    WaitForSeconds healthRegenerationInterval;

    void Awake()
    {
        healthRegenerationInterval = new WaitForSeconds(Mathf.RoundToInt(_gameManager.GameSettings.healthRegenerationInterval));
        StartCoroutine(HealthRegenerationCoroutine(this.GetComponent<CharacterStats>()));
    }

    IEnumerator HealthRegenerationCoroutine(CharacterStats regeneratingCharacter)
    {
        while (true)
        {
            while (_gameManager.IsGamePaused == false)
            {
                yield return healthRegenerationInterval;
                if (regeneratingCharacter.characterData.isInCombat)
                {
                    regeneratingCharacter.RestoreHealth(regeneratingCharacter.characterData.healthRegenerationAmount);
                }
                else
                {
                    regeneratingCharacter.RestoreHealth(regeneratingCharacter.characterData.healthRegenerationAmount * _gameManager.GameSettings.outOfCombatRegenerationMultiplier);
                }
            }
            yield return null;
        }
    }
}
