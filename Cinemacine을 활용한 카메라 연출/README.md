## 시네머신을 활용한 카메라 연출

구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/c88b0a32-27f2-4296-8015-e7121b570301/image.png)|

- PlayerCamera에는 카메라를 이용한 연출 기능을 담당한다.

- 카메라 줌 인, 줌 아웃, 카메라 흔들림 등의 기능이 구현되어 있다.

- 또한 카메라의 자연스러운 이동, 빠른 이동을 선택하여 기능을 사용할 수 있다.

- BossEvent에는 보스의 연출을 보여주기 위한 시네머신이 따로 존재하며, 필요하면 보스방 전체를 비추는 방식으로 구현한다.(Boss02는 방 전체를 보여줌)

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/bc171ff4-b7ad-44f3-8a50-9a97ac67f070/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/96c5b994-1605-408b-b46f-4d4118e332dd/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/4627ae95-efd4-45a8-8f29-f8ce476ba658/image.png)|

- 카메라 흔들림, 카메라 줌 인, 줌 아웃
