using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public GameObject Manager;
    public GameObject Container;
    public int State = 0;
    public int CardValue;
    public bool Initialized = false;

    private Sprite _cardBack;
    private Sprite _cardFront;

    public void setupGraphics()
    {
        _cardBack = Manager.GetComponent<GameManager>().getCardBack();
        _cardFront = Manager.GetComponent<GameManager>().getCardFace(CardValue);

        GetComponent<Image>().sprite = _cardBack;
    }

    public void updateGraphics()
    {
        if(State == 0)
        {
            GetComponent<Image>().sprite = _cardBack;
        }
        else if(State == 1)
        {
            GetComponent<Image>().sprite = _cardFront;
        }
    }

    public void flipCard()
    {
        if (State == 0)
        {
            State = 1;
        }
        else if (State == 1)
        {
            State = 0;
        }
        updateGraphics();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Container.GetComponent<ValuesContener>().DND && State != 1)
        {
            flipCard();
            Container.GetComponent<ValuesContener>().ClickedCounter++;
            if (Container.GetComponent<ValuesContener>().ClickedCounter == 2)
            {
                StartCoroutine(pause());
            }
        }
    }

    IEnumerator pause()
    {
        Container.GetComponent<ValuesContener>().DND = true;
        yield return new WaitForSeconds(3.0f);
        Manager.GetComponent<GameManager>().CheckCards();
        Container.GetComponent<ValuesContener>().ClickedCounter = 0;
        Container.GetComponent<ValuesContener>().DND = false;
    }
}
