### 스파르타 내일배움캠프

[빌드파일 (Google)](https://drive.google.com/file/d/10ilMGH8FcsKDWKJCRfGDyM1auiOyhIiu/view?usp=sharing)<br>
[시연영상 (Youtube)](https://youtu.be/M5mWI8t2BDs)

Unity 게임 개발 숙련주차 팀프로젝트<br>

- 목차
  - [Lobby Scene](#Lobby-Scene)
  - [Main Scene](#Main-Scene)
  - [Ending Scene](#Ending-Scene)
  - [기술 관련](#기술관련)

 
# 게임 설명
컨셉 : 유적 탐험과 숨겨진 보물 찾기
장르 : 3D 퍼즐 플랫포머
조작 : 키보드 및 마우스

### Lobby Scene
![image](https://github.com/user-attachments/assets/27a034e4-4aca-4741-aee9-a8fc5770e8e5)
![image](https://github.com/user-attachments/assets/3cf65444-35a6-409a-b34c-c943630f2b8a)
- START 버튼 클릭시 캐릭터가 걸어들어가는 연출 / 페이드 아웃 효과와 함께 Main Scene으로 이동 합니다.
- Setting 버튼 클릭시 사운드 조절 및 마우스 감도, 키설정 가능 합니다.

### Main Scene
#### 스테이지 1
![image](https://github.com/user-attachments/assets/a48578d6-904f-47d7-b1ae-4f9cb2745fcf)
- 미끄러운 얼움 위를 이동
- 방향 조절 중요
- 목적지 도달 과제
- 전력적 이동 필요
#### 스테이지 2
![image](https://github.com/user-attachments/assets/0c3da9ac-bcf6-43f3-9db9-7fbeb5ecc62a)
- 보이지 않는 발판
- 스위치로 발판 확인
- 기억력 테스트
- 신중한 이동 필요
#### 스테이지 3
![image](https://github.com/user-attachments/assets/1d5ca7e4-092b-4246-97cd-ad525edc8ace)
- 복잡한 미로구조
- 방향 감각 시험
- 다양한 장애물
- 탈출구 찾기
- 논리적 사고 필요

### Ending Scene
![image](https://github.com/user-attachments/assets/dc90d239-fb42-4530-af2f-1ce7eaa818d9)
- 엔딩크레딧이 종료되면 Lobby Scene으로 이동합니다.

# 기술관련
### 초기 구상
![image](https://github.com/user-attachments/assets/6d08084d-ab16-4e7d-97c7-6f1baf49ed2f)

### 와이어 프레임
![image](https://github.com/user-attachments/assets/ad2bfc20-deb4-4d82-9dcd-fd69f371e216)

### 키 바인딩
![image](https://github.com/user-attachments/assets/ed5d213c-b346-4752-8c46-c930b8204260)
- KeyBindData
  - ScriptableObject로 구성
  - 담당하는 조작키와 해당 조작에 대한 이름을 가짐
- RebindingOperation
  - 실질적인 키 바인딩 변경을 실행
  - WithControlsExcluding("Mouse")
    : 마우스 입력을 제한
  - OnMatchWaitForAnother(0.1f)
    : 다중 입력 제한을 위한 입력 시간 차
  - OnComplete()
    : 리바인딩 완료 후 호출할 메서

### 미끄러지는 얼음 길
![image](https://github.com/user-attachments/assets/2cf2411a-26ed-4273-9e75-d2615e38f6ba)
- 플레이어 방향 벡터 계산
  - 정확한 전방 방향 계산
  - Right 프로퍼티 활용
- Atan2와 Rad2Deg 각도 변환
  - Atan2로 라디안 각도 계산
  - Rad2Deg로 도(Degree) 변환
  - 정확한 방향 결정
- 4방향 비교 및 AddForce 구현
  - 0,89,180,-90도 비교
  - 가장 가까운 방향 선택
  - AddForce로 미끄러짐 구현

### 투명한 발판
![image](https://github.com/user-attachments/assets/f5c5906b-7334-42d0-9736-2c3615c6ab1e)
- 스위치 발판 트리거
  - 이동 가능한 발판의 상태를 변경
  - Material 변경을 통한 투명한 발판 구현
- Invisible Wall
  - 벽 활성화/비활성화
  - 바닥이 보일 때는 플레이어의 이동을 제한
 
### UI 키 기능 변경
![image](https://github.com/user-attachments/assets/50d44f0c-902b-4d13-8efa-199c494e17fd)
- ISettingUI 활용
  - ISettingUI 인터페이스 정의
  - UI 오브젝트에서 인터페이스 검색
  - 버튼 기능 메서드 동적할당
- 동적 기능 구현
  - 취소/적용 버튼에 기능 할당
  - 현재 UI 상태에 따른 동작 설정
  - 코드 재사용성 증가
- 버튼 기능 할당
  - 활성화된 UI 오브젝트 확인
  - ISettingUI 구현 클래스 찾기
  - 해당 인터페이스의 메서드 호출
