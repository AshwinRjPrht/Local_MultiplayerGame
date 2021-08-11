using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectUi : MonoBehaviour
{
    public GameObject JoinText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.activePlayers.Count >= GameManager.instance.maxPlayer)
        {
            JoinText.SetActive(false);
        }
        else
        {
            JoinText.SetActive(true);
        }
    }
}
