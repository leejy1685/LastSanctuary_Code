## Kinematic을 이용한 지형 충돌과 이동 기능

구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/c5a21f7e-19c1-4d69-8844-f81c1fc6fa1b/image.png)|

- 키네마틱으로 바꾸게 되면서 추가하게 된 기능이다.

- 키네마틱 충돌을 코드로 정의 해야하는 특징을 가지고 있는데, 그 기능을 공유하기 위해서 KinematicMove 스크립트를 만들었다.

- 그리고 해당 캐릭터들의 특징에 맞춰서 상속하고 기능을 확장 및 재정의 하였다.
ex) 플레이어는 위로 올라갈 때, 공중 발판을 통과해야함.
ex) 몬스터는 공중 발판에서 떨어지지 말아야 함.

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/5f54dcc2-94cd-43f8-a2d0-de0a5e0fbe9a/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/fae0e68e-f7a4-45e7-b288-c2ec8973e48b/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/1a438253-864e-4b74-a557-ff34d582406b/image.png)|

- 벽이나 바닥에 충돌이 발생하면 bool 변수를 활성화 하고 그 방향으로 이동하려 할 때 이동을 막는 코드를 구현.
