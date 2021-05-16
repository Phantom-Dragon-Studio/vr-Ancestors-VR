using Zenject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_E_FRegeneration : MonoBehaviour
{
    private IGameManager _gameManager;

    [Inject]
    public void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    WaitForSeconds M_E_FRegenerationInterval;

    void Awake()
    {
        M_E_FRegenerationInterval = new WaitForSeconds(Mathf.RoundToInt(_gameManager.GameSettings.M_E_FRegenerationInterval));
        StartCoroutine(M_E_FRegenerationCoroutine(this.GetComponent<CharacterStats>()));
    }

    IEnumerator M_E_FRegenerationCoroutine(CharacterStats regeneratingCharacter)
    {
        while (true)
        {
            while (_gameManager.IsGamePaused == false)
            {
                yield return M_E_FRegenerationInterval;
                if (regeneratingCharacter.characterData.isInCombat)
                {
                    regeneratingCharacter.RestoreM_E_F(regeneratingCharacter.characterData.M_E_FRegenerationAmount);
                }
                else
                {
                    regeneratingCharacter.RestoreM_E_F(regeneratingCharacter.characterData.M_E_FRegenerationAmount * _gameManager.GameSettings.outOfCombatRegenerationMultiplier);
                }
            }
            yield return null;
        }
    }

}
