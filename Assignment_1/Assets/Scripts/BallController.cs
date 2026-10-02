using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class BallController : MonoBehaviour
{
    private Vector3 initialLocation;
    private Vector3 initialVelocity;
    private Vector3 initialAngular;
    private Vector3 originalRotation;
    private Rigidbody rigidbody;
    public float jumpForce = 5.0f;
    private bool isTouchingTable = true;
    private GameObject table;
    public GameObject moneyParticleSystem;
    public bool teleport_cd = false;
    public int teleport_wait = 2;
    public float counter = 0;

    // Audio
    private AudioSource myAudio, effectsAudio;
    public AudioClip ballHitClip, ballRollClip, coinClip;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialLocation = rigidbody.transform.position;
        initialVelocity = rigidbody.linearVelocity;
        initialAngular = rigidbody.angularVelocity;
        originalRotation = transform.rotation.eulerAngles;

        myAudio = gameObject.AddComponent<AudioSource>();
        effectsAudio = gameObject.AddComponent<AudioSource>();

        //ballHitClip = Resources.Load<AudioClip>("Assets/Audios/ballHit.wav");
        //ballRollClip = Resources.Load<AudioClip>("Assets/Audios/ballRoll.wav");

        table = GameObject.Find("Tables");
        if (table == null)
        {
            Debug.Log("Couldn't find table game object");
        }
    }

    void Update()
    {
        if (teleport_cd) {
            if (counter > teleport_wait) {
                teleport_cd = false;
                counter = 0;
            } else {
                counter += Time.deltaTime;
            }
        }
        

        if (Input.GetKey(KeyCode.R))
        {
            // Stop all velocities and move to original position
            rigidbody.transform.position = initialLocation;
            rigidbody.linearVelocity = initialVelocity;
            rigidbody.angularVelocity = initialAngular;

            // Rotate the ball back to how it was originally
            transform.rotation = Quaternion.Euler(originalRotation);

            // Reset Table
            if (table != null)
            {
                table.transform.rotation = Quaternion.Euler(new Vector3(0, table.transform.rotation.eulerAngles.y, 0));
            }
        }

        // Handle Jumping!
        //if (Input.GetKey(KeyCode.Space) && isTouchingTable /*Physics.Raycast(transform.position,Vector3.down,0.51f)*/)
        //{
        //    rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, jumpForce, rigidbody.linearVelocity.z);
        //}

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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            effectsAudio.pitch = 1;
            effectsAudio.PlayOneShot(coinClip);
            Destroy(other.gameObject);

            ScoreDisplay.scoreValue++;
            Instantiate(moneyParticleSystem, other.gameObject.transform.position, Quaternion.identity, other.gameObject.transform.parent.transform);
        } else if (other.gameObject.tag == "Teleporter") {
            if (teleport_cd) { return; }
            teleport_cd = true;
            List<GameObject> portals = new List<GameObject>(GameObject.FindGameObjectsWithTag("Teleporter"));
            portals.Remove(this.gameObject);

            GameObject random_portal = portals[Random.Range(0,portals.Count)];
            rigidbody.transform.position = random_portal.transform.position;
        }
    }
}
