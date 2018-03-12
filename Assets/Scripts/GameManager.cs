using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text matchText;

    private bool _init = false;
    private int _matches = 8;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!_init)
        {
            initializeCards();
        }

        if(Input.GetMouseButton(0))
        {
            checkCards();
        }
	}

    void initializeCards()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 1; j < 9; j++)
            {
                bool test = false;
                int choice = 0;
                while(!test)
                {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<Card>().initialized);
                }
                cards[choice].GetComponent<Card>().cardValue = j;
                cards[choice].GetComponent<Card>().initialized = true;
            }
        }
        foreach(GameObject c in cards)
        {
            c.GetComponent<Card>().setupGraphics();
        }
        if(!_init)
        {
            _init = true;
        }
    }
    public Sprite getCardBack()
    {
        return cardBack;
    }

    public Sprite getCardFace(int i)
    {
        return cardFace[i - 1];
    }

    void checkCards()
    {
        List<int> c = new List<int>();

        for(int i = 0; i < cards.Length; i++)
        {
            if(cards[i].GetComponent<Card>().state == 1)
            {
                c.Add(i);
            }
            if(c.Count == 2)
            {
                cardComparison(c);
            }
        }
    }

    void cardComparison(List<int> c)
    {
        Card.DND = true;

        int x = 0;

        if(cards[c[0]].GetComponent<Card>().cardValue == cards[c[1]].GetComponent<Card>().cardValue)
        {
            x = 2;
            _matches--;
            if(_matches == 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        for(int i = 0; i < c.Count; i++)
        {
            cards[c[i]].GetComponent<Card>().state = x;
            cards[c[i]].GetComponent<Card>().falseCheck();
        }
    }
}
