using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PaddleAgent : Agent
{
    public bool training = false;
    public float moveSpeed = 1f;
    public bool player;

    public GameObject opponent, puck;
    public GameObject goal, opponentGoal;
    public GameManager gameManager;

    bool endEpisode = false;

    Rigidbody rb, puckRb;
    Vector3 startPos, puckStartPos;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        puckRb = puck.GetComponent<Rigidbody>();
        startPos = transform.localPosition;
        puckStartPos = puck.transform.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        endEpisode = false;
        transform.localPosition = startPos;
        puck.transform.localPosition = puckStartPos;
        puckRb.velocity = Vector3.zero;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 dir = transform.InverseTransformPoint(hit.point).normalized;
                continuousActionsOut[0] = dir.x;
                continuousActionsOut[1] = dir.z;
            }
        }
        else
        {
            continuousActionsOut[0] = 0;
            continuousActionsOut[1] = 0;
        }
    }

    // 0: X movement
    // 1: Z movement
    public override void OnActionReceived(ActionBuffers actions)
    {
        var continuousActions = actions.ContinuousActions;
        Vector3 xVel = transform.right * moveSpeed * continuousActions[0];
        Vector3 zVel = transform.forward * moveSpeed * continuousActions[1];
        rb.velocity = xVel + zVel;

        if (endEpisode) EndEpisode();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //sensor.AddObservation(transform.localPosition);
        //sensor.AddObservation(opponent.transform.localPosition);
        //sensor.AddObservation(puck.transform.localPosition);
        //sensor.AddObservation(puckRb.velocity);
    }

    public void Win()
    {
        AddReward(1f);
        endEpisode = true;

        if (!training)
        {
            gameManager.IncreaseScore(player);
        }
    }

    public void Lose()
    {
        AddReward(-1f);
        endEpisode = true;
    }
}