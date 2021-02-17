using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharController : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] private bool smoothTurn;
    [SerializeField] [Range(0, 1)] private float turnSmoothing = 0.1f;
    

    private Vector3 forward, right;
        
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("IsoHorizontal") || Input.GetButton("IsoVertical"))
            Move();
    }

    void Move()
    {
        Vector3 rightMovement, forwardMovement, heading;
        if (smoothTurn)
        {
            rightMovement = Input.GetAxis("IsoHorizontal") * right;
            rightMovement *= moveSpeed * Time.deltaTime;
        
            forwardMovement = Input.GetAxis("IsoVertical") * forward;
            forwardMovement *= moveSpeed * Time.deltaTime;
            
            heading = Vector3.Normalize(rightMovement + forwardMovement);

            transform.forward = Vector3.Lerp(transform.forward, heading, turnSmoothing);
        }
        else
        {
            rightMovement = Input.GetAxisRaw("IsoHorizontal") * right;
            rightMovement *= moveSpeed * Time.deltaTime;
        
            forwardMovement = Input.GetAxisRaw("IsoVertical") * forward;
            forwardMovement *= moveSpeed * Time.deltaTime;
            
            heading = Vector3.Normalize(rightMovement + forwardMovement);
            
            transform.forward = heading;
        }

        // Vector3 heading = Vector3.Normalize(rightMovement + forwardMovement);

        // transform.forward = heading;
        // transform.forward = Vector3.Lerp(transform.forward, heading, turnSmoothing);
        
        transform.position += rightMovement;
        transform.position += forwardMovement;

    }
    
}
