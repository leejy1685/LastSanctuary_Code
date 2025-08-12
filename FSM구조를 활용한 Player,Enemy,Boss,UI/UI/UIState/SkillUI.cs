using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillUI : UnifiedUI
{
    [Header("SkillUI")]
    [SerializeField] private TextMeshProUGUI skillUIGoldText;
    
    private SellectSkillUI[] _sellectSkills;
    private SkillDescUI _skillDescUI;

    
    public SkillUI(UIStateMachine uiStateMachine) : base(uiStateMachine) { }
    
    public override void Init(UIStateMachine uiStateMachine)
    {
        base.Init(uiStateMachine);
        
        _sellectSkills = _uiManager.GetComponentsInChildren<SellectSkillUI>(true);
        _skillDescUI = _uiManager.GetComponentInChildren<SkillDescUI>(true);

        for (int i = 0; i < _sellectSkills.Length; i++)
        {
            int index = i;
            _sellectSkills[i].OnSelect += () => OnSelect(_sellectSkills[index]);
            _sellectSkills[i].OnOpen += () => OnOpen(_sellectSkills[index]);
        }
    }

    public override void Enter()
    {
        base.Enter();

        centerLinePos.localPosition = new Vector3(_data.centerLinePosition, 0, 0);
        _mouseLeftDesc.text = _data.skillLeftClickDesc;
        _mouseRightDesc.text = _data.sKillRightClickDesc;
        mouseLeft.SetActive(true);
        mouseRight.SetActive(true);

        UpdateGoldText();
        
        gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        
        gameObject.SetActive(false);
    }
    
    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.E))
        {   //셋팅 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.SettingUI);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {   //성물 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.RelicUI);
        }
    }

    //좌클릭 시 실행
    private void OnSelect(SellectSkillUI sellect)
    {
        _skillDescUI.SetSkillDesc(sellect);
    }
    
    //우클릭 시 실행
    private void OnOpen(SellectSkillUI sellect)
    {
        //이미 오픈 되어있거나 오픈 할 스킬이 아니면 열리지 않음.
        if (_playerSkill.GetSkill(sellect.Skill).open ||
            sellect.Skill == Skill.None)
        {
            DebugHelper.Log("이미 오픈된 스킬입니다.");
            return;
        }

        //선행 스킬이 오픈되지 않았다면
        if (sellect.PreSkill != Skill.None && 
            !_playerSkill.GetSkill(sellect.PreSkill).open)
        {
            DebugHelper.Log("선행 스킬이 오픈되지 않았습니다.");
            return;
        }
        
        //충분한 골드가 있다면 오픈
        if (_playerInventory.UseGold(sellect.NeedGold))
        {
            _playerSkill.GetSkill(sellect.Skill).open = true;
            _playerSkill.SetSkillData(sellect.Skill);
            sellect.Unlock();
            UpdateGoldText();
        }
        //골드 부족 알림창이 있으면 좋을 듯?
    }
    
    private void UpdateGoldText()
    {
        skillUIGoldText.text = _playerInventory.Gold.ToString();
    }
}
