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
    void Start()
    {

    }

    public void ShotMove()
    {
        float distance = cameraOriginTf.localPosition.magnitude;

        Sequence sequence = DOTween.Sequence()
        .Append(transform.DOLocalMove(camera1Tf.localPosition, 1f).SetEase(Ease.Linear))
        .Join(transform.DOLocalRotate(camera1Tf.eulerAngles, 2f).SetEase(Ease.Linear))
        .Append(transform.DOLocalMove(cameraOriginTf.localPosition, 2f).SetEase(Ease.Linear))
        .Join(transform.DOLocalRotate(cameraOriginTf.eulerAngles, 2f).SetEase(Ease.Linear))
        .Append(transform.DOLocalMove(camera2Tf.localPosition, 2f).SetEase(Ease.Linear))
        .Join(transform.DOLocalRotate(camera2Tf.eulerAngles, 2f).SetEase(Ease.Linear))
        .Append(transform.DOLocalMove(cameraOriginTf.localPosition, 1f).SetEase(Ease.Linear))
        .Join(transform.DOLocalRotate(cameraOriginTf.eulerAngles, 1f).SetEase(Ease.Linear));



    }

    public void RotateAround(Vector3 targetPos)
    {
        transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * 4f);
    }

}
