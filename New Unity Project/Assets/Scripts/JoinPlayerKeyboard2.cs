using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinPlayerKeyboard2 : MonoBehaviour
{
    public GameObject playerToLoad;
    private bool hasloadedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasloadedPlayer && GameManager.instance.activePlayers.Count < GameManager.instance.maxPlayer)
        {
            if(Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame || Keyboard.current.rightShiftKey.wasPressedThisFrame || Keyboard.current.iKey.wasReleasedThisFrame)
            {
                Instantiate(playerToLoad, transform.position, transform.rotation);
                hasloadedPlayer = true;
            }
        }
    }
}
