using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpriteAnimation : MonoBehaviour
{
    [SerializeField]internal Image _image;
    [SerializeField]internal Sprite[] sprites = new Sprite[112];
    [SerializeField]internal float delay;
    
    public abstract void StartAnimation();
    
    public abstract void ResetToDefault();
}
