using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UISystem
{
	public class UIController
	{
		private Dictionary<string, List<ItemUI>> _items = new ();

		public void AddItemUI(string id, ItemUI item)
		{
			if (_items.TryGetValue(id, out List<ItemUI> items))
			{
				items.Add(item);
			}
			else
			{
				List<ItemUI> newItems = new List<ItemUI> { item };
				_items.Add(id, newItems);
			}
		}
		
		public Transform GetTransformParent(string id)
		{
			if (_items.TryGetValue(id, out List<ItemUI> items))
			{
				return items[0].Tr;
			}

			return null;
		}
		
		public void SetAction(string id, UnityAction func)
		{
			if (_items.TryGetValue(id, out List<ItemUI> items))
			{
				foreach (var item in items)
				{
					if (item.Btn == null) continue;
					item.Btn.onClick.AddListener(func);
				}
			}
		}
		
		public void SetInteractable(string id, bool value)
		{
			if (_items.TryGetValue(id, out List<ItemUI> items))
			{
				foreach (var item in items)
				{
					if (item.Btn == null) continue;
					item.Btn.interactable = value;
				}
			}
		}
		
		public void SetAction(string id, UnityAction<string> func)
		{
			if (_items.TryGetValue(id, out List<ItemUI> items))
			{
				foreach (var item in items)
				{
					if (item.Btn == null) continue;
					item.Btn.onClick.AddListener(() => func(item.Parm));
				}
			}
		}


		public void SetText(string id, string text)
		{
			if (_items.TryGetValue(id, out List<ItemUI> items))
			{
				foreach (var item in items)
				{
					if (item.TextTMP == null) continue;
					item.TextTMP.text = text;
				}
			}
		}
	}

	public class ItemUI
	{
		public Button Btn;
		public Transform Tr;
		public TMP_Text TextTMP;
		public string Parm;

		public ItemUI(Button btn)
		{
			Btn = btn;
		}

		public ItemUI(TMP_Text text)
		{
			TextTMP = text;
		}

		public ItemUI(Transform tr)
		{
			Tr = tr;
		}
		
		public ItemUI(Button btn, string parm)
		{
			Btn = btn;
			Parm = parm;
		}
	}
}
