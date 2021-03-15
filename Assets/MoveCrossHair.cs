using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrossHair : MonoBehaviour
{
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * Time.fixedDeltaTime);
    }
}
