using System.Collections.Generic;

/// <summary>
/// A finite state machine for Unity.
/// </summary>
public class StateMachine
{
    public IState CurrentState { get; private set; }

    /// <summary>
    /// All the possible state transitions are evaluated in order.
    /// </summary>
    /// <remarks>
    /// Implemented as an array for improved performance.
    /// </remarks>
    private readonly Transition[] _transitions;


    /// <summary>
    /// Constructor for the state machine.
    /// </summary>
    /// <param name="initialState">The initial state of the object.</param>
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
        /*  Loop through each of the available transitions. If the from
            *  state is our cuurrent state, evaluate each of the conditions.
            *  All conditions must return true to change state.
            *  
            *  If the state can change, run OnStateExit, change the state, and
            *  run OnStateEnter and Update for the new state.
            */
        foreach (var transition in _transitions)
        {
            if (transition.From == CurrentState)
            {
                var change = true;
                foreach (var condition in transition.Conditions)
                {
                    if (!condition())
                    {
                        change = false;
                        break;
                    }
                }

                if (change)
                {
                    CurrentState.OnStateExit();
                    CurrentState = transition.To;
                    CurrentState.OnStateEnter();
                    break;
                }
            }
        }

        CurrentState.Update();
    }

    public void LateUpdate()
    {
        CurrentState.LateUpdate();
    }
}
