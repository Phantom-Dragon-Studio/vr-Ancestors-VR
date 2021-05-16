public abstract class State : IState
{
    abstract public void OnStateEnter();

    abstract public void OnStateExit();

    public void Tick() { }
}
