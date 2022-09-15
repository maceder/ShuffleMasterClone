using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    public float environmentMoveSpeed;



    private void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * environmentMoveSpeed;
    }
}
