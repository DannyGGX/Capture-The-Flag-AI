/// <summary>
/// A base class for all states to inherit from
/// </summary>
public abstract class BaseState : IState
{
    //protected Animator Animator { get; set; }

    // protected BaseState()
    // {
    //     
    // }
    
    public virtual void OnEnter()
    {
    }
    public virtual void Update()
    {
    }
    public virtual void FixedUpdate()
    {
    }
    public virtual void OnExit()
    {
    }
}