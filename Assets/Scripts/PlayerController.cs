using UnityEngine;
using UnityEngine.InputSystem; // Unity�� ���ο� �Է� �ý����� ����ϱ� ���� ����
using static BoardManager; // BoardManager�� ���� Ŭ����(CellData)�� ����ϱ� ���� ����

// �÷��̾��� �̵��� �����ϴ� Ŭ����
public class PlayerController : MonoBehaviour
{
    private BoardManager m_Board; // ���� �÷��̾ ���� ����(��) �Ŵ���
    private Vector2Int m_CellPosition; // ���� �÷��̾��� ��(Ÿ��) ��ġ�� �����ϴ� ����

    // �� �����Ӹ��� ȣ��Ǵ� Update �Լ�
    private void Update()
    {
        Move(); // �̵� ó�� �Լ� ȣ��
    }

    // �÷��̾ Ư�� �� ��ġ�� �����ϴ� �Լ�
    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        m_Board = boardManager; // ���� �Ŵ��� ����
        m_CellPosition = cell; // �ʱ� �� ��ġ ����

        // ������ �� ��ǥ�� ���� ��ǥ�� ��ȯ�Ͽ� �÷��̾ �ش� ��ġ�� ��ġ
        transform.position = m_Board.CellToWorld(cell);
    }

    // �÷��̾� �̵��� ó���ϴ� �Լ�
    private void Move()
    {
        Vector2Int newCellTarget = m_CellPosition; // �̵��� ��ǥ �� ��ǥ (�⺻��: ���� ��ġ)
        bool hasMoved = false; // �̵� ���θ� �����ϴ� �÷���

        // Ű �Է��� �����Ͽ� �̵� ���� ����
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) // �� Ű�� ������ ��
        {
            newCellTarget.y += 1; // �������� �̵�
            hasMoved = true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame) // �� Ű�� ������ ��
        {
            newCellTarget.y -= 1; // �Ʒ������� �̵�
            hasMoved = true;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame) // �� Ű�� ������ ��
        {
            newCellTarget.x += 1; // ���������� �̵�
            hasMoved = true;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame) // �� Ű�� ������ ��
        {
            newCellTarget.x -= 1; // �������� �̵�
            hasMoved = true;
        }

        // �̵� Ű�� ���ȴٸ� ����
        if (hasMoved)
        {
            m_CellPosition = newCellTarget; // ���ο� ��ġ�� ���� ��ġ�� ������Ʈ
            transform.position = m_Board.CellToWorld(m_CellPosition); // �̵��� ��ġ�� ���� ��ǥ�� ��ȯ�Ͽ� �ݿ�
        }
    }
}
