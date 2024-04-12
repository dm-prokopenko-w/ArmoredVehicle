using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static Game.Constants;

namespace UISystem
{
	public class PopupController : IStartable
    {
		[Inject] private UIController _uiController;

		private Dictionary<string, PopupView> _popups = new ();
		private string _currentPopup = "";
		
		public void Start()
		{
			_uiController.SetAction(ActivePopupID + true, (id) => ActivePopup(id, true));
			_uiController.SetAction(ActivePopupID + false, (id) => ActivePopup(id, false));
		}

		public void AddPopupView(string id, PopupView popupView) => _popups.Add(id, popupView);

		private void ActivePopup(string id, bool value)
		{
			string keyName = value ? ShowKey : HideKey;
			if (_popups.TryGetValue(id, out PopupView popup))
			{
				popup.GetAnimator().Play(keyName);
			}
		}
	}
}