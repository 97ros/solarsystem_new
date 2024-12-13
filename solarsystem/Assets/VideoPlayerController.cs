using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Prepara il video in anticipo
        videoPlayer.Prepare();

        // Quando il video è pronto, avvialo
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        vp.Play();
    }
}
