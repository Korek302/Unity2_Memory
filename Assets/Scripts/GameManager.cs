
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool DND = false;
    public int ClickedCounter = 0;
    public int MatchSize = 2;
    public Sprite[] cardFaces;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text ScoreText;
    public Text FinalScoreText;
    public GameObject GamePanel;
    public GameObject FinalPanel;

    private int _matches = 0;
    private int _score = 0;
    private int _penalty = -2;
    private int _prize = 10;
    
	void Start ()
    {
        FinalPanel.gameObject.SetActive(false);
		initializeCards();
	}

    void initializeCards()
    {
        for(int i = 0; i < MatchSize; i++)
        {
            for(int j = 0; j < cards.Length/MatchSize; j++)
            {
                bool _temp = false;
                int _choice = 0;
                while(!_temp)
                {
                    _choice = Random.Range(0, cards.Length);
                    _temp = !(cards[_choice].GetComponent<Card>().Initialized);
                }
                cards[_choice].GetComponent<Card>().CardValue = j;
                cards[_choice].GetComponent<Card>().Initialized = true;
            }
        }
        foreach(GameObject card in cards)
        {
            card.GetComponent<Card>().setupGraphics();
        }
    }

    public void CheckCards()
    {
        int _place = 0;
        GameObject[] _toCompareList = new GameObject[MatchSize];

        for(int i = 0; i < cards.Length; i++)
        {
            if(cards[i].GetComponent<Card>().State == 1)
            {
                _toCompareList[_place] = cards[i];
                _place++;
            }
            if(_place == MatchSize)
            {
                cardComparison(_toCompareList);
                i = cards.Length;
            }
        }
    }

    void cardComparison(GameObject[] toCompareList)
    {
        bool _allMatch = true;
        for(int i = 0; i < toCompareList.Length - 1; i++)
        {
            if(toCompareList[i].GetComponent<Card>().CardValue != toCompareList[i + 1].GetComponent<Card>().CardValue)
            {
                _allMatch = false;
            }
        }
        if(_allMatch)
        {
            _matches++;
            _score += _prize;
            if(_matches == cards.Length/MatchSize)
            {
                FinalScoreText.text = "Your final score: " + _score;
                GamePanel.gameObject.SetActive(false);
                FinalPanel.gameObject.SetActive(true);
            }
            for(int i = 0; i < toCompareList.Length; i++)
            {
                toCompareList[i].GetComponent<Card>().State = 2;
            }
        }
        else
        {
            _score = _score < 1 ? _score : _score + _penalty;

            for (int i = 0; i < toCompareList.Length; i++)
            {
                toCompareList[i].GetComponent<Card>().State = 0;
            }
        }
        ScoreText.text = "Score: " + _score;
        for (int i = 0; i < toCompareList.Length; i++)
        {
            toCompareList[i].GetComponent<Card>().updateGraphics();
        }
    }
}
