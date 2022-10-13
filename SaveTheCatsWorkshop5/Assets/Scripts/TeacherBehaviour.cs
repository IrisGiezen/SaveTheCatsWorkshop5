using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class TeacherBehaviour : MonoBehaviour
{
    [SerializeField] private float viewDistance = 10;
    [SerializeField] private float teacherHeight = 2;
    [SerializeField] private float teacherWidth = 1;
    [SerializeField] private BoxCollider teacherFov;
    private GameObject[] wayPoints;
    private GameObject player;
    private TeacherStateEnum teacherState = new TeacherStateEnum();
    private Vector3 targetWpLocation;
    private Vector3 lastLocation;
    private Vector3 secondToLastLocation;
    private NavMeshAgent navMeshAgent;
    private Animator _animatorController;

    private enum TeacherStateEnum
    {
        Idle,
        Walk,
        Chase
    }

    void Start()
    {
        _animatorController = GetComponentInChildren<Animator>();
        wayPoints = GameObject.FindGameObjectsWithTag("TeacherWaypoint");
        player = GameObject.FindGameObjectWithTag("Player");
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
                _animatorController.SetInteger("teacherState", 0);
                StartPatrolling();
                break;
            case TeacherStateEnum.Walk:
                _animatorController.SetInteger("teacherState", 1);
                Walk();
                Look();
                break;
            case TeacherStateEnum.Chase:
                _animatorController.SetInteger("teacherState", 2);
                Chase();
                break;
            default:
                break;
        }
    }

    private void Look()
    {
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, out hit, transform.rotation, viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("I Chase Cat");
                navMeshAgent.SetDestination(player.transform.position);
                teacherState = TeacherStateEnum.Chase;
            }
        }
        else
        {
            teacherState = TeacherStateEnum.Idle;
        }
    }

    private void StartPatrolling()
    {
        Debug.Log("Start Patrolling");
        GameObject closestWP = null;
        foreach (var wp in wayPoints)
        {
            if (wp.transform.position != lastLocation && wp.transform.position != secondToLastLocation)
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
            secondToLastLocation = lastLocation;
            lastLocation = targetWpLocation;
            teacherState = TeacherStateEnum.Idle;
        }
    }

    private void Chase()
    {
        Debug.Log("Start Chasing");
        transform.rotation.SetLookRotation(player.transform.position * Time.deltaTime);
    }
}
