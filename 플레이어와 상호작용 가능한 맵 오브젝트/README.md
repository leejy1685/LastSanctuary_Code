## 플레이어와 상호작용 가능한 맵 오브젝트

구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/fb6fd3f3-55e2-4c91-8cb0-60095d9ab99b/image.png)|

- 맵 오브젝트는 공격해서 작동 가능한 것과 상호작용해서 작동 가능한 것으로 나뉜다.

- SavePoint는 상호작용 가능한 맵 오브젝트로 상호작용하면, 적, 아이템 리스폰, 플레이어 스폰위치 저장, 체력 및 소모 아이템 회복 등의 기능을 한다.

- Lever는 공격으로 인해 작동하는 오브젝트로 다른 오브젝트를 활성화시키는 기능을 한다.

- 여기서 퍼즐 구현을 위해서 PuzzleManager를 만들고 필요한 오브젝트를 UnityEvent로 연결해서 성공과 실패 이벤트를 구현하였다.

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/c720406e-8dc7-475b-84c3-40033d800128/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/27a4fb8a-b984-4973-b54a-ff3760d1257a/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/259e007f-f72e-4f83-ac52-fb4d80b975ef/image.png)|

- Lever는 IDamagable를 상속 받아 피해를 받을 때 작동을 구조이다.

- RopeObject는 레버가 활성화하면 밧줄이 생성된다.
