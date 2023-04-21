using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ImageLoader : MonoBehaviour
{
    /*List<string> urls = new List<string>();*/
    DateTime now = DateTime.Now;
    DateTime today = DateTime.Today;

    public Image[] images;
    string[] urls ={ "https://wallpaperaccess.com/full/87731.jpg",
            "https://wallpaperaccess.com/full/1126085.jpg",
            "https://wallpaperaccess.com/full/1308917.jpg",
            "https://wallpaperaccess.com/full/281585.jpg" };

    void Start()
    {
        WebImage.Init();
       StartCoroutine(Download());
        /*WebImage.GetTexture(urls[0], (string error) =>
        {
            Debug.Log("Error: " + error);
        },

            (Texture2D texture2D) =>
            {
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 10);
                images[0].sprite = sprite;
            });*/
    }

    IEnumerator Download()
    {
        for(int i = 0; i < urls.Length; i++)
        {
            WebImage.imgDownloaded = false;
            WebImage.GetTexture(urls[i],(string error) =>
            {
                Debug.Log("Error: " + error);
            }, 

            (Texture2D texture2D) =>
            {
                Debug.Log(i);
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 10);
                images[i].sprite = sprite;
                
            }, i.ToString());
            yield return new WaitUntil(() => WebImage.imgDownloaded);
        }
    }
        
}
