## 어드레서블을 활용한 SoundManager

구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/7d15ba56-db92-4b8d-8c87-df25cc7bf0df/image.png)|

- SoundManager는 생성되면 ResourceLoader를 이용하여 어드레서블로 볼륨믹서, BGM, SFX프리펩 등을 가져온다.

- 그리고 ObjectPoolManager를 통해 SFX프리펩을 생성하고 그걸 실행 시키는 구조이다.

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/74818eeb-b6c5-41fd-99af-820ad4fc5189/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/251faa94-c6e0-421c-9ef4-d916cca4d083/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/84d1134a-fac2-419e-84e8-4c9d6bb9ff1d/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/d28e896a-a637-4b11-960e-5bc5d8c78e21/image.png)|

- 생성 시, 볼륨믹서, 효과음 프리펩, BGM등을 어드레서블로 불러온다.
