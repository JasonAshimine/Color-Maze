using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    [CreateAssetMenu]
    public class GameEventData : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameEventListenerData> eventListeners =
            new List<GameEventListenerData>();

        public void Raise(object data = null)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(data);
        }

        public void RegisterListener(GameEventListenerData listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListenerData listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}