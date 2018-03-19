using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public static bool DND = false;

    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text scoreText;

    private int _matches = 0;
    
	void Start ()
    {
		initializeCards();
	}

    void initializeCards()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < cards.Length/2; j++)
            {
                bool test = false;
                int choice = 0;
                while(!test)
                {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<Card>().Initialized);
                }
                cards[choice].GetComponent<Card>().CardValue = j;
                cards[choice].GetComponent<Card>().Initialized = true;
            }
        }
        foreach(GameObject c in cards)
        {
            c.GetComponent<Card>().setupGraphics();
        }
    }

    public Sprite getCardBack()
    {
        return cardBack;
    }

    public Sprite getCardFace(int i)
    {
        return cardFace[i];
    }

    public void CheckCards()
    {
        List<int> c = new List<int>();


        for(int i = 0; i < cards.Length; i++)
        {
            if(cards[i].GetComponent<Card>().State == 1)
            {
                c.Add(i);
            }
            if(c.Count == 2)
            {
                cardComparison(c);
                break;
            }
        }
    }

    void cardComparison(List<int> c)
    {
        if(cards[c[0]].GetComponent<Card>().CardValue == cards[c[1]].GetComponent<Card>().CardValue)
        {
            _matches++;
            if(_matches == 8)
            {
                SceneManager.LoadScene("MainMenu");
            }
            cards[c[0]].GetComponent<Card>().State = 2;
            cards[c[1]].GetComponent<Card>().State = 2;
        }
        else
        {
            cards[c[0]].GetComponent<Card>().State = 0;
            cards[c[1]].GetComponent<Card>().State = 0;
        }
        cards[c[0]].GetComponent<Card>().updateGraphics();
        cards[c[1]].GetComponent<Card>().updateGraphics();
    }
}
