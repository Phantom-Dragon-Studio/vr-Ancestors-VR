using Zenject;
using System.Collections;
using UnityEngine;

public class StaminaRegeneration : MonoBehaviour
{
    private IGameManager _gameManager;
    [Inject]
    public void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    WaitForSeconds staminaRegenerationInterval;

    private void Awake()
    {
        staminaRegenerationInterval = new WaitForSeconds(Mathf.RoundToInt(_gameManager.GameSettings.staminaRegenerationInterval));
        StartCoroutine(StaminaRegenerationCoroutine(this.GetComponent<CharacterStats>()));
    }

    IEnumerator StaminaRegenerationCoroutine(CharacterStats regeneratingCharacter)
    {
        while (true)
        {
            while (_gameManager.IsGamePaused == false)
            {
                yield return new WaitForSeconds(_gameManager.GameSettings.staminaRegenerationInterval);
                if (regeneratingCharacter.characterData.isInCombat)
                {
                    regeneratingCharacter.RestoreStamina(regeneratingCharacter.characterData.staminaRegenerationAmount);
                }
                else
                {
                    regeneratingCharacter.RestoreStamina(regeneratingCharacter.characterData.staminaRegenerationAmount * _gameManager.GameSettings.outOfCombatRegenerationMultiplier);
                }
            }
            yield return null;
        }
    }

}
