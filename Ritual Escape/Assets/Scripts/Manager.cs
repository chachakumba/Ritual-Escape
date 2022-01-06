using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class Manager : MonoBehaviour
{
    public bool _cursorVisible;
    public static Manager instance;
    public Player player;
    public Demon demon;
    public delegate void SoundEvent (SoundArgs arg);
    public event SoundEvent OnSound;
    public List<Transform> wanderSpots;
    public int amountOfItems = 0;
    public float itemsSpawnRadius = 10;
    public float nextFloorRad = 1;
    public List<Items> activatedItems = new List<Items>();
    public List<Items> inactiveItems = new List<Items>();
    public List<HolyPentagram> pentagrams = new List<HolyPentagram>();
    public List<HidingSpot> hidingSpots = new List<HidingSpot>();
    [Header("Interface")]
    public GameObject useInterface;
    public TMP_Text useDescription;
    public GameObject pentagramInterface;
    public Slider pentagramSliderInterface;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(AfterStart());
    }
    IEnumerator AfterStart()
    {
        yield return null;
    }
    public void CreateRandomItem()
    {
        int rand = UnityEngine.Random.Range(0, amountOfItems);
        switch (rand)
        {
            case 0:
                CreateItem(Item.stunScroll);
                break;
            default:

                break;
        }
    }
    public void CreateItem(Item createdItem)
    {
        int rand;
        bool done = false;
        int counter = 0;
        while (!done || counter < 20)
        {
            rand = UnityEngine.Random.Range(0, inactiveItems.Count);
            /*
            if(Mathf.Abs(player.transform.position.y - inactiveItems[rand].transform.position.y) > nextFloorRad ||
                Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(inactiveItems[rand].transform.position.x, inactiveItems[rand].transform.position.z))>  itemsSpawnRadius)*/
            if(Vector3.Distance(player.transform.position, inactiveItems[rand].transform.position) > itemsSpawnRadius)
            {
                inactiveItems[rand].currentItem = createdItem;
                inactiveItems[rand].gameObject.SetActive(true);
                activatedItems.Add(inactiveItems[rand]);
                inactiveItems.RemoveAt(rand);
                done = true;
            }
            counter++;
        }
    }
    public void RemoveItem(Items itemToRemove)
    {
        activatedItems.Remove(itemToRemove);
        if (!inactiveItems.Contains(itemToRemove)) inactiveItems.Add(itemToRemove);
        itemToRemove.gameObject.SetActive(false);
    }
    public void InvokeOnSound(SoundArgs arg)
    {
        OnSound.Invoke(arg);
    }
    public void CheckWin()
    {
        int countOfFinished = 0;
        bool foundUnfinished = false;
        foreach(HolyPentagram pent in pentagrams)
        {
            if (!pent.isDone) foundUnfinished = true;
            else countOfFinished++;
        }
        Debug.Log($"Finished {countOfFinished} pentagrams");
        if (!foundUnfinished) Win();
    }
    public void Win()
    {
        Debug.Log("Win!");
    }
    public void Lose()
    {
        Debug.Log("Lose!");
    }
    public void StunDemon(float seconds)
    {
        demon.Stun(seconds);
    }
    public void ShowUseInterface(string toDoWhat)
    {
        useInterface.SetActive(true);
        useDescription.text = toDoWhat;
    }
    public void HideUseInterface()
    {
        useInterface.SetActive(false);
    }
    public void ShowPentagramInterface()
    {
        pentagramInterface.SetActive(true);
    }
    public void HidePentagramInterface()
    {
        pentagramInterface.SetActive(false);
    }
}
public class SoundArgs : EventArgs
{
    public Vector3 soundPos;
    public float loudness;
    public Floor floor;
    public bool loud;
    public SoundArgs(Vector3 pos, float loud) { soundPos = pos; loudness = loud; }
}
public enum Floor
{
    cellar,first,second
}