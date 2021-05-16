using Zenject;
using UnityEngine;

public class ExperienceCalculator : IExperienceCalculator, IInitializable
{
    private IGameManager _gameManager;
    [Inject]
    public void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Initialize()
    {
        CalcEXP();
    }

    [HideInInspector]
    public float[] experienceRequiredAtLevel = new float[50];     //Number of Levels In the Game.

    private void CalcEXP()
    {
        for (int i = 0; i < experienceRequiredAtLevel.Length; i++)
        {
            experienceRequiredAtLevel[i] += 2 * _gameManager.GameSettings.baseExperiencePerLevel * (i * _gameManager.GameSettings.levelModifier);
        }
    }

    public void UpdateEarnedExperience(float experience, CharacterClass _characterInformation)
    {
        if (experience <= 0)
        {
            _characterInformation.totalExperience -= experience;
        }
        else if (experience > 0)
        {
            _characterInformation.totalExperience += experience;
        }
        UpdateTotalLevel(_characterInformation); //TODO----This needs to be done outside of combat in order to avoid bugs at this time.
        //If done inside combat, when the stat _calculations are performed, and the attributes wear off it causes negative values.
    }

    private void UpdateTotalLevel(CharacterClass _characterInformation)
    {
        Debug.Log("Level Check Engaged");
        for (int i = 0; i < experienceRequiredAtLevel.Length; i++)
        {
            if (_characterInformation.totalExperience >= experienceRequiredAtLevel[i])
            {
                _characterInformation.characterLevel = i + 1;
            }
        }
        _characterInformation.CalculatePrimaryAttributes();
    }
}
