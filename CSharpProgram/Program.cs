﻿using System;
using System.Diagnostics;

namespace CSharpProgram
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"int형 변수에 할당된 메모리: {sizeof(int)}Byte");

			int bigNumber = 1_000_000;
			Console.WriteLine((bigNumber - 1000).ToString());

			Shape shape = Shape.Rect;
			Console.WriteLine($"{(int)shape}");

			string? myName = Console.ReadLine();
			Console.WriteLine(myName);

			float input;
			float.TryParse(Console.ReadLine(), out input);
			const float PI = 3.14f;
			float S = 4f * PI * input * input;
			float V = 4f / 3f * PI * input * input * input;
			Console.WriteLine($"구의 겉넓이: {S}, 구의 부피: {V}");
		}
	}

	enum Shape
	{
		Circle,
		Tri,
		Rect
	}
}

//comment
/*
 * comment2
 * 메모 기능
 */

/**
* 하드웨어 - 물리적 장치 <->소프트웨어 - 하드웨어에 설치된 OS, 앱 등
* 프로그램 - 하고자 하는 작업을 컴퓨터에게 전달해 주는 역할을 하는 소프트웨어로 명령어들로 구성된다
* 프로그래밍, 코딩 - 소프트웨어를 만드는 행위
*
* 기계어 - 0과 1로 구성된 컴퓨터가 인식 가능한 언어
* 프로그래밍 언어 - 난해한 기계어보다 이해하기 쉽다
* 프로그래머, 개발자 - 프로그래밍 언어로 소프트웨어를 만드는 사람
*
* 코드, 소스 - 프로그래밍 언어 문법에 맞춘 텍스트로 된 명령 집합
* 컴파일 -소스코드를 기계어로 번역
* 컴파일러 - 컴파일 소프트웨어
*
* 프로그래밍 과정
* 1.소스 작성 후 저장-> 2.컴파일해서 실행 프로그램 생성-> 3.실행
* 빌드 - 1단계 + 2단계
*
* 버그 - 오류 발생 <->디버그, 트러블 슈팅 - 오류 탐색 및 수정
*
* 1.C# 코드 -> 2. IL -> 3. 기계어
*
* [C#]
* -.net 기반 언어 중 하나
* -다중 패러다임 언어(절차적, 객체지향적, 함수형)
* -GC로 자동 메모리 관리
* -컴파일 기반 언어
* -strongly typed 언어
* -generic, LINQ 등 편리한 기능 제공
* -대, 소문자 구분
*
* [.Net]
* 닷넷은 소프트웨어 프레임워크(API 및 서비스 모음)
*
* 함수 - 프로그램에서 사용하기 위한 기능의 단위
* 라이브러리 - 함수의 모음
* API - 문서와 함께 제공되는 라이브러리, 함수의 모음
*
* 플랫폼 - 프로그램 실행 배경 환경 또는 운영체제
*
* C# 프로그램 - class + Main() 메서드 + 하나 이상의 문장(statement)
*
* Main() - 프로그램의 시작점으로 2개 이상 정의 불가
* 메서드 - 멤버 함수
* static 메서드 - 개체 생성없이 실행 가능한 메서드
* 네임스페이스 - 변수, 메서드, 클래스의 이름 중복 방지
* 중괄호 - 프로그램에서 범위 구분
* 세미콜론 - 문장의 종료
*
* 문법 -프로그래밍 언어의 규칙
* 스타일 - 프로그래밍 가이드라인
* 패턴 - 자주 사용하는 규칙, 스타일 모음
*
* 정규화된 이름(fully qualified names) - 네임스페이스 이름과 형식 이름 모두 작성한 것
*
* 들여쓰기 - 소스 코드의 가독성을 위해 사용. 4칸 공백 또는 탭 사용
* C#에서 명령어 사이, 기호와 괄호 사이의 공백, 탭, 줄 바꿈은 무시
* 이스케이프 시퀀스 - 역슬래시 + 기호로 조합된 확장 문자
* 문자열 보간법(=문자열 템플릿) - 문자열을 묶어서 처리하는 기능. 문자열 간의 + 연산은 느리기 때문에 사용. C#에서는 string.Format() 메서드, $"{}"(C# 6.0 부터)
* 
* 변수 - 임시 저장공간에 보관되는 데이터.
* 변수 선언 - 메모리에 데이터를 저장할 공간 확보
* 변수 정의 - 확보한 공간에 값을 저장
* 변수 초기화 - 초기값으로 정의. 지역변수는 초기화하지 않으면 쓰레기값 저장
* 변수 이름 작성 규칙 - 첫 글자는 문자만 가능, 영문자(대/소문자)와 숫자와 언더스코어 조합으로 작성, 길이는 255자 이하, 공백 포함 불가
* 
* 데이터 형식 - 변수에 저장할 수 있는 데이터의 종류. C#에서 기본으로 제공하는 자료형(int, string, bool, object 등)을 기본 형식이라고 한다
* int - 정수형, float - 실수형(부동소수점 형태), bool - 논리값, char - 문자, string - 문자열, object - 모든 자료형의 부모
* 
* 상수 - const 키워드가 붙은 변수는 지역 상수가 된다. 값을 바꿀 수 없기 때문에 선언 시 초기화해야 한다. 대문자로 작성
* 리터럴 - 변수에 대입하는 값 자체
* 실수형 리터럴은 끝에 f
* 문자형 리터럴은 작은 따옴표로 묶어서 표현
* 문자열 리터럴은 큰 따옴표로 묶어서 표현
* 숫자 구분자(digit separator)(C# 7.0부터) - 언더스코어(_) 문자를 사용. 숫자 형식을 표현할 때는 언더스코어 문자는 무시. 긴 숫자 표기 시 가독성 증가
* 
* null - C#에서 null 키워드는 아무것도 없는 값을 의미. 값 형식 변수 선언 시 ?기호를 붙이면 nullable 형식으로 변경되어 null 대입 가능 ex) int?, float?
* 자동 타입 추론(automatic type deduction) - 변수에 대입하는 값의 자료형이 명시적이거나 명확할 때 컴파일러가 추론 가능하기 때문에 생략 가능.
* 자동 타입 추론으로 기본 형식에 default 값 대입 가능(C# 6.0부터). 기본 형식마다 정해진 default 값이 존재
* 
* C#에서 열거형은 상수에 이름을 대응시켜 관리하는 표현 방식이다. 열거형은 클래스 범위 내에서 정의해야 한다
* 
* 표준 입출력 - 키보드로 입력, 모니터로 출력
* 
* 형 변환 - 캐스팅 연산자, 암시적 형 변환, 명시적 형 변환. casting, Convert, Parse, TryParse
*/

//(alt + shift + .) + (alt + shift + 화살표) = 다중캐럿

//이스케이프 시퀀스, 메모리 세이프티