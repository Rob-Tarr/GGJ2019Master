using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Boundary : MonoBehaviour
{
    void OnTriggerEnter(Collider Other)
    {
        Debug.Log("TOUHCED");
        if (Other.name == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
