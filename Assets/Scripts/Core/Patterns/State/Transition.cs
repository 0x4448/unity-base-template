using System;

/// <summary>
/// Represents the requirements for a state change.
/// </summary>
/// <remarks>
/// All conditions must be true to change state.
/// </remarks>
public class Transition
{
    /// <summary>
    /// The current state.
    /// </summary>
    public IState From { get; }

    /// <summary>
    /// The new state.
    /// </summary>
    public IState To { get; }

    /// <summary>
    /// Change to the new state if the condition(s) evaluates to true.
    /// </summary>
    /// <remarks>
    /// Implemented as an array for improved performance.
    /// </remarks>
    public Func<bool>[] Conditions { get; private set; }

    /// <inheritdoc cref="Transition"/>
    /// <param name="from"><inheritdoc cref="From" path="/summary"/></param>
    /// <param name="to"><inheritdoc cref="To" path="/summary"/></param>
    public Transition(IState from, IState to)
    {
        From = from;
        To = to;
    }

    /// <inheritdoc cref="Transition(IState, IState)"/>
    /// <param name="conditions"><inheritdoc cref="Conditions" path="/summary"/></param>
    public Transition(IState from, IState to, Func<bool> conditions)
    {
        From = from;
        To = to;
        Conditions = new Func<bool>[] { conditions };
    }

    /// <inheritdoc cref="Transition(IState, IState)"/>
    /// <param name="conditions"><inheritdoc cref="Conditions" path="/summary"/></param>
    public Transition(IState from, IState to, params Func<bool>[] conditions)
    {
        From = from;
        To = to;
        Conditions = conditions;
    }

    // Fluent interface for adding conditions for more legible code.
    /// <summary>
    /// Add a condition to the transition.
    /// <inheritdoc cref="Transition" path="/remarks"/>
    /// </summary>
    /// <param name="condition"><inheritdoc cref="Conditions" path="/summary"/></param>
    public Transition When(Func<bool> condition)
    {
        if (Conditions is null)
        {
            Conditions = new Func<bool>[] { condition };
        }
        else
        {
            // Extend the array with the new condition.
            var conditions = new Func<bool>[Conditions.Length + 1];
            Conditions.CopyTo(conditions, 0);
            conditions[^1] = condition;
            Conditions = conditions;
        }
        return this;
    }
}
