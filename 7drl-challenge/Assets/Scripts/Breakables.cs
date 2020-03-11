﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           if (PlayerController.instance.dashCounter > 0)
           {
                Destroy(gameObject);

                // drop pieces
                int piecesToDrop = Random.Range(1, maxPieces);

                for (int i = 0; i < piecesToDrop; i++)
                {
                    int randomPiece = Random.Range(0, brokenPieces.Length);

                    Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
                }

                // drop item
                if (shouldDropItem)
                {
                    float dropChance = Random.Range(0f, 100f);

                    if (dropChance < itemDropPercent)
                    {
                        int randomItem = Random.Range(0, itemsToDrop.Length);

                        Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                    }

                }
           }
        }
    }
}
