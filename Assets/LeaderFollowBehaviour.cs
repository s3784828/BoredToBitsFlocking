using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Follow Leader")]

public class LeaderFollowBehaviour : MonoBehaviour
{
    public Rigidbody2D leaderRB;
    public Transform leaderTransform;
    public GameObject debug;
    public float seperationRadius;
    public float maxSeperation;
    
    public float leaderBehindDistance;
    public float weight;

    public Vector2 FollowLeader()
    {
        // We want the flock to get a postion behind the velocity
        Vector2 targetVelocity = leaderRB.velocity * -1;

        targetVelocity = targetVelocity.normalized * leaderBehindDistance;

        return (Vector2)leaderTransform.position + targetVelocity;
    }

    public Vector2 Position()
    {
        return (Vector2)leaderTransform.position;
    }

    public Vector2 LeaderSteeredPosition(FlockAgent agent, float maxSpeed)
    {
        Vector2 desiredVelocity = (Vector2)leaderTransform.position - (Vector2)agent.transform.position;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;
        Vector2 steer = desiredVelocity - agent.Rigidbody2D.velocity;
        
        return weight * steer;
    }

    public Vector2 LeaderAvoidBehaviour(FlockAgent agent, List<Transform> context)
    {
        Vector2 force = Vector2.zero;
        int neighbourCount = 0;

        foreach (Transform transform in context)
        {
            if (Distance((Vector2)agent.transform.position, (Vector2)transform.position) <= seperationRadius )
            {
                force.x += agent.transform.position.x - transform.position.x;
                force.y += agent.transform.position.y - transform.position.y;
                neighbourCount += 1;
            }
        }

        if (neighbourCount != 0)
        {
            force.x /= neighbourCount;
            force.y /= neighbourCount;
            force *= -1;
        }

        force = force.normalized;
        force *= maxSeperation;

        return force;
    }

    public float Distance(Vector2 pos1, Vector2 pos2)
    {
        return Mathf.Sqrt((pos1.x - pos1.y) * (pos1.x - pos1.y) + (pos2.x - pos2.y) * (pos2.x - pos2.y));
    }
}
