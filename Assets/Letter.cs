using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Letter", order = 1)]
public class Letter : ScriptableObject
{
    public char letterChar;
    public Sprite letterSprite;
    public List<string> animals;
    public List<string> objects;
    public List<string> country;

   
}


