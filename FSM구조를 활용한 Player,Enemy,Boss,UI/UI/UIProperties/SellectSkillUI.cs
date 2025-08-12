using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellectSkillUI : MonoBehaviour,IPointerClickHandler
{
    public Action OnSelect;
    public Action OnOpen;

    [SerializeField] private Skill preSkill;
    [SerializeField] private Skill skill;
    [SerializeField] private string skillName;
    [SerializeField] private string controlKeys; 
    [SerializeField] private string skillDescription;
    [SerializeField] private string skillOpenConditions;
    [SerializeField] private int needGold;
    [SerializeField] private GameObject lockImage;
    
    public Skill PreSkill => preSkill;
    public Skill Skill => skill;
    public string SkillName => skillName;
    public string ControlKeys => controlKeys;
    public string SkillDescription => skillDescription;
    public string SkillOpenConditions => skillOpenConditions;
    public int NeedGold => needGold;
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnSelect.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnOpen.Invoke();
        }
    }
    
    public void Unlock()
    {
        lockImage.SetActive(false);
    }
}
