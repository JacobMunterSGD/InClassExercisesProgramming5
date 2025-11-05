using System;
using System.Collections;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CatFact : MonoBehaviour
{

    [SerializeField] TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GetWebRequest());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();   
            StartCoroutine(GetWebRequest());
        }
    }

    IEnumerator GetWebRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://catfact.ninja/fact"))
        {
            // Request and wait for the desired page (must be a coroutine !)
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                //string json = JsonUtility.ToJson(myObject);

                Fact fact = JsonUtility.FromJson<Fact>(webRequest.downloadHandler.text);

                text.text = fact.fact;
            }
        }
    }
}

public class Fact
{
    public string fact = "";
    public int integer;
}
