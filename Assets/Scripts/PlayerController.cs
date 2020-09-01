using UnityEngine;

public class PlayerController : MonoBehaviour{

    [Header("Movimations Settings")]
    public float speed = 15;
    public float jumpHigh = 9;
    
    [Header("Sounds Sources")]
    public AudioSource jumpSound;

    [Header("General")]
    public GameObject mainCamera;

    /*##privateSettings##*/
    //directionControllers
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Vector3 movement;
    //rotationController
    private bool lookingRight;
    private bool lookingLeft;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    
        lookingRight = true;
        lookingLeft = false;
    }

    void OnCollisionEnter(Collision col){
        if (col.gameObject.tag == ("Floor") && isGrounded == false){
            isGrounded = true;
        }
    }

    void FixedUpdate(){    
        //move lateral
        float moveHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rb.AddForce (movement * speed);

        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Jump= " + isGrounded);
        }

        //jumping
        //if (isGrounded){
            if (Input.GetKeyDown(KeyCode.Space)){
                rb.velocity += Vector2.up * jumpHigh;
                isGrounded = false;
                jumpSound.Play();
            }
        //}    

        //update camera position to follow player
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);

        //turn
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            if (!lookingLeft){
                transform.Rotate(new Vector3(0, -180, 0), Space.Self);
                lookingLeft = true;
                lookingRight = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            if (!lookingRight){
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                lookingRight = true;
                lookingLeft = false;
            }
        }
    }
}
