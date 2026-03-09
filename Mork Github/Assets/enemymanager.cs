using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;


public class enemymanager : MonoBehaviour
{
    public GameObject[] plots = new GameObject[3];
    public List<GameObject> cip = new List<GameObject>();
    public GameObject[] plotpos = new GameObject[3];
    public enemyhandskript hand;
    public enemiepileskript pile;
    public enemystonepileskript stonepile;
    public managerskript manag;
    public int mana;
    public int health;
    public int aktionen;
    public List<GameObject> cardstosacrifice = new List<GameObject>();
    public List<GameObject> cardstoplay = new List<GameObject>();
    public GameObject stone;
    public bool isfilled;
    public GameObject trash;
   public bool thereissacrifice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hand = this.gameObject.GetComponent<enemyhandskript>();
        pile = GameObject.Find("enemypile").GetComponent<enemiepileskript>();
        stonepile = GameObject.Find("enemystonepile").GetComponent<enemystonepileskript>();
        manag = GameObject.Find("Manager").GetComponent<managerskript>();
        aktionen = 3;
        trash = GameObject.Find("trash");
        
    }

    // Update is called once per frame
    void Update()
    {
        cardstosacrifice.Clear();
        cardstoplay.Clear();
        thereissacrifice = false;
        isfilled = plots != null && plots.Any(e => e != null);
        drawing();
        Debug.Log("phase " + manag.phase);
        if (manag.phase == 2)
        {
            
            checkingformana();

            Debug.Log("checkedformana");
            playing();
            Debug.Log("played");
            manag.phase = 3;
            Debug.Log("end "+manag.phase);
            
        }
    }
    public void checkingformana()
    {
        Debug.Log("trying to check for mana");
        int available = 2;
        int highestvalue = 0;
        GameObject cardwithvalue = null;
        GameObject onetimesacrifice = null;
        List<GameObject> needtosacrifice = new List<GameObject>();
        int stonesneeded = 0;
        int sacrificevalue = 0;
        if (isfilled)
        {
            for (int i = plots.Length - 1; i >= 0; i--)
            {
                if (plots[i] != null)
                {
                    available = available + plots[i].GetComponent<enemycardskript>().health + 1;
                }
            }
        }
        Debug.Log("available: " + available);
        Debug.Log(hand.handCards.Count());
        for (int i = hand.handCards.Count() - 1; i >= 0; i--)
        {
            Debug.Log("inSchleife");
                if (hand.handCards[i].transform.tag == "enemyspell")
                {
                    Debug.Log("inif1"+highestvalue);
                    if (hand.handCards[i].GetComponent<enemycardskript>().value > highestvalue && hand.handCards[i].GetComponent<enemycardskript>().health <= available)
                    {
                        Debug.Log("inif2");
                        highestvalue = hand.handCards[i].GetComponent<enemycardskript>().value;
                        cardwithvalue = hand.handCards[i];
                        sacrificevalue = cardwithvalue.GetComponent<enemycardskript>().value;
                        Debug.Log("DasObject" + cardwithvalue.transform.name);
                    }
                }
        }




        if (cardwithvalue.GetComponent<enemycardskript>().health > 2)
        {
            if (isfilled)
            {
                for (int i = plots.Length - 1; i >= 0; i--)
                {
                    if (plots[i].GetComponent<enemycardskript>().health + 1 == cardwithvalue.GetComponent<enemycardskript>().health)
                    {
                        if (plots[i].GetComponent<enemycardskript>().value <= cardwithvalue.GetComponent<enemycardskript>().value)
                        {
                            onetimesacrifice = plots[i];
                        }
                    }
                }
                if (onetimesacrifice == null)
                {
                    for (int i = plots.Length - 1; i >= 0; i--)
                    {
                        if (plots[i].GetComponent<enemycardskript>().health + 1 <= sacrificevalue)
                        {
                            needtosacrifice.Add(plots[i]);
                            sacrificevalue = sacrificevalue - plots[i].GetComponent<enemycardskript>().health;
                        }
                        else if (sacrificevalue - 1 == 0)
                        {
                            stonesneeded = 1;
                        }
                        else if (sacrificevalue - 2 == 0)
                        {
                            stonesneeded = 2;
                        }
                    }
                }
            }
            else
            {
                stonesneeded = 3;
                cardwithvalue = null;
            }
        }
        else
        {
            stonesneeded = 2;
        }
        
        for (int i = stonesneeded; i > 0; i--)
        {
            cardstoplay.Add(hand.handCards.FirstOrDefault(go => go != null && go.CompareTag("enemystone")));
        }
        cardstoplay.Add(cardwithvalue);
        for (int i = needtosacrifice.Count-1; i >= 0; i--)
        {
            cardstosacrifice.Add(needtosacrifice[i]);
            thereissacrifice = true;
        }
        if (onetimesacrifice != null)
        {
            cardstosacrifice.Add(onetimesacrifice);
            thereissacrifice = true;
        }
        
    }
    public void playing()
    {
        Debug.Log("trying to play");
        if (thereissacrifice != false)
        {
            for (int i = cardstosacrifice.Count() - 1; i >= 0; i--)
            {
                cardstosacrifice[i].GetComponent<enemycardskript>().opfern();
            }
        }
        for (int i = cardstoplay.Count() -1; i >= 0; i--)
        {
            GameObject newpoint = null;
            if (cardstoplay[i].transform.tag != "enemystone")
            {
                for (int a = plots.Length - 1; a >= 0; a--)
                {
                    if (plots[a] == null && newpoint == null)
                    {
                        newpoint = plotpos[a];
                        if (newpoint != null)
                        {
                            cardstoplay[i].GetComponent<enemycardskript>().Kartelegen(newpoint);
                            plots[a] = cardstoplay[i];
                            hand.handCards.Remove(cardstoplay[i]);
                            
                        }
                    }
                    
                }
            }
            else
            {
                cardstoplay[i].GetComponent<enemystoneskript>().Kartelegen(trash);
                hand.handCards.Remove(cardstoplay[i]);
            }
        }
    }
    public void drawing()
    {
        if (hand.handCards.Count < 6)
        {
            
            int spells = 0;
            int stones = 0;
            for (int i = hand.handCards.Count - 1; i >0; i--)
            {
                
                if (hand.handCards[i].transform.tag == "enemyspell")
                {
                    spells = spells + 1;
                    
                }
                if (hand.handCards[i].transform.tag == "enemystone")
                {
                    stones = stones + 1;
                }
            }
            if (stones < 2)
            {
                stonepile.DrawStone();
            }
            if (spells < 4)
            {
                pile.DrawSpell();
            }
        }
    }
    
}
