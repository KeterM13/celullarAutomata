using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    //public CharacterController characterController;
    public Rigidbody rb;
    public Animator anim;
    public float speed=5f;
    public float jumpSpeed=20f;
    public int randomChar;
    public static int numChar;

    [Header ("Characters options")]
    public bool isCat;
    public bool isFire;
    public bool isWater;
    public GameObject characterFire;
    public GameObject characterWater;
    public GameObject characterCat;
    public GameObject attackFire;
    public GameObject attackWater;

    // Start is called before the first frame update
    void Start()
    {
        
        //characterController.GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        switch (numChar) {
            case 0:
                characterCat.SetActive(true);
                isCat = true;
                break;
            case 1:
                characterFire.SetActive(true);
                characterCat.SetActive(false);
                isCat = false;
                isFire = true;
                break;
            case 2:
                characterWater.SetActive(true);
                characterCat.SetActive(false);
                isCat = false;
                isWater = true;
                break;
            default:
                characterCat.SetActive(true);
                isCat = true;
                break;
        }
        anim.SetBool("IsWalking", false);
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
            anim.SetBool("IsWalking", true);
        }
        if (Input.GetButton("Jump")) {
            rb.AddForce(Vector3.up * jumpSpeed);
        }
        if(Input.GetMouseButtonDown(0) && isWater == true) {
            attackWater.SetActive(true);
        }
        if(Input.GetMouseButtonUp(0) && isWater==true) {
            attackWater.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0) && isFire == true) {
            attackFire.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0) && isFire == true) {
            attackFire.SetActive(false);
        }
        rb.MovePosition(transform.position + (move * Time.deltaTime * speed));
    }
}
