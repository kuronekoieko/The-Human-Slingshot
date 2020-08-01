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
    [SerializeField] CameraController cameraController;
    [SerializeField] GliderController gliderController;
    Vector3 startMousePos;
    Vector3 startPlayerPos;
    Vector3 endPlayerPos;
    float slingshotLetherDistance = 1f;
    float mouseDistanceStartToEnd;
    float maxPullLength = 10f;
    PlayerState playerState;
    Vector3 prePos;
    void Awake()
    {

    }

    void Start()
    {
        startPlayerPos = transform.position;
        endPlayerPos = startPlayerPos - Vector3.forward * 6f;
        ragdollController.EnableRagdoll(enabled: false);
        playerState = PlayerState.Sling;
        cameraController.SetTarget(transform);
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
                gliderController.Glide();
                break;
            default:
                break;
        }
        cameraController.FollowTarget();
    }

    void LateUpdate()
    {
        prePos = transform.position;
    }

    void Sling()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
            Vector3 endMousePos = startMousePos - Vector3.up * 400f;
            mouseDistanceStartToEnd = Vector3.Distance(startMousePos, endMousePos);
            animator.SetTrigger("BackWards");
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
            ragdollController.AddForce(shootVec * 500f);
            playerState = PlayerState.Flying;
            animator.transform.parent = null;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Floor")) return;
        Debug.Log(other.gameObject.name + " =======================");
        ragdollController.RefrectFloor(Vector3.up);
    }
}
