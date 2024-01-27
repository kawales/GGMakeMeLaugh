using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using System.Linq;
using System;

using UnityEngine.UI;
public class GameManagerScr : MonoBehaviour
{
    [SerializeField]GameObject BaseCards;
    Transform HandObj;
    [SerializeField]int offsetCards = 50;
    TMP_Text speechBubble;
    Player pl;
    string queuedText= "The <i>quick brown fox</i> jumps over the <b>lazy dog</b>.";
    int cardsUsed=0;
    float textTempo=0.05f;
    int maxCardsInHand=3;
    List<Card> deck;
    IEnumerator printText;
    //selected cards
    GameObject selected1;
    GameObject selected2;
    Enemy enemy;
    // Start is called before the first frame update 
    void Start()
    {   
        
        pl = GameObject.Find("Player").GetComponent<Player>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        HandObj = GameObject.Find("Hand").transform;
        speechBubble = GameObject.Find("SpeechBubbleText").GetComponent<TMP_Text>();
        deck=pl.vratiDek();
        for(int i=0;i<pl.vratiMaxBrojKarataURuci();i++)
        {
            AddCard();
        }
        //Debug.Log(deck.Count);
        printText=spellOutText();


        queuedText = enemy.vratiQuote();
        StartCoroutine(printText);
       // discard();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //AddCard();
         //   discard();
        }
        if(Input.GetKeyDown(KeyCode.Space) && queuedText!="")
        {
            StopCoroutine(printText);
            speechBubble.text=queuedText;
            queuedText="";
            //Get new quote for next turn
        }
    }
    public void merge()
    {
        Card karta1 = selected1.GetComponent<Card>();
        Card karta2 = selected2.GetComponent<Card>();
        //int index = selected2.transform.GetSiblingIndex();

        karta1.spojiKarte(karta2);
        Destroy(HandObj.Find(selected2.transform.name).gameObject);
        if(selected2 == null)
        {
            Debug.Log("obrisan drugi");
        }
    }
    public void AddCard()
    {
        if(deck.Count==0)
        {
            Debug.Log("EMPTY DECK");
            return;
        }
        Card pulledCard = deck.Last();
        deck.Remove(deck.Last());
        cardsUsed++;
        GameObject tempCard = Instantiate(BaseCards,HandObj.transform);
        //tempCard.>();
        tempCard.GetComponent<Card>().LoadCard(pulledCard);
        if(cardsUsed>1)
        {   
            //Debug.Log(cardsUsed);
            Vector3 pos = HandObj.Find("Card "+(cardsUsed-1)).position + Vector3.right*offsetCards;
            tempCard.transform.position=pos;
            
        }
        tempCard.GetComponent<Button>().onClick.AddListener(() => { selectCard(tempCard); });
        tempCard.name="Card "+cardsUsed;
        
    }

    private void shuffleDeck()
    {
       // _rand = new Random();
         this.deck = deck.OrderBy(_ => Guid.NewGuid()).ToList();
    }

    public void discard()
   {
        int brojKarataURuci = HandObj.childCount;
        shuffleDeck();
        foreach (Card item in deck)
        {
            Debug.Log("deck pre vracanja" + item);
        }
        
        List<Card> karteKojeSeVracajuUDek = new List<Card>();
        for(int i=0; i<brojKarataURuci; i++)
        {
            //Debug.Log("karte iz ruke :  " + HandObj.GetChild(i).GetComponent<Card>());
            karteKojeSeVracajuUDek.Add(HandObj.GetChild(i).GetComponent<Card>());
        }

        foreach (Card item in karteKojeSeVracajuUDek)
        {
            Debug.Log("Karte iz ruke " + item);
        }
        for(int i = brojKarataURuci-1; i>= 0; i--)
        {
            Destroy(HandObj.GetChild(i).gameObject);
        }
        deck.AddRange(karteKojeSeVracajuUDek);

        //shufle funk
        shuffleDeck();

        foreach (Card item in deck)
        {
            Debug.Log("Karte diskard na kraju " + item);
        }
        for (int i = 0; i < pl.vratiMaxBrojKarataURuci(); i++)
        {
            AddCard();
        }
      

    }

    IEnumerator spellOutText()
    {
        while(speechBubble.text.Length<queuedText.Length)
        {
            if(queuedText[speechBubble.text.Length]=='<' && (queuedText[speechBubble.text.Length+2]=='>' ||  queuedText[speechBubble.text.Length+3]=='>'))
            {
                
                if(queuedText[speechBubble.text.Length+1]=='/')
                {
                    //Debug.Log(speechBubble.text);
                    speechBubble.text += queuedText[speechBubble.text.Length];
                }
                else if(queuedText[speechBubble.text.Length+1]=='#')
                {
                    //Debug.Log(speechBubble.text);
                    textTempo =  0.5f/int.Parse(queuedText[speechBubble.text.Length]+"");
                }
                speechBubble.text += queuedText[speechBubble.text.Length];
                speechBubble.text += queuedText[speechBubble.text.Length];
                speechBubble.text += queuedText[speechBubble.text.Length];
            }
            speechBubble.text += queuedText[speechBubble.text.Length];
            yield return new WaitForSeconds(textTempo);
        }
        
    }

    public void selectCard(GameObject c)
    {
        if(selected1==c)
        {
            highlightSelected(true);
            selected1=null;
        }
        else if(selected2==c)
        {
            highlightSelected(true);
            selected2=null;
        }
        else if(selected1==null)
        {
            selected1=c;
        }
        else if(selected1.GetComponent<Card>().vratiMozeDaSeMerguje() && c.GetComponent<Card>().vratiMozeDaSeMerguje())
        {
            highlightSelected(true);
            selected2=c;
        }
        Debug.Log(selected1);
        Debug.Log(selected2);
        highlightSelected();
        
    }

    void highlightSelected(bool turnBack = false)
    {
        if(turnBack)
        {
            if(selected1!=null)
            {
                selected1.GetComponent<Image>().color=Color.white;
            }
            if(selected2!=null)
            {
                selected2.GetComponent<Image>().color=Color.white;
            }
            return;
        }
        if(selected1!=null)
        {
            selected1.GetComponent<Image>().color=Color.yellow;
        }
        if(selected2!=null)
        {
            selected2.GetComponent<Image>().color=Color.yellow;
        }

    }


    public void NextTurn()
    {
        //calcCombo
        if(selected1!=null)
        {
            enemy.uradiDmg(selected1.GetComponent<Card>());
            RemoveCard(selected1);
        }
        highlightSelected(true);
        selected1=null;
        selected2=null;
        StopCoroutine(printText);
        speechBubble.text="";
        enemy.samnjiPotez();
        queuedText=enemy.vratiQuote();
        printText=spellOutText();
        StartCoroutine(printText);
        
        
    }

    public void RemoveCard(GameObject c)
    {
        HandObj.transform.Find(c.name).SetParent(null);
        Destroy(c);
        HandObj.GetChild(0).transform.position=HandObj.position;
        for(int i = 1;i<HandObj.childCount;i++)
        {   
            //Debug.Log(cardsUsed);
            Vector3 pos = HandObj.GetChild(i-1).transform.position + Vector3.right*offsetCards;
            HandObj.GetChild(i).transform.position=pos;
            
        }
    }

}
