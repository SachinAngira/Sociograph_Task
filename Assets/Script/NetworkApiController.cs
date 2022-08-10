using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NetworkApiController : MonoBehaviour
{
    enum StatusStates
    {
        Calling_Api,
        Please_Select_customer_state,
        Playing_Audio,
        Gettng_Audio
    }



    [SerializeField]
    private string Address;
    [SerializeField]
    private JsonRequestBodys jsonRequestBodys;
    [SerializeField]
    private List<Header> header;
    [SerializeField]
    private JsonResponseBody jsonObj;
    [SerializeField]
    private Image StatusImage;
    [SerializeField]
    private AudioSource audioSource;
    bool AudioPlaying;

    [Header("----UI----")]
    [SerializeField]
    private Text ContentText;
    [SerializeField]
    private Text StatusText;



    private void Start()
    {
        SetStatusText(StatusStates.Please_Select_customer_state);
    }

    private void Update()
    {
        if (AudioPlaying && !audioSource.isPlaying)
        {
            SetStatusText(StatusStates.Please_Select_customer_state);
            StatusImage.color = Color.white;
            SetContentText(string.Empty);
        }
    }
    public void InitApiCall(Text text)
    {
        AudioPlaying = false;
        SetStatusText(StatusStates.Calling_Api);
        jsonRequestBodys.customer_state = text.text;
        StatusImage.color = Color.cyan;
        string Json = JsonUtility.ToJson(jsonRequestBodys);
        StartCoroutine(CallApi(Json));
    }

    IEnumerator CallApi(string json)
    {

        var request = new UnityWebRequest(Address, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        foreach (var item in header)
        {
            request.SetRequestHeader(item.Key, item.value);
        }
        yield return request.SendWebRequest();

        Debug.Log("Status Code: " + request.responseCode);
        Debug.Log(request.downloadHandler.text);
        if (!(request.responseCode == 200))
        {
            StatusImage.color = Color.gray;
            SetContentText("Something went Wrong");
        }
        else
        {
            StatusImage.color = Color.green;
            jsonObj = JsonUtility.FromJson<JsonResponseBody>(request.downloadHandler.text);
            SetContentText(jsonObj.placeholder);
            SetStatusText(StatusStates.Gettng_Audio);
            StartCoroutine(PlayAudio(jsonObj.response_channels.voice));
        }
    }

    //This function changes the Status text based on the status value recived
    void SetStatusText(StatusStates Status)
    {
        StatusText.text = Status.ToString();
    }

    void SetContentText(string response)
    {
        ContentText.text = response;
    }

    IEnumerator PlayAudio(string url)
    {
        
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                  AudioClip  myClip = DownloadHandlerAudioClip.GetContent(www);
                    audioSource.clip = myClip;
                    audioSource.Play();
                    Debug.Log("Audio is playing.");
                    SetStatusText(StatusStates.Playing_Audio);
                    StatusImage.color = Color.yellow;
                    AudioPlaying = true;
            }
            }
        

    }
}



