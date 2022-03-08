using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
	public enum PopupTypes { Default, WithButton }
	enum States { Idle, PoppingUp, PopupShown, Dismissing }

	float currentDisplayTime = 0;
	[SerializeField] float displayTime = 5.0f;

	public static PopupManager instance
    {
		private set;
		get;
    }

	void Awake()
    {
		if(instance == null) 
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else if(instance != this)
        {
			Destroy(gameObject);
			return;
        }

		BPopUp[] childPopups = GetComponentsInChildren<BPopUp>(true);
		for (int i = 0; i < childPopups.Length; i++)
		{
			BPopUp popup = childPopups[i];
			popup.parentManager = this;
			popups.Add(popup.popupType, popup);
			popup.gameObject.SetActive(false);
		}
	}

	public class PopupInfo
	{
		public PopupTypes _popupType = PopupTypes.Default;                  //表示タイプ
		public string title = String.Empty;									//タイトル
		public string messageBody = String.Empty;							//テキスト
		public string confirmText = "OK";									//(ボタン表示時のみ)決定テキスト
		public string cancelText = "Cancel";								//(ボタン表示時のみ)キャンセルテキスト
		public Action confirmCallback = null;								//(ボタン表示時のみ)決定時処理
		public Action cancelCallback = null;								//(ボタン表示時のみ)キャンセル時処理
	}

	States state = States.Idle;
	Dictionary<PopupTypes, BPopUp> popups = new Dictionary<PopupTypes, BPopUp>();
	BPopUp currentPopup;
	PopupInfo currentPopupInfo;
	float animSpeed, animProgress;
	Action dismissCallback;
	Queue<PopupInfo> queuedPopups = new Queue<PopupInfo>();

	/// <summary>
	/// アップデート処理
	/// </summary>
	void Update()
	{
		switch (state)
		{
			case States.Idle:
				gameObject.SetActive(false);
				break;

			case States.PoppingUp:
				animProgress += animSpeed * Time.deltaTime;
				if (animProgress > 1f)
				{
					animProgress = 1f;
					state = States.PopupShown;
				}
				currentPopup.UpdateShowAnim(animProgress);
				break;

			case States.PopupShown:
				//デフォルトタイプの時、一定時間で閉じる
				if(currentPopupInfo._popupType == PopupTypes.Default)
                {
					currentDisplayTime += Time.deltaTime;
					if(currentDisplayTime > displayTime)
                    {
						currentDisplayTime = 0;
						animProgress = 0;
						state = States.Dismissing;
					}
				}
				break;

			case States.Dismissing:
				animProgress += animSpeed * Time.deltaTime;
				if (animProgress < 1f)
					currentPopup.UpdateHideAnim(animProgress);
				else
				{
					if (dismissCallback != null)
						dismissCallback();

					currentPopup.gameObject.SetActive(false);
					currentPopup = null;
					if (queuedPopups.Count > 0)
						ShowNextPopup();
					else
						state = States.Idle;
				}
				break;

			default:
				throw new UnityException("Unhandled state " + state);
		}
	}

	/// <summary>
	/// 最初にポップアップを実行したときの初期化処理
	/// </summary>
	void InitChildPopups()
	{

	}

	#region Public interface

	/// <summary>
	/// ポップアップをキューに入れ、表示しているポップアップがなければそのまま表示する
	/// </summary>
	/// <param name="_popupInfo"> = 表示するポップアップ情報</param>
	public void ShowOrEnqueue(PopupInfo _popupInfo)
	{
		if (popups.Keys.Count == 0)
			InitChildPopups();

		queuedPopups.Enqueue(_popupInfo);

		if (state == States.Idle)
			ShowNextPopup();
	}

	/// <summary>
	/// 次のポップアップを表示する
	/// </summary>
	void ShowNextPopup()
	{
		currentPopupInfo = queuedPopups.Dequeue();
		currentPopup = popups[currentPopupInfo._popupType];
		currentPopup.StartShowing(currentPopupInfo);
		gameObject.SetActive(true);

		animSpeed = currentPopup.ShowAnimSpeed;
		animProgress = 0f;
		state = States.PoppingUp;
	}

	/// <summary>
	/// 押されたボタンの処理を実行
	/// </summary>
	/// <param name="_dismissCallback"> = 対応ボタンの処理</param>
	public void PopupDismissed(Action _dismissCallback)
	{
		dismissCallback = _dismissCallback;

		//値をセット
		animSpeed = currentPopup.HideAnimSpeed;
		animProgress = 0f;
		state = States.Dismissing;
	}

	public void PopupConfirmed() { PopupDismissed(currentPopupInfo.confirmCallback); }
	public void PopupCancelled() { PopupDismissed(currentPopupInfo.cancelCallback); }

	#endregion
}
