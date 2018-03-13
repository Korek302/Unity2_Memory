using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject Manager;
    public int State;
    public int CardValue;
    public bool Initialized = false;

    private Sprite _cardBack;
    private Sprite _cardFront;

    private void Start()
    {
        State = 0;
    }

    public void setupGraphics()
    {
        _cardBack = Manager.GetComponent<GameManager>().getCardBack();
        _cardFront = Manager.GetComponent<GameManager>().getCardFace(CardValue);

        GetComponent<Image>().sprite = _cardBack;
    }

    public void setGraphics()
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
        if (!GameManager.DND)
        {
            if (State == 0)
            {
                State = 1;
            }
            else if (State == 1)
            {
                State = 0;
            }

            if (State == 0)
            {
                GetComponent<Image>().sprite = _cardBack;
            }
            else if (State == 1)
            {
                GetComponent<Image>().sprite = _cardFront;
            }
        }
    }
}
