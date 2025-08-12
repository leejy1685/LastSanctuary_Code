using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보스의 공격 데이터
/// </summary>
[System.Serializable]
public class BossAttackInfo
{
    public float multiplier;
    public float knockbackForce;
    public float coolTime;
    public string animParameter;
    public AnimationClip attackAnim;
    public float AnimTime => attackAnim.length;
    //투사체 있는 경우
    public GameObject projectilePrefab;
    public int projectilePower;
    //사운드
    public AudioClip[] attackSounds;
}

/// <summary>
/// 보스의 초기 데이터
/// </summary>
[CreateAssetMenu(fileName = "Boss", menuName = "new Boss")]
public class BossSO : ScriptableObject
{
    [Header("Info")]
    public int _key;
    public string _name;
    
    [Header("Move")] 
    public float gravityPower;
    
    [Header("Condition")] 
    public int attack;
    public int defense;
    public int hp;
    public int groggyGauge;
    public float groggyDuration;
    public float damageDelay;
    
    [Header("DeathState")]
    public int dropGold;
    public AnimationClip deathAnim;
    public float deathEventDuration; 
    public float deathTime => deathAnim.length + deathEventDuration + 2;
    
    [Header("ChaseState")]
    public float moveSpeed = 2f;
    
    [Header("SpawnState")]
	public float spawnFallVelocity = 20f;
    public AnimationClip spawnAnim;
    public float SpawnAnimeTime {get => spawnAnim.length;}

    [Header("AttackState")] 
    public float attackRange = 2f;
    public float attackIdleTime;
    public float knockbackForce;
    public BossAttackInfo[] attacks;
    public float defpen;

    [Header("Attack2")] 
    public float backjumpDistance;
    public float backjumpHeight;
    public float backjumpSpeed;

    [Header("PhaseShiftState")] 
    public float phaseShiftHpRatio;
    public AnimationClip phaseShiftAnim;
    public float PhaseShiftTime => phaseShiftAnim.length;
    public float phaseShiftDuration;
    
    [Header("Sound")] 
    public AudioClip spawnSound;
    public AudioClip breathSound;
    public AudioClip howlingSound;
    public AudioClip jumpSound;
    public AudioClip landingSound;
    public AudioClip deathSound;
    public AudioClip phaseShiftSound;
    public AudioClip walkSound;
    public AudioClip hitSound;
    
    [Header("Materials")]
    public Material[] materials;

    [Header("TeleportState")]
    public AnimationClip teleportAnim;
    public float TeleportTime => teleportAnim.length;
        
    
}
