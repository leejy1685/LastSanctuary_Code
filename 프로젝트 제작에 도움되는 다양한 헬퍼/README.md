## 프로젝트 제작에 도움되는 다양한 헬퍼

구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/0f0fa8f2-139e-499f-a488-fafe9b2b5346/image.png)|

- DebugHelper는 제작 중에는 자유롭게 Debug를 찍고, 빌드 파일에는 성능에 영향이 있는 디버그를 사용하지 않게하는 클래스이다.

- DebugWindow는 제작한 기능을 손쉽게 테스트하기 위해 사용하는 클래스이다.

- StringNameSpace는 오류가 발생할 확률이 높은 스트링 값을 상수로 한눈에 보기 쉽게 보관하는 클래스이다.

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/bc651e0f-2428-46e6-b8a3-0326daf4a179/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/0ec0c1d4-b057-40d4-bb44-3a379d1347eb/image.png)|

- 디버그 로그는 비교적 무거운 코드인데, 이 코드를 까먹고 안 지워도 빌드할 때 안들어가게 도와주는 디버그 헬퍼

- 기능을 구현했을 때 손쉽게 테스트 할 수 있는 디버그 윈도우
