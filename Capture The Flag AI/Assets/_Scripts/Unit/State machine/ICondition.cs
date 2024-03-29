/// <summary>
/// Predicate is like a condition that can be evaluated
/// </summary>
public interface ICondition
{
    /// <summary>
    /// Check if the condition is met
    /// </summary>
    bool Evaluate();
}