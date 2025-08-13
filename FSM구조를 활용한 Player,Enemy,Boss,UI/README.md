
## FSM구조를 활용한 Player, Enemy, Boss, UI

### 구현한 상태머신의 특징
- 사용한 FSM 구조는 인터페이스 방식이고, 각각의 상태마다 CS 파일을 만들어서 구현하였다.

- 각 상태는 Enter() 메서드를 통해 각 상태의 처음 부분을 정의하고

- Update()에서는 현 상태에서 사용되거나 주변 상황에 따라 상태를 변화시킨다.

- HendleInput()에서는 입력 값에 따라 상태를 변화 시킨다.

- PhysicsUpdate()에서는 물리적인 처리를 담당한다.

### Player상태머신의 특징

Player구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/436e7996-bdb2-47bf-a746-f284a9cf8389/image.png)|

- 플레이어는 PlayerController 스크립트에서 조작을 감지하고 HendleInput에서 바뀐 Bool 변수를 통해 상태를 변환 시킨다.

- 플레이어의 상태는 크게 기초, 바닥, 공중, 공격 상태로 나뉘며

- 바닥 상태를 상속 받은 상태들은 바닥에서만 사용 가능한 상태

- 공중 상태를 상속 받은 상태들은 공중에서만 사용 가능한 상태

- 공격 상태를 상속 받은 상태들은 공격의 파생 상태들이다.

- 예외로 기초 상태들은 어떠한 위치에 있는 사용 가능한 상태들이다.

예시 코드|
-|
플레이어 공중 상태|
-|
![](https://velog.velcdn.com/images/ucc1685/post/785f8e8d-faec-4ac5-b329-70d6a6907589/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/b466abaa-3928-435f-b298-b01e460f3fcb/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/b1e7cae4-32a2-4bcb-91a7-a9182658b341/image.png)|

- 여기 이렇게 정의된 것은 플레이어 공중 상태에 포함되는 점프, 추락에 동시에 사용되는 것들이다.

- Idle과 Move가 상속하는 Ground상태도 같은 구조로 되어 있다.




### Enemy상태머신의 특징

Enemy구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/5b0e66f6-b989-485c-8c56-464b17b44f5e/image.png)|

- 적은 다양한 종류가 있으므로 하나의 상태머신을 다 같이 돌려 사용하게 만들기 위해, 부모 상태를 선언하고 생성은 자식 상태를 하는 방식으로 구현하였다.

- 생성될 자식 상태는 Enum타입을 만들어 직렬화 하였고 각 몬스터 프리펩에 설정해 두었다.

- ex) 적이 플레이어를 발견하기 전까지 가만히(Idle)있는 적이 있고, 순찰(Patrol)하는 적이 있다.

-> EIdleState를 상속하는 EnemyIdleState와 EnemyPatrolState.
-> StateMachine에선 `EIdleState IdleState = new EnemyPatrolState(this);` 로 상태를 선택함.

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/50037eda-7acd-4606-a3d1-0ee7bd7e4762/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/14716d56-4a27-464a-b3ad-8bd890332504/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/ae51166a-aab9-4135-86d0-55e176fbe765/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/0535a1cc-3b88-4c98-b2f7-b99b894f94fe/image.png)|

- 선택된 Enum 값에 따라서 선언되는 자식 상태가 달라지게 끔하여 여러 적을 구현하였다.


### Boss상태머신의 특징

Boss구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/dd09eeff-bc1b-4cc0-9f71-ba09c506b5d3/image.png)|

- 완전 상속 방식을 만드려고 했으나 시간부족으로 인해 실패한 구조

- 이상적인 구조는 Boss를 상속 받는 Boss01과 Boss02와 BossStateMachine을 상속 받는 Boss01StateMachine, Boss02StateMachine을 만들어야 하나, 이렇게 진행하기 위해선 BossIdle을 상속 받는 Boss01Idle, Boss02Idle 이런식으로 모든 상태를 구현해야함.

- 그러기엔 시간이 부족하였기 때문에 결국 하나의 BossBaseState에 Boss01에서 사용하는 객체와 Boss02에서 사용하는 객체가 따로 선언되었음.

예시 코드|
-|

Boss01|
-|
![](https://velog.velcdn.com/images/ucc1685/post/ed7f5479-3862-4543-b5ad-59a332b2ea4e/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/3caeedee-69f8-4a9f-815f-2c864b3cb566/image.png)|

- 첫 번째 보스의 공격 방식은 쿨타임이 다 찬 공격이 Queue에 들어가고 가장 먼저 들어온 공격을 실행하는 구조이다.

Boss02|
-|
![](https://velog.velcdn.com/images/ucc1685/post/dd100257-a312-4897-a6b4-817393967511/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/bd263d42-734f-48db-ba87-417dcede1371/image.png)|

- 두 번째 보스의 공격 방식은 거울 위치로 순간이동 -> 이동한 위치에 따라 공격 패턴을 판단 -> 공격이기 때문데 다르게 구현 하였다.

### UI 상태머신의 특징

UI 구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/4b1817e5-1c35-4331-99fc-dcc10bab70d5/image.png)|

- UI는 다른 상태 머신과의 차이점이라면 MonoBehavior가 상속되어 있다는 것이다.

- UI 상태머신을 구현하던 중 UIManager에 필요한 직렬화 데이터가 많아졌고, 

- 그 데이터를 분산 시키기 위해서 MonoBehavior를 상속하였다.

- MonoBehavior를 상속할 수 있는 이유는 각각의 형태가 존재하는 상태들이기 때문이다.

- 또 SettingUI는 TitleScene에서 사용 가능하므로 상태머신이 없어도 기능을 사용할 수 있게 하였다.

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/bc5e29cd-fc4d-4ee2-90b6-44e304e38e65/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/ed2c5676-13f3-406e-bb07-f711a359fb8c/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/8556ecbd-2186-49fb-b555-11f73e3c3311/image.png)|

- UI는 직렬화 데이터가 많이 필요하고 실체가 있는 형태이기 때문에 MonoBehavior를 상속하였다.
