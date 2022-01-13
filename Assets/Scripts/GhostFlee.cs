using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFlee : GhostManager
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer vunerable;
    public SpriteRenderer flash;

    public bool hasBeenEaten { get; private set; }

    public override void EnableForDuration(float duration)
    {
        base.EnableForDuration(duration);
        this.body.enabled = false;
        this.eyes.enabled = false;
        this.vunerable.enabled = true;
        this.flash.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();
        this.body.enabled = true;
        this.eyes.enabled = true;
        this.vunerable.enabled = false;
        this.flash.enabled = false;
    }

    private void Flash()
    {
        if (!this.hasBeenEaten)
        {
            this.vunerable.enabled = false;
            this.flash.enabled = true;
            this.flash.GetComponent<SpriteAnimator>().RestartLoop();
        }
        
    }

    private void BeenEaten()//go back home after being eaten
    {
        this.hasBeenEaten = true;
        Vector3 position = this.ghost.ghostBase.baseTransform.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;

        this.ghost.ghostBase.EnableForDuration(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.vunerable.enabled = false;
        this.flash.enabled = false;

    }

    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.hasBeenEaten = false;

    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.hasBeenEaten = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                BeenEaten();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDist = float.MinValue;

            foreach (Vector2 avialableDirection in node.availableDirections)//check all the available directions
            {
                Vector3 newPos = this.transform.position + new Vector3(avialableDirection.x, avialableDirection.y, 0.0f);//calculate the postion
                float distance = (this.ghost.target.position - newPos).sqrMagnitude;//calculate the distance to the target based on that position

                if (distance > maxDist)//if it's closer than the current position then choose that direction to chase the target
                {
                    direction = avialableDirection;
                    maxDist = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}
