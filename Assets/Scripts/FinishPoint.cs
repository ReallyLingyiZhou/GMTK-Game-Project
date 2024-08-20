using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     SceneManager.LoadSceneAsync(0);
    //     UnlockNewLevel();
    // }
    public EndMenu endMenu;
    public TaskTracker taskTracker;
    void OnTriggerEnter(Collider collision)
    {
        UnlockNewLevel();
        endMenu.OpenEndMenu(taskTracker.GetAllTaskSummery());
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

    public void tool_goToNextLevel()
    {
        endMenu.GameObject().SetActive(true);
        UnlockNewLevel();
    }
}
