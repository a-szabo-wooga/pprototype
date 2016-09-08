﻿using System.Collections.Generic;
using UnityEngine;

namespace pPrototype
{
	public class LevelManagerScript : MonoBehaviour
	{
		public const float BGSIZE = 2.2f;
		public const float CAM_DISTANCE = -9.5f;

		public const float CAM_POS_X = 5.4f;
		public const float CAM_POS_Y = 2.6f;
		public const float CAM_POS_Z = -10f;

		public const float CAM_ROT_X = 20f;
		public const float CAM_ROT_Y = 342f;
		public const float CAM_ROT_Z = 2.2f;

		public CellScript CellPrefab;
		public GameObject CellContainer;

		private Dictionary <int, CellScript> _cells;

		public void Setup(LevelPlayModel lpm)
		{
			DeleteExistingChildren();
			CreateNewContainer();
			SpawnCells(lpm);
			PositionCamera(lpm);
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

		public void FakeSwipe(PlayerMove move, float magnitude)
		{
			if (move != null)
			{
				foreach (var id in move.UpdatedIndices)
				{
					_cells[id].FakeSwipe(move.Input, magnitude);
				}
			}
		}

		private void PositionCamera(LevelPlayModel lpm)
		{
			Camera.main.transform.position = new Vector3(CAM_POS_X, CAM_POS_Y, CAM_POS_Z);
			Camera.main.transform.rotation = Quaternion.Euler(new Vector3(CAM_ROT_X, CAM_ROT_Y, CAM_ROT_Z));
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
				SetupCellData(column, row, cell, bgColour, lpm.Foreground.GetCubeModelOrNull(column, row));
				_cells[lpm.Background.GetID(column, row)] = cell;
			}
		}

		private void SetupCellData(int column, int row, CellScript cell, Colour bgColour, CubeModel cube)
		{
			cell.Setup(column, row, bgColour, cube);
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