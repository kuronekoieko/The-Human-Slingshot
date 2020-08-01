using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] ragdollRigidbodies;
    Collider[] ragdollColliders;
    [SerializeField] Animator animator;
    [SerializeField] float downForce;
    [SerializeField] Rigidbody handRRb;
    [SerializeField] Rigidbody handLRb;
    [SerializeField] PlayerController playerController;

    public Rigidbody RootRb => ragdollRigidbodies[0];

    void Awake()
    {
        ragdollRigidbodies = animator.GetComponentsInChildren<Rigidbody>();
        ragdollColliders = animator.GetComponentsInChildren<Collider>();
    }
    void Start()
    {

    }

    void Update()
    {
        foreach (var rb in ragdollRigidbodies)
        {
            rb.AddForce(-Vector3.up * downForce);
        }
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

    float refrectForce = 150f;
    public void RefrectFloor(Vector3 normal)
    {
        Vector3 inVelocity = ragdollRigidbodies[0].velocity;
        //Vector3 refrectVec = Vector3.Reflect(inVelocity, normal);
        Debug.Log(inVelocity.sqrMagnitude);
        if (inVelocity.sqrMagnitude < 10f)
        {
            playerController.Result();
            return;
        }

        foreach (var rb in ragdollRigidbodies)
        {
            //rb.velocity = refrectVec * 0.5f;
            rb.AddForce(Vector3.up * refrectForce, ForceMode.Impulse);
        }
        refrectForce *= 0.5f;
    }

    public void SetVelocity(Vector3 vel)
    {
        foreach (var rb in ragdollRigidbodies)
        {
            rb.velocity = vel;
        }
    }

    public void SetGliderHandle(Transform handleR, Transform handleL)
    {
        handRRb.transform.position = handleR.position;
        handLRb.transform.position = handleL.position;
    }
}
