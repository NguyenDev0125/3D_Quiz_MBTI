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
        SetNextTarget();
      
    }

    private void LateUpdate()
    {
        Vector3 targetPos = currTarget.position;
        targetPos.y = player.position.y;
        Vector3 direction = targetPos - player.position;
        arrow.transform.forward = direction.normalized;
        arrow.transform.position = player.position + new Vector3(0f,0.1f,0f);
    }
    public void SetNextTarget()
    {
        
        if(targetList.Count > 0)
        {
            currTarget = targetList[0];
            targetList.RemoveAt(0);
            
            //bool check = agent.CalculatePath(currTarget.position , path);
            //if (!check) Debug.Log("Path not found");
            //for(int i = 0; i < path.corners.Length -1;i++)
            //{
            //    GameObject c = Instantiate(arrow);
            //    c.transform.forward = path.corners[i + 1] - path.corners[i];
            //    c.transform.position = new Vector3(path.corners[i].x , transform.position.y , path.corners[i].z);
            //}
            //Instantiate(arrow, path.corners[path.corners.Length -1] , Quaternion.identity);
        }
        else
        {
            arrow.gameObject.SetActive(false);
        }
    }
}
