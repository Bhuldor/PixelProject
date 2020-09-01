using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("General")]
    public float speed;
    public float startWaitTime;
    public GameObject leftSpot;
    public GameObject rightSpot;
    public Animator animator;
    public Transform moveSpot;

    private float waitTime;
    private int randomSpot;
    private float y;
    private bool toLeft = false;
    private bool whichMov = false;
    private bool enemyDeath = false;

    void Start(){
        if (Random.Range(0,2) == 0){
            whichMov = true;
        }

        waitTime = startWaitTime;

        if (whichMov){
            moveSpot.position = new Vector3(Random.Range(leftSpot.transform.position.x, rightSpot.transform.position.x), moveSpot.position.y, transform.position.z);
        }else{
            if (Random.Range(0, 2) == 0){
                moveSpot.position = new Vector3(rightSpot.transform.position.x, moveSpot.position.y, transform.position.z);
            }else{
                moveSpot.position = new Vector3(leftSpot.transform.position.x, moveSpot.position.y, transform.position.z);
            }
        }
    }

    void Update(){
        if (!enemyDeath) {
            if (transform.position.x > moveSpot.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
                toLeft = true;
            }
            else if (transform.position.x < moveSpot.position.x)
            {
                transform.localScale = new Vector2(1, 1);
                toLeft = false;
            }

            if (Camera.main.fieldOfView == 78f)
            {
                transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(moveSpot.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                //{ 
                if (waitTime <= 0)
                {
                    animator.SetFloat("Speed", Mathf.Abs(1));
                    if (whichMov)
                    {
                        moveSpot.position = new Vector3(Random.Range(leftSpot.transform.position.x, rightSpot.transform.position.x), moveSpot.position.y, transform.position.z);
                    }
                    else
                    {
                        if (toLeft)
                            moveSpot.position = new Vector3(rightSpot.transform.position.x, moveSpot.position.y, transform.position.z);
                        else
                            moveSpot.position = new Vector3(leftSpot.transform.position.x, moveSpot.position.y, transform.position.z);
                    }
                    waitTime = startWaitTime;
                }
                else
                {
                    if (moveSpot.position.x == transform.position.x)
                    {
                        animator.SetFloat("Speed", Mathf.Abs(0));
                    }
                    waitTime -= Time.deltaTime;
                }
                //}
            }
        }
    }

    public void EnemyDeath(){
        enemyDeath = true;
    }
}
