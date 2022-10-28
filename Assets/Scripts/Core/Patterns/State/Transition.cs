using System;

/// <summary>
/// Represents the requirements for a state change.
/// </summary>
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
    /// Change to the new state if the conditions evaluates to true.
    /// </summary>
    public Func<bool> Condition { get; }

    /// <inheritdoc cref="Transition"/>
    /// <param name="from"><inheritdoc cref="From" path="/summary"/></param>
    /// <param name="to"><inheritdoc cref="To" path="/summary"/></param>
    /// <param name="condition"><inheritdoc cref="Condition" path="/summary"/></param>
    public Transition(IState from, IState to, Func<bool> condition)
    {
        From = from;
        To = to;
        Condition = condition;
    }
}
