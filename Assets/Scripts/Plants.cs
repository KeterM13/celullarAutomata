using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public int health;
    public Transform plantTransform;
    public float time;
    public float timeToRespawn;
    private void Start() {
        plantTransform = transform;  
    }

    public void Damage() {
        health -= 1;
    }

    private void Update() {
        if (health <= 0) {
            gameObject.SetActive(false);
        }
        time += Time.deltaTime;
        if (time >= timeToRespawn) {
            health = 3;
            gameObject.SetActive(true);
            time = 0;
        }
    }

    
}
