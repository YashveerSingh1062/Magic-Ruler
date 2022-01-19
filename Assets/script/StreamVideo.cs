using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
     public RawImage rawImage;
     public VideoPlayer videoPlayer;
     public AudioSource audioSourse;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    // Update is called once per frame
    IEnumerator PlayVideo()
    {
         videoPlayer.Prepare();
         WaitForSeconds waitForSeconds = new WaitForSeconds(1);
         while(!videoPlayer.isPrepared)
         {
             yield return waitForSeconds;
             break;
         }
         rawImage.texture = videoPlayer.texture;
         videoPlayer.Play();
         audioSourse.Play();

    }
}
