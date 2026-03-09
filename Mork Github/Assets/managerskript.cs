using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;

public class managerskript : MonoBehaviour
{
    public int phase;
    public int aktion;
    public bool fight;
    public GameObject chosen;
    public int deletetimer;
    public TMPro.TextMeshProUGUI aktionencounter;
    public List<GameObject> playerscreatures = new List<GameObject>();
    public List<GameObject> enemyscreatures = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phase = 1;
        aktion = 0;
        fight = false;
        deletetimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        playerscreatures = new List<GameObject>(GameObject.FindGameObjectsWithTag("besetzt"));
        aktionencounter.text = (3-aktion).ToString();
        if (aktion == 3)
        {
            phase = 2;
            aktionencounter.gameObject.SetActive(false);
            aktion = 4;
        }
        if (phase == 3)
        {
            phase = 4;
        }
        if (phase == 4)
        {
            fight = true;
        }
        if (deletetimer == 2)
        {
            StartCoroutine("deletethetimer");
            deletetimer = 3;
        }
    }
    public IEnumerator deletethetimer()
    {
        yield return new WaitForSeconds(1f);
        deletetimer = 1;
    }
}
