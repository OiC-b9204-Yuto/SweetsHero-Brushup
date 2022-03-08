using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_WithButton : BPopUp
{
	#region Inspector variables

	[Header("Popup Animation")]
	[SerializeField] AnimationCurve showAnimCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1.1f), new Keyframe(0.8f, 0.95f), new Keyframe(1f, 1f));
	[SerializeField] AnimationCurve hideAnimCurve = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(0.2f, 1.2f), new Keyframe(1f, 0f));

	#endregion

	/// <summary>
	/// 表示時のアニメーション
	/// </summary>
	/// <param name="_progress"> = 経過時間</param>
	public override void UpdateShowAnim(float _progress)
	{
		myTrans.localScale = Vector3.one * showAnimCurve.Evaluate(_progress);
	}

	/// <summary>
	/// 隠すときのアニメーション
	/// </summary>
	/// <param name="_progress"> = 経過時間</param>
	public override void UpdateHideAnim(float _progress)
	{
		myTrans.localScale = Vector3.one * hideAnimCurve.Evaluate(_progress);
	}
}
