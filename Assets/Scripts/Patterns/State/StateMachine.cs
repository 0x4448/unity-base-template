using System.Collections.Generic;

/// <summary>
/// A finite state machine for Unity.
/// </summary>
public class StateMachine
{
    public IState CurrentState { get; private set; }

    /// <summary>
    /// All the possible state transitions evaluated in order.
    /// </summary>
    /// <remarks>
    /// Implemented as an array for improved performance.
    /// </remarks>
    private readonly Transition[] _transitions;


    /// <inheritdoc cref="StateMachine"/>
    /// <param name="initialState"></param>
    /// <param name="transitions"><inheritdoc cref="_transitions" path="/summary"/></param>
    public StateMachine(IState initialState, ICollection<Transition> transitions)
    {
        var index = 0;
        _transitions = new Transition[transitions.Count];
        foreach (var transition in transitions)
        {
            _transitions[index] = transition;
            index++;
        }

        CurrentState = initialState;
        CurrentState.OnStateEnter();
    }

    public void FixedUpdate()
    {
        CurrentState.FixedUpdate();
    }

    public void Update()
    {
        foreach (var transition in _transitions)
        {
            if (transition.From == CurrentState && transition.Condition())
            {
                CurrentState.OnStateExit();
                CurrentState = transition.To;
                CurrentState.OnStateEnter();
                break;
            }
        }

        CurrentState.Update();
    }

    public void LateUpdate()
    {
        CurrentState.LateUpdate();
    }
}
