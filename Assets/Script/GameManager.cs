using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int cupCount = 1;
    private int correctCount;
    [SerializeField] private GameObject cup;
    [SerializeField] private Transform table;
    private List<Cup> cups = new();
    [SerializeField] private List<CupType> correctList= new();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        for (int i = 0; i < cupCount; i++)
        {
            CupType randomType = GetRandomEnumValue<CupType>();
            while (randomType == CupType.Empty) randomType = RanCup();
            correctList.Add(randomType);
        }
        for (int i = 0; i < cupCount; i++)
        {
            Transform temp = Instantiate(cup, Vector3.zero, Quaternion.identity).transform;
            Cup tcup = temp.GetComponent<Cup>();
            cups.Add(tcup);
            temp.parent = table;
        }
        correctCount = 0;
    }

    void Update()
    {
        while (cups.Count > cupCount)
        {
            foreach (Cup cup in cups)
            {
                Destroy(cup.gameObject);
            }
            cups.Clear();
            correctList.Clear();
            Load();
        }
        while (cups.Count < cupCount)
        {
            foreach (Cup cup in cups)
            {
                Destroy(cup.gameObject);
            }
            cups.Clear();
            correctList.Clear();
            Load();
        }
    }
    public void Load()
    {
        for (int i = 0; i < cupCount; i++)
        {
            CupType randomType = GetRandomEnumValue<CupType>();
            while (randomType == CupType.Empty) randomType = RanCup();
            correctList.Add(randomType);
        }
        for (int i = 0; i < cupCount; i++)
        {
            Transform temp = Instantiate(cup, Vector3.zero, Quaternion.identity).transform;
            Cup tcup = temp.GetComponent<Cup>();
            cups.Add(tcup);
            temp.parent = table;
        }
        correctCount = 0;
    }
    public void IncCup()
    {
        cupCount++;
    }
    public void DecCup()
    {
        cupCount--;
    }
    public void CheckTrue()
    {
        correctCount = 0;
        for (int i = 0; i< cupCount; i++)
        {
            if (cups[i].GetCupBase().type == correctList[i])
            {
                correctCount++;
            }
        }
        UIManager.Instance.ShowCorrectText();

        if (correctCount == cupCount)
        {
            UIManager.Instance.ShowWinText();
        }
    }
    private CupType RanCup()
    {
        return GetRandomEnumValue<CupType>();
    }
    public T GetRandomEnumValue<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        System.Random random = new System.Random();
        return (T)values.GetValue(random.Next(values.Length));
    }
    public int GetCorrectCount()
    {
        return correctCount;
    }
    public int GetCupCount()
    {
        return cupCount;
    }
}
 