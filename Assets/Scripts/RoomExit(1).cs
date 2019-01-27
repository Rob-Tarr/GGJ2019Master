using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomExit : MonoBehaviour
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

    private void OnMouseOver()
    {
        // transition effect
    }

    private void DetectRoom()
    {

        if(roomName == ("Bedroom"))
        {
            SceneManager.LoadScene("02 Living Room");
        }

        if (roomName == ("Living"))
        {
            SceneManager.LoadScene("04 Kitchen");
        }

        if (roomName == ("Kitchen"))
        {
            SceneManager.LoadScene("06 Laundry Room");
        }

        if (roomName == ("Laundry"))
        {
            SceneManager.LoadScene("08 End");
        }
    }

}
