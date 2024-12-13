using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += PlayVideo;
    }

    void PlayVideo(VideoPlayer vp)
    {
        vp.Play();
    }
}
