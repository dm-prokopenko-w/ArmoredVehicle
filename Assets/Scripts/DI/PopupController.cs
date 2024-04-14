using System.Collections.Generic;
using VContainer;
using VContainer.Unity;
using static Game.Constants;

namespace ItemSystem
{
	public class PopupController : IStartable
    {
		[Inject] private ItemController _itemController;

		private Dictionary<string, PopupView> _popups = new ();
		
		public void Start()
		{
			_itemController.SetAction(ActivePopupID + true, (id) => ActivePopup(id, true));
			_itemController.SetAction(ActivePopupID + false, (id) => ActivePopup(id, false));
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