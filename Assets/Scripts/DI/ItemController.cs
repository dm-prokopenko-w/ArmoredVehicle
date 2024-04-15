using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ItemSystem
{
	public class ItemController
	{
		private Dictionary<string, List<Item>> _items = new ();

		public void AddItemUI(string id, Item item)
		{
			if (_items.TryGetValue(id, out List<Item> items))
			{
				items.Add(item);
			}
			else
			{
				List<Item> newItems = new List<Item> { item };
				_items.Add(id, newItems);
			}
		}
		
		public Transform GetTransformParent(string id)
		{
			if (_items.TryGetValue(id, out List<Item> items))
			{
				return items[0].Tr;
			}

			return null;
		}
		
		public void SetAction(string id, UnityAction func)
		{
			if (_items.TryGetValue(id, out List<Item> items))
			{
				foreach (var item in items)
				{
					if (item.Btn == null) continue;
					item.Btn.onClick.AddListener(func);
				}
			}
		}
		
		public void PlayAnim(string id, string idAnim)
		{
			if (_items.TryGetValue(id, out List<Item> items))
			{
				foreach (var item in items)
				{
					if (item.Anim == null) continue;

					item.Anim.Play(idAnim);
				}
			}
		}
		
		public void SetInteractable(string id, bool value)
		{
			if (_items.TryGetValue(id, out List<Item> items))
			{
				foreach (var item in items)
				{
					if (item.Btn == null) continue;
					item.Btn.interactable = value;
				}
			}
		}

		public void SetActivBtn(string id, bool value)
		{
			if (_items.TryGetValue(id, out List<Item> items))
			{
				foreach (var item in items)
				{
					if (item.Btn == null) continue;
					item.Btn.gameObject.SetActive(value);
				}
			}
		}
		public void SetAction(string id, UnityAction<string> func)
		{
			if (_items.TryGetValue(id, out List<Item> items))
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
			if (_items.TryGetValue(id, out List<Item> items))
			{
				foreach (var item in items)
				{
					if (item.TextTMP == null) continue;
					item.TextTMP.text = text;
				}
			}
		}
	}

	public class Item
	{
		public Button Btn;
		public Transform Tr;
		public TMP_Text TextTMP;
		public string Parm;
		public Animator Anim;

		public Item(Button btn)
		{
			Btn = btn;
		}

		public Item(TMP_Text text)
		{
			TextTMP = text;
		}

		public Item(Transform tr)
		{
			Tr = tr;
		}
		
		public Item(Animator anim)
		{
			Anim = anim;
		}
		
		public Item(Button btn, string parm)
		{
			Btn = btn;
			Parm = parm;
		}
	}
}
