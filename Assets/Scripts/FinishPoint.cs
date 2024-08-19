using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     SceneManager.LoadSceneAsync(0);
    //     UnlockNewLevel();
    // }
    private void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.tag == "Player")
        // {
        SceneManager.LoadSceneAsync(0);
        UnlockNewLevel();
        // }
    }
    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 0) + 1);
            PlayerPrefs.Save();
        }
    }
}
