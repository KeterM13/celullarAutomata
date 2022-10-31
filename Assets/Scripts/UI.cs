using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject pauseUI;
    public void PlayGame() {
        SceneManager.LoadScene(1);
    }
    public void GoToMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void ContinueGame() {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
