using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Boss01Event : BossEvent
{
    //필드
    private CinemachineBlendDefinition _originBlend;
    private Vector3 _originCameraPosition;
    private float _originCameraSize;
    private Boss _boss;

    [Header("Boss Spawn")]
    [SerializeField] private float blackDuration = 1f;
    [SerializeField] private float blinkInterval;
    [SerializeField] private GameObject[] parts;
    [SerializeField] private float targetLesSise = 7f;
    
    [Header("Boss Death")]
    [SerializeField] private float sloweventDuration = 2f;
    [SerializeField] private float cameraZoom = 5f;
    [SerializeField] private Material redSilhouette;
    [SerializeField] private float shakeDuration;
    [SerializeField] private LodeScenePortal _lodeScenerPortal;

    //초기 설정
    protected override void Start()
    {
        base.Start();
        
        _boss = FindAnyObjectByType<Boss>();
        _boss.gameObject.SetActive(false);
        
        //카메라 기본 설정
        _originBlend = _brain.m_DefaultBlend; 
        _originCameraPosition = _bossCamera.transform.position;
        _originCameraSize = _bossCamera.m_Lens.OrthographicSize;
    }
    
    //플레이어 입장 시
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(MapManager.IsBossAlive == false) return;
        if (other.CompareTag(StringNameSpace.Tags.Player))
        {
            _player = other.GetComponent<Player>();
            StartCoroutine(Spawn_Coroutine());
        }
        
    }

    //플레이어 퇴장 시
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(StringNameSpace.Tags.Player))
        {
            _player = null;
            if (_boss.gameObject.activeInHierarchy)
            { 
                _boss.gameObject.SetActive(false);
            }
            //벽 치우기
            foreach (MoveObject moveObject in _moveObjects)
            {
                moveObject.MoveObj();
            }
            
            SoundManager.Instance.PlayBGM(BGM.Tutorials_Sound);
            UIManager.Instance.SetBossUI(false);
        }
    }
    
       //스폰 이벤트
    IEnumerator Spawn_Coroutine()
    {
        
        //카메라 초기화 
        _bossCamera.transform.position = _originCameraPosition;
        _bossCamera.m_Lens.OrthographicSize = _originCameraSize;
        _brain.m_DefaultBlend = _originBlend; 
        
        //플레이어 이벤트 상태(조작 불가 + 업데이트, 물리 업데이트 막기)
        _player.EventProduction(true);

        _player.StateMachine.ChangeState(_player.StateMachine.FallState);
        //공중이라면 착지 시키기
        Vector2 gravityScale = Vector2.zero;
        gravityScale += _player.Move.Vertical(Vector2.down, _player.Data.gravityPower);
        while (!_player.Move.IsGrounded)
        {
            _player.Move.Move(gravityScale);
            yield return null;
        }
        
        //방향 계산
        Vector2 dir = _player.Move.Horizontal(playerPosition.position - _player.transform.position,_player.Data.moveSpeed);
        //거리 계산
        float distance = Mathf.Abs(_player.transform.position.x - playerPosition.position.x);
        //플레이어 걷기 상태
        _player.StateMachine.ChangeState(_player.StateMachine.MoveState);
        //실질적인 이동 처리
        while (distance > 0.1f)
        {
            distance = Mathf.Abs(_player.transform.position.x - playerPosition.position.x);
            _player.Move.Move(dir);
            yield return _player.Move.WaitFixedUpdate;
        }
        //대기 상태
        _player.StateMachine.ChangeState(_player.StateMachine.IdleState);

        //벽이 올라와서 막힘
        foreach (MoveObject moveObject in _moveObjects)
        {
            moveObject.MoveObj();
        }

        //천천히 암전
        Color originColor = _backGroundSprite.color;
        float elapsed = 0f;
        while (elapsed <= blackDuration)
        {
            elapsed += Time.deltaTime;
            _backGroundSprite.color = Color.Lerp(originColor, Color.black, elapsed / blackDuration);
            yield return null;
        }
        
        //배경음 종료
        SoundManager.Instance.StopBGM();

        //카메라 변경
        _bossCamera.Priority = 20;
        yield return new WaitForSeconds(blackDuration);

        //빨간색 순차적으로 점멸
        foreach (GameObject part in parts)
        {
            part.SetActive(true);
            yield return new WaitForSeconds(blinkInterval);
        }

        //다시 끄기
        foreach (GameObject part in parts)
        {
            part.SetActive(false);
        }
        
        //카메라 줌 아웃
        while (_bossCamera.m_Lens.OrthographicSize < targetLesSise)
        {
            _bossCamera.m_Lens.OrthographicSize += Time.deltaTime * 10;
            yield return null;
        }

        //색을 돌리기
        elapsed = 0f;
        while (elapsed <= blackDuration)
        {
            elapsed += Time.deltaTime;
            _backGroundSprite.color = Color.Lerp(Color.black, originColor, elapsed / blackDuration);
            yield return null;
        }

        //보스 스폰 상태
        _boss.transform.position = transform.position;
        _boss.gameObject.SetActive(true);
        _boss.Init(this);
    }
    
    //보스 페이즈전환 연출
    public override void OnTriggerBossPhaseShift()
    {
        StartCoroutine(PhaseShift_Coroutine());
    }
    IEnumerator PhaseShift_Coroutine()
    {
        //보스 포커싱
        _brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        _bossCamera.transform.position = new Vector3(_boss.transform.position.x, _boss.transform.position.y, _bossCamera.transform.position.z);
        _bossCamera.m_Lens.OrthographicSize = cameraZoom;
        _bossCamera.Priority = 20;
        //UI 끄기
        _player.EventProduction(true);
        //조작 불가
        _player.PlayerInput.enabled = false;
        yield return new WaitForSeconds(_boss.Data.PhaseShiftTime);
        
        //카메라 초기 설정 복구
        _brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 2f);
        StartBattle();
    }
    
    //보스 사망 연출
    public override void OnTriggerBossDeath()
    {
        StartCoroutine(Death_coroution());
    }
    IEnumerator  Death_coroution()
    {
        //보스 포커싱
        _brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        _bossCamera.transform.position = new Vector3(_boss.transform.position.x, _boss.transform.position.y, _bossCamera.transform.position.z);
        _bossCamera.m_Lens.OrthographicSize = cameraZoom;
        _bossCamera.Priority = 20;
        
        //슬로우 모션
        Time.timeScale = 0.2f;
        float timer = 0f;
        
        //UI 끄기
        _player.EventProduction(true);
        
        //순간 암전
        Color originbackGroundColor = _backGroundSprite.color;
        _backGroundSprite.color = Color.black;
        
        //빨간 실루엣
         SpriteRenderer bossSprite = _boss.SpriteRenderer;
         SpriteRenderer playerSprite = _player.SpriteRenderer;
         
         Material originBossMaterial = bossSprite.material;
         Material originplayerMaterial = playerSprite.material;
         
         bossSprite.material = redSilhouette;
         playerSprite.material = redSilhouette;
        
        //연출 유지
        yield return new WaitForSecondsRealtime(sloweventDuration);

        //색 돌리기
        Time.timeScale = 1f;
        _backGroundSprite.color = originbackGroundColor;
        bossSprite.material = originBossMaterial;
        playerSprite.material = originplayerMaterial;

        //보스 정지
        _boss.Animator.speed = 0f;
        yield return new WaitForSeconds(1f);
        
        //카메라 흔들림
        CameraShake(shakeDuration);
        yield return new WaitForSeconds(shakeDuration);
        
        //아이템 드롬
        _boss.ItemDropper.DropItems();
        
        //죽는 이벤트 시간만큼 대기
        yield return new WaitForSeconds(_boss.Data.deathEventDuration);

        //설정 복구
        _brain.m_DefaultBlend = _originBlend;
        _boss.Animator.speed = 1f;
        yield return new WaitForSeconds(3f);
        StartBattle();
        //카메라 초기 설정 복구
        _brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 2f);
        
        
        //문 열기
        foreach (MoveObject moveObject in _moveObjects)
        {
            moveObject.MoveObj();
        }
        //포탈 생성
        _lodeScenerPortal.gameObject.SetActive(true);
        //브금 복구
        SoundManager.Instance.PlayBGM(BGM.Tutorials_Sound);
    }
}
