using DG.Tweening;
using UnityEngine;

public class enemystoneskript : MonoBehaviour
{
    public enemyhandskript enemy;
    public managerskript manager;
    public enemymanager deinMudda;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<enemyhandskript>();
        enemy.handCards.Add(this.gameObject);
        enemy.UpdateCardPositions();
        manager = GameObject.FindWithTag("Manager").GetComponent<managerskript>();
        deinMudda = GameObject.FindWithTag("enemy").GetComponent<enemymanager>();
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
}