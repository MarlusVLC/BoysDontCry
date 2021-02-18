using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CharController_HybridMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Space]
    [Header("Keyboard Movement")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private bool smoothTurn;
    [SerializeField] [Range(0, 1)] private float turnSmoothing = 0.1f;
    [Space]
    [Header("Mouse Movement")]
    [SerializeField] private float agentNormalSpeed = 6;
    [SerializeField] private float agentBoostSpeed = 12;
    
    private Vector3 _forward, _right;
    private Vector3 _currDestination;

    private NavMeshAgent _agent;


    private void Awake()
    {
    }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = agentNormalSpeed;
        
        _forward = cam.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            _agent.enabled = true;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _agent.SetDestination(hit.point);

                if (Vector3.Distance(_currDestination,_agent.destination) < 0.5f)
                    // && Math.Truncate(_agent.speed) == Math.Truncate(agentNormalSpeed))
                {
                    if (Math.Truncate(_agent.speed) == Math.Truncate(agentNormalSpeed))
                        _agent.speed = agentBoostSpeed;
                }
                else
                {
                    _agent.speed = agentNormalSpeed;
                }

                _currDestination = _agent.destination;
            }
        }

        if (_agent.enabled)
        {
            if (_agent.isStopped)
            {
                _agent.speed = agentNormalSpeed;
            }  
        }

    }
    
    void FixedUpdate()
    {
        if (Input.GetButton("IsoHorizontal") || Input.GetButton("IsoVertical"))
        {
            _agent.speed = agentNormalSpeed;
            _agent.enabled = false;
            Move();
        }
    }
    
    
    
    private void Move()
    {
        Vector3 rightMovement, forwardMovement, heading;
        if (smoothTurn)
        {
            rightMovement = Input.GetAxis("IsoHorizontal") * _right;
            rightMovement *= moveSpeed * Time.deltaTime;
        
            forwardMovement = Input.GetAxis("IsoVertical") * _forward;
            forwardMovement *= moveSpeed * Time.deltaTime;
            
            heading = Vector3.Normalize(rightMovement + forwardMovement);

            transform.forward = Vector3.Lerp(transform.forward, heading, turnSmoothing);
        }
        else
        {
            rightMovement = Input.GetAxisRaw("IsoHorizontal") * _right;
            rightMovement *= moveSpeed * Time.deltaTime;
        
            forwardMovement = Input.GetAxisRaw("IsoVertical") * _forward;
            forwardMovement *= moveSpeed * Time.deltaTime;
            
            heading = Vector3.Normalize(rightMovement + forwardMovement);
            
            transform.forward = heading;
        }

        // transform.position += rightMovement;
        // transform.position += forwardMovement;
        transform.position += heading * moveSpeed;

    }

    // private void AgentMove()
    // {
    //     if (_agent.hasPath)
    //         _agent.speed *= 2;
    // }
    
}
