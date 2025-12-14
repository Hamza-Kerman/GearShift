using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndController : MonoBehaviour {
    public VideoPlayer videoPlayer;

    void Start() {
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd; // Video bitince
    }

    void OnVideoEnd(VideoPlayer vp) {
        SceneManager.LoadScene("s"); // Video bittiðinde oyunun ana sahnesine geç
    }
}

