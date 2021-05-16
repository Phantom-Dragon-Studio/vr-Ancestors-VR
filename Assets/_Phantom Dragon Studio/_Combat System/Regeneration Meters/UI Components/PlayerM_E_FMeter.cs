using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerM_E_FMeter : MonoBehaviour {

    public TextMeshProUGUI M_E_Ftext;
    Image M_E_FMeterImage;
    float M_E_FMeterValue;
    CharacterClass mainCharacter;
    CharacterStats mainCharacterStats;

    

	// Use this for initialization
	void Awake () {
        M_E_FMeterImage = this.GetComponent<Image>();
        mainCharacter = FindObjectOfType<CharacterClass>();
        mainCharacterStats = mainCharacter.GetComponent<CharacterStats>();
        mainCharacterStats.PlayerM_E_FValueChanged += UpdateM_E_FDisplay;
    }

    private void UpdateM_E_FDisplay(float characterCurrentM_E_F, float chracterMaximumM_E_F)
    {
        this.M_E_FMeterValue = characterCurrentM_E_F / characterCurrentM_E_F;
        this.M_E_FMeterImage.fillAmount = this.M_E_FMeterValue;
        M_E_Ftext.text = Math.Round(mainCharacterStats.characterData.currentHealth) + " / " + Math.Round(mainCharacterStats.characterData.maximumHealth);
    }

    public void ChangeDisplayBarStyle(_WorldObjectData characterMeterToDisplay)
    {
        this.M_E_FMeterImage.sprite = characterMeterToDisplay.M_E_FBar;
    }
}
