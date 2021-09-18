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
        transform.localPosition = startPos;
        puck.transform.localPosition = puckStartPos;
        puckRb.velocity = Vector3.zero;
    }

    public override void Heuristic(float[] actionsOut)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Vector3 dir = hit.point - transform.position;
            actionsOut[0] = dir.x;
            actionsOut[1] = dir.z;
        }
    }

    // 0: X movement
    // 1: Z movement
    public override void OnActionReceived(float[] vectorAction)
    {
        rb.velocity = new Vector3(vectorAction[0] * moveSpeed, 0, vectorAction[1] * moveSpeed);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(opponent.transform.localPosition);
        sensor.AddObservation(puck.transform.localPosition);
        sensor.AddObservation(puckRb.velocity);
    }
}
