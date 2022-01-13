using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostManager : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float duration;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;

    }

    public void Enable()
    {
        EnableForDuration(this.duration);

    }

    public virtual void EnableForDuration(float duration)
    {
        this.enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);

    }

    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();

    }

}
