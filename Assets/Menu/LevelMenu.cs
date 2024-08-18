using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 0);
        for (int i = 0; i <= unlockedLevel; i++)
        {
            ToggleButtonAwake(buttons[i], true);
        }
        for (int i = unlockedLevel + 1; i < buttons.Length; i++)
        {
            ToggleButtonAwake(buttons[i], false);
        }
    }
    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }

    private void ToggleButtonAwake(Button button, bool state)
    {
        button.interactable = state;
        button.gameObject.transform.GetChild(0).gameObject.SetActive(state);
        button.gameObject.transform.GetChild(1).gameObject.SetActive(state);
        button.gameObject.transform.GetChild(2).gameObject.SetActive(!state);
    }
}
