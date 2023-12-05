using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArrowDirection : MonoBehaviour
{
    public Transform player;
    public GameObject arrow;
    
    public List<Transform> targetList;

    private Transform currTarget;
    [SerializeField] NavMeshAgent agent;
    NavMeshPath path;
    private void Awake()
    {
        path = new NavMeshPath();
    }

    private void LateUpdate()
    {
        if (currTarget == null) return;
        Vector3 targetPos = currTarget.position;
        targetPos.y = player.position.y;
        Vector3 direction = targetPos - player.position;
        arrow.transform.forward = direction.normalized;
        arrow.transform.position = player.position + new Vector3(0f,0.1f,0f);
    }
    public void SetTarget(Transform target)
    {
        currTarget = target;
        arrow.SetActive(true);
    }

    public void Hide()
    {
        arrow.SetActive(false);
    }
}
