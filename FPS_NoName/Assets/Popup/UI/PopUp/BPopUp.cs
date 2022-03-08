using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class BPopUp : MonoBehaviour
{
	#region Inspector variables

	[Header("ID")]
	public PopupManager.PopupTypes popupType = PopupManager.PopupTypes.Default;

	[Header("Hierarchy")]
	[SerializeField] Text title = null;
	[SerializeField] Text messageBody = null;
	[SerializeField] Text confirmLabel = null;
	[SerializeField] Text cancelLabel = null;

	[Header("Popup Animation")]
	[SerializeField] float showAnimDuration = 0.35f;
	[SerializeField] float hideAnimDuration = 0.25f;

	#endregion

	internal PopupManager parentManager;
	protected Transform myTrans;

	public float ShowAnimSpeed { get { return 1f / showAnimDuration; } }
	public float HideAnimSpeed { get { return 1f / hideAnimDuration; } }

	/// <summary>
	/// �\���J�n
	/// </summary>
	/// <param name="_popupInfo"></param>
	public virtual void StartShowing(PopupManager.PopupInfo _popupInfo)
	{
		if (myTrans == null)
			myTrans = transform;

		if (title != null)
			title.text = _popupInfo.title;
		if (messageBody != null)
			messageBody.text = _popupInfo.messageBody;
		if (confirmLabel != null)
			confirmLabel.text = _popupInfo.confirmText;
		if (cancelLabel != null)
			cancelLabel.text = _popupInfo.cancelText;

		UpdateShowAnim(0f);
		gameObject.SetActive(true);
	}
	public abstract void UpdateShowAnim(float _progress);
	public abstract void UpdateHideAnim(float _progress);
}
