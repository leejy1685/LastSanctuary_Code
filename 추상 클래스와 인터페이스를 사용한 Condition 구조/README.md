## 추상 클래스와 인터페이스를 사용한 Condition 구조

구조|
-|
![](https://velog.velcdn.com/images/ucc1685/post/53aeff05-0da6-4f27-8c69-f0737beffc78/image.png)|

- 모든 컨디션은 Condition 추상 클래스를 상속하고, 주체에 따라 사용되는 인터페이스를 상속 받습니다.

- ex) 보스는 넉백을 당하지 않음. 하지만 그로기 게이지가 존재해서 IGroggyable을 상속

예시 코드|
-|
![](https://velog.velcdn.com/images/ucc1685/post/bb326f15-9ded-4e7a-8121-76e41a614671/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/41851cd7-403d-4a26-9820-0ece2403ad93/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/dcdf7933-8a9d-4cae-9388-cf1b9200499d/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/06ba27d0-c3e5-41ad-af96-555997fd9605/image.png)|
![](https://velog.velcdn.com/images/ucc1685/post/9e7efd09-b11e-436d-95e7-adf4afeea31b/image.png)|

- 모든 컨디션은 Condition 추상 클래스를 상속받고, 거기에 필요한 기능에 따라 다른 Interface를 상속받는 방식으로 구현하였다.
