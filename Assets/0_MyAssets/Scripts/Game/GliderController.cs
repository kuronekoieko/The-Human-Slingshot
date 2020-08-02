using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderController : MonoBehaviour
{
    [SerializeField] Transform gliderTf;
    [SerializeField] RagdollController ragdollController;
    [SerializeField] Transform handleR;
    [SerializeField] Transform handleL;
    float radius = 100f;
    Vector3 center;

    void Start()
    {
        gliderTf.gameObject.SetActive(false);
    }

    public void Glide()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gliderTf.gameObject.SetActive(true);
            Vector3 cross = Vector3.Cross(ragdollController.RootRb.velocity, Vector3.right);
            center = transform.position + cross.normalized * radius;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 vectorFromCenter = transform.position - center;
            Vector3 flyVec = Vector3.Cross(vectorFromCenter, Vector3.right);
            ragdollController.SetVelocity(flyVec.normalized * Vector3.Distance(ragdollController.RootRb.velocity, Vector3.zero));
            ragdollController.SetGliderHandle(handleR, handleL);
        }
        if (Input.GetMouseButtonUp(0))
        {
            gliderTf.gameObject.SetActive(false);
        }
    }
}
