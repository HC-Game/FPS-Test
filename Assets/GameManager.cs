using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Rigidbody player;
    public Transform playerCam;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

  
}
