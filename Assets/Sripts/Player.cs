using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] List<Card> dekKarata { get; set; }
    [SerializeField] Card[] cards;
    
    


    public void dodajKartuUdek(Card card)
    {
        dekKarata.Add(card);
    }
    private void dodajUDekAkoJePrazan()
    {   
        dekKarata = new List<Card>();
        for(int i = 0; i < cards.Length; i++)
        {
            Debug.Log("karte: " + cards[i]);
            dekKarata.Add(cards[i]);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        dodajUDekAkoJePrazan();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
