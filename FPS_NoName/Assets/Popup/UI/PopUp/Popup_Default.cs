using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Default : BPopUp
{
	#region Inspector variables

	[Header("Popup Animation")]
	[SerializeField] AnimationCurve showAnimCurve = new AnimationCurve(new Keyframe(0f, 1f, -7f, -7f), new Keyframe(0.4f, 0f, -0.6f, -0.6f), new Keyframe(1f, 0f, 0.1f, 0.1f));
	[SerializeField] AnimationCurve hideAnimCurve = new AnimationCurve(new Keyframe(0f, 0f, -1f, -1f), new Keyframe(1f, 1f, 1f, 1f));

	[Header("SlidePos Specific")]
	[SerializeField] Vector2 slideDistance = new Vector2(-2000f, 0);

	#endregion

	RectTransform myRectTrans;
	Vector2 origAnchoredPos;

	/// <summary>
	/// �\���J�n
	/// </summary>
	/// <param name="_popupInfo"> = �|�b�v�A�b�v���</param>
	public override void StartShowing(PopupManager.PopupInfo _popupInfo)
	{
		if (myRectTrans == null)
		{
			myRectTrans = GetComponent<RectTransform>();
			origAnchoredPos = myRectTrans.anchoredPosition;
		}

		base.StartShowing(_popupInfo);
	}

	/// <summary>
	/// �\�����̃A�j���[�V����
	/// </summary>
	/// <param name="_progress"> = �o�ߎ���</param>
	public override void UpdateShowAnim(float _progress)
	{
		myRectTrans.anchoredPosition = origAnchoredPos + (slideDistance * showAnimCurve.Evaluate(_progress));
	}

	/// <summary>
	/// �B���Ƃ��̃A�j���[�V����
	/// </summary>
	/// <param name="_progress"> = �o�ߎ���</param>
	public override void UpdateHideAnim(float _progress)
	{
		myRectTrans.anchoredPosition = origAnchoredPos + (slideDistance * hideAnimCurve.Evaluate(_progress));
	}
}
