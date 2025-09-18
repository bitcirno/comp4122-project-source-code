using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class WebManager : MonoBehaviour
{
    public String output;

    private void Update()
    {
        GameObject.Find("TextOutput").GetComponent<TMP_Text>().text = output;
    }

    public void GetRequest(string uri)
    {
        Debug.Log("Get");
        StartCoroutine(GetRequestOri(uri));
    }

    public void PutRequest(String uri, String data)
    {
        Debug.Log("Put");
        StartCoroutine(PutRequestOri(uri, data));
    }

    public void PostRequest(String uri, String field, String data)
    {
        Debug.Log("Post");
        StartCoroutine(PostRequestOri(uri, field, data));
    }

    public IEnumerator GetRequestOri(string uri)
    {
        Debug.Log("GET called");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            // Debug.Log(pages[2]);

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    output = pages[page] + ": Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    output = pages[page] + ": HTTP Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.Success:
                    output = pages[page] + ":Received:\n " + webRequest.downloadHandler.text;
                    break;
            }
        }
    }

    public IEnumerator PutRequestOri(String uri, String data)
    {

        using (UnityWebRequest www = UnityWebRequest.Put(uri, data))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                output = www.error;
            }
            else
            {
                output = "Upload complete!";
                Debug.Log("Upload complete!");
            }
        }
    }

    public IEnumerator PostRequestOri(String uri, String field, String data)
    {
        WWWForm form = new WWWForm();
        form.AddField(field, data);

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                output = www.error;
                Debug.Log(www.error);
            }
            else
            {
                output = "Form upload complete!";
                Debug.Log("Form upload complete!");
            }
        }
    }
}
