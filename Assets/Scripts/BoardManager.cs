using UnityEngine;
using UnityEngine.Tilemaps; // Unity�� Ÿ�ϸ� �ý����� ����ϱ� ���� ����

// ����(��)�� �����ϴ� Ŭ����
public class BoardManager : MonoBehaviour
{
    // [���� Ŭ����] �� ��(Ÿ��)�� ������ �����ϴ� Ŭ����
    public class CellData
    {
        public bool Passable; // �� ���� ������ �� �ִ��� ���� (true: ��� ����, false: ��� �Ұ���)
    }

    private CellData[,] m_BoardData; // ���� ��� �� �����͸� �����ϴ� 2���� �迭

    private Tilemap m_Tilemap; // Ÿ�ϸ��� �����ϱ� ���� ����

    public int width;  // ���� ���� ũ��
    public int height; // ���� ���� ũ��

    public Tile[] GroundTiles; // �ٴ� Ÿ�ϵ��� ��Ƴ��� �迭 (�������� ������ �� �ֵ��� �迭�� ����)
    public Tile[] WallTiles;   // �� Ÿ�ϵ��� ��Ƴ��� �迭

    private Grid m_Grid; // Ÿ�ϸ��� ���� �׸���(Grid) ����

    public PlayerController player; // �÷��̾� ��Ʈ�ѷ� ����

    // Start �Լ�: ������ ���۵� �� �� �� ����Ǵ� �Լ�
    void Start()
    {
        // ���� ������Ʈ(BoardManager)�� �ڽ� ������Ʈ �� Tilemap�� ������
        m_Tilemap = GetComponentInChildren<Tilemap>();

        // ���� ������Ʈ(BoardManager)�� �ڽ� ������Ʈ �� Grid�� ������
        m_Grid = GetComponentInChildren<Grid>();

        // width * height ũ���� 2���� �迭�� �����Ͽ� ���� ������ ������ �غ� ��
        m_BoardData = new CellData[width, height];

        // 2�� for���� ����Ͽ� ��� Ÿ���� ��ġ
        for (int y = 0; y < height; ++y) // ����(y) �������� �� �پ� Ž��
        {
            for (int x = 0; x < width; ++x) // ����(x) �������� �̵��ϸ� Ÿ�� ��ġ
            {
                Tile tile; // ���� ��ġ�� ��ġ�� Ÿ���� ������ ����
                m_BoardData[x, y] = new CellData(); // ���ο� �� �����͸� �����Ͽ� �迭�� ����

                // ���� �׵θ� �κ� (x=0, y=0, x=width-1, y=height-1)�� ��(WallTile)�� ����
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    // �� Ÿ�� �� ������ Ÿ���� �����Ͽ� ��ġ
                    tile = WallTiles[Random.Range(0, WallTiles.Length)];

                    // ���� ������ �� �����Ƿ� Passable�� false�� ����
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    // �ٴ� Ÿ�� �� ������ Ÿ���� �����Ͽ� ��ġ
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];

                    // �ٴ��� ������ �� �����Ƿ� Passable�� true�� ����
                    m_BoardData[x, y].Passable = true;
                }

                // ������ Ÿ���� (x, y) ��ġ�� ��ġ
                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        // �÷��̾ (1,1) ��ǥ�� ���� (���� �Ŵ����� �Ѱ��༭ �� ������ ����)
        player.Spawn(this, new Vector2Int(1, 1));
    }

    // Ư�� ���� ��ġ�� ���� ��ǥ�� ��ȯ�ϴ� �Լ�
    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }
}
