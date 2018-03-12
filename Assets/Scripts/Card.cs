using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public static bool DND = false;

    public GameObject Manager;
    public int State;
    public int CardValue;
    public bool Initialized = false;

    private Sprite _cardBack;
    private Sprite _cardFront;

    private GameObject _manager;

    private void Start()
    {
        State = 1;
        Manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void setupGraphics()
    {
        _cardBack = Manager.GetComponent<GameManager>().getCardBack();
        _cardFront = Manager.GetComponent<GameManager>().getCardFace(CardValue);

        flipCard();
    }
    public void flipCard()
    {
        if(State == 0)
        {
            State = 1;
        }
        else if(State == 1)
        {
            State = 0;
        }

        if (State == 0 && !DND)
        {
            GetComponent<Image>().sprite = _cardBack;
        }
        else if (State == 1 && !DND)
        {
            GetComponent<Image>().sprite = _cardFront;
        }
    }

    public int cardValue
    {
        get { return CardValue; }
        set { CardValue = value; }
    }
    
    public int state
    {
        get { return State; }
        set { State = value; }
    }

    public bool initialized
    {
        get { return Initialized; }
        set { Initialized = value; }
    }

    public void falseCheck()
    {
        StartCoroutine(pause());
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(1);
        if(State == 0)
        {
            GetComponent<Image>().sprite = _cardBack;
        }
        else if(State == 1)
        {
            GetComponent<Image>().sprite = _cardFront;
        }
        DND = false;
    }

}
