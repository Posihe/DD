using UnityEngine;
using UnityEngine.Tilemaps; // Unity의 타일맵 시스템을 사용하기 위해 포함

// 보드(맵)를 관리하는 클래스
public class BoardManager : MonoBehaviour
{
    // [보조 클래스] 각 셀(타일)의 정보를 저장하는 클래스
    public class CellData
    {
        public bool Passable; // 이 셀을 지나갈 수 있는지 여부 (true: 통과 가능, false: 통과 불가능)
    }

    private CellData[,] m_BoardData; // 맵의 모든 셀 데이터를 저장하는 2차원 배열

    private Tilemap m_Tilemap; // 타일맵을 조작하기 위한 변수

    public int width;  // 맵의 가로 크기
    public int height; // 맵의 세로 크기

    public Tile[] GroundTiles; // 바닥 타일들을 담아놓는 배열 (랜덤으로 선택할 수 있도록 배열로 저장)
    public Tile[] WallTiles;   // 벽 타일들을 담아놓는 배열

    private Grid m_Grid; // 타일맵이 속한 그리드(Grid) 참조

    public PlayerController player; // 플레이어 컨트롤러 참조

    // Start 함수: 게임이 시작될 때 한 번 실행되는 함수
    void Start()
    {
        // 현재 오브젝트(BoardManager)의 자식 오브젝트 중 Tilemap을 가져옴
        m_Tilemap = GetComponentInChildren<Tilemap>();

        // 현재 오브젝트(BoardManager)의 자식 오브젝트 중 Grid를 가져옴
        m_Grid = GetComponentInChildren<Grid>();

        // width * height 크기의 2차원 배열을 생성하여 맵의 정보를 저장할 준비를 함
        m_BoardData = new CellData[width, height];

        // 2중 for문을 사용하여 모든 타일을 배치
        for (int y = 0; y < height; ++y) // 세로(y) 방향으로 한 줄씩 탐색
        {
            for (int x = 0; x < width; ++x) // 가로(x) 방향으로 이동하며 타일 배치
            {
                Tile tile; // 현재 위치에 배치할 타일을 저장할 변수
                m_BoardData[x, y] = new CellData(); // 새로운 셀 데이터를 생성하여 배열에 저장

                // 맵의 테두리 부분 (x=0, y=0, x=width-1, y=height-1)은 벽(WallTile)로 설정
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    // 벽 타일 중 랜덤한 타일을 선택하여 배치
                    tile = WallTiles[Random.Range(0, WallTiles.Length)];

                    // 벽은 지나갈 수 없으므로 Passable을 false로 설정
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    // 바닥 타일 중 랜덤한 타일을 선택하여 배치
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];

                    // 바닥은 지나갈 수 있으므로 Passable을 true로 설정
                    m_BoardData[x, y].Passable = true;
                }

                // 선택한 타일을 (x, y) 위치에 배치
                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        // 플레이어를 (1,1) 좌표에 생성 (보드 매니저를 넘겨줘서 맵 정보를 전달)
        player.Spawn(this, new Vector2Int(1, 1));
    }

    // 특정 셀의 위치를 월드 좌표로 변환하는 함수
    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }
}
