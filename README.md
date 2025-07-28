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

📦 02. Scripts/  
├── 📂Base/  
│   └── BaseController.cs  
├── 📂Camera/  
│   ├── CamaraZoom.cs  
│   └── FlollowCamera.cs  
├── 📂Enemy/  
│   └── EnemyController.cs  
├── 📂Entity/  
│   ├── AnimationHandler.cs  
│   ├── ResourceController.cs  
│   └── StatHandler.cs  
├── 📂Manager/  
│   ├── EnemyManager.cs  
│   ├── GameManager.cs  
│   ├── LayerManager.cs  
│   ├── ProjectileManager.cs  
│   ├── SoundManager.cs  
│   └── UIManager.cs    
├── 📂MiniMap/  
│   └── MiniMap_Size.cs  
├── 📂NPC/    
│   ├── NPCController.cs  
│   ├── NPCResourceController.cs  
│   ├── NPCState.cs  
│   ├── NpcDialog.cs  
│   └── NpcLookAtPlayer.cs  
├── 📂Player/  
│   └── PlayerController.cs    
├── 📂Sound/  
│   └── SoundSource.cs    
├── 📂TheStack/  
│   ├── DestroyZone.cs    
│   ├── Stack_UIManager.cs    
│   ├── TheStack.cs  
│   └── 📂UI/    
│       ├── Stack_BaseUI.cs    
│       ├── Stack_GameUI.cs    
│       ├── Stack_HomeUI.cs  
│       └── Stack_ScoreUI.cs  
├── 📂UI/  
│   ├── BaseUI.cs  
│   ├── GameOverUI.cs      
│   ├── GameUI.cs  
│   ├── HardGameOverUI.cs  
│   ├── HomeUI.cs  
│   ├── InteractionButton.cs    
│   └── ReturnToVillageButton.cs  
├── 📂Weapon/  
│   ├── 📂Handler/  
│   │   ├── MeleeWeaponHandler.cs  
│   │   └── WeaponHandler.cs    
│   ├── 📂Muzzle_Flash/      
│   │   └── MuzzleFlash.cs    
│   ├── M4_WeaponHandler.cs  
│   ├── ProjectileController.cs  
│   └── RangeWeaponHandler.cs  
  

## 👤 개발자
이재은 (NextTheWAT)
스파르타 코딩클럽 게임 개발 부트캠프 참가자
Unity와 C#을 기반으로 2D/3D 게임을 제작하며, UI 시스템, 씬 전환, 캐릭터 제어, 리더보드 구현 등 다양한 기능을 직접 개발했습니다.
주요 작업: 플레이어 시스템, NPC 대화, 총알 상쇄, UI 관리, 리더보드 등


## 🛠 트러블슈팅 (문제 해결 기록)
1. ❌ 게임 오버 후 씬 재시작 시 상태가 초기화되지 않음
- **문제정의**: 씬은 재시작되었는데 GameManager가 정상적으로 초기화되지 않아 게임이 시작되지 않음
- **원인**: SceneManager.LoadScene()으로 씬만 재시작하고, GameManager.StartGame()을 따로 호출하지 않았음
- **해결방법**: 씬 로드 이후에 GameManager.instance.StartGame()을 명시적으로 다시 호출함
- **교훈**:  씬 전환만으로는 싱글톤 객체의 내부 상태까지 초기화되지 않기 때문에, 상태 초기화 코드를 명확히 호출해야 함
- **대비책**:  씬 로드 직후 필요한 초기화 로직을 별도 함수로 분리하여 공통적으로 호출하도록 구조화함
---
2. ❌ NPC 상호작용 후 씬 이동이 안 됨
- **문제정의**: 엘프와 F 키로 대화했는데 씬이 이동하지 않고 멈춰있음
- **원인**: 대화 조건은 만족되었지만 SceneManager.LoadScene() 호출이 빠져 있었음
- **해결방법**: 조건 분기 이후 명시적으로 SceneManager.LoadScene("TopDownScene") 호출하여 씬 전환 처리
- **교훈**:  시나리오 흐름을 나눈다고 끝이 아니라, 실제 씬 이동 코드를 잊지 않고 연결해줘야 한다는 걸 다시 느낌
- **대비책**:  씬 이동 조건을 담당하는 함수와 실제 전환 호출을 한 함수로 묶어 관리
---
3. ❌ NPC 사망 시 대사가 반복되지 않음
- **문제정의**: NPC가 피격 후 사망한 뒤, 자동으로 출력되던 잡담 대사가 멈춤
- **원인**: NPC 상태가 사망 상태로 고정되며, 대사 출력 함수 RandomChat()이 중단됨
- **해결방법**: 사망 상태에서도 moodState에 따라 대사가 계속 출력되도록 조건 수정
- **교훈**:  상태 변화 이후의 흐름도 미리 고려해서, 죽음 이후에도 "세계는 계속 돌아간다"는 걸 시스템에도 반영해야 함
- **대비책**:  상태에 따른 출력 중단 여부를 명확히 구분해서 처리
---
4. ❌ 총알 상쇄가 작동하지 않음
- **문제정의**: 플레이어 총알과 몬스터 총알이 충돌해도 서로 제거되지 않음
- **원인**: OnTriggerEnter2D()에서 태그 또는 레이어 비교 조건 누락
- **해결방법**: 충돌한 객체의 태그를 비교하여 Destroy(gameObject)를 명확하게 실행함
- **교훈**:  충돌 조건을 제어할 땐 Tag, Layer, LayerCollision Matrix까지 반드시 확인해야 한다
- **대비책**:  충돌 조건은 디버그 로그를 남겨가며 빠짐없이 체크하기
---
5. ❌ Stack 리더보드 점수가 덮어써짐
- **문제정의**: Stack 게임 종료 후 현재 점수가 이전 최고 점수보다 낮아도 저장됨
- **원인**: 점수 비교 없이 무조건 PlayerPrefs.SetInt()로 덮어쓰기함
- **해결방법**: if (currentScore > bestScore) 조건을 추가해 더 높은 점수일 때만 갱신
- **교훈**:  저장 로직에서도 "조건"을 잊지 말아야 하고, 무조건 저장은 실수로 이어질 수 있음
- **대비책**: Set 전에 항상 Get해서 비교하고, UI에도 명확히 반영되도록 설계
---
