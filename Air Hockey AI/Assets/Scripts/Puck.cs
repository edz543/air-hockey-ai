using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    public PaddleAgent player, opponent;

    int touching = 0;
    float timer = 0, timerMax = 3;

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
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if(timer >= timerMax)
        {
            player.AddReward(-0.001f);
            opponent.AddReward(-0.001f);
        }
    }
}
