using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip finishClip;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource my_audio;

    bool istransitioning=false;
    bool disapleCollision=false;

    void Start(){
        my_audio=GetComponent<AudioSource>();
    }

    void Update() {
        RespondDebugKeys();
    }

    void RespondDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){    //load next lv scene
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C)){    // disable collision 
            Debug.Log("Disaple collision state = "+disapleCollision);
            disapleCollision=!disapleCollision;
        }
    }

    void OnCollisionEnter(Collision other) {
        
        if(istransitioning || disapleCollision)
            return; 
        
        switch(other.gameObject.tag){
            case "Finish":
                istransitioning=true;
                my_audio.Stop();
                finishParticles.Play();
                my_audio.PlayOneShot(finishClip);
                DisablePlayerMovement();
                Invoke("LoadNextLevel",2);

                break;

            case "Friendly":
                Debug.Log("Friendly");
                break;

            case "Fuel":
                Debug.Log("Rocket refueled");
                break;

            default:        //crash
                istransitioning=true;
                my_audio.Stop();
                crashParticles.Play();
                my_audio.PlayOneShot(crashClip);
                DisablePlayerMovement();
                Invoke("LoadCurrentLevel",2);
                break;
        }
    }


    void DisablePlayerMovement(){
        GetComponent<RocketMove>().enabled=false;
    }
    
    void EnablePlayerMovement(){
        GetComponent<RocketMove>().enabled=true;
    }

    void LoadNextLevel(){
        int nextLevelIndex=SceneManager.GetActiveScene().buildIndex+1;
        if(nextLevelIndex==SceneManager.sceneCountInBuildSettings){
            nextLevelIndex=0;
        }
        SceneManager.LoadScene(nextLevelIndex);  //reload scene
    }

    void LoadCurrentLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //reload scene
    }
}
