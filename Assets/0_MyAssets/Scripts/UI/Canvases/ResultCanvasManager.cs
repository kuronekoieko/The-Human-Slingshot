using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ResultCanvasManager : BaseCanvasManager
{
    [SerializeField] Button retryButton;
    [SerializeField] Text distanceText;
    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.Result);
        retryButton.onClick.AddListener(OnClickRetryButton);
        gameObject.SetActive(false);
    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen()) { return; }

    }

    protected override void OnOpen()
    {
        DOVirtual.DelayedCall(2, () =>
        {
            gameObject.SetActive(true);
            distanceText.text = Variables.distance.ToString("000") + "m";
        });

    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
    }

    void OnClickRetryButton()
    {
        base.ReLoadScene();
    }
}
