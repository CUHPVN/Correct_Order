using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Cup cup;
    private bool isDragging;
    private Vector3 offset;
    [SerializeField]private List<Transform> colliderList = new();
    private float maxDistance = 0.35f;
    private GameObject clone;

    private void Awake()
    {
        cup = transform.parent.GetComponent<Cup>();
    }
    void OnMouseDown()
    {
        colliderList.Clear();
        if (cup.GetIsInfinity()||!cup.GetIsEmpty())
        {
            clone = Instantiate(transform.parent.gameObject, transform.parent.position, transform.rotation);
            isDragging = true;
            offset = clone.transform.position - GetMouseWorldPosition();
            clone.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            if (cup.GetIsInfinity()) return;
            ChangeToEmpty();
        }
        else
        {
            isDragging = true;
            offset = transform.parent.position - GetMouseWorldPosition();
        }
    }
    
    void OnMouseUp()
    {
        isDragging = false;
        if (clone != null)
        {
            CheckDrop(clone.transform.position, clone.GetComponent<Cup>());
            GameObject.Destroy(clone);
        }
    }

    void Update()
    {
        Dragging();
        CheckCollider();
    }
    private void Dragging()
    {
        if (isDragging && clone != null)
        {
            clone.transform.position = GetMouseWorldPosition() + offset;
        }
        else
        {
            if (isDragging)
            {
                transform.parent.position = GetMouseWorldPosition() + offset;
            }
        }
    }
    private void CheckCollider()
    {
        if (clone == null) return;

        Cup[] cups = FindObjectsOfType<Cup>();
        foreach (var cup in cups)
        {
                if (colliderList.Contains(cup.transform)) return;
                else
                {
                    colliderList.Add(cup.transform);
                }
        }
    }
    void OnDrawGizmos()
    {
        if (clone == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(clone.transform.position, maxDistance);
    }
    void ChangeToEmpty()
    {
        cup.SetToEmpty();
    }
    void RevertToCloneBase()
    {
        cup.SetCupBase(clone.GetComponent<Cup>().GetCupBase());
    }
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.parent.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void CheckDrop(Vector2 pos, Cup tcup)
    {
        bool isAir = true;
        foreach (Transform trans in colliderList)
        {
            if (Vector2.Distance(trans.position, pos) <= maxDistance)
            {
                isAir = false;
                if (trans.GetComponentInParent<Cup>().GetIsInfinity()) return;
                Cup placeCup = trans.GetComponentInParent<Cup>();              
                if (cup.GetIsInfinity()) placeCup.SetCupBase(tcup.GetCupBase());
                else
                {
                    CupBase temp = placeCup.GetCupBase();
                    placeCup.SetCupBase(tcup.GetCupBase());
                    cup.SetCupBase(temp);
                }
            }
        }
        if (isAir)
        {
            RevertToCloneBase();
        }
    }
}
