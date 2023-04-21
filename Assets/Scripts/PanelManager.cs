using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public RectTransform[] panels;
    private CanvasScaler scaler;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 idealPos = new Vector2(Screen.width, Screen.height);
        Vector2 additivePos = new Vector2(Screen.width, 0);
        scaler = GetComponentInParent<CanvasScaler>();

        GetComponent<RectTransform>().sizeDelta = idealPos;
        scaler.referenceResolution = idealPos;

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].sizeDelta = idealPos;
            panels[i].anchoredPosition = additivePos * i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
