using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRope : MonoBehaviour
{
   
    [SerializeField]private Transform target;
  
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target);
        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 oldScale = transform.localScale;
        oldScale[2] = distance;
        transform.localScale = oldScale;
    }
}
