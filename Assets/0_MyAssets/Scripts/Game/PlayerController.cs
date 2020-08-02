using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Sling,
    Flying,
    Landing,
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] SlingShotController slingShotController;
    [SerializeField] Animator animator;
    [SerializeField] RagdollController ragdollController;
    [SerializeField] CameraController cameraController;
    [SerializeField] GliderController gliderController;
    [SerializeField] FlagController flagController;
    Vector3 startMousePos;
    Vector3 startPlayerPos;
    Vector3 endPlayerPos;
    float slingshotLetherDistance = 1f;
    float mouseDistanceStartToEnd;
    float maxPullLength = 10f;
    PlayerState playerState;
    Vector3 prePos;
    float landingTimer;
    float proportion;
    void Awake()
    {

    }

    void Start()
    {
        startPlayerPos = transform.position;
        endPlayerPos = startPlayerPos - Vector3.forward * 6f;
        ragdollController.EnableRagdoll(enabled: false);
        playerState = PlayerState.Sling;

        ChangeTag(transform);
        Variables.distance = 0;
    }

    void ChangeTag(Transform transform)
    {
        foreach (Transform child in transform)
        {
            child.tag = "Player";
            ChangeTag(child);
        }
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
                LandCheck();
                var distance = transform.position.z - slingShotController.CenterPos.z;
                Variables.distance = distance < 0 ? 0 : distance;
                cameraController.RotateOnFly(transform.position);
                break;
            case PlayerState.Landing:
                cameraController.RotateOnLanding(transform.position);
                break;
            default:
                break;
        }
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
            proportion = mouseDistance / mouseDistanceStartToEnd;
            transform.position = Vector3.Lerp(startPlayerPos, endPlayerPos, proportion);
            slingShotController.SetPosition(transform.position);
        }

        if (proportion < 0.1f) return;

        if (Input.GetMouseButtonUp(0))
        {
            slingShotController.Release();

            var shootVec = (slingShotController.CenterPos - transform.position).normalized;
            ragdollController.EnableRagdoll(enabled: true);
            ragdollController.AddForce(shootVec * 500f);
            playerState = PlayerState.Flying;
            animator.transform.parent = null;
            //cameraController.ShotMove();
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Floor")) return;
        if (playerState != PlayerState.Flying) return;
        ragdollController.RefrectFloor(Vector3.up);
    }

    public void LandCheck()
    {
        if (!ragdollController.IsStop())
        {
            landingTimer = 0;
            return;
        }

        landingTimer += Time.deltaTime;
        if (landingTimer < 1f) return;

        playerState = PlayerState.Landing;
        flagController.Show(transform.position);
        if (Variables.screenState != ScreenState.Game) return;
        Variables.screenState = ScreenState.Result;
    }
}
