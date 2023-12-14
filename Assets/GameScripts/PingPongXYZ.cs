using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongXYZ : MonoBehaviour
{
    // SerializedField allows user Unity editor to set value of property
    [SerializeField]
    private float speed = 0;

    // Time for platform to wait at the ends to allow player to get on / off
    [SerializeField]
    private float pauseTimeAtEnd = 0;

    // Vector3.zero sets xyz to 0
    [SerializeField]
    private Vector3 distance = Vector3.zero;

    // Allow platform to be auto on or triggered on when player lands on it
    [SerializeField]
    private bool playOnStart = false;

    // Allows platform to be a one-way trip or go back and forth
    [SerializeField]
    private bool loop = false;



    // How far to travel, using a Vector3 allows multiple directions of movement
    // To make script useable on multiple assets.
    private Vector3 origPos;
    private Vector3 targetPos;
    private float curDist;
    private float moveSpeed;
    private bool posDir;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        // Determine start position
        origPos = transform.localPosition;

        // Determine target position
        targetPos = origPos + distance;

        curDist = 0;

        // moveSpeed is the percentage to move the platform
        // each second. This script will use Lerp() which is percentage based
        moveSpeed = speed / Vector3.Distance(origPos, targetPos);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playOnStart) return;

        
        if (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            return;
        }
        MoveObject();
        //Vector3 pos = transform.position;
        //pos.x += speed * Time.deltaTime;
        //transform.position = pos;
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<RollerBallMover>())
        {
            if (collision.gameObject.GetComponent<Rigidbody>())
            {
                Vector3 localRotation = transform.up.normalized;
                
            }
        }


    }

    private void MoveObject()
    {
        // Calculate distance to move based on how fast each frame calculates
        // This causes the platform to move at a consistent speed regardless of 
        // The FPS, so slower/faster computers run the same
        if (posDir)
        {
            curDist += moveSpeed * Time.deltaTime;
        }
        else
        {
            curDist -= moveSpeed * Time.deltaTime;
        }
        

        // Lerp() uses a percentage to determine position between two points
        // 0% is at the start point (origPos) and 100% is at tthe end point (targetPos)
        transform.localPosition = Vector3.Lerp(origPos, targetPos, curDist);

        if (posDir)
        {
            if (loop)
            {

            }
            if(curDist >= 1.0f)
            {
                // Loop back
                if (loop)
                {
                    posDir = false;
                    timer = pauseTimeAtEnd;
                }
                else
                {
                    // One way trip
                    playOnStart = false;

                }
                
            }
        }
        else
        {
            if (curDist <= 0.0f)
            {
                if (loop)
                {
                    posDir = true;
                    timer = pauseTimeAtEnd;

                }
                else
                {
                    //One way trip
                    playOnStart = false;
                }
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Player landed on platform, start movement
        if (!playOnStart)
        {
            playOnStart = true;
        }
    }

}
