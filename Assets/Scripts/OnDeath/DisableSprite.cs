using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSprite : MonoBehaviour
{
    public void DisableSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void EnableSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
