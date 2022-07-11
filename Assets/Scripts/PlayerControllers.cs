using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerControllers : MonoBehaviour
{
    [SerializeField] private Transform goal;
    [SerializeField] private float maximumAcceptableDistance;
    
    private ThirdPersonCharacter thirdPersonCharacter;

    void Awake()
    {
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void setTarget(Transform newGoal)
    {
        this.goal = newGoal;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(goal.position , this.transform.position) > maximumAcceptableDistance)
        {
            thirdPersonCharacter.Move(goal.position- transform.position ,false ,false);   
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero ,false ,false);   
        }
    }
}
