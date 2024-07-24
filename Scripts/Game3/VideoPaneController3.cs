using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPanelController3 : MonoBehaviour
{
    public GameObject videoPanel; 
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = videoPanel.GetComponentInChildren<VideoPlayer>();

    }

    public void HideVideoPanel()
    {
        videoPlayer.Stop(); // Videoyu durdur
        videoPanel.SetActive(false); // Paneli gizle
    }


}
