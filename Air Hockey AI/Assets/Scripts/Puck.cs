using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    public PaddleAgent player, opponent;

    int touching = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            touching++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            touching--;
        }
    }

    private void Update()
    {
        if(touching == 2)
        {
            player.AddReward(-0.01f);
            opponent.AddReward(-0.01f);
        }
    }
}
