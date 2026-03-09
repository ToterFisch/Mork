using DG.Tweening;
using UnityEngine;

public class enemystonepileskript : MonoBehaviour
{
    public enemyhandskript enemy;
    public GameObject enemystone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<enemyhandskript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DrawStone()
    {
        if (enemy.handCards.Count >= enemy.maxHandSize) return;
        Instantiate(enemystone, transform.position, transform.rotation);
        enemy.UpdateCardPositions();
    }
}
