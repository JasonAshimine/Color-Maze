using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Variable;

public class VolumeScript : MonoBehaviour
{
    [SerializeField]
    private SoundDataSet _soundData;

    [SerializeField]
    private Slider _slider;

    void Start()
    {
        _slider.value = _soundData.musicLevel.volume;
    }

    public void HandleVolumeChange(float value)
    {
        _soundData.SetMusicVolume(value);
    }
}
