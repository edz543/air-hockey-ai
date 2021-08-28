using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PaddleAgent : Agent
{
    public float moveSpeed = 1f;

    public GameObject opponent;
    public GameObject puck;

    CharacterController cc;
    Rigidbody puckRb;
    Vector3 startPos, puckStartPos;

    public override void Initialize()
    {
        cc = GetComponent<CharacterController>();
        puckRb = puck.GetComponent<Rigidbody>();
        startPos = transform.localPosition;
        puckStartPos = puck.transform.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = startPos;
        puck.transform.localPosition = puckStartPos;
        puckRb.velocity = Vector3.zero;
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }

    // 0: X movement
    // 1: Z movement
    public override void OnActionReceived(float[] vectorAction)
    {
        cc.Move(new Vector3(vectorAction[0] * moveSpeed, 0, vectorAction[1] * moveSpeed));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(opponent.transform.localPosition);
        sensor.AddObservation(puck.transform.localPosition);
        sensor.AddObservation(puckRb.velocity);
    }
}
