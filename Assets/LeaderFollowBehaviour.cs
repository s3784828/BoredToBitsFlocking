using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Follow Leader")]

public class LeaderFollowBehaviour : MonoBehaviour
{
    public Rigidbody2D leaderRB;
    public Transform leaderTransform;
    public GameObject debug;
    
    public float leaderBehindDistance;

    public Vector2 FollowLeader()
    {
        // We want the flock to get a postion behind the velocity
        Vector2 targetVelocity = leaderRB.velocity * -1;

        targetVelocity = targetVelocity.normalized * leaderBehindDistance;

        debug.GetComponent<Transform>().position = (Vector2) leaderTransform.position + targetVelocity;

        //Debug.Log("2. target velocity = " + targetVelocity);

        //Debug.Log("3. final vector " + (Vector2)leaderTransform.position + targetVelocity);

        return (Vector2)leaderTransform.position + targetVelocity;
    }
}
