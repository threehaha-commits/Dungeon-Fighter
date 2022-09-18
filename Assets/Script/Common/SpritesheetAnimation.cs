using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesheetAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private float ChangeAnimationRate;
    private SpriteRenderer SpriteRenderer;
    private IEnumerator AnimPlayer;
    private int index;
    private int Index
    {
        get => index;
        set
        {
            index = value;
            if (index == 4)
                index = 0;
        }
    }

    private void Start() 
    { 
        SpriteRenderer = GetComponent<SpriteRenderer>();
        AnimPlayer = Play();
        GetComponent<CoroutineHandler>().AddCoroutine(this, AnimPlayer);
    }

    private IEnumerator Play()
    {
        while (true)
        {
            SpriteRenderer.sprite = Sprites[Index];
            yield return new WaitForSeconds(ChangeAnimationRate);
            Index++;
        }
    }
}