using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public GameObject Manager;
    public int State = 0;
    public int CardValue;
    public bool Initialized = false;

    private Sprite _cardBack;
    private Sprite _cardFront;
    private float _waitTime = 1.0f;

    public void setupGraphics()
    {
        _cardBack = Manager.GetComponent<GameManager>().cardBack;
        _cardFront = Manager.GetComponent<GameManager>().cardFaces[CardValue];

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

    void flipCard()
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
        if (!Manager.GetComponent<GameManager>().DND && State == 0)
        {
            flipCard();
            Manager.GetComponent<GameManager>().ClickedCounter++;
            if (Manager.GetComponent<GameManager>().ClickedCounter == Manager.GetComponent<GameManager>().MatchSize)
            {
                StartCoroutine(pause());
            }
        }
    }

    IEnumerator pause()
    {
        Manager.GetComponent<GameManager>().DND = true;
        yield return new WaitForSeconds(_waitTime);
        Manager.GetComponent<GameManager>().CheckCards();
        Manager.GetComponent<GameManager>().ClickedCounter = 0;
        Manager.GetComponent<GameManager>().DND = false;
    }
}
