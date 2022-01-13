using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int points = 200;
    public Movement movement { get; private set; }
    public GhostBase ghostBase { get; private set; }
    public GhostPatrol patrol { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFlee flee { get; private set; }

    public GhostManager ghostManager;

    public Transform target;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.ghostBase = GetComponent<GhostBase>();
        this.patrol = GetComponent<GhostPatrol>();
        this.chase = GetComponent<GhostChase>();
        this.flee = GetComponent<GhostFlee>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        this.flee.Disable();
        this.chase.Disable();
        this.patrol.Enable();

        if (this.ghostBase != this.ghostManager)
        {
            this.ghostBase.Disable();
        }

        if (this.ghostManager != null)
        {
            this.ghostManager.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.flee.enabled)
            {
                FindObjectOfType<GameManager>().GhostsEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }


}
