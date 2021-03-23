using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfollowing : MonoBehaviour
{
    public LineRenderer pathLR;
    public LineRenderer pathLR0;
    public LineRenderer pathLR1;
    public LineRenderer debugLR;
    public LineRenderer debugLR01;
    public List<Transform> positionList;
    private Path[] path;
    public float predictionDistance;
    public float directionDistance;
    public float startingBestDistance;
    public float radius;
    private int ignorePath;
    public Rigidbody2D rb;
    public SteeringBehaviour sb;


    class Path
    {
        public Vector2 start;
        public Vector2 end;

        public Path(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DrawPath();
    }

    

    // Update is called once per frame
    void Update()
    {
        DrawPath();
        FollowPath();
        //sb.Avoid();
        /*
         * pointA = is the vector from the start of the path, to the predicted location.
         * pointB = is the vector from the start of the path, to the end of the path.
         */
        //Vector2 predictedLoc = PredictedLocation(predictionDistance);
        //Vector2 pointA = predictedLoc - path[0].start;
        //Vector2 pointB = path[0].end - path[0].start;

        //pointB = pointB.normalized;
        //pointB = pointB * VectorUtility.DotProduct(pointA, pointB);

        /*
         * normal point = is the normal in the path that point towards pointB.
         */
        //Vector2 normalPoint = path[0].start + pointB;

        
        

        //float distance = VectorUtility.distance(predictedLoc, normalPoint);

        ///*
        // * if the distance is from the predicted location, to the normal point
        // * is greater than the radius of the path, then it must return to the
        // * path.
        // */
        //if (distance > radius)
        //{
        //    pointB = pointB.normalized;
        //    pointB *= predictionDistance;
        //    Vector2 target = normalPoint + pointB;
        //    debugLR.SetColors(Color.red, Color.red);
        //    sb.Seek(target);
        //}
        //else
        //    debugLR.SetColors(Color.green, Color.green);

    }

    void DrawPath()
    {     
      

        pathLR.SetColors(Color.green, Color.green);
        pathLR.loop = true;
        
        int numPositions = positionList.Count;
        path = new Path[numPositions];
        
        pathLR.positionCount = numPositions;
        pathLR0.positionCount = numPositions;
        pathLR1.positionCount = numPositions;
        for (int i = 0; i < numPositions; i++)
        {
            //Debug.Log(i + " numPositions = " + numPositions);
            
            //Debug.Log(pathPositions[position].position);
            //pathLR.SetPosition(position, transform.position);
            //pathLR0.SetPosition(i, pathPositions[i].position - Vector3.one * radius);
            //pathLR1.SetPosition(i, pathPositions[i].position + Vector3.one * radius);
            //path[i] = new Path((Vector2)pathPositions[i].position, (Vector2)pathPositions[i + 1].position);
            if (i < 3)
            {
                //Debug.Log(i);
                path[i] = new Path((Vector2)positionList[i].position, (Vector2)positionList[i + 1].position);
            }
            //if (position < childCount - 2)
            //{

            //    path[position] = new Path((Vector2)transform.position, (Vector2)pathPositions[position + 1].position);
            //}

        }

        path[3] = new Path((Vector2)path[2].end, (Vector2)path[0].start);

        //pathLR.SetPosition(numPositions, pathPositions[0].position);
        //pathLR0.SetPosition(numPositions, pathPositions[0].position - Vector3.one * radius);
        //pathLR1.SetPosition(numPositions, pathPositions[0].position + Vector3.one * radius);

        for (int i = 0; i < path.Length; i++)
        {
            pathLR.SetPosition(i, path[i].end);
        }
    }

    void FollowPath()
    {
        /*
         * predictedLocation = is the predicted location the gameobject will reach with
         * its current velocity.
         * pointA = is the vector from the start of the path, to the predicted location.
         * pointB = is the vector from the start of the path, to the end of the path.
         */
        Vector2 predictedLoc = GetPredictedLocation(predictionDistance);
        //Vector2 pointA = path[0].start;
        //Vector2 pointB = path[0].end;

        Vector2 pointA = Vector2.zero;
        Vector2 pointB = Vector2.zero;
        Vector2 normalPoint = Vector2.zero;

        float bestDistance = startingBestDistance;
        Vector2 targetNormalPoint = Vector2.zero;
        for (int i = 0; i < path.Length; i++)
        {
            pointA = path[i].start;
            pointB = path[i].end;
            /*
            * normalPoint = the point where the shark should be in the path, from its predicted location.
            */
            normalPoint = GetNormalPoint(predictedLoc, pointA, pointB);

            if (normalPoint.x < pointA.x || normalPoint.x > pointB.x)
            {
                normalPoint = pointB;

            }

            float normalDistance = VectorUtility.distance(predictedLoc, normalPoint);

            if (normalDistance < bestDistance)
            {
                bestDistance = normalDistance;
                targetNormalPoint = normalPoint;
            }
        }

        /*
         * normalPoint = the point where the shark should be in the path, from its predicted location.
         */
        //Vector2 normalPoint = GetNormalPoint(predictedLoc, pointA, pointB);

        //debugLR.SetPosition(0, predictedLoc);
        //debugLR.SetPosition(1, targetNormalPoint);

        //debugLR01.SetPosition(0, predictedLoc);
        //debugLR01.SetPosition(1, transform.position);

        Vector2 direction = pointB - pointA;
        direction = direction.normalized;
        direction *= directionDistance;

        

        Vector2 target = targetNormalPoint + direction;
        float distance = VectorUtility.distance(targetNormalPoint, predictedLoc);
        if (distance > radius)
        {
            debugLR.SetColors(Color.red, Color.red);
            sb.Seek(target);
        }
        else
        {
            debugLR.SetColors(Color.green, Color.green);
        }

    }

    Vector2 GetNormalPoint(Vector2 predictedLoc, Vector2 pointA, Vector2 pointB)
    {
        Vector2 ap = predictedLoc - pointA;
        Vector2 ab = pointB - pointA;

        ab = ab.normalized;
        ab = ab * (VectorUtility.DotProduct(ap, ab));
        Vector2 normalPoint = pointA + ab;

        return normalPoint;
    }

    Vector2 GetPredictedLocation(float predictionLength)
    {
        Vector2 predict = rb.velocity;
        predict = predict.normalized;

        /*
         * Have the predicted velocity look ahead by a certain amount
         */
        predict *= predictionLength;

        /*
        * Add predict vector to current location to work out predicted
        * location.
        */
        return (Vector2)transform.position + predict;
    }

    

}
