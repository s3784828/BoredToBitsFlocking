using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class FlockAgent : MonoBehaviour
{


    Flock agentFlock;

    Rigidbody2D rb;

    public Rigidbody2D Rigidbody2D { get { return rb; } }

    public Flock AgentFlock { get { return agentFlock; } }

    
    private Collider2D agentCollider;

    public Collider2D AgentCollider {  get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {

        transform.up = velocity;
        rb.velocity = velocity;

    }
}
