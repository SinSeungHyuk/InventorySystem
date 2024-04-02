using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveData  // ������ �����͸� ����ִ� ��ü SaveData
{
   // public List<string> collectedItems; // ȹ���� ������ ����Ʈ

    public SerializableDictionary<string, InventorySaveData> chestDictionary; // ���� ������
    // public SerializableDictionary<string, ItemPickUpSaveData> activeItems; // ȹ���� ������ ������
    public SerializableDictionary<string, ShopSaveData> shopKeeperDictionary;

    public InventorySaveData playerInventory; // �÷��̾� �κ��丮 ������ (�ϳ��� �����ϹǷ� ��ųʸ� X)

    public SaveData()
    {
       // collectedItems = new List<string>();

        chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        shopKeeperDictionary = new SerializableDictionary<string, ShopSaveData>();
       // activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();

        playerInventory = new InventorySaveData();
    }
}
