using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VirusControl : MonoBehaviour
{
  public Transform chaseTarget;
  public NavMeshAgent navMeshAgent;
  public bool isDead;

  void Start()
  {
    isDead = false;
    chaseTarget = GameObject.FindWithTag("Player").transform;
  }

  void Update()
  {
    navMeshAgent.destination = chaseTarget.position;
    navMeshAgent.Resume();
  }

  void OnTriggerEnter(Collider other)
  {
        Debug.Log("hit");
        if (other.tag == "Projectile") {
            isDead = true;
            Destroy(other.gameObject);
           
        }
  }
}
