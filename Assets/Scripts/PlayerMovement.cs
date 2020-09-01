using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("General")]
    public GameObject mainCamera;
	public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    public GameManager gameManager;

    float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
    float zoomIn = 140;
    float zoomInStart = 140;

    private void Start(){
        zoomInStart = zoomIn;
        Camera.main.fieldOfView = zoomIn;
        Camera.main.transform.position = new Vector3(-4, 1, Camera.main.transform.position.z);
    }
    
    void Update () {
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
		if (Input.GetButtonDown("Jump")){
			jump = true;
        }

		if (Input.GetButtonDown("Crouch")){
			crouch = true;
            animator.SetBool("Crouch", true);
        } else if (Input.GetButtonUp("Crouch")){
			crouch = false;
            animator.SetBool("Crouch", false);
        }

        if (Input.GetMouseButtonDown(0)){
            PlayerAttack(true, 0);
            StartCoroutine(WaitCoroutineAttack(.4f, 0));            
        }

        if (Input.GetMouseButtonDown(1)){
            PlayerAttack(true, 1);
            StartCoroutine(WaitCoroutineAttack(.4f, 1));
        }
    }

	void FixedUpdate (){
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

        Debug.Log(controller.getGrounded());

        if (controller.getGrounded()){            
            animator.SetBool("Jump", false);
        }else{
            animator.SetBool("Jump", true);
        }
        
        mainCamera.transform.position = new Vector3(transform.position.x, 3, mainCamera.transform.position.z);
        
	}

    IEnumerator WaitCoroutine(float amountOfTime){
        yield return new WaitForSeconds(amountOfTime);
    }

    IEnumerator WaitCoroutineAttack(float amountOfTime, int whichAtk){
        yield return new WaitForSeconds(amountOfTime);
        PlayerAttack(false, whichAtk);
    }

    private void OnCollisionEnter2D(Collision2D other){
        //Game Over
        if (other.gameObject.tag == "Death" || other.gameObject.tag == "Enemy"){
            Debug.Log(other.gameObject.name);
            //Time.timeScale = 0f;
            animator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void PlayerAttack(bool attack, int whichAtk){
        switch (whichAtk){
            case 0:
                animator.SetBool("Attack", attack);
                break;
            case 1:
                animator.SetBool("Throw", attack);
                break;
            default:
                animator.SetBool("Attack", attack);
                break;
        }

        
        gameManager.PlayerAttacking(attack);
    }
}
