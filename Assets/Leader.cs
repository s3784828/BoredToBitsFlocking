using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxSpeed;
    public float maxForce;
    public float slowingDistance;
    public Transform targetLocation;
    public LineRenderer lrSteer;
    public LineRenderer lrDV;
    public LineRenderer lrV;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector2.zero;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        //Seek();
        FaceVelocity();
    }

    Vector2 DetermineSteeringVelocity(Vector2 desiredVelocity, Vector2 currentVelocity)
    {
        return Vector2.ClampMagnitude(desiredVelocity - currentVelocity, maxForce);
    }

    Vector2 DetermineDesiredVelocity(Vector2 targetLocation, Vector2 currentLocation)
    {
        Vector2 desiredVelocity = targetLocation - currentLocation;
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;
        return desiredVelocity;
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(x * maxSpeed, y * maxSpeed) * Time.fixedDeltaTime;
    }

    void Seek()
    {
        Vector2 desiredVelocity = (Vector2)targetLocation.position - (Vector2)transform.position;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;
        Vector2 steer = desiredVelocity - rb.velocity;

        float magnitude = Mathf.Sqrt(steer.x * steer.x + steer.y * steer.y);

        if (magnitude > maxForce)
            steer = new Vector2(steer.x * maxForce / magnitude,
                steer.y * maxForce / magnitude);

        VisualiseSteeing(steer, desiredVelocity);

        rb.AddForce(steer * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    void FaceVelocity()
    {
        transform.up = rb.velocity.normalized;
    }

    void VisualiseSteeing(Vector2 steer, Vector2 desiredVelocity)
    {
        lrSteer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        lrSteer.SetPosition(1, new Vector3(steer.x, steer.y, 0));

        //lrDV.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        //lrDV.SetPosition(1, new Vector3(desiredVelocity.x, desiredVelocity.y, 0));

    }
}
