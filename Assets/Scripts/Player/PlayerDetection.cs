using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDetection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CircleCollider2D _detectCollider;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.IsTouching(_detectCollider))
            return;

        if (collision.GetComponent<ICollectable>() == null)
            return;

        ICollectable collectable = collision.GetComponent<ICollectable>();
        collectable.Collect(GetComponent<Player>());
    }
}
