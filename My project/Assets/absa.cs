using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class absa : MonoBehaviour
{
    public Transform aboasfa;
    NavMeshAgent fasf;

    // Update is called once per frame
    void Start()
    {
        fasf = GetComponent<NavMeshAgent>();
        fasf.updateRotation = false;
        fasf.updateUpAxis = false;
    }

    private void Update()
    {
        fasf.SetDestination(aboasfa.position);
    }
}
