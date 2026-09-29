# Oss Jump Action Game

충북대 오픈소스SW 수업(2022) 팀 프로젝트로 만든 Unity 2D 점프 액션 게임입니다.
원본 저장소: [djmachine/Oss_Jump_Action_Game](https://github.com/djmachine/Oss_Jump_Action_Game)

- Unity **2021.3.14f1**
- 씬: `Assets/Scenes/` (Main Menu / In Game / Store)
- 조작: ←/→ 이동, Space 점프, 적을 위에서 밟으면 처치

## 2026 버그 수정

| 증상 | 원인 / 수정 |
| --- | --- |
| 플레이어 빌드 실패 | 런타임 스크립트에 에디터 전용 `using`(`Microsoft.Unity.VisualStudio.Editor`, `UnityEditor`) → 제거 |
| 재시작 후 한 대만 맞아도 즉사 | `Restart()`가 체력·하트 UI를 복구하지 않음 → 최대 체력으로 리셋 |
| 캐릭터 구매 후 코인이 안 쌓이고 상점 구매 불가 | 코인은 `stagepoint`에만, 구매는 `totalpoint`에서만 처리 → 현재 캐릭터 기준으로 적립/차감 |
| 캐릭터를 두 번 사면 게임 진행이 멈춤 | `change`가 2가 되어 모든 분기 무시 → 1회만 구매 가능 |
| 죽은 뒤에도 이동·점프 가능, 죽은 캐릭터 색이 복구됨 | 사망 상태 플래그 추가, 피격 해제 예약 취소 |
| 밟은 적이 사라지지 않고 계속 움직임 | `Turn()`의 `CancelInvoke()`가 `De_Active` 예약까지 취소 → `Think`만 취소, 사망 시 이동 중지 |
| 떨어지는 발판이 영구히 사라져 리스폰 후 진행 불가 | 일정 시간 뒤 원래 위치에 다시 생성 |
| 상하 이동 발판에서 플레이어가 미끄러지고 튕김 | 발판 위 플레이어를 발판 이동량만큼 함께 이동 |
| 교수님 발판 초기화가 연속으로 여러 번 발생 | `FixedUpdate`마다 `Invoke`가 중복 예약 → 예약 중이면 무시 |
| 대포알 속도가 모니터 주사율에 따라 달라짐 | 프레임 기준 이동 → `Time.deltaTime` 보정 |
| GitHub에서 한글 주석 깨짐 | 스크립트 인코딩 EUC-KR → UTF-8 |
