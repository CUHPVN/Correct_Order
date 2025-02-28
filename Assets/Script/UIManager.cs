using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform Top;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private TMP_Text cupCount;
    public float displayTime = 2.0f;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        winText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cupCount.text = GameManager.Instance.GetCupCount()+"";
    }
    public void ShowCorrectText()
    {
        Transform text = SpawnManager.Instance.Spawn("CorrectCount", Top.transform.position.x, Top.transform.position.y-1, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = GameManager.Instance.GetCorrectCount() + " Correct!";
    }
    public void ShowWinText()
    {
        ShowPopup(winText,"You Win!");
    }
    public void ShowPopup(TMP_Text TMPtext,string message)
    {
        TMPtext.text = message;
        TMPtext.gameObject.SetActive(true);
        StartCoroutine(HidePopupAfterDelay(TMPtext));
    }

    IEnumerator HidePopupAfterDelay(TMP_Text TMPtext)
    {
        yield return new WaitForSeconds(displayTime);
        TMPtext.gameObject.SetActive(false);
    }
}
