using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NaveController : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogFormat("Entrou o player");
        if(other.transform.tag == "Player"){
            SceneManager.LoadScene("End");
        }
    }
}
