using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public enum SlotItemType
{
    Letter,
    Object,
    City,
    Animal
}
[System.Serializable]
public struct SlotItem
{
    public Image _image;
    public SlotItemType slotItem;
    public RectTransform handleParent;
}
public class Slot : MonoBehaviour
{
    public SlotItem slotOne;
    public SlotItem slotTwo;
    public SlotItem slotThree;
    public SlotItem slotFour;
    [SerializeField]
    Spin spinBtn;
    [SerializeField]
    internal bool spinPressed;
    [SerializeField]
    Handle handle;
    [SerializeField] Sprite citySprite;
    [SerializeField] Sprite objSprite;
    [SerializeField] Sprite animalSprite;
    public RectTransform imagePrefab;

    [SerializeField] float animationTime;
    [SerializeField] List<AnimationCurve> curves = new List<AnimationCurve>();
    [SerializeField] Vector2 target;
    List<RectTransform> destroyObjects = new List<RectTransform>();
    [SerializeField]int objectLength;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetRandomSlot()
    {
        slotOne.slotItem = SlotItemType.Letter;
        slotTwo.slotItem = GetRandomType();
        slotThree.slotItem = GetRandomType();
        slotFour.slotItem = GetRandomType();

        SetImage(slotOne);
        SetImage(slotTwo);
        SetImage(slotThree);
        SetImage(slotFour);

        GenerateRandom();
    }
    void GenerateRandom()
    {
        foreach (var item in destroyObjects)
        {
            Destroy(item.gameObject);
        }
        destroyObjects = new List<RectTransform>();

        for (int i = 0; i < objectLength; i++)
        {
            RectTransform RT=  Instantiate(imagePrefab, slotOne.handleParent);
            RT.SetAsFirstSibling();
            RT.GetComponent<Image>().sprite = GameManager.instance.RandomLetterSprite();
            destroyObjects.Add(RT);
        }
        for (int i = 0; i < objectLength; i++)
        {
            RectTransform RT_2 = Instantiate(imagePrefab, slotTwo.handleParent);
            RT_2.SetAsFirstSibling();
            RT_2.GetComponent<Image>().sprite = GetRandomItem();

            RectTransform RT_3 = Instantiate(imagePrefab, slotThree.handleParent);
            RT_3.SetAsFirstSibling();
            RT_3.GetComponent<Image>().sprite = GetRandomItem();

            RectTransform RT_4 = Instantiate(imagePrefab, slotFour.handleParent);
            RT_4.SetAsFirstSibling();
            RT_4.GetComponent<Image>().sprite = GetRandomItem();
            destroyObjects.Add(RT_2);
            destroyObjects.Add(RT_3);
            destroyObjects.Add(RT_4);

        }
    }
    Sprite GetRandomItem()
    {
        int x = Random.Range(0, 101);
        if (x >= 0 && x <= 33)
        {
            return animalSprite ;
        }
        else if (x > 33 && x <= 66)
        {
            return citySprite;
        }
        else
        {
            return objSprite;
        }
    }
    public void SetImage(SlotItem item)
    {
        if(item.slotItem== SlotItemType.Letter)
        {
            item._image.sprite = GameManager.instance.FindLetter();
        }
        else if (item.slotItem == SlotItemType.Animal)
        {
            item._image.sprite = animalSprite;
        }
        else if (item.slotItem == SlotItemType.Object)
        {
            item._image.sprite = objSprite;
        }
        else if (item.slotItem == SlotItemType.City)
        {
            item._image.sprite = citySprite;
        }
    }
  
    SlotItemType GetRandomType()
    {
        int x = Random.Range(0, 101);
        if(x>= 0 && x <= 33)
        {
            return SlotItemType.Animal;
        }
        else if (x > 33 && x <= 66)
        {
            return SlotItemType.City;
        }
        else
        {
            return SlotItemType.Object;
        }        
    }

    public void SpinePress()
    {
        if (!spinPressed)
        {
            spinPressed = true;
            handle.StartAnimation();
            spinBtn.StartAnimation();
            SetRandomSlot();
            Animate();
        }
    }
    
  
    void Animate()
    {
        slotOne.handleParent.anchoredPosition = Vector2.zero;
        slotTwo.handleParent.anchoredPosition = Vector2.zero;
        slotThree.handleParent.anchoredPosition = Vector2.zero;
        slotFour.handleParent.anchoredPosition = Vector2.zero;

        slotOne.handleParent.DOLocalMove(target, animationTime).SetEase(curves[Random.Range(0, curves.Count)]);
        slotTwo.handleParent.DOLocalMove(target, animationTime).SetEase(curves[Random.Range(0, curves.Count)]);
        slotThree.handleParent.DOLocalMove(target, animationTime).SetEase(curves[Random.Range(0, curves.Count)]);
        slotFour.handleParent.DOLocalMove(target, animationTime)
            .SetEase(curves[Random.Range(0, curves.Count)]).OnComplete(()=> 
            {
                GameManager.instance.result.gameObject.SetActive(true);
            });
    }
}
