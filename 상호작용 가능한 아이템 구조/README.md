## 상호작용 가능한 아이템 구조

구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/cd4249ba-cfed-4368-9c08-7ab3717d0737/image.png)|

- 아이템은 상호작용 가능한 인터페이스를 만들어서 구분하였다.

- 이렇게 아이템에 상호작용하면, ItemManager는 PlayerInventory, PlayerCondition에 직접 적용을 해준다.

- 사실 접촉된 플레이어에게 바로 적용해도 되지만, 나중에 세이브 데이터를 적용시키기 위해서 이렇게 구조 작성하였다.

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/aeecc471-db4d-48f5-b01d-be681d26e4b0/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/d6dfa9dc-a517-470e-90dc-8e9c4a2865a6/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/19bc6cdd-5cf8-4421-a832-5c49487fcbe7/image.png)|

- 상호작용 가능한 인터페이스를 상속받아서 플레이어가 상호작용을 하면 매니저들을 통해서 이벤트가 발생한다.
