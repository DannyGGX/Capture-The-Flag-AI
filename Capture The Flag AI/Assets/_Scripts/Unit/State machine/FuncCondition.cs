using System;

/// <summary>
/// For functions that return a bool. Like the parameter of an if statement
/// </summary>
public class FuncCondition : ICondition
{
    private readonly Func<bool> func;

    public FuncCondition(Func<bool> func)
    {
        this.func = func;
    }

    public bool Evaluate()
    {
        return func.Invoke();
    }
}