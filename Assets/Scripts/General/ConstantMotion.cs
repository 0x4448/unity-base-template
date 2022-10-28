using UnityEngine;
using UnityEngine.Events;

namespace UBT
{
    public class ConstantMotion : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private Vector3 _velocity;
        [SerializeField] private Vector3 _angularVelocity;

        [Header("Expiration")]
        [SerializeField] private bool _expires;
        [SerializeField] private float _timeToLive;

        [Header("Events")]
        [SerializeField] private UnityEvent _onDisable;

        private float _timeAlive;

        private void Update()
        {
            transform.Translate(_velocity);
            transform.Rotate(_angularVelocity * Time.deltaTime);

            if (_expires)
            {
                _timeAlive += Time.deltaTime;
                if (_timeAlive > _timeToLive)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnDisable()
        {
            _onDisable?.Invoke();
        }
    }
}
