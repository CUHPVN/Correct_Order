using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private CupBase cupBase;
    [SerializeField] private CupBase emptyCupBase;
    private SpriteRenderer spriteRenderer;
    private bool isEmpty = false;
    private bool isInfinity = false;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (transform.parent!=null)
        {
            if(transform.parent.name == "TakeTable") isInfinity = true;
        }
    }


    private void OnEnable()
    {
        spriteRenderer.sprite = cupBase.sprite;
    }
    private void Update()
    {
        EmptyCheck();
        SpriteUpdate();
    }
    private void EmptyCheck()
    {
        if (cupBase.type == CupType.Empty)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
    }
    private void SpriteUpdate()
    {
        spriteRenderer.sprite = cupBase.sprite;
    }
    public bool GetIsInfinity()
    {
        return isInfinity;
    }
    public bool GetIsEmpty()
    {
        return isEmpty;
    }
    public CupBase GetCupBase()
    {
        return cupBase;
    }
    public void SetCupBase(CupBase cup)
    {
        cupBase = cup;
    }
    public void SetToEmpty()
    {
        cupBase = emptyCupBase;
    }
}
