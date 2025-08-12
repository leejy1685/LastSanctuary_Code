using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RelicUI : UnifiedUI
{
    [Header("RelicUI")]
    [SerializeField] private TextMeshProUGUI relicName;
    [SerializeField] private TextMeshProUGUI relicEffectText;
    [SerializeField] private TextMeshProUGUI relicDescText;
    [SerializeField] private RectTransform statUIPivot;
    [SerializeField] private RectTransform equipUIPivot;
    [SerializeField] private RectTransform slotUIPivot;
    
    //필드
    private List<StatUI> _statUIs;
    private List<EquipUI> _equipUIs;
    private List<SlotUI> _slotUIs;

    public RelicUI(UIStateMachine uiStateMachine) : base(uiStateMachine) { }

    public override void Init(UIStateMachine uiStateMachine)
    { 
        base.Init(uiStateMachine);
        
        _statUIs = new List<StatUI>();
        _equipUIs = new List<EquipUI>();
        _slotUIs = new List<SlotUI>();

        //스탯 슬롯 생성
        for (int i = 0; i < _data.statNames.Length; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.statUIPrefab, statUIPivot);

            _statUIs.Add(go.GetComponent<StatUI>());
            _statUIs[i].statName.text = _data.statNames[i];
        }

        //장비 슬롯 생성
        for (int i = 0; i < _data.equipNum; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.equipUIPrefab,equipUIPivot);

            _equipUIs.Add(go.GetComponent<EquipUI>());

            int index = i;
            _equipUIs[i].OnSelect += () => OnSelect(_equipUIs[index].Data);
            _equipUIs[i].OnEquip += () => OnEquip(_equipUIs[index].Data);
            //장비칸 잠그기
            if (i < _data.nonLockEquip) _equipUIs[i].SetLock(false);
            else _equipUIs[i].SetLock(true);

        }

        //성물 슬롯 생성
        for (int i = 0; i < _playerInventory.relics.Count; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.slotUIPrefab, slotUIPivot);

            _slotUIs.Add(go.GetComponent<SlotUI>());
        }
        
    }

    public override void Enter()
    {
        base.Enter();

        _mouseLeftDesc.text = _data.relicLeftClickDesc;
        _mouseRightDesc.text = _data.relicRightClickDesc;
        mouseLeft.SetActive(true);
        mouseRight.SetActive(true);
        
        gameObject.SetActive(true);
        
        UpdateStatus();
        UpdateSlot();
    }

    public override void Exit()
    {
        base.Exit();

        DisUpdateSlot();
        
        gameObject.SetActive(false);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.E))
        {   //스킬 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.SkillUI);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {   //설정 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.SettingUI);
        }
        
    }

    //스테이터스 갱신
    private void UpdateStatus()
    {
        //기초 스탯
        _statUIs[0].basic.text = _playerCondition.Attack.ToString();
        _statUIs[1].basic.text = _playerCondition.Defence.ToString();
        _statUIs[2].basic.text = _playerCondition.MaxHp.ToString();
        _statUIs[3].basic.text = _playerCondition.MaxStamina.ToString();

        //성물 스탯
        _statUIs[0].relic.text = "+" + _playerInventory.EquipAtk;
        _statUIs[1].relic.text = "+" + _playerInventory.EquipDef;
        _statUIs[2].relic.text = "+" + _playerInventory.EquipHp;
        _statUIs[3].relic.text = "+" + _playerInventory.EquipStamina;

        //버프 스탯
        _statUIs[0].buff.text = "+" + _playerCondition.BuffAtk;
        _statUIs[1].buff.text = "+" + _playerCondition.BuffDef;
        _statUIs[2].buff.text = "+" + _playerCondition.BuffHp;
        _statUIs[3].buff.text = "+" + _playerCondition.BuffStamina;
    }

    //좌클릭 시 실행
    private void OnSelect(CollectObjectSO data)
    {
        relicName.text = data.relicName;
        relicEffectText.text = data.effectDesc;
        relicDescText.text = data.relicDesc;
    }


    //우클릭 시 실행
    private void OnEquip(CollectObjectSO data)
    {
        foreach (EquipUI equipUI in _equipUIs)
        {
            if (equipUI.Data == data)
            {
                _playerInventory.UnEquipRelic(data);
                equipUI.Data = null;
                equipUI.SetActive();
                UpdateStatus();
                return;
            }
        }
        
        foreach (EquipUI equipUI in _equipUIs)
        {
            if(equipUI.IsLock)
            {
                break;
            }
            else if (equipUI.Data == null)
            {
                SoundManager.Instance.PlaySFX(_data.equipSound);
                _playerInventory.EquipRelic(data);
                equipUI.Data = data;
                equipUI.SetActive();
                UpdateStatus();
                break;
            }
        }
        
    }

    //성물 슬롯에 이벤트 추가
    private void UpdateSlot()
    {
        for (int i = 0; i < _playerInventory.relics.Count; i++)
        {
            
            if (_playerInventory.relics[i].IsGet)
            {
                _slotUIs[i].data = _playerInventory.relics[i];
                _slotUIs[i].SetActive();

                int index = i;
                _slotUIs[i].OnSelect += () => OnSelect(_slotUIs[index].data.Data);
                _slotUIs[i].OnEquip  += () => OnEquip(_slotUIs[index].data.Data);
            }
        }
    }
    
    //성물 슬롯에 이벤트 제거
    private void DisUpdateSlot()
    {
        for (int i = 0; i < _playerInventory.relics.Count; i++)
        {
            _slotUIs[i].OnSelect = null;
            _slotUIs[i].OnEquip = null;
        }
    }
}
