using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] List<Card> dekKarata { get; set; }
    [SerializeField] Card[] cards;
    [SerializeField] int maxBrojKarataURuci = 6;
    [SerializeField] int kolikoDiscardovaIma;
    [SerializeField] int brojPoteza;

    public void povecajBrojKarataURuci(int zaKolikoGaPocevam)
    {
        maxBrojKarataURuci += zaKolikoGaPocevam;
    }

    public void smanjiPoteze()
    {
        brojPoteza--;
    }


    public void dodajKartuUdek(Card card)
    {
        dekKarata.Add(card);
    }
    private void dodajUDekAkoJePrazan()
    {   
        dekKarata = new List<Card>();
        for(int i = 0; i < cards.Length; i++)
        {
            //Debug.Log("karte: " + cards[i]);
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

    public int vratiMaxBrojKarataURuci()
    {
        return this.maxBrojKarataURuci;
    }
    public List<Card> vratiDek()
    {
        return this.dekKarata;
    }
    public int vratiBrojPoteza()
    {
        return this.brojPoteza;
    }
}
