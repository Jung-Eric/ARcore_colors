# 증강현실 색 추출 퍼즐 게임 Color Collector
전체적인 내용은 documents의 "증강현실 색 추출기반 퍼즐 게임.docx" 를 참조해주세요

## 소개
스마트폰을 통해 본 AR세계와 게임 내 가상 환경을 연결해 진행하는 AR 게임
AR세계에서 획득한 다양한 아이템들로 게임 내 퀘스트들을 돌파해나가며 스토리를 헤쳐나간다.

컴퓨터공학과 학부생 3명이서 개발을 진행
Unity Engine을 사용해 게임을 개발했고 ARcore Tool kit를 활용한 AR환경 구현 했습니다.
게임 플레이 base와 system 개발, 기획과 맵 디자인, AR 기능과 Object Physics 개발
이렇게 3 분야로 나누어 개발을 진행했습니다.

본인은 AR 기능과 Object Physics 개발을 수행했습니다.


## AR core
![image](https://user-images.githubusercontent.com/56705742/126890327-d92f3683-9359-4cf0-8c79-1f526110b962.png)

ARcore는 구글에서 제공하는 AR 개발 tool입니다.
ARcore는 모션 추적, 환경 인식, 조명 추정과 같은 기능을 제공합니다.
모션 추적은 현실의 좌표를 기억해 이를 AR object와 현실의 좌표를 연동하는 기능을 수행합니다.
환경 인식은 지형을 인식하는 지면등의 물체들을 인식하는 기능을 수행합니다.
조명 추정은 조명을 인식하여 적용합니다.
이외에도 여러 기능들이 있으나 해당 기능들이 핵심적인 역할을 합니다.

<img width="480" alt="AR1" src="https://user-images.githubusercontent.com/56705742/126890432-622d7219-91c3-4d8a-9c4e-f3920d3a0dde.png">
 
카메라를 본 현실의 방에 부유하는 지형과 소행성 마법진과 같은 가상 오브젝트와 연동해 AR 세계를 구현합니다.
가상 오브젝트들이 현실 오브젝트의 좌표를 추적해 자신들의 위치 또한 조정합니다.

<img width="480" alt="AR2" src="https://user-images.githubusercontent.com/56705742/126891015-014bed44-6781-4a4b-bb02-b03e23d610b9.png">
 
해당 이미지는 게임 플레이 중 하나로 대상 오브젝트를 터치해 색을 추출하는 장면입니다.
카메라를 통해 보여지는 이미지 일부를 crop해 RGB 색상 분석과 saturation 분석 ARcore의 조명 추정 기능을 통해
특정 색상의  같은 아이템을 생성해 게임의 자원으로 활용 가능합니다.
 
![image](https://user-images.githubusercontent.com/56705742/126891264-b9e2dbf2-7396-4767-bdd5-29268b6afb0d.png)
  
초록색에 해당하는 보석을 생성해 이를 퀘스트에 활용할 수 있습니다.

 ![image](https://user-images.githubusercontent.com/56705742/126891299-052a75dc-443c-450c-a8fc-da6cb80f4869.png)

(퀘스트 기능은 다른 개발자 분이 개발)

## Object Physics

![image](https://user-images.githubusercontent.com/56705742/126892217-d23d9c0e-6de1-4502-b0ab-002249951581.png)

![image](https://user-images.githubusercontent.com/56705742/126892220-2e4746ee-f154-4d13-b63c-5d421b6b0042.png)

![image](https://user-images.githubusercontent.com/56705742/126892223-bd6390e9-2de3-4661-affe-cf33eb6b0dd1.png)

 캐릭터 컨트롤러, 캐릭터 이동, 좌표와 시점에 따른 camera work와 같은 부분을 개발했습니다.
 
 
 
