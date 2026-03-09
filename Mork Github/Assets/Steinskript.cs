using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
public class Steinskript : MonoBehaviour
{
    public int zustand;
    public int took;
    public handmanager handy;
    public GameObject newpoint;
    public GameObject button;
    public GameObject trash;

    public GameObject manager;
    public managerskript manag;
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        manag = manager.GetComponent<managerskript>();
        zustand = 1;
        handy = GameObject.Find("Player").GetComponent<handmanager>();
        trash = GameObject.Find("trash");
        took = 1;
        handy.handCards.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (took == 2)
            {
                Ablegen();
                manag.aktion = manag.aktion + 1;
            }
            if (took == 3)
            {
                Mülllegen();
                manag.aktion = manag.aktion + 1;
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
                        opfern();
                        manag.aktion = manag.aktion + 1;
                        manag.deletetimer = 2;
                    }
                }
                if (zustand == 2)
                {
                    opfern();
                    
                }
               
            }
        }
    }
    public void OnMouseOver()
    {
        if (manag.chosen == null)
        {
            if (zustand == 1)
            {

                this.GetComponent<SpriteRenderer>().sortingOrder = 7;
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                manag.chosen = this.gameObject;
                transform.position = new Vector3(transform.position.x, -2.5f, -7f);
                Debug.Log(transform.position);
                this.transform.DOKill();

            }
            if (zustand == 2)
            {
                if (manag.phase != 1) return;
                button.SetActive(true);
            }
            manag.chosen = this.gameObject;
        }
    }
    public void OnMouseExit()
    {
        StartCoroutine(FadeOut());
    }
    public void Ablegen()
    {
        if (manag.phase != 1) return;
        handy.handCards.Remove(handy.handCards[handy.handCards.IndexOf(this.gameObject)]);
        took = 1;
        zustand = 2;
        transform.position = newpoint.transform.position;
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        newpoint.transform.tag = "besetzt";
        handy.UpdateCardPositions();

    }
    public void Mülllegen()
    {
        if (manag.phase != 1) return;
        handy.handCards.Remove(handy.handCards[handy.handCards.IndexOf(this.gameObject)]);
        took = 1;
        zustand = 3;
        transform.position = newpoint.transform.position;
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        handy.mana = handy.mana + 1;
        handy.UpdateCardPositions();
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
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
    public void OnTriggerExit2D(Collider2D other)
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
    public void HideButton()
    {
        button.SetActive(false);
    }
    public void opfern()
    {
        if (manag.phase != 1) return;   
        newpoint.transform.tag = "ablage";
        Debug.Log("du Opfer");
        zustand = 3;
        transform.DOMove(trash.transform.position, 0.25f);
        newpoint.transform.tag = "ablage";
        handy.mana = handy.mana + 1;
        handy.handCards.Remove(handy.handCards[handy.handCards.IndexOf(this.gameObject)]);
    }
    public IEnumerator FadeOut()
    {

        yield return new WaitForSeconds(0.1f);
        manag.chosen = null;
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        handy.UpdateCardPositions();
        if (zustand == 2)
        {
            Invoke("HideButton", 2f);
        }
    }
}
