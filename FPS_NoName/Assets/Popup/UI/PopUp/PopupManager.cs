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
		public PopupTypes _popupType = PopupTypes.Default;                  //�\���^�C�v
		public string title = String.Empty;									//�^�C�g��
		public string messageBody = String.Empty;							//�e�L�X�g
		public string confirmText = "OK";									//(�{�^���\�����̂�)����e�L�X�g
		public string cancelText = "Cancel";								//(�{�^���\�����̂�)�L�����Z���e�L�X�g
		public Action confirmCallback = null;								//(�{�^���\�����̂�)���莞����
		public Action cancelCallback = null;								//(�{�^���\�����̂�)�L�����Z��������
	}

	States state = States.Idle;
	Dictionary<PopupTypes, BPopUp> popups = new Dictionary<PopupTypes, BPopUp>();
	BPopUp currentPopup;
	PopupInfo currentPopupInfo;
	float animSpeed, animProgress;
	Action dismissCallback;
	Queue<PopupInfo> queuedPopups = new Queue<PopupInfo>();

	/// <summary>
	/// �A�b�v�f�[�g����
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
				//�f�t�H���g�^�C�v�̎��A��莞�Ԃŕ���
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
	/// �ŏ��Ƀ|�b�v�A�b�v�����s�����Ƃ��̏���������
	/// </summary>
	void InitChildPopups()
	{

	}

	#region Public interface

	/// <summary>
	/// �|�b�v�A�b�v���L���[�ɓ���A�\�����Ă���|�b�v�A�b�v���Ȃ���΂��̂܂ܕ\������
	/// </summary>
	/// <param name="_popupInfo"> = �\������|�b�v�A�b�v���</param>
	public void ShowOrEnqueue(PopupInfo _popupInfo)
	{
		if (popups.Keys.Count == 0)
			InitChildPopups();

		queuedPopups.Enqueue(_popupInfo);

		if (state == States.Idle)
			ShowNextPopup();
	}

	/// <summary>
	/// ���̃|�b�v�A�b�v��\������
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
	/// �����ꂽ�{�^���̏��������s
	/// </summary>
	/// <param name="_dismissCallback"> = �Ή��{�^���̏���</param>
	public void PopupDismissed(Action _dismissCallback)
	{
		dismissCallback = _dismissCallback;

		//�l���Z�b�g
		animSpeed = currentPopup.HideAnimSpeed;
		animProgress = 0f;
		state = States.Dismissing;
	}

	public void PopupConfirmed() { PopupDismissed(currentPopupInfo.confirmCallback); }
	public void PopupCancelled() { PopupDismissed(currentPopupInfo.cancelCallback); }

	#endregion
}
