using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Variable
{
    [CreateAssetMenu(fileName = "FadeDataSet", menuName = "Data/Fade")]
    public class FadeDataSet : ScriptableObject
    {

        [SerializeField, Range(0f, 1f)]
        public float min = 0.2f;

        [SerializeField, Range(0f, 1f)]
        public float max = 1f;

        [Space()]

        public List<SpriteRenderer> renderList;
        public float target = 1f;

        public void Reset()
        {
            renderList.Clear();
            target = max;
        }
    }
}