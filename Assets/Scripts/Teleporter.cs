using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetPos;
    public Transform player;
    public ParticleSystem vfx;
    public ArrowDirection arrowDirection;

    public bool flag = false;
    public float delayTime;
    private float timer = 0;
    private void OnTriggerEnter(Collider other)
    {
        vfx.Play();
    }
    private void OnTriggerStay(Collider other)
    {
        timer += Time.fixedDeltaTime;
        if (timer > delayTime)
        {

            player.position = targetPos.position;
            if (!flag)
            {
                arrowDirection.SetNextTarget();
                flag = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0f;
    }
}
