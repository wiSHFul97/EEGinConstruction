using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform rotation;
    [SerializeField] private Transform radius ;
    [SerializeField] private Transform rope;

    [SerializeField] private float ropeDistanceToTargetY;

    // [SerializeField] private float smothnessSpeed;

    [SerializeField] private float rotationSmoothnessSpeed;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = target.position - rotation.position;
        
        //ratation
        Vector3 relativeRotation = relativePos;
        relativeRotation[1] = 0;
        relativeRotation = Quaternion.AngleAxis(90, Vector3.up) * relativeRotation;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion targetRotation = Quaternion.LookRotation(relativeRotation, Vector3.up);
        // rotation.rotation = targetRotation;
        rotation.rotation = Quaternion.Slerp(rotation.rotation , targetRotation , rotationSmoothnessSpeed);

        //gollab
        Vector3 radiusVector3 = target.position;
        radiusVector3.y = radius.position.y;
        // radius.position = radiusVector3;
        radius.position = Vector3.Lerp(radius.position , radiusVector3 , 0.2f);
        
        //rope
        Vector3 ropePos = rope.transform.position;
        ropePos.y = target.position.y + ropeDistanceToTargetY;
        // rope.position = ropePos;
        
        rope.position = Vector3.Lerp(rope.position, ropePos , 0.2f);
    }
}
