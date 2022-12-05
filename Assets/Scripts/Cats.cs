using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cats : MonoBehaviour
{
    public float speed;
    public float runSpeed;
    public bool mustFlip;
    public bool mustMove;
    public bool hardFlip;
    public bool mustEat;
    public bool hadEaten;
    public bool canLove;
    public int energy;
    public int age;
    public float runTime;
    public float time;
    public float timeToLooseEnergy;
    public float energyTime;
    public GameObject babyCat;
    private int genderSelec;
    public Rigidbody rb;
    public List<GameObject> perception;
    public int health;
    public string gender;

    private int aiUpdateRate = 10;
    private int frameCount;

    public State currentState;
    public enum State {
        spawn,
        move,
        eat,
        noEnergy,
        dying,
        off
    }

    void Start() {
        genderSelec = Random.Range(0, 2);
        switch (genderSelec) {
            case 0:
                gender = "Male";
                break;
            case 1:
                gender = "Female";
                break;
            default:
                break;
        }
        canLove = false;
        energy = Random.Range(50, 101);
        age = Random.Range(1, 49);
        rb = GetComponent<Rigidbody>();
        currentState = State.spawn;
        health = 3;
    }

    void Update() {
        timeToLooseEnergy += Time.deltaTime;
        if (timeToLooseEnergy >= 2) {
            energy -= 1;
            timeToLooseEnergy = 0;
        }

        if (currentState == State.move) {
            Move();
        }
        if (currentState == State.spawn) {
            Spawn();
        }

        if (energy <= 20 && energy > 1) {
            currentState = State.eat;
        }
        if (energy <= 1) {
            currentState = State.noEnergy;
        }

        frameCount++;
        if (frameCount % aiUpdateRate == 0) {
            CheckForTarget();
        }
        if (currentState == State.eat) {
            Eat();
        }
        if (currentState == State.noEnergy) {
            NoEnergy();
        }
        if (currentState == State.dying) {
            Dying();
        }
        if (currentState == State.off) {
            Off();
        }
        if(health<=0) {
            currentState = State.dying;
        }
        //if (canLove) {
        //    Love();
        //}
    }

    public void Spawn() {
        currentState = State.move;
    }
    public void Move() {
        if (mustMove) {

            rb.velocity = transform.forward * speed * Time.deltaTime;

        }
        if (mustFlip) {
            Flip();
        }
    }
    public void Eat() {
       

        for (int i = 0; i < perception.Count; i++) {
            if (perception[i].TryGetComponent<mouseBehavior>(out mouseBehavior mouseTransform)) {

                transform.LookAt(mouseTransform.transform);
                mustEat = true;

                break;
            }

        }

        mustMove = true;
        currentState = State.move;


    }
    public void NoEnergy() {
        time += Time.deltaTime;
        if (time >= energyTime && energy <= 1) {
            currentState = State.dying;
        }
    }
    public void Dying() {
        gameObject.SetActive(false);
        currentState = State.off;
    }

    public void Off() {
        Destroy(this);
    }

    public void Flip() {
        mustFlip = false;
        mustMove = false;
        if (!hardFlip) {
            transform.Rotate(0, 45, 0);
            mustFlip = false;
        }
        else {
            transform.Rotate(0, 180, 0);
            hardFlip = false;
        }
        mustMove = true;

    }

    public void Love() {
        Instantiate(babyCat, this.transform);
        canLove = false;
    }

    public void CheckForTarget() { //cambiar a ratones
        for (int i = 0; i < perception.Count; i++) {
            if (perception[i].TryGetComponent<mouseBehavior>(out mouseBehavior mouseObj)) {
                hardFlip = true;
                break;
            }

        }
    }

    public void MoveTo(Transform trans) {
        transform.position = Vector3.MoveTowards(transform.position, trans.position, speed);
    }


    private void OnCollisionEnter(Collision collision) {
        mustFlip = true;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        mustFlip = true;

        
    }

    private void OnCollisionStay(Collision collision) {
        if (mustEat == true && collision.gameObject.CompareTag("Mouse")) {
            energy += 70;
            collision.gameObject.GetComponent<mouseBehavior>().health -= 1;
            hadEaten = true;
            currentState = State.move;
            mustEat = false;

        }
    }

    private void OnTriggerEnter(Collider other) {

        perception.Add(other.gameObject);



        if (other.gameObject.CompareTag("Cat")&& age>=48) {
            if (other.GetComponent<Cats>().gender != this.gender && other.GetComponent<Cats>().age>=48) {
                canLove = true;
            }
        }
        if(other.gameObject.CompareTag("Water")) {
            hardFlip = true;
            mustFlip = true;
        }
        if (other.gameObject.CompareTag("Fire")) {
            health -= 1;
        }
    }
}
