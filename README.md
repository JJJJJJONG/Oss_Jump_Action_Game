<h1 align="center">🏃 CBNU Jump Action</h1>

<p align="center">
  충북대학교 캠퍼스를 배경으로 달리고 점프하는 <b>Unity 2D 플랫포머 게임</b><br>
  충북대 오픈소스SW 수업 팀 프로젝트 (2022.11 ~ 2022.12)
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Unity-2021.3-000000?style=for-the-badge&logo=unity&logoColor=white">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white">
  <img src="https://img.shields.io/badge/Platform-Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white">
</p>

<p align="center">
  <img src="docs/images/stage1_play.jpg" width="80%">
</p>

## 📌 프로젝트 소개

실제 충북대학교 **정문**과 **중문** 사진을 스테이지 배경으로 사용한 횡스크롤 점프 액션 게임입니다.
플레이어는 발판을 건너며 코인을 모으고, 적을 밟아 처치하면서 결승 깃발까지 도달해야 합니다.
모은 코인으로 상점에서 능력치를 올리거나 새 캐릭터(교복 캐릭터)를 구매할 수 있습니다.

## 🎮 주요 기능

| 분류 | 내용 |
| --- | --- |
| **스테이지** | 정문 → 중문 2개 스테이지, 결승 지점 도달 시 다음 스테이지로 전환 |
| **플레이어** | 이동·점프·애니메이션(대기/걷기/점프/피격), 적 밟기 공격, 피격 시 넉백과 2초 무적 |
| **체력 시스템** | 하트 UI, 낙하/가시/적 충돌 시 감소, 모두 잃으면 Retry |
| **코인 & 점수** | 금(200) · 은(100) · 동(50) 코인, 적 처치 시 100점 |
| **상점** | 이동속도 증가, 점프력 증가, 새 캐릭터 구매(체력 +1, 전용 UI) |
| **기믹 발판** | 밟으면 떨어지는 발판(일정 시간 뒤 복구), 위아래로 움직이는 발판, 밟으면 떠오르는 비행 발판, 날아오는 대포알 |
| **적 AI** | 무작위로 이동 방향을 정하고, 낭떠러지 앞에서는 레이캐스트로 감지해 돌아섬 |
| **사운드** | 점프·공격·피격·코인·사망·클리어·구매 효과음 |

## 🖼 스크린샷

| 메인 메뉴 | 상점 |
| :---: | :---: |
| <img src="docs/images/main_menu.png"> | <img src="docs/images/store.png"> |
| **Stage 1 · 정문** | **Stage 2 · 중문** |
| <img src="docs/images/stage1_gate.jpg"> | <img src="docs/images/stage2_wall.jpg"> |

## 🕹 조작법

| 키 | 동작 |
| --- | --- |
| ← / → | 이동 |
| Space | 점프 |
| 적 위로 착지 | 공격 |

## 🛠 기술 스택 & 구조

- **Engine**: Unity 2021.3.14f1 (2D, Tilemap, Animator, Physics2D)
- **Language**: C#

```
Assets/Scripts
├── GameManager.cs            # 스테이지 전환, 점수·체력, 상점 구매, 재시작
├── Player_move.cs            # 이동/점프/공격/피격/사망, 효과음
├── Enemy_move.cs             # 적 순찰 AI (Invoke 기반 행동 결정 + 낭떠러지 레이캐스트)
├── Falling_Platform.cs       # 밟으면 떨어졌다가 다시 생기는 발판
├── Up_Down_Move_Platform.cs  # 두 지점을 오가는 발판 (탑승한 플레이어도 함께 이동)
├── Professor.cs              # 밟으면 떠오르는 비행 발판
└── Score_F.cs                # 일정 거리를 날아오는 대포알 장애물
```

## 👥 팀원

| 팀원 | 담당 |
| --- | --- |
| **박종은** ([@JJJJJJONG](https://github.com/JJJJJJONG)) | 게임 메인 로직(GameManager·플레이어·적·상점) 개발 및 통합, 움직이는/떨어지는 발판, 정문·중문 스테이지 |
| 김대중 ([@djmachine](https://github.com/djmachine)) | 프로젝트 초기 세팅, 플레이어·배경 에셋 |
| [@anhyo3539](https://github.com/anhyo3539) | 대포알 장애물, 비행 발판 |
| [@MilaEvans](https://github.com/MilaEvans) | 랭킹 시스템 (`RankingSystem` 브랜치) |

## ▶ 실행 방법

1. [Releases](../../releases)에서 Windows 빌드를 받아 `Jump.exe` 실행
2. 또는 Unity Hub에서 **Unity 2021.3.14f1**로 이 폴더를 열고 `Assets/Scenes/In Game.unity` 실행

> 원본 저장소: [djmachine/Oss_Jump_Action_Game](https://github.com/djmachine/Oss_Jump_Action_Game)
> 2026년 백업하면서 남아 있던 버그(스테이지 2 전환·배경 가림, 재시작 체력, 상점 결제 등)를 정리해 완성본으로 올렸습니다.
