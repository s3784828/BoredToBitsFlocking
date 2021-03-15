using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class FlockAgent : MonoBehaviour
{


    Flock agentFlock;

    public Flock AgentFlock { get { return agentFlock; } }

    
    private Collider2D agentCollider;
    private Vector2 steering;
    

    public Collider2D AgentCollider {  get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity, float speed, float force, float arrivalRadius)
    {
        ////Making forward direction i.e up of agent to turn towards
        ////the velocity.
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
        //transform.up = rb.velocity;
        //Vector2 desiredVelocity = target - (Vector2)transform.position;
        //desiredVelocity = desiredVelocity.normalized;
        //desiredVelocity *= speed;

        //Vector2 steer = desiredVelocity - rb.velocity;

        //rb.velocity = (steer * Time.fixedDeltaTime);

        //float distance = Mathf.Sqrt(desiredVelocity.x * desiredVelocity.x + desiredVelocity.y * desiredVelocity.y);

        //if (distance < arrivalRadius)
        //{
        //    desiredVelocity = desiredVelocity.normalized * speed * (distance / arrivalRadius);
        //}
        //else
        //{
        //    desiredVelocity = desiredVelocity.normalized * speed;
        //}
        //steering = desiredVelocity - rb.velocity;

        //Vector2 desiredVelocity = target - (Vector2)transform.position;
        //desiredVelocity = desiredVelocity.normalized * speed;
        //Vector2 steer = desiredVelocity - rb.velocity;

        //float magnitude = Mathf.Sqrt(steer.x * steer.x + steer.y * steer.y);

        //if (magnitude > force)
        //    steer = new Vector2(steer.x * force / magnitude,
        //        steer.y * force / magnitude);

    }
}
