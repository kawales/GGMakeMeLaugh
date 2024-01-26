using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] string tekstKarte { get; set; }  
    [SerializeField] Sprite izgledKarte { get; set; }
    [SerializeField] Dictionary<string, int> statoviKarte { get; set; }
    [SerializeField] bool mozeDaSeMerguje { get; set; }  


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
