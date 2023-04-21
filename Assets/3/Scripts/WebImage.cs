using System.Collections;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using UnityEngine.Serialization;
using UnityEngine.Android;
using Unity.VisualScripting;

public static class WebImage
{
    private class WebRequestsMono : MonoBehaviour { }

    private static WebRequestsMono webRequestsMono;

    public static bool imgDownloaded;

    static bool cacheImages = true;
            
    public static void Init()
    {
        if(webRequestsMono == null)
        {
            GameObject gameObject = new GameObject("WebRequests");
            webRequestsMono = gameObject.AddComponent<WebRequestsMono>();

        }
    }
    public static void Get(string url, Action<string> onError, Action<string> onSuccess)
    {
        webRequestsMono.StartCoroutine(GetCoroutine(url, onError, onSuccess));
    }

    private static IEnumerator GetCoroutine(string url, Action<string> onError, Action<string> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
        {
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                onError(unityWebRequest.error);
            }
            else
                onSuccess(unityWebRequest.downloadHandler.text);
        }
    }
    public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess, string i)
    {
        webRequestsMono.StartCoroutine(GetTextureCoroutine(url, onError, onSuccess, i));
    }

    private static IEnumerator GetTextureCoroutine(string url,Action<string> onError, Action<Texture2D> onSuccess,string i)
    {
        string filename = "Image" + i + ".png";
        string fileLoc = Application.persistentDataPath+ "/" + filename;
        Debug.Log(fileLoc);
        if (!File.Exists(fileLoc))
        {
            using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    onError(unityWebRequest.error);
                }
                else
                {
                    DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
                    onSuccess(downloadHandlerTexture.texture);
                    var downloadData = downloadHandlerTexture.data;
                    imgDownloaded = true;
                    if(cacheImages)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fileLoc));
                        File.WriteAllBytes(fileLoc, downloadData);
                        Caching.AddCache(Path.GetDirectoryName(fileLoc));
                    }
                }

            }
        }
        else
        {
            var picBytes = File.ReadAllBytes(fileLoc);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(picBytes);
            onSuccess(texture);
            imgDownloaded = true;
        }


    }

}
