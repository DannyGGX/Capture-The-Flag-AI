using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private StateNode currentNode;
    private Dictionary<System.Type, StateNode> nodes = new();
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
    
    public void CheckAndHandleTransition()
    {
        var transition = GetTransition();

        if (transition != null)
        {
            ChangeState(transition.TargetState);
        }
    }
    
    public void SetState(IState state) // starting state
    {
        currentNode = nodes[state.GetType()];
        currentNode.State?.OnEnter();
    }

    public void ChangeState(IState targetState)
    {
        if (targetState == currentNode.State) return;
        
        var previousState = currentNode.State;
        var nextState = nodes[targetState.GetType()].State;
        previousState?.OnExit();
        nextState?.OnEnter();
        currentNode = nodes[targetState.GetType()];
    }
    
    public ITransition GetTransition()
    {
        foreach (var transition in anyTransitions)
        {
            if (transition.Condition.Evaluate())
            {
                return transition;
            }
        }

        foreach (var transition in currentNode.Transitions)
        {
            if (transition.Condition.Evaluate())
            {
                return transition;
            }
        }

        return null;
    }
    /// <summary>
    /// For transitions that happen between specific states
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="condition"></param>
    public void AddTransition(IState from, IState to, ICondition condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }
    
    /// <summary>
    /// For states that transition to any state
    /// </summary>
    /// <param name="to"></param>
    /// <param name="condition"></param>
    public void AddAnyTransition(IState to, ICondition condition)
    {
        anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }

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
    
    public class StateNode
    {
        public IState State;
        public HashSet<ITransition> Transitions { get; }

        public StateNode(IState state)
        {
            this.State = state;
            this.Transitions = new HashSet<ITransition>();
        }
        public void AddTransition(IState targetState, ICondition condition)
        {
            Transitions.Add(new Transition(targetState, condition));
        }

    }
}
