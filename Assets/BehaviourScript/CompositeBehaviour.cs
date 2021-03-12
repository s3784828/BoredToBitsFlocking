using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{

    public FlockBehaviour[] behaviours;
    public float[] weights;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Handle data mismatch
        if (weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        //set up move
        Vector2 move = Vector2.zero;

        //Iterate through behaviours
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weights[i];

            //if there is some movement being returned.
            if (partialMove != Vector2.zero)
            {
                //Does this overall movement exceed the weight.
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    //if it does, we set precisely back to maximum value of that weight.
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }
}
