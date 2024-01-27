using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[System.Serializable]
public class Card : MonoBehaviour
{
    [SerializeField] string tekstKarte;
    [SerializeField] float kevaDmg ;
    [SerializeField] float redditDmg;
    [SerializeField] float animeDmg;
    [SerializeField] float dadJokeDmg;
    [SerializeField] Sprite izgledKarte;
    //private Dictionary<string, int> statoviKarte { get; set; }
    [SerializeField] bool mozeDaSeMerguje;
    //Vizualni deo
    [SerializeField]TMP_Text cardText;
    [SerializeField]Image cardImage;
    public void spojiKarte(Card card)
    {
        this.kevaDmg += card.kevaDmg;
        this.redditDmg += card.redditDmg;
        this.animeDmg += card.animeDmg;
        this.dadJokeDmg += card.dadJokeDmg;
    }

    //private void napraviMapu(int keva, int reddit, int anime, int dadJoke)
    //{
    //    statoviKarte = new Dictionary<string, int>();
    //    statoviKarte.Add("keva", keva);
    //    statoviKarte.Add("reddit", reddit);
    //    statoviKarte.Add("anime", anime);
    //    statoviKarte.Add("dadJoke", dadJoke);

    //}
    
    // Start is called before the first frame update
    public Card(float a, float b, float c, float d)
    {
        this.kevaDmg = a;
        this.redditDmg = b;
        this.animeDmg = c;
        this.dadJokeDmg = d;
    }

    void Start()
    {
        
        //napraviMapu(kevaDmg, redditDmg, animeDmg, dadJokeDmg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCard(Card c)
    {
        cardText.text = c.vratiTekstKarte();
        if(izgledKarte!=null)
        {
            cardImage.sprite=c.vratiSliku();
        }
    }

    public float vratiKevaDmgStat()
    {
        return this.kevaDmg;
    }
    public float vratiRedditDmgStat()
    {
        return this.redditDmg;
    }
    public float vratiAnimeDmgStat()
    {
        return this.animeDmg;
    }
    public float vratiDadjokeDmgStat()
    {
        return this.dadJokeDmg;

    }

    public Sprite vratiSliku()
    {
        return this.izgledKarte;
    }
    public string vratiTekstKarte()
    {
        return this.tekstKarte;
    }


}
