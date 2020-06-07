using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateEquipButton : MonoBehaviour
{
	public Button btn;

	public UnitInventoryTab lumberJackInvTab;


	void OnEnable()
	{
		btn = transform.Find("Button").GetComponent<Button>();
		btn.onClick.AddListener(UpdateEquipment);
		lumberJackInvTab = transform.parent.parent.Find("UI_UnitInventory(Clone)").GetComponent<UnitInventoryTab>();

	}

	void UpdateEquipment()
	{
		Debug.Log("Update Equip.");
		
		if (lumberJackInvTab != null)
		{
			if (lumberJackInvTab.inventory.GetWeaponItem())
			{
				InventoryManager.UnequipItem(lumberJackInvTab.inventory.GetWeaponItem(), lumberJackInvTab.inventory.gameObject);
			}
			if (lumberJackInvTab.GetWeapon())
			{
				if (!lumberJackInvTab.inventory.equippedItems.Contains(lumberJackInvTab.GetWeapon()))
				{
					InventoryManager.EquipItem(lumberJackInvTab.GetWeapon(), lumberJackInvTab.inventory.gameObject);
				}

			}

			if (lumberJackInvTab.inventory.GetcuriosityItem1())
			{
				InventoryManager.UnequipItem(lumberJackInvTab.inventory.GetcuriosityItem1(), lumberJackInvTab.inventory.gameObject, 1);
			}
			if (lumberJackInvTab.GetCurio1())
			{
				if (!lumberJackInvTab.inventory.equippedItems.Contains(lumberJackInvTab.GetCurio1()))
				{
					InventoryManager.EquipItem(lumberJackInvTab.GetCurio1(), lumberJackInvTab.inventory.gameObject, 1);
				}
			}

			if (lumberJackInvTab.inventory.GetcuriosityItem2())
			{
				InventoryManager.UnequipItem(lumberJackInvTab.inventory.GetcuriosityItem2(), lumberJackInvTab.inventory.gameObject, 2);
			}
			if (lumberJackInvTab.GetCurio2())
			{
				if (!lumberJackInvTab.inventory.equippedItems.Contains(lumberJackInvTab.GetCurio2()))
				{
					InventoryManager.EquipItem(lumberJackInvTab.GetCurio2(), lumberJackInvTab.inventory.gameObject, 2);
				}
			}
		}
		
	}




	private void OnDestroy()
	{
		btn.onClick.RemoveAllListeners();
	}
}
