using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections;

public class Kartenskript : MonoBehaviour
{
    public int zustand;
    public bool zauber;
    public int leben;
    public int schaden;
    public int took;
    public handmanager handy;
    public GameObject newpoint;
    public handmanager player;
    public TMP_Text life;
    public TMP_Text damage;
    public GameObject manager;
    public managerskript manag;
    public GameObject trash;
    public GameObject opfer;
    public bool exhausted;
    public enemymanager enemy;
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        manag = manager.GetComponent<managerskript>();
        trash = GameObject.Find("trash");
        zustand = 1;
        handy = GameObject.Find("Player").GetComponent<handmanager>();
        took = 1;
        handy.handCards.Add(gameObject);
        enemy = GameObject.Find("Enemy").GetComponent<enemymanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (took == 2)
            {
                if (handy.mana >= leben)
                {
                    Debug.Log("fuck");
                    handy.handCards.Remove(handy.handCards[handy.handCards.IndexOf(this.gameObject)]);
                    took = 1;
                    zustand = 2;
                    transform.position = new Vector3(newpoint.transform.position.x, newpoint.transform.position.y,0);
                    transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    newpoint.transform.tag = "besetzt";
                    handy.UpdateCardPositions();
                    manag.aktion = manag.aktion + 1;
                    handy.mana = handy.mana - leben;
                    exhausted = false;
                    enemy.cip.Add(gameObject);
                    this.GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
            }
            if (took == 3)
            {
                handy.handCards.Remove(handy.handCards[handy.handCards.IndexOf(this.gameObject)]); 
                took = 1;
                zustand = 3;
                transform.position = newpoint.transform.position;
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                manag.aktion = manag.aktion + 1;
                handy.UpdateCardPositions();
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (manag.chosen == this.gameObject)
            {
                if (zustand == 1)
                {
                    Debug.Log("worked");
                    transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    if (manag.phase != 1) return;
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0f;
                    transform.position = mousePosition;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    this.GetComponent<SpriteRenderer>().sortingOrder = 11;
                }
                if (zustand == 2)
                {
                    if (exhausted == false)
                    {
                        handy.mana = handy.mana + 1;
                        exhausted = true;
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                }
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (manag.chosen == this.gameObject)
            {
                if (zustand == 1)
                {
                    if (manag.deletetimer == 1)
                    {
                        handy.handCards.Remove(handy.handCards[handy.handCards.IndexOf(this.gameObject)]);
                        took = 1;
                        zustand = 3;
                        transform.position = trash.transform.position;
                        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        manag.aktion = manag.aktion + 1;
                        handy.UpdateCardPositions();
                        manag.chosen = null;
                        manag.deletetimer = 2;
                    }
                }
                if (zustand == 2)
                {
                    manag.chosen = null;
                    opfern();
                }
            }
        }
        if (zustand == 2)
        {
            life.text = leben.ToString();
            damage.text = schaden.ToString();
        }
    }
    public void OnMouseOver()
    {
        if (zustand == 1)
        {
            if (manag.chosen == null) 
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = 7;
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                manag.chosen = this.gameObject;
                transform.position = new Vector3(transform.position.x, -2.5f, -7f);
                Debug.Log(transform.position);
                this.transform.DOKill();
            }
        }
        if (zustand == 2)
        {
            if (manag.chosen == null)
            {
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                life.gameObject.SetActive(true);
                damage.gameObject.SetActive(true);
                if (manag.phase != 1) return;
                
                manag.chosen = this.gameObject;
            }
        }
        
    }
    public void OnMouseExit()
    {


        StartCoroutine(FadeOut());
        Debug.Log("Mouseleft");
    }
    public void HideButton()
    {
        opfer.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (zustand == 1)
        {
            Debug.Log("erkannt");
            if (collision.gameObject.transform.tag == "ablage")
            {
                took = 2;
                newpoint = collision.gameObject;
            }
            if (collision.gameObject.transform.tag == "trash")
            {
                took = 3;
                newpoint = collision.gameObject;
            }
        }
    }
    public void opfern()
    {
        
        newpoint.transform.tag = "ablage";
        Debug.Log("du Opfer");
        zustand = 3;
        transform.DOMove(trash.transform.position, 0.25f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        newpoint.transform.tag = "ablage";
        StartCoroutine(FadeOut());
        if (exhausted == false)
        {
            handy.mana = handy.mana + leben + 1;
        }
        else
        {
            handy.mana = handy.mana + leben;
        }
        enemy.cip.Remove(gameObject);
    }
    public void OnTriggerExit2D (Collider2D other)
    {
        if (zustand == 1)
        {
            Debug.Log("exkannt");
            if (other.gameObject.transform.tag == "ablage")
            {
                took = 1;
                newpoint = null;
            }
            if (other.gameObject.transform.tag == "trash")
            {
                took = 1;
                newpoint = null;
            }
        }
    }
    public IEnumerator FadeOut()
    {

        yield return new WaitForSeconds(0.1f);
        manag.chosen = null;
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        handy.UpdateCardPositions();
        if (zustand == 2)
        {
            life.gameObject.SetActive(false);
            damage.gameObject.SetActive(false);
            
        }
    }
}
