using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;
    
    void Start(){
        clickSound.volume = 0f;
    }
   public void OpenGPWeb(){
        SceneManager.LoadScene("GP-Web");
        clickSound.Play();
        clickSound.volume = 1f;
    }
}
