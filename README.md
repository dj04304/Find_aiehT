# 🐷 FindAieht 🐷
### 👨‍👧8조 김종욱, 박준형, 문원정, 김국민

<br><br>


## 🥥 프로젝트 결과물 소개 🥥

![image](https://github.com/levell1/levell1.github.io/assets/96651722/85f0aaa8-0593-4518-abd2-4d4f60c42ac3)  

[**FindAieht 플레이 영상**](https://www.youtube.com/watch?v=OdmmBRaD1U4)

<br><br>

## 📜 프로젝트 개요 및 목표 📜​
> - **프로젝트 명 : Find Aieht**  
> - **장르 : 액션 RPG, 경영 시뮬레이션 타이쿤**  
> - **2D/3D : 3D**  
> - **스토리 라인**  
마을 식당을 운영하던 식당 주인이 키우던 동물   
Aieht이 어느날 알 수 없는 이유로 인해 사라져 버렸고,   
동시에 다른 가축들도 포악하게 변해버렸다.   
이로인해 가게 운영이 힘들어졌고,   
소중한 친구 Aieht을 찾기위해 마을 밖으로 나가게 되는데…  

<br><br>

## 📖 사용된 기술 스택 📖
![image](https://github.com/levell1/levell1.github.io/assets/96651722/cfd213ae-a907-4f4b-8195-3b0bf0a632af)


<br><br>

## 🧩 클라이언트 구조 🧩
![image](https://github.com/levell1/levell1.github.io/assets/96651722/c977863e-db5e-4a42-8f73-42f959770181)

<br><br>

![image](https://github.com/levell1/levell1.github.io/assets/96651722/ac873286-7608-4093-9ffe-d73bb74b089d)

## 💥 주요 기능 구현

- 타이틀 화면
- 튜토리얼
- 플레이어
- 인벤토리 및 상점
- 강화 및 플레이어 정보
- 사냥터
- 타이쿤
- 던전
- 퀘스트
- 저장 및 로드

## 🎮 내가 했던 기능 구현

### 1. 플레이어 구현

#### 구현 및 이유

- FSM 디자인 패턴을 이용하여 플레이어를 구현함
- FSM 디자인 패턴을 채택한 이유는 플레이어가 상태마다 다양한 동작이 있으며, 상태 사이에도 여러 동작을 관리해야 하기 때문에 FSM 디자인 패턴을 사용했습니다.


#### 구현 방법

- 인터페이스와 추상클래스로 플레이어 상태 관리
```C#
public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}

```

``` C#
public abstract class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState state)
    {
        currentState?.Exit();

        currentState = state;

        currentState?.Enter();
    }   
}
```
</br>

![Player](./Assets/Images/PlayerGIF.gif)

- sub-statemachine을 이용하여 플레이어 애니메이션 관리함
    - 스킬 및 점프시에 애니메이션이 캔슬되지 않게끔 sub-statemachine으로 나눔  
     

</br>

![Animator](./Assets/Images/20240122_232844.png)


- 구글 스프레드 시트를 이용한 플레이어 레벨시 증가하는 데이터 관리  
    - 플레이어의 레벨에 따라 스탯이 달라지는데, 이를 좀 더 유연하게 관리하기 위해 구글 스프레드시트를 사용

</br>

![GoogleSpreadSheet](./Assets/Images/20240326_135405.png)


### 2. 포션 인벤토리

#### 구현 및 이유

- 인벤토리는 채집한 아이템을 담는 인벤토리와 포션만 담겨있는 인벤토리로 나눔
- 처음에는 포션과 인벤토리가 공존하는 인벤토리를 구현하였으나, 포션은 따로 관리하여 장착하는 형식은 어떻냐고 기획부분에서 의견이 나와서 포션은 장착하는 형식, 그리고 기존 인벤토리는 채집물을 담는 형식으로 구현했음
- 상점 구현을 맡았는데, 상점에서 포션을 사는 것과 연결 지을 수 있기 때문에 포션 인벤토리를 구현하게 됨

#### 구현 방법

- 포션 인벤토리는 HP포션 3개, SP포션 3개 총 6개로 고정이기 때문에 비용이 비교적 싼 배열로 생성
- 포션 인벤토리 베이스를 생성하여 이를 상속받아 HP포션과 SP포션을 구현함
- Onclick 이벤트로 포션을 클릭했을 때 해당 포션이 퀵슬롯에 장착되게끔 구현함

</br>

![Potion](./Assets/Images/PotionGIF.gif)


### 3. 상점 구현

#### 구현 및 이유

- 기존 기획은 채집한 물건을 파는 것이 아닌 오로지 타이쿤으로만 골드를 벌 수 있고, 번 골드로 포션을 사는 식의 상점을 구현했음
- 그러나 타이쿤보다 사냥 위주로 하는 사람도 있다고 생각이 들었기도 하고, 타이쿤을 어려워할 수 도 있다고 생각하여 인벤토리의 아이템을 팔 수 있게끔 구현하기로 결정함
- 그리고 상점에서는 포션만 팔게끔 구현함

#### 구현 방법

- 상점에서 구매할 시에 구매할 포션은 마찬가지로 총 6개이기 때문에 리스트가 아닌 배열로 구현
- 슬라이드를 이용하여 포션의 수량을 늘릴 수 있고, 뿐만 아니라 양 옆의 화살표로 +- 를 할 수 있게끔 구현함
- 포션을 구매하면 바로 수량이 늘어나는 식으로 구현하였으며, 만약 골드가 부족할 경우 UI를 붉은색으로 변하게 하여 구매할 수 없게끔 구현하였음

```C#
 private void OnSliderValueChanged(float newValue)
 {
     _itemCurQuantity = Mathf.RoundToInt(newValue); 
     int totalItemGold = _itemCurQuantity * _itemCurGold;

     _itemQuantity.text = _itemCurQuantity.ToString();
     _itemPrice.text = totalItemGold.ToString();

     if (totalItemGold > _playerData.PlayerData.PlayerGold)
     {
         _itemPrice.color = Color.red;
     }
     else
     {
         _itemPrice.color = Color.black;
     }
 }

```

</br>

![Shop](./Assets/Images/Shop.gif)


### 4. 퀘스트

#### 구현 및 이유

#### 구현 방법

## ⚠ 트러블 슈팅
