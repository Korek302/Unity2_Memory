
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool DND = false;
    public int ClickedCounter = 0;
    public Sprite[] cardFaces;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text ScoreText;
    public Text FinalScoreText;
    public GameObject GamePanel;
    public GameObject FinalPanel;

    private int _matches = 0;
    private int _score = 0;
    
	void Start ()
    {
        FinalPanel.gameObject.SetActive(false);
		initializeCards();
	}

    void initializeCards()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < cards.Length/2; j++)
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
                FinalScoreText.text = "Your final score: " + _score;
                GamePanel.gameObject.SetActive(false);
                FinalPanel.gameObject.SetActive(true);
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
