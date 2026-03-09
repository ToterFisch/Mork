using UnityEngine;

public class sonepileskript : MonoBehaviour
{
    public handmanager player;
    public GameObject stone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<handmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        DrawStone();
    }
    public void DrawStone()
    {
        if (player.handCards.Count >= player.maxHandSize) return;
        Instantiate(stone, transform.position, transform.rotation);
        player.UpdateCardPositions();
    }
}
