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

    public Transform target;
    Vector3 offset;
    void Start()
    {

    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        offset = transform.position - target.position;
    }

    public void FollowTarget()
    {
        transform.position = target.position + offset;
    }
}
