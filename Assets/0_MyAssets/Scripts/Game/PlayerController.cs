using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Sling,
    Flying,
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] SlingShotController slingShotController;
    [SerializeField] Animator animator;
    [SerializeField] RagdollController ragdollController;
    Vector3 startMousePos;
    Vector3 startPlayerPos;
    Vector3 endPlayerPos;
    float slingshotLetherDistance = 1f;
    float mouseDistanceStartToEnd;
    float maxPullLength = 10f;
    PlayerState playerState;
    void Awake()
    {

    }

    void Start()
    {
        startPlayerPos = transform.position;
        endPlayerPos = startPlayerPos - Vector3.forward * 6f;
        ragdollController.EnableRagdoll(enabled: false);
        playerState = PlayerState.Sling;
    }


    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Sling:
                Sling();
                break;
            case PlayerState.Flying:
                transform.position = animator.transform.position;
                break;
            default:
                break;
        }
    }


    void Sling()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
            Vector3 endMousePos = startMousePos - Vector3.up * 400f;
            mouseDistanceStartToEnd = Vector3.Distance(startMousePos, endMousePos);
        }

        if (Input.GetMouseButton(0))
        {

            float mouseDistance = Vector3.Distance(startMousePos, Input.mousePosition);
            if (startMousePos.y < Input.mousePosition.y) { mouseDistance = 0; }
            float proportion = mouseDistance / mouseDistanceStartToEnd;

            transform.position = Vector3.Lerp(startPlayerPos, endPlayerPos, proportion);
            slingShotController.SetPosition(transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            slingShotController.Release();

            var shootVec = (slingShotController.CenterPos - transform.position).normalized;
            ragdollController.EnableRagdoll(enabled: true);
            ragdollController.AddForce(shootVec * 100f);
            playerState = PlayerState.Flying;
            animator.transform.parent = null;
        }

    }
}
