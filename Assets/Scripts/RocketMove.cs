using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMove : MonoBehaviour
{
    [SerializeField] float force=10f;
    [SerializeField] float Angle=15f;
    [SerializeField] AudioClip BoostClip;

    [SerializeField] ParticleSystem BoostParticles;
    [SerializeField] ParticleSystem TurnRightParticles;
    [SerializeField] ParticleSystem TurnLeftParticles;


    Rigidbody MyRididBody;
    AudioSource Sound;
    
    // Start is called before the first frame update
    void Start()
    {
        MyRididBody= GetComponent<Rigidbody>();
        Sound=GetComponent<AudioSource>();
        Sound.playOnAwake=false;
    }

    // Update is called once per frame
    void Update()
    {        
        ProcessInput();
    }

   
    void ProcessInput()
    {
        HandleRocket_BoostInput();
        HandleRocket_RotateInput();
    }

    void HandleRocket_RotateInput()
    {
        if (Input.GetKey(KeyCode.A))
        {    // rotate left
            MyRididBody.freezeRotation = true;

            if (!TurnLeftParticles.isPlaying)
                TurnLeftParticles.Play();

            transform.Rotate(Vector3.forward * Angle * Time.deltaTime);
            MyRididBody.freezeRotation = false;

        }
        else if (Input.GetKey(KeyCode.D))
        {    //rotate right
            MyRididBody.freezeRotation = true;

            if (!TurnRightParticles.isPlaying)
                TurnRightParticles.Play();

            transform.Rotate(Vector3.forward * -Angle * Time.deltaTime);
            MyRididBody.freezeRotation = false;
        }
        else
        {
            TurnLeftParticles.Stop();
            TurnRightParticles.Stop();
        }
    }

    void HandleRocket_BoostInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {    //up
            MyRididBody.AddRelativeForce(Vector3.up * force * Time.deltaTime);

            if (!Sound.isPlaying)
            {
                Sound.PlayOneShot(BoostClip);
            }
            if (!BoostParticles.isPlaying)
            {
                BoostParticles.Play();
            }
        }
        else
        {

            if (BoostParticles.isPlaying)
                BoostParticles.Stop();

            if (Sound.isPlaying)
                Sound.Stop();
        }
    }

 
}
