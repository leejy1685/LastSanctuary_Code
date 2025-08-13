## Singleton을 활용한 다양한 매니저

구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/d1a122ce-9d00-4442-92ca-2add9bfbb79d/image.png)|

- ItemManager는 플레이어와 아이템과의 상호작용을 정의한 클래스

- LodeScenesManager(오타)은 LodeScenePortal(오타)과 상호작용한 플레이어를 다른 씬으로 이동시키는 클래스

- ObjectPoolManager는 오브젝트 생성과 파괴에 필요한 메모리를 줄이기 위해 오브젝트를 활성화, 비활성화 시키고 그 오브젝트를 저장하는 클래스

- MapManager는 아이템과 적 등 맵에 재생성이 필요한 오브젝트들을 다시 생성 및 관리하는 클래스

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/29b3145e-531b-478c-bc78-d5ca94a8ce3b/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/00e9633f-d78b-4c21-8493-a545c8b33036/image.png)|

- MapManager는 맵에 존재하는 스폰 포인트를 조작하는 역할을 한다.
