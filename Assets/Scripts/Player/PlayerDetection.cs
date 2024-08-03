using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Candy>() == null)
            return;

        Candy candy = collision.GetComponent<Candy>();
        candy.Collet(GetComponent<Player>());
    }
}
