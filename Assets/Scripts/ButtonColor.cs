using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ButtonColor But;

    public void OnPointerEnter(PointerEventData eventData)
    {
        But.GetComponent<Image>().color = Color.grey;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        But.GetComponent<Image>().color = Color.white;
    }
}
