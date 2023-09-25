using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] internal Letter[] letters = new Letter[26];
    public Letter currentLetter;

    [SerializeField] internal Slot slot;
    [SerializeField] internal Result result;
    [SerializeField] Text scoreText;
    [SerializeField] int score;
    [SerializeField] Bg bg;
    void Awake()
    {
        instance = this;
        score = PlayerPrefs.GetInt("Score", 0);
        AddPoints(0);
        bg.StartAnimation();
    }
    public Sprite RandomLetterSprite()
    {
        int x = UnityEngine.Random.Range(65, 71);
        char c = Convert.ToChar(x);
        Debug.Log("int:" + x + "=Letter:" + c);
        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i].letterChar == c)
            {
                return letters[i].letterSprite;
            }
        }
        return currentLetter.letterSprite;
    }
    public Sprite FindLetter()
    {
        //int x = UnityEngine.Random.Range(65, 91);
        int x = UnityEngine.Random.Range(65, 71);
        char c = Convert.ToChar(x);
        Debug.Log("int:" + x+ "=Letter:" + c);
        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i].letterChar == c)
            {
                currentLetter = letters[i];
                break;
            }
        }

        return currentLetter.letterSprite;
    }
    internal Letter GetCurrentLetter()
    {
        for (int i = 0; i < currentLetter.country.Count; i++)
        {
            currentLetter.country[i] = currentLetter.country[i].ToLower();
        }
        for (int i = 0; i < currentLetter.animals.Count; i++)
        {
            currentLetter.animals[i] = currentLetter.animals[i].ToLower();
        }
        for (int i = 0; i < currentLetter.objects.Count; i++)
        {
            currentLetter.objects[i] = currentLetter.objects[i].ToLower();
        }
        return currentLetter;

    }
    internal void AddPoints(int i)
    {
        score += i;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text ="Score: "+score.ToString();
    }
}
