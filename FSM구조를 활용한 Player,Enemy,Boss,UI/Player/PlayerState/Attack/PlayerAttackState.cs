using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public AttackInfo AttackInfo;
    protected float _animationTime;
    public int StaminaCost { get; protected set; }
    

    //생성자
    public PlayerAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine)
    {
        this.AttackInfo = attackInfo;
        _time = 0;
        _animationTime = attackInfo.animTime;
        StaminaCost = attackInfo.staminaCost;
    }

    public override void Enter()
    {
        base.Enter();
        _input.IsAttack = false;
        
        //공격 상태 머신 진입
        StartAnimation(_player.AnimationDB.AttackParameterHash);
        
        //무기에 대미지 전달
        _player.WeaponInfo.Attack = (int)(_condition.TotalAttack * AttackInfo.multiplier);
        _player.WeaponInfo.KnockBackForce = AttackInfo.knockbackForce;
        _player.WeaponInfo.GroggyDamage = AttackInfo.groggyDamage;
        _player.WeaponInfo.UltimateValue = AttackInfo.ultimateValue;
        
        _playerWeapon.WeaponInfo = _player.WeaponInfo;
        
        //애니메이션 실행
        _player.Animator.SetInteger(_player.AnimationDB.ComboParameterHash, AttackInfo.attackIndex);
        _time = 0;
        
        //무적 공격은 무적상태 추가
        if (AttackInfo.isInvincible)
        {
            _condition.IsInvincible = true;
            _condition.DontKnockBack = true;
        }


    }

    public override void Exit()
    {
        base.Exit();
        
        StopAnimation(_player.AnimationDB.AttackParameterHash);
        
        _player.Animator.SetInteger(_stateMachine.Player.AnimationDB.ComboParameterHash, 0);
        
        //무적 공격은 무적상태 종료
        if (AttackInfo.isInvincible)
        {
            _condition.IsInvincible = false;
            _condition.DontKnockBack = false;
        }
    }

    public override void HandleInput()
    {
        InputDash();
        //공격 중 가드 가능
        if (_move.IsGrounded &&_input.IsGuarding && _stateMachine.comboIndex != 2)
        { 
            _stateMachine.ChangeState(_stateMachine.GuardState);
        }
    }

    //스테미나 회복 막기
    public override void Update()
    {
        //공격 종료
        _time += Time.deltaTime;
        if (_time > (_animationTime + AttackInfo.nextComboTime))
        {
            if(_move.IsGrounded)
                _stateMachine.ChangeState(_stateMachine.IdleState);
            else
                _stateMachine.ChangeState(_stateMachine.FallState);
        }
    }
    

    public override void PhysicsUpdate()
    {
        
    }
    
    //대쉬어택, 3타어택 공용으로 사용
    public override void PlayEvent1()
    {
        Vector2 direction = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        _move.AddForce(direction * AttackInfo.attackForce);
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.attackSound,0.2f);
    }
}
