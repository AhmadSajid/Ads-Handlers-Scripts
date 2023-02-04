using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] frameArray;
    private int currentFrame;
    private float timer;
    private float frameRate = 0.1f;
    private SpriteRenderer _spriteRennderer;

    private void Awake()
    {
        _spriteRennderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame = (currentFrame + 1) % frameArray.Length;
            _spriteRennderer.sprite = frameArray[currentFrame];
        }
    }
}
