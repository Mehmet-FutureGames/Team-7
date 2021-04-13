
public interface IState
{
    void Action();
}
public abstract class State : IState
{
    protected Character character;
    protected StateMachine stateMachine;

    protected State(Character character, StateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void NoteEventUpdate()
    {

    }

    public virtual void Action()
    {
        
    }
}

