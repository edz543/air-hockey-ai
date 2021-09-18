using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    public PaddleAgent player, opponent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Puck") return;

        player.Lose();
        opponent.Win();
    }
}
