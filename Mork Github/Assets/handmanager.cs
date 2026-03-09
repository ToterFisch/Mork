using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using TMPro;

public class handmanager : MonoBehaviour
{
    public List<GameObject> handCards = new List<GameObject>();
    [SerializeField] public int maxHandSize;
    [SerializeField] public GameObject cardPrefab;
    [SerializeField] public SplineContainer splineContainer;
    [SerializeField] public Transform staple;
    public int mana;
    public TMPro.TextMeshProUGUI manacounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        manacounter.text = mana.ToString();
    }
    public void DrawCard()
    {
        if (handCards.Count >= maxHandSize) return;
        GameObject g = Instantiate(cardPrefab, staple.position, staple.rotation);
        handCards.Add(g);
        UpdateCardPositions();
    }
    public void UpdateCardPositions()
    {
        Debug.Log("update");
        if (handCards.Count == 0) return;
        float cardSpacing = 1f / maxHandSize;
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for(int i = 0; i< handCards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            handCards[i].transform.DOMove(splinePosition, 0.25f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
            SpriteRenderer sr = handCards[i].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i; // Karten am Ende der Liste oben zeichnen
            }
        }
    }
}
