using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

          // InventoryHolder�� ��ӹ޾� �κ��丮 �ý��� �ϳ��� ������ �ִ� ����
public class PlayerInventoryHolder : InventoryHolder
{
    // �����κ��丮(�÷��̾�)�� ��ü�Ǿ� �Ҵ�� ���
    public static UnityAction OnPlayerInventoryChanged;
    // �����κ��丮(�÷��̾�) ȭ���� ����ؾ� �ϴ� ���
    public static UnityAction<InventorySystem,int> OnPlayerInventoryDisplayRequested;


    private void Start()
    {
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
    }

    protected override void LoadInventory(SaveData data)
    {   // ���̺� ������ Ȯ���ϰ� ����� ������ ������ �κ��丮 �ʱ�ȭ

        // ���̺굥������ �÷��̾� ���̺굥���� Ȯ���ϱ�
        if (data.playerInventory.invSystem != null)
        {
            this.primaryInventorySystem = data.playerInventory.invSystem;
            OnPlayerInventoryChanged?.Invoke();
        }
    }

    void Update()
    {
        // I Ű�� �κ��丮 Ȱ��ȭ
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);
        }

    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        // ù �κ��丮�� ���� �߰�
        if (primaryInventorySystem.AddToInventory(data, amount))
            return true;

        return false;
    }

    public bool GetNumpadItem(int num)
    {
        if (primaryInventorySystem.InventorySlots[num].ItemData != null)
        {
            primaryInventorySystem.InventorySlots[num].RemoveFromStack(1);
            if (primaryInventorySystem.InventorySlots[num].StackSize < 1)
                primaryInventorySystem.InventorySlots[num].ClearSlot();
            OnPlayerInventoryChanged?.Invoke();

            return true;
        }

        return false;
    }
}