using UnityEngine;
using Game.Events;

namespace Variable
{
    [CreateAssetMenu(fileName = "Color", menuName = "Variable/Color")]
    public class ColorVariable : ScriptableObject
    {
        [SerializeField]
        private Color _defaultValue;

        public Color Value { get; set; }
        public Color DefaultValue => _defaultValue;

        public void Awake()
        {
            Reset();
        }

        public void Reset()
        {
            Value = _defaultValue;
        }
    }
}

