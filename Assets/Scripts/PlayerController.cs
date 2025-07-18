using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Assets (Audios)
    public AudioSource audioWalking;
    public AudioSource audioWalkingInWater;
    public AudioSource audioWalkingOnBridge;

    //Position and isMoving
    private Vector3 position;
    private float posTreshold = 0.01f;
    private Boolean isMoving = false;
    private Boolean isWater = false;
    private Boolean isBridge = false;

    private float moveInterval = 0.2f;
    private float timer;




    void Start()
    {
        position = transform.position;
        timer = moveInterval;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = transform.position;
        timer = timer - Time.deltaTime;

        //check if user is still running
        if (timer <= 0)
        {
            handleWalkSound(newPos);
            timer = moveInterval; //reset timer to start again
        }

    }



    private void handleWalkSound(Vector3 newPos)
    {
        //if currentPos is different than oldPos, the user walks and a walking sound is played
        if (
            isMoving == false &&
            (
                Math.Abs(newPos.x - position.x) > posTreshold ||
                Math.Abs(newPos.y - position.y) > posTreshold ||
                Math.Abs(newPos.z - position.z) > posTreshold
            )
        )
        {
            isMoving = true;

            if (isWater)
            {
                audioWalkingInWater.Play();
            }
            else if (isBridge)
            {
                audioWalkingOnBridge.Play();
            }
            else 
            {
                audioWalking.Play();
            }
        }

        //user does not move and therefore the 
        else if (
            isMoving == true &&
            Math.Abs(newPos.x - position.x) < posTreshold &&
            Math.Abs(newPos.y - position.y) < posTreshold &&
            Math.Abs(newPos.z - position.z) < posTreshold
        )
        {
            isMoving = false;
            
            if (isWater)
            {
                audioWalkingInWater.Stop();
            }
            else if (isBridge)
            {
                audioWalkingOnBridge.Stop();
            }
            else
            {
                audioWalking.Stop();
            }
        }

        position = newPos;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            //toggle
            isWater = true;
            isBridge = false;

            //switch audio
            audioWalking.Stop();
            audioWalkingInWater.Play();
        }

        if (other.CompareTag("Bridge"))
        {
            //toggle
            isWater = false;
            isBridge = true;

            //switch audio
            audioWalking.Stop();
            audioWalkingOnBridge.Play();
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            //toggle
            isWater = false;

            //switch audio
            audioWalkingInWater.Stop();
            audioWalking.Play();
        }

        if (other.CompareTag("Bridge"))
        {
            //toggle
            isBridge = false;

            //switch audio
            audioWalkingOnBridge.Stop();
            audioWalking.Play();
        }
    }

}