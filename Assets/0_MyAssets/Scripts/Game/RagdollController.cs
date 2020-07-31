using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] ragdollRigidbodies;
    Collider[] ragdollColliders;
    [SerializeField] Animator animator;
    public Vector3 RootRbPos => ragdollRigidbodies[0].transform.position;

    void Awake()
    {
        ragdollRigidbodies = animator.GetComponentsInChildren<Rigidbody>();
        ragdollColliders = animator.GetComponentsInChildren<Collider>();
    }
    void Start()
    {

    }

    public void EnableRagdoll(bool enabled)
    {
        foreach (var rb in ragdollRigidbodies)
        {
            rb.isKinematic = !enabled;
        }
        foreach (var col in ragdollColliders)
        {
            col.enabled = enabled;
        }
        animator.enabled = !enabled;
    }


    public void AddForce(Vector3 force)
    {
        foreach (var rb in ragdollRigidbodies)
        {
            rb.AddForce(force, ForceMode.Impulse);
        }
    }


}
