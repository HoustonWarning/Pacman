using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostManager
{
    private void OnDisable()
    {
        this.ghost.patrol.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.flee.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDist = float.MaxValue;

            foreach(Vector2 avialableDirection in node.availableDirections)//check all the available directions
            {
                Vector3 newPos = this.transform.position + new Vector3(avialableDirection.x, avialableDirection.y, 0.0f);//calculate the postion
                float distance = (this.ghost.target.position - newPos).sqrMagnitude;//calculate the distance to the target based on that position

                if (distance < minDist)//if it's closer than the current position then choose that direction to chase the target
                {
                    direction = avialableDirection;
                    minDist = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}

