using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Cell[,] _map;

    public Cell[,] Map => _map;
   

    public void Awake()
    {
        
        List<Cell> cells = new List<Cell>();
        int maxCellX = 0;
        int maxCellY = 0;
        cells.AddRange(GetComponentsInChildren<Cell>());
        foreach(Cell cell in cells)
        {
            if(cell.transform.localPosition.x> maxCellX)
            {
                maxCellX = (int)cell.transform.localPosition.x;
            }
            if (cell.transform.localPosition.z > maxCellY)
            {
                maxCellY = (int)cell.transform.localPosition.z;
            }
        }
        maxCellX++;
        maxCellY++;

         _map = new Cell[maxCellX, maxCellY];

        if (cells.Count >= maxCellX * maxCellY)
        {
            for (int i = 0; i < maxCellX * maxCellY; i++)
            {
                
                _map[(int)cells[0].transform.localPosition.x, (int)cells[0].transform.localPosition.z] = cells[0];
                cells.Remove(cells[0]);
            }
        }
    }
    public bool CheckingExitFromMap(Vector3 CellPosition)//повозмодности периименовать
    {
        bool[] checks = new bool[4];
        checks[0] = CellPosition.x >= 0;
        checks[1] = CellPosition.x < Map.GetLength(0);
        checks[2] = CellPosition.z >= 0;
        checks[3] = CellPosition.z < Map.GetLength(1);

        return checks[0] && checks[1] && checks[2] && checks[3];
    }

    public bool CanWalkCell(Vector3 CellPosition, out Cell targetCell)
    {

        if (CheckingExitFromMap(CellPosition))
        {


            targetCell = _map[(int)CellPosition.x, (int)CellPosition.z];
            if (targetCell != null)
            {
                if (targetCell.CanPass == true)
                {
                    return true;
                }
            }

            return false;
        }
        targetCell = null;
        return false;
    }
}
