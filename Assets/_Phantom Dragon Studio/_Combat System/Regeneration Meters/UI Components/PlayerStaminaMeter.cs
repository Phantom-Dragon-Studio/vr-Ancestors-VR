using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaMeter : MonoBehaviour {

    public TextMeshProUGUI staminaText;

    Image staminaBarImage;
    float staminaMeterValue;
    CharacterClass mainCharacter;
    CharacterStats mainCharacterstats;

    

	// Use this for initialization
	void Awake () {
        staminaBarImage = this.GetComponent<Image>();
        mainCharacter = FindObjectOfType<CharacterClass>();
        mainCharacterstats = mainCharacter.GetComponent<CharacterStats>();
        mainCharacterstats.PlayerStaminaValueChanged += UpdateStaminaDisplay;
    }

    private void UpdateStaminaDisplay(float currentCharacterStamina, float maximumCharacterStamina)
    {
        this.staminaMeterValue = currentCharacterStamina / maximumCharacterStamina;
        this.staminaBarImage.fillAmount = this.staminaMeterValue;
        staminaText.text = Math.Round(mainCharacterstats.characterData.currentStamina) + " / " + Math.Round(mainCharacterstats.characterData.maximumStamina);
    }

    public void ChangeDisplayuBarStyle(_WorldObjectData characterMeterToDisplay)
    {
        this.staminaBarImage.sprite = characterMeterToDisplay.healthBar;
    }
}
