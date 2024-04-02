using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]

                             // 인벤토리 홀더, 상호작용 인터페이스
public class ChestInventory : InventoryHolder, IInteractable
{
    // 인벤토리 홀더를 상속받았으므로 고유 인벤토리 시스템 보유
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }


    private void Start()
    {
        // 현재 상자의 정보 저장하기
        var chestSaveData = new InventorySaveData(primaryInventorySystem, transform.position, transform.rotation);
        SaveGameManager.data.chestDictionary.Add(GetComponent<UniqueID>().ID, chestSaveData);
    }

    protected override void LoadInventory(SaveData data)
    {   // 세이브 데이터 확인하고 저장된 데이터 가져와 인벤토리 초기화

        // 세이브데이터의 상자딕셔너리의 ID 값 가져와 TryGetValue로 확인하기
        if (data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
        {
            this.primaryInventorySystem = chestData.invSystem;
            this.transform.position = chestData.position;
            this.transform.rotation = chestData.rotation;
        }
    }

    // 이 인벤토리의 시스템을 바탕으로 동적인벤토리 출력 함수 호출
    public void Interact(Interactor interator, out bool interactSuccessful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem,0);
        interactSuccessful = true;
    }

    public void EndInteraction()
    {

    }


}

