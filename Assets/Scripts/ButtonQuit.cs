﻿
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonQuit : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}
