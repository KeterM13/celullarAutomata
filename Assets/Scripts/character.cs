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
    // Start is called before the first frame update
    void Start()
    {
        //characterController.GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        anim.SetBool("IsWalking", false);
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
            anim.SetBool("IsWalking", true);
        }
        if (Input.GetButton("Jump")) {
            rb.AddForce(Vector3.up * jumpSpeed);
        }
        rb.MovePosition(transform.position + (move * Time.deltaTime * speed));
    }
}
