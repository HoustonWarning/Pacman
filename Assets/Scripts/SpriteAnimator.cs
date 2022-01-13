using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float transitionTime = 0.25f;
    public int animationFrame { get; private set; }
    public bool shouldLoop = true;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(LoopMove), this.transitionTime, this.transitionTime);//repeating the loop of sprite renderer for the transition time
    }

    private void LoopMove()//animate the sprite
    {
        if (!this.spriteRenderer.enabled)
        {
            return;//if there are no sprite rendereres then return
        }

        this.animationFrame++;

        if (this.animationFrame >= this.sprites.Length && this.shouldLoop)
        {
            this.animationFrame = 0;//if the sprtie passed the animation frame but it should loop then go back to zero
        }

        if(this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];//if its within bounds of array then continue to loop
        }
    }

    public void RestartLoop()//restart the animation loop
    {
        this.animationFrame = -1;

        LoopMove();
    }
}
