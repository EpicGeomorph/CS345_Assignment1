using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    private Vector3 initialLocation;
    private Vector3 initialVelocity;
    private Vector3 initialAngular;
    private Vector3 originalRotation;
    private Rigidbody rigidbody;
    public float jumpForce = 5.0f;
    private bool isTouchingTable = true;

    // Audio
    private AudioSource myAudio;
    public AudioClip ballHitClip, ballRollClip;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialLocation = rigidbody.transform.position;
        initialVelocity = rigidbody.linearVelocity;
        initialAngular = rigidbody.angularVelocity;
        originalRotation = transform.rotation.eulerAngles;

        myAudio = gameObject.AddComponent<AudioSource>();

        //ballHitClip = Resources.Load<AudioClip>("Assets/Audios/ballHit.wav");
        //ballRollClip = Resources.Load<AudioClip>("Assets/Audios/ballRoll.wav");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            // Stop all velocities and move to original position
            rigidbody.transform.position = initialLocation;
            rigidbody.linearVelocity = initialVelocity;
            rigidbody.angularVelocity = initialAngular;

            // Rotate the ball back to how it was originally
            transform.rotation = Quaternion.Euler(originalRotation);
        }

        // Handle Jumping!
        if (Input.GetKey(KeyCode.Space) && isTouchingTable /*Physics.Raycast(transform.position,Vector3.down,0.51f)*/)
        {
            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, jumpForce, rigidbody.linearVelocity.z);
        }

        // Update Sound Settings
        myAudio.volume = rigidbody.linearVelocity.magnitude / 5.0f;

        if (isTouchingTable && rigidbody.linearVelocity != Vector3.zero && !myAudio.isPlaying)
        {
            myAudio.PlayOneShot(ballRollClip);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // ball hitting table first time
        if (collision.gameObject.tag == "Table" && !isTouchingTable)
        { 
            myAudio.PlayOneShot(ballHitClip);
            isTouchingTable = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        isTouchingTable = false;
    }
}
