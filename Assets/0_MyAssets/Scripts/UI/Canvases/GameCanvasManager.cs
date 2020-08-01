using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] Text stageNumText;
    [SerializeField] TutrialHandController tutrialHandController;
    [SerializeField] RectTransform startPointRt;
    [SerializeField] RectTransform endPointRt;
    [SerializeField] Image arrowImage;
    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.Game);

        this.ObserveEveryValueChanged(distance => Variables.distance)
            .Subscribe(distance => { ShowDistance(distance); })
            .AddTo(this.gameObject);

        gameObject.SetActive(true);
        tutrialHandController.OnStart();
    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen()) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            tutrialHandController.Kill();
            arrowImage.gameObject.SetActive(false);
        }
    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);

    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
    }

    public override void OnInitialize()
    {
        tutrialHandController.DragAnim(startPointRt.anchoredPosition, endPointRt.anchoredPosition);
        arrowImage.gameObject.SetActive(true);
    }


    void ShowDistance(float distance)
    {
        stageNumText.text = distance.ToString("000") + "m";
    }


}
