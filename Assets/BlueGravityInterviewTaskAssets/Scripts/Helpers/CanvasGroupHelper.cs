using System;
using DG.Tweening;
using UnityEngine;

public static class CanvasGroupHelper {
	public static void FadeCanvasGroup(this CanvasGroup canvasGroup, bool active, float duration = 0, float delay = 0, Action onCompleteAction = null)
	{
		if (onCompleteAction != null)
		{
			if (active)
			{
				canvasGroup.blocksRaycasts = true;
				canvasGroup.DOFade(1, duration).SetDelay(delay).OnComplete(() => {
					canvasGroup.interactable = true;
					onCompleteAction();
				});
			}
			else
			{
				canvasGroup.interactable = false;
				canvasGroup.blocksRaycasts = false;
				canvasGroup.DOFade(0, duration).SetDelay(delay).OnComplete(() => onCompleteAction());
			}
		}
		else
		{
			if (active)
			{
				canvasGroup.blocksRaycasts = true;
				canvasGroup.DOFade(1, duration).SetDelay(delay).OnComplete(() => {
					canvasGroup.interactable = true;
				});
			}
			else
			{
				canvasGroup.interactable = false;
				canvasGroup.blocksRaycasts = false;
				canvasGroup.DOFade(0, duration).SetDelay(delay);
			}
		}
	}
}

