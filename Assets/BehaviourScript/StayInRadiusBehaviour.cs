using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay in Radius")]

public class StayInRadiusBehaviour : FlockBehaviour
{

    public Vector2 centre;
    public float radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 centreOffset = centreOffset = centre - (Vector2)agent.transform.position;

        // if t = 0 then we are the centre, if t = 1 then we are right at the radius, if t > 1 then we are 
        // outside of the radius,
        float t = centreOffset.magnitude / radius;

        if (t < 0.9)
        {
            return Vector2.zero;
        }


        return centreOffset * t * t;
    }
}
