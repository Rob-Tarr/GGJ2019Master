using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    [SerializeField] string roomName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        

        DetectRoom();


    }

    private void DetectRoom()
    {
        if(roomName == ("Living"))
        {
            SceneManager.LoadScene("03 Level 1");
        }

        if(roomName == ("Kitchen"))
        {
            SceneManager.LoadScene("05 Level 2");
        }

        if(roomName == ("Laundry"))
        {
            SceneManager.LoadScene("07 Level 3");
        }
    }

    private void OnMouseOver()
    {
        // transition effect
    }
}
