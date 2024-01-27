using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
public class GameManagerScr : MonoBehaviour
{
    [SerializeField]GameObject BaseCards;
    Transform HandObj;
    [SerializeField]int offsetCards = 50;
    TMP_Text speechBubble;
    Player pl;
    string tempText= "The <i>quick brown fox</i> jumps over the <b>lazy dog</b>.";
    int cardsUsed=0;
    float textTempo=0.1f;
    int maxCardsInHand=3;
    List<Card> deck;
    IEnumerator printText;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        HandObj = GameObject.Find("Hand").transform;
        speechBubble = GameObject.Find("SpeechBubbleText").GetComponent<TMP_Text>();
        //ucitaj deck i max hands
        for(int i=0;i<3;i++)
        {
            AddCard();
        }
        printText=spellOutText();
        StartCoroutine(printText);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddCard();
        }
        if(Input.GetKeyDown(KeyCode.Space) && tempText!="")
        {
            StopCoroutine(printText);
            speechBubble.text=tempText;
            tempText="";
            //Get new quote for next turn
        }
    }

    public void AddCard()
    {
        cardsUsed++;
        if(HandObj.childCount==0)
        {
            GameObject tempCard = Instantiate(BaseCards,HandObj.transform);
            tempCard.name="Card "+cardsUsed;
        }
        else
        {   
            Debug.Log(cardsUsed);
            Vector3 pos = HandObj.Find("Card "+(cardsUsed-1)).position + Vector3.right*offsetCards;
            GameObject tempCard = Instantiate(BaseCards,HandObj.transform);
            tempCard.transform.position=pos;
            tempCard.name="Card "+cardsUsed;
        }
        
    }

    IEnumerator spellOutText()
    {
        while(speechBubble.text.Length<tempText.Length)
        {
            if(tempText[speechBubble.text.Length]=='<' && (tempText[speechBubble.text.Length+2]=='>' ||  tempText[speechBubble.text.Length+3]=='>'))
            {
                
                if(tempText[speechBubble.text.Length+1]=='/')
                {
                    //Debug.Log(speechBubble.text);
                    speechBubble.text += tempText[speechBubble.text.Length];
                }
                else if(tempText[speechBubble.text.Length+1]=='#')
                {
                    //Debug.Log(speechBubble.text);
                    textTempo =  0.5f/int.Parse(tempText[speechBubble.text.Length]+"");
                }
                speechBubble.text += tempText[speechBubble.text.Length];
                speechBubble.text += tempText[speechBubble.text.Length];
                speechBubble.text += tempText[speechBubble.text.Length];
            }
            speechBubble.text += tempText[speechBubble.text.Length];
            yield return new WaitForSeconds(textTempo);
        }
        
    }


}
