using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : SpriteAnimation
{

    IEnumerator Animate()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            _image.sprite = sprites[i];
        }
    }
    [ContextMenu("Animate")]
    public override void StartAnimation()
    {
        StartCoroutine(Animate());
    }

    [ContextMenu("Reset")]
    public override void ResetToDefault()
    {
        _image.sprite = sprites[0];
    }
}
