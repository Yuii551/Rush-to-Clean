using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float spinSpeed = 20.0f;

    void Update()
    {
        // rotate the object around the y-axis
        transform.Rotate(0, 5.0f * spinSpeed * Time.deltaTime, 0, Space.Self);
    }
}