using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLayoutManager : MonoBehaviour
{
    public RectTransform[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height / 10);
        int[] arr = { -2, -1, 0, 1, 2 };
        Vector2 posX = new Vector2(Screen.width / 5, 0);
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<RectTransform>().anchoredPosition = posX * arr[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
