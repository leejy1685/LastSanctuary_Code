## 상속 구조를 이용한 Weapon

구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/081c4e8a-6c5e-43fa-9297-32bbb339adbc/image.png)|

- 무기는 플레이어, 보스, 몬스터가 공통되는 부분이 있고 다른 부분도 있어서 Weapon클래스에 공통된 부분을 정의하고 이 Weapon을 상속받아서 구현하였다.

- 구현하면서 가장 힘든게 기획서에 따라서 구현하다가 대미지를 입히는 구조가 조금 바뀔 때 마다 매서드에 매개변수를 추가하면서 작업을 하였는데,

- 그럴때 마다 수정해야 하는 코드를 너무 많이 생겨서 힘들었다.(Weapon, IDamagable, Condition 등)

- 이러한 매개변수가 많아짐에 따라 수정할 코드가 많이 생긴 문제를 WeaponInfo 구조체로 묶어서 작업하면서 개선하였다.

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/17070817-2333-4e61-b061-821ded5272a0/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/65efab3b-9867-40ec-adcd-78ed1a2325b6/image.png)|

- 충돌시 피해를 입히는 구조.

- 필요한 데이터는 WeaponInpo 구조체로 묶어서 이용하였다.
