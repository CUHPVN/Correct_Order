using UnityEngine;

public class HorizontalObjectGroup : MonoBehaviour
{
    public float spacing = 1.0f;

    void Start()
    {
        ArrangeObjects();
    }
    private void Update()
    {
        ArrangeObjects();
    }
    void ArrangeObjects()
    {
        int childCount = transform.childCount;
        float totalWidth = (childCount - 1) * spacing;
        float startX = -totalWidth / 2;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.localPosition = new Vector3(startX + i * spacing, 0, 0);
        }
    }
}
