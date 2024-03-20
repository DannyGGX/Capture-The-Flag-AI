using System;


/// <summary>
/// For functions that return a bool. Like an if statement
/// </summary>
public class FuncPredicate : IPredicate
{
    private readonly Func<bool> func;

    public FuncPredicate(Func<bool> func)
    {
        this.func = func;
    }

    public bool Evaluate()
    {
        func.Invoke();
        return true;
    }
}