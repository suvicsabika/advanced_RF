using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentScript : Agent
{
    private enum ACTIONS
    { 
        LEFT = 0,
        FORWARD= 1,
        RIGHT = 2,
        BACKWARD = 3
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 0.0f, 0);

        TargetTransform.localPosition = new Vector3(Random.Range(-85, 85), 0.0f, Random.Range(-85, 85));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.z);

        sensor.AddObservation(TargetTransform.localPosition.x);
        sensor.AddObservation(TargetTransform.localPosition.z);

    }

    public override void OnActionReceived(float[] vectorAction)
    {
        var actionTaken = actions.DiscreteActions[0];

        switch (actionTaken)
        {
            case (int)ACTIONS.FORWARD:
                transform.rotation = Quaternion.Euler(0, 0, 0);
            case (int)ACTIONS.LEFT:
                transform.rotation = Quaternion.Euler(0, -90, 0);
            case(int)ACTIONS.RIGHT:
                transform.rotation = Quaternion.Euler(0, 90, 0);
            case (int)ACTIONS.BACKWARD:
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

        AddReward(-0.001f);
    }

    public override void Heuristic(float[] actionsOut)
    {

    }
}
