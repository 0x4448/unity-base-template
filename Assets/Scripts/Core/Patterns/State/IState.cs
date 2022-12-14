/// <summary>
/// Defines properties and methods for a state.
/// </summary>
/// <remarks>
/// Represents the state of an individual object and executes code while
/// the object remains in the state.
/// </remarks>
public interface IState
{
    /// <summary>
    /// Called by the state machine when entering a state.
    /// </summary>
    public void OnStateEnter();

    /// <summary>
    /// Called by the state machine when exiting a state.
    /// </summary>
    public void OnStateExit();

    /// <summary>
    /// Called by the state machine during Unity's FixedUpdate method.
    /// </summary>
    public void FixedUpdate();

    /// <summary>
    /// Called by the state machine during Unity's Update method.
    /// </summary>
    public void Update();

    /// <summary>
    /// Called by the state machine during Unity's LateUpdate method.
    /// </summary>
    public void LateUpdate();
}
