
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    // 이 슬롯UI에 할당될 인벤토리 슬롯 데이터 (슬롯UI는 이 인벤토리 슬롯의 정보를 '출력'하는 장치)
    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button btn;
    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set;}

    private void Awake()
    {
        ClearSlot(); // 빈 슬롯UI로 시작

        btn = GetComponent<Button>();
        btn?.onClick.AddListener(OnUIISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void ClearSlot() // 이 슬롯UI의 슬롯정보 포함 전부 초기화
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot; // 인벤토리 슬롯 받아오기
        UpdateUISlot(slot); // 받아온 슬롯 데이터로 슬롯UI 업데이트
    }

    public void UpdateUISlot(InventorySlot slot) // 슬롯UI 업데이트. 화면상에 슬롯 정보를 출력해주기
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

    public void UpdateUISlot() // 새로운 데이터는 아니고 스택만 바뀔때 호출됨
    {
        if (assignedInventorySlot != null)
            UpdateUISlot(assignedInventorySlot);
    }

    public void OnUIISlotClick()
    {
        // 부모오브젝트에 클릭이벤트로 이 슬롯을 넘겨줌
        ParentDisplay?.SlotClicked(this);
    }
}
