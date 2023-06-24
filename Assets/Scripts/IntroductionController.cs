using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionController : MonoBehaviour
{
    public GameObject firstScene;
    public GameObject secondScene;
    public GameObject menu;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        firstScene.SetActive(true);
        secondScene.SetActive(false);
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(time>=2f && time<=3f){
            firstScene.SetActive(false);
            secondScene.SetActive(true);
        }
        if(time>5f){
            firstScene.SetActive(false);
            secondScene.SetActive(false);
            menu.SetActive(true);
        }
        time += Time.deltaTime;
    }

    
    public void StartGame(){
        SceneManager.LoadScene("JorgeLands");
    }
}

