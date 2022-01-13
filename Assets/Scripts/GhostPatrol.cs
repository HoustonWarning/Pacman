using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPatrol : GhostManager
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.flee.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)//if the direction that is randomly selected is the opposite of current on then we change it(to prevent backtrack)
            {
                index++;
                if (index >= node.availableDirections.Count)//to prevent overflow
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
