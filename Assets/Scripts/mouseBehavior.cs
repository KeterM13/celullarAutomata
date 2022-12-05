using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseBehavior : MonoBehaviour
{
    public float speed;
    public float runSpeed;
    public bool mustFlip;
    public bool mustMove;
    public bool hardFlip;
    public bool mustEat;
    public bool catNear;
    public bool hadEaten;
    public int energy;
    public float runTime;
    public float time;
    public float timeToEscape;
    public float timeToLooseEnergy;
    public float energyTime;
    public int health;
    public GameObject babyMouse;
    private int genderSelec;
    public Rigidbody rb;
    public List<GameObject> perception;
    [SerializeField] private Transform food;
    public string gender;
    public int age;

    private int aiUpdateRate = 10;
    private int frameCount;
    
    public State currentState;
    public enum State {
        spawn,
        move,
        eat,
        escape,
        noEnergy,
        dying,
        off
    }
    // Start is called before the first frame update
    void Start()
    {
        genderSelec = Random.Range(0,2);
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
        energy = Random.Range(60, 101);
        age = Random.Range(1, 31);
        rb = GetComponent<Rigidbody>();
        currentState = State.spawn;
        health = 1;
    }
    

    // Update is called once per frame
    void Update()
    {
        timeToLooseEnergy += Time.deltaTime;
        if(timeToLooseEnergy>=2) {
            energy -= 1;
            age += 1;
            timeToLooseEnergy = 0;
        }

        if (currentState == State.move) {
            Move();
        }
        if(currentState==State.spawn) {
            Spawn();
        }

        if (energy <= 20 && energy>1) {
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
        if(currentState==State.escape) {
            Escape();
        }
        if (currentState == State.noEnergy) {
            NoEnergy();
        }
        if(currentState==State.dying) {
            Dying();
        }
        if(currentState==State.off) {
            Off();
        }
        if (age >= 48) {
            currentState = State.dying;
        }
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
        if (catNear) {
            Escape();
        }
    }
    public void Eat() {
       

        for (int i = 0; i <perception.Count; i++) {
            if (perception[i].TryGetComponent<Plants>(out Plants plantTrans)) {

                //MoveTo(plantTrans.transform);
                transform.LookAt(plantTrans.gameObject.transform);
                mustEat = true;
                
                break;
            }
            
        }

        mustMove = true;
        currentState = State.move;


    }
    public void Escape() {
        speed = runSpeed;
        //energy -= 10;
        timeToEscape += Time.deltaTime;
        if (timeToEscape >= runTime) {
            speed = 200;
            timeToEscape = 0;
        }
        currentState = State.move;
    }
    public void NoEnergy() {
        time += Time.deltaTime;
        if(time>=energyTime && energy<=1) {
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
        Instantiate(babyMouse, this.transform);
    }

    public void CheckForTarget() {
        for (int i = 0; i < perception.Count; i++) {
            if (perception[i].TryGetComponent<Cats>(out Cats catObject)) {
                hardFlip = true;
                currentState = State.escape;
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
        if (mustEat == true && collision.gameObject.CompareTag("Plant")) {
            energy += 70;
            collision.gameObject.GetComponent<Plants>().health -= 1;


            mustEat = false;

        }
    }

    private void OnTriggerEnter(Collider other) {

        perception.Add(other.gameObject);

        if (other.gameObject.CompareTag("Cat")) {
            catNear = true;
        }

        if (other.gameObject.CompareTag("Mouse") && age>=15) {
            if (other.GetComponent<mouseBehavior>().gender != this.gender && other.GetComponent<mouseBehavior>().age>=15) {
                Love();
            }
        }
    }
}
