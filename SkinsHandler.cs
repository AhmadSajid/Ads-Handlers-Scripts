using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsHandler : MonoBehaviour
{
    public Sprite[] _skinSprites;

    public void ChangeSkin()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = _skinSprites[Random.Range(0, _skinSprites.Length)];
    }

}
