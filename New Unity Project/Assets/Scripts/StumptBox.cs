using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StumptBox : MonoBehaviour
{
    public int stomptDamage;
    public float bounceforce = 12f;
    public PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (GameManager.instance.canFight)
            {
                other.GetComponent<PlayerHealthController>().DamagePlayer(stomptDamage);
            
            }

            thePlayer.rbd.velocity = new Vector2(thePlayer.rbd.velocity.x, bounceforce);
        }
    }
}
