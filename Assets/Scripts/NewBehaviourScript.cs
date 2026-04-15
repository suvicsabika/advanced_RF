using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private enum ACTIONS
    { 
        LEFT = 0,
        FORWARD= 1,
        RIGHT = 2,
        BACKWARD = 3
    }

    //public override void OnEpisodeBegin()
    //{
    //    // Reset the agent and environment
    //    transform.localPosition = new Vector3(150, 7.0f, 150);

    //    TargetTransform.localPosition = new Vector3(Random.Range(100, 200), 7.0f, Random.Range(100, 200));
    //}
}
