using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events { 
    public abstract class  EventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;
        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public virtual void OnEventRaised()
        {

        }
    }
}
