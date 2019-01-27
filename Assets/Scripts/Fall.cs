using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.name == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
