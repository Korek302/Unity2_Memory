using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool DND = false;
    public int ClickedCounter = 0;
    public Sprite[] cardFaces;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text ScoreText;

    private int _matches = 0;
    private int _score = 0;
    
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
                bool _temp = false;
                int choice = 0;
                while(!_temp)
                {
                    choice = Random.Range(0, cards.Length);
                    _temp = !(cards[choice].GetComponent<Card>().Initialized);
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

    public void CheckCards()
    {
        int[] _toCompareList = new int[2];
        int _place = 0;

        for(int i = 0; i < cards.Length; i++)
        {
            if(cards[i].GetComponent<Card>().State == 1)
            {
                _toCompareList[_place] = i;
                _place++;
            }
            if(_place == 2)
            {
                cardComparison(_toCompareList);
                i = cards.Length;
            }
        }
    }

    void cardComparison(int[] toCompareList)
    {
        if(cards[toCompareList[0]].GetComponent<Card>().CardValue == cards[toCompareList[1]].GetComponent<Card>().CardValue)
        {
            _matches++;
            _score += 10;
            if(_matches == 8)
            {
                SceneManager.LoadScene("MainMenu");
            }
            cards[toCompareList[0]].GetComponent<Card>().State = 2;
            cards[toCompareList[1]].GetComponent<Card>().State = 2;
        }
        else
        {
            _score = _score < 1 ? _score : _score -= 2;

            cards[toCompareList[0]].GetComponent<Card>().State = 0;
            cards[toCompareList[1]].GetComponent<Card>().State = 0;
        }
        ScoreText.text = "Score: " + _score;
        cards[toCompareList[0]].GetComponent<Card>().updateGraphics();
        cards[toCompareList[1]].GetComponent<Card>().updateGraphics();
    }
}
