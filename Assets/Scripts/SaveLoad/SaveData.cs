using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveData  // 저장할 데이터를 담고있는 객체 SaveData
{
   // public List<string> collectedItems; // 획득한 아이템 리스트

    public SerializableDictionary<string, InventorySaveData> chestDictionary; // 상자 데이터
    // public SerializableDictionary<string, ItemPickUpSaveData> activeItems; // 획득한 아이템 데이터
    public SerializableDictionary<string, ShopSaveData> shopKeeperDictionary;

    public InventorySaveData playerInventory; // 플레이어 인벤토리 데이터 (하나만 존재하므로 딕셔너리 X)

    public SaveData()
    {
       // collectedItems = new List<string>();

        chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        shopKeeperDictionary = new SerializableDictionary<string, ShopSaveData>();
       // activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();

        playerInventory = new InventorySaveData();
    }
}
