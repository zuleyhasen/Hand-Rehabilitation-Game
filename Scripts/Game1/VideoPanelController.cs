using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPanelController : MonoBehaviour
{
    public GameObject videoPanel; // Panel GameObject'i
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
