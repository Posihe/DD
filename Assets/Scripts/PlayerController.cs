using UnityEngine;
using UnityEngine.InputSystem; // Unity의 새로운 입력 시스템을 사용하기 위해 포함
using static BoardManager; // BoardManager의 내부 클래스(CellData)를 사용하기 위해 포함

// 플레이어의 이동을 제어하는 클래스
public class PlayerController : MonoBehaviour
{
    private BoardManager m_Board; // 현재 플레이어가 속한 보드(맵) 매니저
    private Vector2Int m_CellPosition; // 현재 플레이어의 셀(타일) 위치를 저장하는 변수

    // 매 프레임마다 호출되는 Update 함수
    private void Update()
    {
        Move(); // 이동 처리 함수 호출
    }

    // 플레이어를 특정 셀 위치에 생성하는 함수
    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        m_Board = boardManager; // 보드 매니저 설정
        m_CellPosition = cell; // 초기 셀 위치 설정

        // 보드의 셀 좌표를 월드 좌표로 변환하여 플레이어를 해당 위치에 배치
        transform.position = m_Board.CellToWorld(cell);
    }

    // 플레이어 이동을 처리하는 함수
    private void Move()
    {
        Vector2Int newCellTarget = m_CellPosition; // 이동할 목표 셀 좌표 (기본값: 현재 위치)
        bool hasMoved = false; // 이동 여부를 저장하는 플래그

        // 키 입력을 감지하여 이동 방향 결정
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) // ↑ 키가 눌렸을 때
        {
            newCellTarget.y += 1; // 위쪽으로 이동
            hasMoved = true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame) // ↓ 키가 눌렸을 때
        {
            newCellTarget.y -= 1; // 아래쪽으로 이동
            hasMoved = true;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame) // → 키가 눌렸을 때
        {
            newCellTarget.x += 1; // 오른쪽으로 이동
            hasMoved = true;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame) // ← 키가 눌렸을 때
        {
            newCellTarget.x -= 1; // 왼쪽으로 이동
            hasMoved = true;
        }

        // 이동 키가 눌렸다면 실행
        if (hasMoved)
        {
            m_CellPosition = newCellTarget; // 새로운 위치를 현재 위치로 업데이트
            transform.position = m_Board.CellToWorld(m_CellPosition); // 이동한 위치를 월드 좌표로 변환하여 반영
        }
    }
}
