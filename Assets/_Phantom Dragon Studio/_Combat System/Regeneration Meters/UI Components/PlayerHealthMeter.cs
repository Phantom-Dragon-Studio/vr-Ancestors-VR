using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthMeter : MonoBehaviour {

    public TextMeshProUGUI healthText;

    Image healthBarImage;
    float healthMeterValue;
    CharacterClass mainCharacter;
    CharacterStats mainCharacterstats;

    

	// Use this for initialization
	void Awake () {
        healthBarImage = this.GetComponent<Image>();
        mainCharacter = FindObjectOfType<CharacterClass>();
        mainCharacterstats = mainCharacter.GetComponent<CharacterStats>();
        mainCharacterstats.PlayerHealthValueChanged += UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay(float currentCharacterHealth, float maximumCharacterHealth)
    {
        this.healthMeterValue = currentCharacterHealth / maximumCharacterHealth;
        this.healthBarImage.fillAmount = this.healthMeterValue;
        healthText.text = Math.Round(mainCharacterstats.characterData.currentHealth) + " / " + Math.Round(mainCharacterstats.characterData.maximumHealth);
    }

    private void ChangeDisplayuBarStyle(Sprite characterMeterToDisplay)
    {
        this.healthBarImage.sprite = characterMeterToDisplay;
    }
}
