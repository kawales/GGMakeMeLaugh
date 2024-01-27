using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] Dictionary<string, int> dmgMult;//
    [SerializeField] float kevaMult;
    [SerializeField] float redditMult;
    [SerializeField] float animeMult;
    [SerializeField] float dadJokeMult;
    [SerializeField] string[] quoteBitan;
    [SerializeField] string[] quoteNebitan;
    [SerializeField] int brojPoteza;
    [SerializeField] int brojBitnihQuoteova;
    private float procenatZaBitanQuote = 80f;
    Card c = new Card(2,1,0,0);
    private int nizBezBitnog = 0;


    public void samnjiPotez()
    {
        brojPoteza--;
    }

    public string vratiQuote() {
        string s = "";
        
        if(brojPoteza == 7)
        {
            brojBitnihQuoteova--;
            return quoteBitan[Random.Range(0,quoteBitan.Length)];
        }
        else
        {
            if(brojBitnihQuoteova == 0)
            {
                return quoteNebitan[Random.Range(0,quoteNebitan.Length)];
            }
            else
            {
                if(nizBezBitnog == 2)
                {
                    nizBezBitnog = 0;
                    return quoteBitan[Random.Range(0, quoteBitan.Length)];
                }
                else
                {
                    if (Random.Range(0f, 100f) < procenatZaBitanQuote)
                    {
                        procenatZaBitanQuote -= 20f;
                        brojBitnihQuoteova--;
                        return quoteBitan[Random.Range(0, quoteBitan.Length)];
                    }
                    else
                    {
                        nizBezBitnog++;
                        return quoteNebitan[Random.Range(0, quoteNebitan.Length)];
                    }
                }
            
            }
        }
        
    }
    
    public void uradiDmg(Card card)
    {
      
        float sumDmg;
        float kevaDmg = card.vratiKevaDmgStat();
        float redditDmg = card.vratiRedditDmgStat();
        float animeDmg = card.vratiAnimeDmgStat();
        float dadJokeDmg = card.vratiDadjokeDmgStat();
        sumDmg = kevaDmg*kevaMult + redditDmg*redditMult + animeDmg*animeMult + dadJokeDmg*dadJokeMult;
        Debug.Log("Damage received: " + sumDmg);
        health = health - sumDmg;
        Debug.Log("HP: " + health);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
