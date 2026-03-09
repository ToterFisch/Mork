using DG.Tweening;
using UnityEngine;

public class enemycardskript : MonoBehaviour
{
    public GameObject trash;
    public enemyhandskript enemy;
    public managerskript manager;
    public enemymanager deinMudda;
    public int health;
    public int damage;
    public int value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<enemyhandskript>();
        enemy.handCards.Add(this.gameObject);
        enemy.UpdateCardPositions();
        manager = GameObject.FindWithTag("Manager").GetComponent<managerskript>();
        deinMudda = GameObject.FindWithTag("enemy").GetComponent<enemymanager>();
        trash = GameObject.Find("trash");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseOver()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    }
    public void OnMouseExit()
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); 
    }
    public void Kartelegen(GameObject position)
    {
       
        transform.rotation = position.transform.rotation;
        transform.DOMove(position.transform.position, 1f);
    }
    public void opfern()
    {
        transform.DOMove(trash.transform.position, 1f);
        deinMudda.plots[System.Array.IndexOf(deinMudda.plots, this.gameObject)] = null ;
    }
}
