## 🎮 게임 소개
Sparta Metaverse Project는 마을을 중심으로 시작되는 탑다운 슈팅 기반의 메타버스 게임입니다.
플레이어는 마을에서 자유롭게 이동하며 엘프 NPC와의 상호작용을 통해 게임의 흐름을 선택하게 됩니다.

- 마을에 위치한 엘프 NPC에게 다가가 F 키를 눌러 상호작용하면 TopDown Easy 모드가 시작됩니다.

- 반대로 엘프 NPC를 공격해 죽이면, 곧바로 TopDown Hard 모드가 시작됩니다.

- 이 외에도 마을에는 다른 공간으로 연결되는 상호작용 요소가 존재하며, 특정 위치에서는 The Stack 미니게임 씬으로 진입할 수 있습니다.

게임은 하나의 메타버스 공간에서 다양한 게임 경험(탑다운 슈팅, 미니게임, NPC 대화 등)을 플레이어의 선택에 따라 자연스럽게 분기되도록 설계되어 있습니다.


## 🕹️ 플레이 방법

- 게임은 마을에서 시작되며, 플레이어는 키보드와 마우스를 사용해 캐릭터를 자유롭게 조작할 수 있습니다.

- W, A, S, D 키를 사용해 캐릭터를 이동합니다.

- 마우스를 움직이면 캐릭터의 조준 방향이 따라갑니다.

- 마우스 왼쪽 클릭으로 총을 발사할 수 있습니다.

- 마을에 있는 엘프 NPC 앞에서 F 키를 누르면, 대화가 발생하며 TopDown Easy 모드로 진입하게 됩니다.

- 반대로, 엘프 NPC를 공격해 죽이면 곧바로 TopDown Hard 모드가 시작됩니다.

- 특정 위치에 접근하면 자동으로 The Stack 미니게임 씬으로 이동하게 됩니다.

- 게임의 흐름은 플레이어의 행동(상호작용 또는 공격)에 따라 자연스럽게 분기되며, 각 씬에서는 별도의 전투 및 점수 시스템이 작동합니다.

> 🔄 게임 오버 시, 버튼을 눌러 재시작 가능


## 🛠 사용 기술

이 프로젝트는 Unity 2D 환경을 기반으로 개발되었으며,
다양한 시스템 구현을 위해 다음과 같은 기술과 구조를 사용했습니다.

- Unity 2022.3.17f1
씬 구성, 애니메이션, 충돌 처리 등 Unity 엔진의 핵심 기능을 기반으로 게임 제작

- C#
각 시스템을 명확히 분리하여 객체지향적으로 관리
MonoBehaviour를 기반으로 스크립트를 구성하고 역할별 책임을 나눔

- Unity Input System
기존의 Input.GetAxis 방식이 아닌, Unity Input System 패키지를 활용해
이동, 점프, 공격 등 플레이어 입력을 모듈화하여 처리

- TextMeshPro
NPC 말풍선과 UI 텍스트 출력에 사용
랜덤 대사와 상태 기반 반응을 동적으로 표시함

- Animator + AnimationHandler
캐릭터의 이동, 점프, 피격 등의 상태를 Animator 파라미터로 제어
AnimationHandler에서 해시 값으로 전환하여 깔끔하게 상태 처리

- 싱글톤 패턴
GameManager, SoundManager, UIManager 등은 싱글톤으로 구성되어
전역에서 접근 가능하며, 씬 전환 시에도 객체를 유지함

- SceneManager
NPC와의 상호작용 혹은 사망 조건에 따라 Easy/Hard/Stack 씬으로 이동 처리
SceneManager.LoadScene()을 통해 자연스럽게 분기 구현

- UI 상태 관리
BaseUI를 상속한 구조로 Home/Game/Score/GameOver UI 구현
UI 상태 전환은 UIManager를 통해 관리됨

- 2D 충돌 및 피격 처리
Rigidbody2D, Collider2D, OnTriggerEnter2D를 활용해
총알 간 상쇄, 적 피격, 사망 등의 충돌 기반 로직을 구현

- 리더보드 시스템
Stack 게임 내에서 점수를 기록하고, 최고 점수를 리더보드에 표시
점수 비교 및 UI 갱신 로직을 통해 플레이어 도전 욕구를 자극


## ✨ 주요 구현 기능

- ✅ 플레이어 이동 및 점프 기능 (PlayerController)
- ✅ 마우스를 이용한 방향 조준 및 총 발사 (MeleeWeaponHandler, BulletSpawner)
- ✅ 적 AI 이동 및 공격 로직 구현 (EnemyController)
- ✅ 플레이어와 몬스터 간 총알 충돌 시 탄환 상쇄 처리 (Bullet)
- ✅ NPC의 자동 대사 및 상호작용 처리 (NPCController, NpcDialog)
- ✅ 엘프 NPC와 대화 시 Easy 모드, 사망 시 Hard 모드로 분기
- ✅ The Stack 씬으로 진입하는 공간 이동 로직 (GameManager와 SceneManager 활용)
- ✅ The Stack 게임 내 점수 저장 및 리더보드 기능 구현 (ScoreManager, LeaderboardUI)
- ✅ UI 상태 전환 및 게임 오버 UI 출력 처리 (UIManager, GameOverUI)
- ✅ 효과음 재생 및 사운드 일괄 제어 (SoundManager)
- ✅ 카메라 따라가기 및 확대 축소 기능 (FollowCamera, CamaraZoom)


## 🖼️ 게임 화면

> 실제 게임 플레이 장면입니다.
><img width="1396" height="782" alt="화면 캡처 2025-07-28 103737" src="https://github.com/user-attachments/assets/410c8937-ca09-430c-b09b-c6bda866ebdf" />
> 플레이 링크
> https://youtu.be/nfImlG5H5oc


## 📂 프로젝트 폴더 구조  

02. Scripts/
├── Base/
│   └── BaseController.cs
├── Camera/
│   ├── CamaraZoom.cs
│   └── FlollowCamera.cs
├── Enemy/
│   └── EnemyController.cs
├── Entity/
│   ├── AnimationHandler.cs
│   ├── ResourceController.cs
│   └── StatHandler.cs
├── Manager/
│   ├── EnemyManager.cs
│   ├── GameManager.cs
│   ├── LayerManager.cs
│   ├── ProjectileManager.cs
│   ├── SoundManager.cs
│   └── UIManager.cs
├── MiniMap/
│   └── MiniMap_Size.cs
├── NPC/
│   ├── NPCController.cs
│   ├── NPCResourceController.cs
│   ├── NPCState.cs
│   ├── NpcDialog.cs
│   └── NpcLookAtPlayer.cs
├── Player/
│   └── PlayerController.cs
├── Sound/
│   └── SoundSource.cs
├── TheStack/
│   ├── DestroyZone.cs
│   ├── Stack_UIManager.cs
│   ├── TheStack.cs
│   └── UI/
│       ├── Stack_BaseUI.cs
│       ├── Stack_GameUI.cs
│       ├── Stack_HomeUI.cs
│       └── Stack_ScoreUI.cs
├── UI/
│   ├── BaseUI.cs
│   ├── GameOverUI.cs
│   ├── GameUI.cs
│   ├── HardGameOverUI.cs
│   ├── HomeUI.cs
│   ├── InteractionButton.cs
│   └── ReturnToVillageButton.cs
├── Weapon/
│   ├── Handler/
│   │   ├── MeleeWeaponHandler.cs
│   │   └── WeaponHandler.cs
│   ├── Muzzle_Flash/
│   │   └── MuzzleFlash.cs
│   ├── M4_WeaponHandler.cs
│   ├── ProjectileController.cs
│   └── RangeWeaponHandler.cs


  

## 👤 개발자
| 이름 | 역할 |  
| 이재은 | 전체 게임 설계, Unity 개발, 전투 시스템, UI 및 사운드 연동 |

본 프로젝트는 내일배움캠프에서의 학습 내용을 바탕으로 직접 구현한 탑다운 슈팅 게임입니다.  
설계부터 구현, 디버깅까지 혼자 해결하며 실전 개발 역량을 키웠습니다.


## 🛠 트러블슈팅 (문제 해결 기록)

### 1. ❌🏹 화살 투사체가 보이지 않던 문제
- **문제정의**: 활을 쐈을 때 투사체(화살)가 생성되지만 화면에 보이지 않음
- **원인**: ProjectileController 프리팹에 SpriteRenderer에 Sprite가 할당되어 있지 않았음
- **해결방법**: SpriteRenderer에 적절한 화살 이미지(Sliced Sprite)를 연결함
- **교훈**: 프리팹은 정상적으로 인스턴스화되어도 Sprite가 없으면 화면에 나타나지 않음
- **대비책**: 프리팹 등록 시 SpriteRenderer의 Sprite와 Sorting Layer를 항상 확인할 것
---
### 2. ❌🧟‍♂️ 몬스터가 생성되지 않던 문제
- **문제정의**: EnemyManager에서 몬스터가 스폰되지 않음
- **원인**: GameManager에서 EnemyManager.Init()은 호출되었지만 StartWave() 호출이 누락됨
- **해결방법**: 게임 시작 시 StartWave()를 명시적으로 호출함
- **교훈**: 매니저 간 호출 순서와 흐름 제어가 중요함
- **대비책**: Init() → Start() 순서를 명확히 하고, 필수 트리거 함수 호출 여부를 점검할 것
---
### 3. ❌🎮 Player 입력(Input)이 작동하지 않던 문제
- **문제정의**: PlayerController에서 OnMove()가 정의되어 있음에도 캐릭터가 움직이지 않음
- **원인**: Input Action 이름을 "Look"으로 설정해야 하는데, "Lock"으로 오타 작성됨
- **해결방법**: Input Action 이름을 "Look"으로 정확히 수정함
- **교훈**: Unity Input System은 함수명과 액션명이 정확히 일치해야 이벤트가 바인딩됨
- **대비책**: Input System 사용 시 액션 이름과 메서드 이름이 정확히 일치하는지 확인할 것
---
### 4. ❌🔊 AudioSource 컴포넌트 누락 예외
- **문제정의**: SoundManager에서 AudioSource가 없다는 예외 발생 (MissingComponentException)
- **원인**: SoundManager 오브젝트에 AudioSource 컴포넌트가 누락됨
- **해결방법**: Unity 에디터에서 AudioSource를 수동으로 추가함
- **교훈**: 필수 컴포넌트는 코드에서 접근 전에 반드시 붙어 있어야 함
- **대비책**: [RequireComponent(typeof(AudioSource))]를 클래스에 추가또는 Awake()에서 null 체크하여 예외를 방지할 것
---

## 📌 앞으로 개선할 점
- 🔁 적 종류 다양화 및 보스 몬스터 추가
- 🎮 게임 패드 지원 (Input System 확장)
- 📱 모바일 대응 (터치 UI 포함)
- 💾 데이터 저장 기능 (최고 점수, 세팅 값)
- 📊 게임 통계 UI (적 처치 수, 생존 시간 등)


