using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))] // ���θ��� ���� ID ����
public class ShopKeeper : MonoBehaviour, IInteractable // ��ȣ�ۿ� ���
{
    [SerializeField] private ShopItemList _shopItemsHeld; // ������ ���� ������
    [SerializeField] private ShopSystem _shopSystem; // ������ ���� �����ý���(�κ��丮 ����)

    private string id;
    private ShopSaveData shopSaveData;

    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowRequested;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }


    private void Awake()
    {   // �� ������ �����ý��� ����
        _shopSystem = new ShopSystem(
            _shopItemsHeld.Items.Count,
            _shopItemsHeld.MaxAllowedGold,
            _shopItemsHeld.BuyMarkUp,
            _shopItemsHeld.SellMarkUp
            );

        foreach (var item in _shopItemsHeld.Items)
        {   // ������ ���� �����۸�� �޾ƿ� ���� �κ��丮�� �߰�
            _shopSystem.AddToShop(item.ItemData,item.Amount);
            Debug.Log(item.ItemData.DisplayName + " : " + item.Amount);
        }

        id = GetComponent<UniqueID>().ID;
        shopSaveData = new ShopSaveData(_shopSystem);

    }

    private void Start()
    {
        if (!SaveGameManager.data.shopKeeperDictionary.ContainsKey(id))
            SaveGameManager.data.shopKeeperDictionary.Add(id, shopSaveData);
    }

    private void OnEnable()
    {
        SaveLoad.OnLoadGame += LoadInventory;
    }
    private void OnDisable()
    {
        SaveLoad.OnLoadGame -= LoadInventory;
    }

    private void LoadInventory(SaveData data)
    {
        if (!data.shopKeeperDictionary.TryGetValue(id, out ShopSaveData saveData)) return;

        shopSaveData = saveData;
        _shopSystem = saveData.shopSystem;
    }


    public void Interact(Interactor interator, out bool interactSuccessful)
    {
        var playerInv = interator.GetComponent<PlayerInventoryHolder>();

        if (playerInv != null)
        {
            OnShopWindowRequested?.Invoke(_shopSystem, playerInv);
            interactSuccessful = true;
        }
        else
        {
            interactSuccessful = false;
            Debug.Log("player inventory null");
        }
    }
    public void EndInteraction()
    {
    }

}

[System.Serializable]
public struct ShopSaveData
{
    public ShopSystem shopSystem;

    public ShopSaveData(ShopSystem system)
    {
        shopSystem = system;
    }
}