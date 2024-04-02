
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    // �� ����UI�� �Ҵ�� �κ��丮 ���� ������ (����UI�� �� �κ��丮 ������ ������ '���'�ϴ� ��ġ)
    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button btn;
    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set;}

    private void Awake()
    {
        ClearSlot(); // �� ����UI�� ����

        btn = GetComponent<Button>();
        btn?.onClick.AddListener(OnUIISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void ClearSlot() // �� ����UI�� �������� ���� ���� �ʱ�ȭ
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot; // �κ��丮 ���� �޾ƿ���
        UpdateUISlot(slot); // �޾ƿ� ���� �����ͷ� ����UI ������Ʈ
    }

    public void UpdateUISlot(InventorySlot slot) // ����UI ������Ʈ. ȭ��� ���� ������ ������ֱ�
    {
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;

            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        } else
            ClearSlot();

        if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
        else itemCount.text = "";
    }

    public void UpdateUISlot() // ���ο� �����ʹ� �ƴϰ� ���ø� �ٲ� ȣ���
    {
        if (assignedInventorySlot != null)
            UpdateUISlot(assignedInventorySlot);
    }

    public void OnUIISlotClick()
    {
        // �θ������Ʈ�� Ŭ���̺�Ʈ�� �� ������ �Ѱ���
        ParentDisplay?.SlotClicked(this);
    }
}
