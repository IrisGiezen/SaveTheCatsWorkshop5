using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TeacherBehaviour : MonoBehaviour
{
    [SerializeField] private float viewDistance = 10;
    [SerializeField] private float teacherHeight = 2;
    [SerializeField] private float teacherWidth = 1;
    [SerializeField] private BoxCollider teacherFov;
    private GameObject[] wayPoints;
    private TeacherStateEnum teacherState = new TeacherStateEnum();
    private Vector3 targetWpLocation;
    private Vector3 lastLocation;
    private NavMeshAgent navMeshAgent;

    private enum TeacherStateEnum
    {
        Idle,
        Walk,
        Chase
    }

    void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("TeacherWaypoint");
        navMeshAgent = GetComponent<NavMeshAgent>();
        teacherFov.center = new Vector3(0, (teacherHeight/2), (viewDistance / 2));
        teacherFov.size = new Vector3(teacherWidth,teacherHeight,viewDistance);
        teacherState = TeacherStateEnum.Idle;
    }

    void Update()
    {
        //Debug.Log("State: " + teacherState.ToString());
        StateMachine();
    }

    private void StateMachine()
    {
        switch (teacherState)
        {
            case TeacherStateEnum.Idle:
                StartPatrolling();
                break;
            case TeacherStateEnum.Chase:
                Chase();
                break;
            case TeacherStateEnum.Walk:
                Walk();
                break;
            default:
                break;
        }
    }

    private void StartPatrolling()
    {
        Debug.Log("Start Patrolling");
        GameObject closestWP = null;
        foreach (var wp in wayPoints)
        {
            if (wp.transform.position != lastLocation)
            {
                if (closestWP == null)
                {
                    closestWP = wp;
                }
                else
                {
                    if (Vector3.Distance(transform.position, wp.transform.position) < (Vector3.Distance(transform.position, closestWP.transform.position)))
                    {
                        closestWP = wp;
                    }
                }
            }
        }
        targetWpLocation = closestWP.transform.position;
        navMeshAgent.SetDestination(targetWpLocation);
        teacherState = TeacherStateEnum.Walk;
    }

    private void Walk()
    {
        transform.rotation.SetLookRotation(targetWpLocation * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetWpLocation) <= 1.5f)
        {
            lastLocation = targetWpLocation;
            teacherState = TeacherStateEnum.Idle;
        }
    }

    private void Chase()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            teacherState = TeacherStateEnum.Chase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            teacherState = TeacherStateEnum.Idle;
        }
    }
}
