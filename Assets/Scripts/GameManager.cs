using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startUI;
    private void Awake() {
        Time.timeScale = 0;
    }
    public void ChooseCat() {
        character.numChar = 0;
        Time.timeScale = 1;
        startUI.SetActive(false);
    }
    public void ChooseFire() {
        character.numChar = 1;
        Time.timeScale = 1;
        startUI.SetActive(false);
    }
    public void ChooseWater() {
        character.numChar = 2;
        Time.timeScale = 1;
        startUI.SetActive(false);
    }
}
