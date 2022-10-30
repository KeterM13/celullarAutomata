using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    private void Update() {
        
    }
}
