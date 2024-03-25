
public class Transition : ITransition
{
    public IState TargetState { get; }
    public ICondition Condition { get; }

    public Transition(IState targetState, ICondition condition)
    {
        TargetState = targetState;
        Condition = condition;
    }
}