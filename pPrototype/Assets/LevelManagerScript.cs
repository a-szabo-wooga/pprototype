using System.Collections.Generic;
using UnityEngine;

namespace pPrototype
{
	public class LevelManagerScript : MonoBehaviour
	{
		public const float BGSIZE = 2.2f;
		public const float CAM_DISTANCE = -10f;

		public CellScript CellPrefab;
		public GameObject CellContainer;

		private Dictionary <int, CellScript> _cells;

		public void Setup(LevelPlayModel lpm)
		{
			DeleteExistingChildren();
			CreateNewContainer();
			SpawnCells(lpm);
			CenterCamera(lpm);
		}

		public void Refresh(LevelPlayModel model)
		{
			var lastMove = model.GetLastMove();

			if (lastMove != null)
			{
				foreach (var id in lastMove.UpdatedIndices)
				{
					_cells[id].Refresh(lastMove.Input);
				}
			}
		}

		private void CenterCamera(LevelPlayModel lpm)
		{
			var width = lpm.Background.Columns;
			var height = lpm.Background.Rows;

			var cameraPositionX = (width - 1) * (BGSIZE / 2f);
			var cameraPositionY = (height - 1) * (BGSIZE / 2f);

			Camera.main.transform.position = new Vector3(cameraPositionX, -cameraPositionY, CAM_DISTANCE);
		}

		private void SpawnCells(LevelPlayModel lpm)
		{
			for (int i = 0; i < lpm.Background.Columns; ++i)
			{
				for (int j = 0; j < lpm.Background.Rows; ++j)
				{
					SpawnCell(i, j, lpm);
				}
			}
		}

		private void SpawnCell(int column, int row, LevelPlayModel lpm)
		{
			Colour bgColour;

			if (lpm.Background.TryGet(column, row, out bgColour))
			{
				var cell = Object.Instantiate<CellScript>(CellPrefab);
				SetCellPosition(cell, column, row);
				SetupCellData(cell, bgColour, lpm.Foreground.GetCubeModelOrNull(column, row));
				_cells[lpm.Background.GetID(column, row)] = cell;
			}
		}

		private void SetupCellData(CellScript cell, Colour bgColour, CubeModel cube)
		{
			cell.Setup(bgColour, cube);
		}

		private void SetCellPosition(CellScript cell, int column, int row)
		{
			cell.transform.name = string.Format("Cell_{0}_{1}", column, row);
			cell.transform.SetParent(CellContainer.transform);
			cell.transform.position = new Vector3(column * BGSIZE, -row * BGSIZE, 0f);
		}

		private void CreateNewContainer()
		{
			CellContainer = new GameObject("CellContainer");
			CellContainer.gameObject.transform.SetParent(this.transform);
		}

		private void DeleteExistingChildren()
		{
			_cells = new Dictionary<int, CellScript>();

			if (CellContainer != null)
			{
				Destroy(CellContainer.gameObject);
			}
		}
	}
}