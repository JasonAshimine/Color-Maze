using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Events;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameEventData Event;
    [SerializeField] private int id;

    private Button _button;

    private void Start()
    {
        _button = gameObject.GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            Raise(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            Raise(false);
        }
    }

    private void Raise(bool toggle)
    {
        Event.Raise(new HoverData(id, toggle));
    }
}

struct HoverData 
{
    public HoverData(int id, bool toggle) 
    {
        this.id = id;
        this.toggle = toggle;
    }

    public int id;
    public bool toggle;
}

