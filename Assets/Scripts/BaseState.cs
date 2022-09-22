public abstract class BaseState
{
    protected PlayerController Character;
    protected StateMachine StateMachine;

    protected BaseState(PlayerController character, StateMachine stateMachine)
    {
        Character = character;
        StateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void Exit() { }
}
