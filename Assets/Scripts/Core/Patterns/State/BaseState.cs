/// <summary>
/// An empty implementation of the IState interface.
/// </summary>
/// <remarks>
/// <para>
/// An instance of this class represents the state of one GameObject.
/// </para>
/// <para>
/// Derived classes should implement a public constructor. The state's
/// dependencies should be passed in through the constructor.
/// </para>
/// <example>
/// If the GameObject needs to follow another GameObject when
/// entering this state, then pass in a NavMeshAgent instance.
/// </example>
/// </remarks>
public abstract class BaseState : IState
{
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public virtual void FixedUpdate() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }
}
