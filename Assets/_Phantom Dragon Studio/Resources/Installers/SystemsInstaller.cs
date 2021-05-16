using UnityEngine;
using Zenject;

public class SystemsInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject gameSettingsManager;

    [SerializeField]
    private GameObject levelManager;

    public override void InstallBindings()
    {
        Container.Bind<LevelHandler>().FromComponentOn(levelManager).AsCached().NonLazy();

        Container.Bind<IGameManager>().To<GameSettingsManager>().FromComponentOn(gameSettingsManager).AsCached().NonLazy();

        Container.Bind<IExperienceCalculator>().To<ExperienceCalculator>().AsCached().NonLazy();

        Container.Bind<ICalculations>().To<Calculations>().AsCached().NonLazy();

        Container.Bind<IConditions>().To<Conditions>().AsCached().NonLazy();
    }
}