using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Variable;

public class SoundMenuScript : MonoBehaviour
{
    [SerializeField] private SoundDataSet _soundData;
    [SerializeField] private List<Button> _buttons;

    [SerializeField] private ColorDirection _colorData;

    // Start is called before the first frame update
    void Start()
    {
        SetupEvent();
    }

    public void HandleColorChangeEvent(object o)
    {
        updateImages();
    }


    private void SetupEvent()
    {
        for(int i = 0; i < _buttons.Count; i++)
        {
            int index = i;
            _buttons[i].onClick.AddListener(() => handleClick(index));
        }

        updateImages();
    }

    private void handleClick(int i)
    {
        _soundData.menuIndex = i;
        _soundData.Raise(SoundEventType.Music);
        updateImages();
    }

    private void updateImages()
    {
        for (int index = 0; index < _buttons.Count; index++)
        {
            float intensity = (index == _soundData.menuIndex) ? 1f : 0.5f;
            Image image = _buttons[index].GetComponent<Image>();
            Color color = _colorData.GetColorAll(index).color;
            color.a = intensity;
            image.color = color;
        }
    }
}
