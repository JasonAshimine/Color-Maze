using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;

namespace Variable
{
    public abstract class EventScriptableObject<T> : ScriptableObject
    {
        [SerializeField]
        protected GameEventData _event;
        public T state;

        public virtual void Raise(T data)
        {
            state = data;
            _event.Raise(data);
        }
    }

    public abstract class EventToggleScriptableObject<T> : EventScriptableObject<T>
    {
        public bool toggle;
        public override void Raise(T data)
        {
            Raise(data, true);
        }

        public virtual void Raise(T data, bool toggle)
        {
            this.toggle = toggle;
            state = data;
            _event.Raise(new EventToggleData<T>(data, toggle));
        }
    }


    public struct EventToggleData<T>
    {
        public EventToggleData(T state, bool toggle)
        {
            this.state = state;
            this.toggle = toggle;
        }

        public T state;
        public bool toggle;
    }
}