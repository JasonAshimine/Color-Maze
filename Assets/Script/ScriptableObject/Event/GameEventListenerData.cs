using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    [System.Serializable]
    public class UnityEventData : UnityEvent<object>
    {
    }

    public class GameEventListenerData : MonoBehaviour
    {
        public GameEventData Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEventData Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }
        public void OnEventRaised(object data = null)
        {
            Response.Invoke(data);
        }
    }
}
