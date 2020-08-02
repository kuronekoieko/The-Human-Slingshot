using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Unityで解像度に合わせて画面のサイズを自動調整する
/// http://www.project-unknown.jp/entry/2017/01/05/212837
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cameraOriginTf;
    [SerializeField] Transform camera1Tf;
    [SerializeField] Transform camera2Tf;
    RotateStateOnFly rotateStateOnFly;
    Vector3 startLocalPos;
    Quaternion startLocalRotation;
    void Start()
    {
        rotateStateOnFly = RotateStateOnFly.ToLeft;
        startLocalPos = transform.localPosition;
        startLocalRotation = transform.localRotation;
    }

    public void RotateOnFly(Vector3 targetPos)
    {
        var vec = transform.localPosition;
        vec.y = 0;
        float angle = Vector3.SignedAngle(-Vector3.forward, vec, Vector3.up);
        switch (rotateStateOnFly)
        {
            case RotateStateOnFly.ToLeft:
                transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * 40f);
                if (angle > 30f) rotateStateOnFly = RotateStateOnFly.ToRight;
                break;
            case RotateStateOnFly.ToRight:
                transform.RotateAround(targetPos, Vector3.up, -Time.deltaTime * 25f);
                if (angle < -30f) rotateStateOnFly = RotateStateOnFly.ToCenter;
                break;
            case RotateStateOnFly.ToCenter:
                float diff = Time.deltaTime * 40f;
                if (angle + diff > 0f)
                {
                    transform.localPosition = startLocalPos;
                    transform.localRotation = startLocalRotation;
                    rotateStateOnFly = RotateStateOnFly.End;
                    break;
                }
                transform.RotateAround(targetPos, Vector3.up, diff);
                break;
            case RotateStateOnFly.End:
                break;
            default:
                break;
        }

    }

    public void RotateOnLanding(Vector3 targetPos)
    {
        transform.RotateAround(targetPos, Vector3.up, -Time.deltaTime * 5f);
    }

}

public enum RotateStateOnFly
{
    ToLeft,
    ToRight,
    ToCenter,
    End,
}