// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<EventListener> eventListeners = 
            new List<EventListener>();

        public void Raise()
        {
            for(int i = eventListeners.Count -1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(EventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(EventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}