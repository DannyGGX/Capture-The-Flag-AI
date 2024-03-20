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
        //no op
    }
    public virtual void Update()
    {
        //no op
    }
    public virtual void FixedUpdate()
    {
        //no op
    }
    public virtual void OnExit()
    {
        //no op
    }
}