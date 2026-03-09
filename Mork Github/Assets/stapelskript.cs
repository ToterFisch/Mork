using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class stapelskript : MonoBehaviour
{
    public List<GameObject> deck = new List<GameObject>();
    public handmanager player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<handmanager>();
        shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        DrawSpell();
    }
    public void DrawSpell()
    {
        if (player.handCards.Count >= player.maxHandSize) return;
        
        
        Instantiate(deck[0], transform.position, transform.rotation);
        deck.Remove(deck[0]);
        
        player.UpdateCardPositions();
        
    }
    public void shuffle()
    {
        for (int i= deck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // Zufällige Position von 0 bis i
            var tmp = deck[i];
            deck[i] = deck[j];
            deck[j] = tmp;
        }
    }

}
