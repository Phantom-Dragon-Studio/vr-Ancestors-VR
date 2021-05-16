using Zenject;

public interface IGameManager
{
    GameSettings GameSettings { get; }
    bool IsGamePaused { get; }
}
