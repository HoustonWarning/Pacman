using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellets : MonoBehaviour
{
    public int points = 10;

    protected virtual void EatPellet()//eating pallet behaviour. this will be overriden and powerpellet can access it too
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            EatPellet();
        }
    }


}
