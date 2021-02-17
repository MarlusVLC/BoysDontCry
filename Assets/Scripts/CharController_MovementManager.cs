using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CharController_MovementManager : MonoBehaviour
{
    private CharController_KeyBoard _keyBoard;
    private CharController_PointNClick _pointNClick;
    private NavMeshAgent _agent;

    private void Start()
    {
        _keyBoard = GetComponent<CharController_KeyBoard>();
        _pointNClick = GetComponent<CharController_PointNClick>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("IsoHorizontal") || Input.GetButtonDown("IsoVertical"))
        {
            _keyBoard.enabled = true;
            _pointNClick.enabled = false;
            _agent.enabled = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            _keyBoard.enabled = false;
            _pointNClick.enabled = true;
            _agent.enabled = true;
            // _pointNClick.Invoke("Update()",0.01f);
        }
        
        //TODO
        //Chamar uma funcao de movimento do script de PointNClick assim que ele for ativado
    }
}
