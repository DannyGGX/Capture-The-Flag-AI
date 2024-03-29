using System.Collections.Generic;

/// <summary>
/// Finite state machine that supports transitions from any state and can handle transitions internally.
/// To use it: declare states, then define transitions between them and finally call SetInitialState
/// States can be transitioned to from within other state classes if the state machine and the to-state is passed in the state constructor.
/// Con: transitions are checked very often for every state, which can be computationally expensive depending on the conditions
/// </summary>
public class StateMachine
{
    private StateNode currentNode; // for the active state
    
    /// <summary>
    /// each node is a different child class of IState.
    /// The class (Type) is used to differentiate between nodes
    /// </summary>
    private Dictionary<System.Type, StateNode> nodes = new();
    
    /// <summary>
    /// Transitions from any state
    /// </summary>
    private HashSet<ITransition> anyTransitions = new();
    
    public void Update()
    {
        currentNode.State?.Update();
    }

    public void FixedUpdate()
    {
        CheckAndHandleTransition(); // can be put into Update instead
        currentNode.State.FixedUpdate();
    }
    
    private void CheckAndHandleTransition()
    {
        ITransition transition = GetTransition();
        
        if (transition != null)
        {
            ChangeState(transition.TargetState);
        }
    }
    
    public void SetInitialState(IState state)
    {
        currentNode = nodes[state.GetType()];
        currentNode.State?.OnEnter();
    }
    
    /// <summary>
    /// Other classes can call this to change the state
    /// </summary>
    public void ChangeState(IState targetState)
    {
        if (targetState == currentNode.State) return;
        
        currentNode.State?.OnExit();
        currentNode = nodes[targetState.GetType()];
        currentNode.State?.OnEnter();
    }
    
    public IState GetCurrentState()
    {
        return currentNode.State;
    }
    
    private ITransition GetTransition()
    {
        // check the anyTransitions first
        // The order that transitions are added matter
        foreach (var transition in anyTransitions)
        {
            if (transition.Condition.Evaluate())
            {
                return transition;
            }
        }

        foreach (var transition in currentNode.SpecificTransitions)
        {
            if (transition.Condition.Evaluate())
            {
                return transition;
            }
        }

        return null; // no transition condition met
    }
    /// <summary>
    /// For transitions that happen between specific states
    /// </summary>
    public void AddSpecificTransition(IState from, IState to, ICondition condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }
    
    /// <summary>
    /// For states that can be transitioned to from any other state.
    /// Transitions that are declared before other transitions will be checked first.
    /// </summary>
    public void AddAnyTransition(IState to, ICondition condition)
    {
        anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }

    /// <summary>
    /// To add a state to the state machine without defining a transition from this state to another.
    /// The calling script will control the transitioning to another state.
    /// </summary>
    public void AddManualTransitionState(IState state)
    {
        GetOrAddNode(state);
    }

    /// <summary>
    /// Helper function that stops duplicate nodes from being created
    /// </summary>
    private StateNode GetOrAddNode(IState state)
    {
        StateNode node = nodes.GetValueOrDefault(state.GetType(), null); // null if not found

        if (node == null)
        {
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);
        }
        return node;
    }
    
    /// <summary>
    /// States are represented with nodes in order for them to be connected. Transitions connect them
    /// </summary>
    private class StateNode
    {
        public IState State;
        public HashSet<ITransition> SpecificTransitions { get; }

        public StateNode(IState state)
        {
            this.State = state;
            this.SpecificTransitions = new HashSet<ITransition>();
        }
        public void AddTransition(IState targetState, ICondition condition)
        {
            SpecificTransitions.Add(new Transition(targetState, condition));
        }
    }
}
