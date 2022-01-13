using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDir;
    public LayerMask obstacleLayer;

    public Rigidbody2D rigidBody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDir { get; private set; }//in order to queue up the movements to simplfy the movement of pacman
    public Vector3 startingPos{ get; private set;}

    private void Awake()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.startingPos = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDir;
        this.nextDir = Vector2.zero;
        this.transform.position = this.startingPos;
        this.rigidBody.isKinematic = false;//for ghosts so they can pass through walls
        this.enabled = true;

    }

    private void Update()
    {
        if (this.nextDir != Vector2.zero)//queueing up the next direction
        {
            SetDirection(this.nextDir);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigidBody.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rigidBody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool isForced = false)
    {
        if (isForced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDir = Vector2.zero;//emptying the queue move
        }
        else
        {
            this.nextDir = direction;
        }
    }

    public bool Occupied(Vector2 direction)//to check to see if a place is not occupied as a prerequisite for movement
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }
}
