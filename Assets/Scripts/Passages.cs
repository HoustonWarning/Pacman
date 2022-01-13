using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passages : MonoBehaviour
{
    public Transform portal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = collision.transform.position;
        position.x = this.portal.position.x;
        position.y = this.portal.position.y;

        collision.transform.position = position;
    }
}
