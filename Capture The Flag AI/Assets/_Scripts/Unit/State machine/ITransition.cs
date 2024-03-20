using System;

public interface ITransition
{
    IState TargetState { get; }
    IPredicate Condition { get; }
}