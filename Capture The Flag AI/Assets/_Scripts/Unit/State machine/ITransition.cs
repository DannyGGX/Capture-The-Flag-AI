using System;

public interface ITransition
{
    IState TargetState { get; }
    
    /// <summary>
    /// The condition that must be met to transition to the target state
    /// </summary>
    ICondition Condition { get; }
}