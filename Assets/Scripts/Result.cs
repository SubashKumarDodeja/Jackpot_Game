using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct ResultSlot
{
    public Image _image;
    public Image _rightWrong;
    public SlotItemType itemType;
}
public class Result : MonoBehaviour
{
    [SerializeField]Sprite correct;
    [SerializeField]Sprite wrong;

   
    public ResultSlot[] resultSlot = new ResultSlot[3];
    ResultSlot currentSlot;
    [SerializeField] InputField writeWord;

    Letter currentLetterData;
    GameManager gm;
    int slotIndex = 0;
    [SerializeField]
    GameObject NextRountBtn;

    private void OnEnable()
    {
        gm = GameManager.instance;
        currentLetterData = gm.GetCurrentLetter();
        SetResultImages(0, gm.slot.slotTwo);
        SetResultImages(1, gm.slot.slotThree);
        SetResultImages(2, gm.slot.slotFour);
        slotIndex = 0;
        StatTimer();
    }
    void SetResultImages(int i, SlotItem inpSlot)
    {
        resultSlot[i].itemType = inpSlot.slotItem;
        resultSlot[i]._image.sprite = inpSlot._image.sprite;
        resultSlot[i]._rightWrong.gameObject.SetActive(false);
        Debug.Log("outSlot:" + resultSlot[i].itemType + " inpSlot:" + inpSlot.slotItem);
    }
    public void TextEnter()
    {

        if (writeWord.text.Length <= 0 && timer>0)
            return;
        StatTimer();
        ResultSlot currentSlot = resultSlot[slotIndex];
        currentSlot._rightWrong.gameObject.SetActive(true);
        if (CheckWord(currentSlot))
        {
            currentSlot._rightWrong.sprite = correct;
            gm.AddPoints(5);
        }
        else
        {
            currentSlot._rightWrong.sprite = wrong;
        }

        if(slotIndex< resultSlot.Length-1)
        {
            slotIndex++;
        }
        else
        {
            EndTime();
            NextRountBtn.SetActive(true);
        }
        writeWord.text = "";
    }
    public void NextRound()
    {
        gm.result.gameObject.SetActive(false);
        gm.slot.spinPressed = false;
        NextRountBtn.SetActive(false);

    }
    public bool CheckWord(ResultSlot currentSlot)
    {
        Debug.Log("writeWord.text.ToLower():" + writeWord.text.ToLower());
        Debug.Log("currentSlot.itemType:" + currentSlot.itemType);
        if (currentSlot.itemType == SlotItemType.City)
        {
            Debug.Log("Country");
            if (currentLetterData.country.Contains(writeWord.text.ToLower()))
                return true;
            else
                return false;
        }
        else if (currentSlot.itemType == SlotItemType.Animal)
        {
            Debug.Log("Animal");
            if (currentLetterData.animals.Contains(writeWord.text.ToLower()))
                return true;
            else
                return false;
        }
        else if (currentSlot.itemType == SlotItemType.Object)
        {
            Debug.Log("Object");
            if (currentLetterData.objects.Contains(writeWord.text.ToLower()))
                return true;
            else
                return false;
        }
        return false;
    }
    [SerializeField]
    Text timerText;
    [SerializeField]
    float timer = 10f;
    string tempTimer;
    bool enableUpdate = false;
    public void StatTimer()
    {
        timer = 11;
        enableUpdate = true;


    }
    public void EndTime()
    {
        enableUpdate = false;
    }
    void Update()
    {
        if (enableUpdate)
            TimerTextSet();
    }
    float seconds;
    float minutes;
    void TimerTextSet()
    {

        timer -= Time.deltaTime;
        minutes = Mathf.FloorToInt(timer / 60);
        seconds = Mathf.FloorToInt(timer % 60);
        tempTimer = String.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = tempTimer;
        if (timer <= 0)
        {
            timerText.text = "00:00";
            TextEnter();
        }

    }
}
