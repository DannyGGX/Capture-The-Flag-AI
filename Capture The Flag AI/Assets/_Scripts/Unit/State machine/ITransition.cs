using System;

public interface ITransition
{
    IState TargetState { get; }
    ICondition Condition { get; }
}