using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
public class enemiepileskript : MonoBehaviour
{
    public List<GameObject> deck = new List<GameObject>();
    public enemyhandskript enemy; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<enemyhandskript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DrawSpell()
    {
        if (enemy.handCards.Count >= enemy.maxHandSize) return;


        Instantiate(deck[0], transform.position, transform.rotation);
        deck.Remove(deck[0]);

        enemy.UpdateCardPositions();

    }
    public void shuffle()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // Zufällige Position von 0 bis i
            var tmp = deck[i];
            deck[i] = deck[j];
            deck[j] = tmp;
        }
    }
}
