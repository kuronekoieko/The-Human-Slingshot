using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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


    public void RefrectFloor(Vector3 normal)
    {
        Vector3 inVelocity = ragdollRigidbodies[0].velocity;
        float boundForce = Mathf.Abs(inVelocity.y) * 3f;
        foreach (var rb in ragdollRigidbodies)
        {
            rb.AddForce(Vector3.up * boundForce, ForceMode.Impulse);
        }
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


    public bool IsStop()
    {
        float velocityAverage = ragdollRigidbodies.Average(r => r.velocity.magnitude);
        return velocityAverage < 0.5f;
    }
}
