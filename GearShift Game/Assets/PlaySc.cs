using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySc : MonoBehaviour {
    // Play butonuna baðlanacak
    public void PlayGame() {
        SceneManager.LoadScene("Video"); 
    }
}