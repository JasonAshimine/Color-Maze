using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;

namespace Variable
{
    [CreateAssetMenu(fileName = "StateDataSet", menuName = "Data/State")]
    public class StateDataSet : EventScriptableObject<GameStage>
    {
        public GameStage previous;
        public bool isCancel;

        public override void Raise(GameStage data)
        {
            if (data == state)
                return;

            previous = state;
            isCancel = false;

            base.Raise(data);
        }

        public void Cancel(GameStage type)
        {
            if(state != type)
            {
                return;
            }

            isCancel = true;

            base.Raise(type);
        }
    }
}