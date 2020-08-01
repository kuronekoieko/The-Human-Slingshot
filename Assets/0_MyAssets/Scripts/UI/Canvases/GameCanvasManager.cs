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

    public override void OnStart()
    {


        base.SetScreenAction(thisScreen: ScreenState.Game);

        this.ObserveEveryValueChanged(distance => Variables.distance)
            .Subscribe(distance => { ShowDistance(distance); })
            .AddTo(this.gameObject);

        gameObject.SetActive(true);

    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen()) { return; }

    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
    }

    protected override void OnClose()
    {
        // gameObject.SetActive(false);
    }

    void ShowDistance(float distance)
    {
        stageNumText.text = distance.ToString("000") + "m";
    }
}
