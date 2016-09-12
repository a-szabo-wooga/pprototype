using pPrototype;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class LevelPlayModelTest
{
	[Test]
	public void CreateEmptyLevelPlayModelTest()
	{
		var model = LevelPlayModelFactory.Create();
		Assert.IsTrue(model != null && model.CurrentState == LevelPlayState.Unstarted);
	}
}

[TestFixture]
public class LevelParserTest
{
	[Test]
	public void SimpleTest()
	{
		var data = "RcwrBGoy";
		var level = new List<string> { data };
		var levelPlayModel = LevelPlayModelFactory.Create(level, 1, 1);

		Assert.IsTrue(levelPlayModel.CurrentState == LevelPlayState.Unstarted);
		Assert.IsFalse(levelPlayModel.CellCorrect(0, 0));

		levelPlayModel.MakeAMove(new Move(0, 0, MoveInput.SwipeRight));

		Assert.IsTrue(levelPlayModel.CurrentState == LevelPlayState.Ongoing);
		Assert.IsFalse(levelPlayModel.CellCorrect(0,0));

		levelPlayModel.MakeAMove(new Move(0, 0, MoveInput.SwipeRight));

		Assert.IsTrue(levelPlayModel.CellCorrect(0, 0));
		Assert.IsTrue(levelPlayModel.CurrentState == LevelPlayState.Won);
	}

	[Test]
	public void DoubleTest()
	{
		var cellOne = "RcwrBGoy";
		var cellTwo = "RcrwBGoy";

		var level = new List<string> { cellOne, cellTwo };
		var lpm = LevelPlayModelFactory.Create(level, 2, 1);

		Assert.IsTrue(lpm.CurrentState == LevelPlayState.Unstarted);

		lpm.MakeAMove(new Move(0, 0, MoveInput.SwipeRight));
		lpm.MakeAMove(new Move(0, 0, MoveInput.SwipeRight));

		Assert.IsTrue(lpm.CurrentState == LevelPlayState.Ongoing);

		lpm.MakeAMove(new Move(1, 0, MoveInput.SwipeUp));
		lpm.MakeAMove(new Move(1, 0, MoveInput.SwipeUp));

		Assert.IsTrue(lpm.CurrentState == LevelPlayState.Won);
	}
}

[TestFixture]
public class LaneLockTest
{
	[Test]
	public void SimpleTwoLaneTest()
	{
		var cellOne = "RcWWrwRW";
		var cellTwo = "RcWWrwRW";

		var level = new List<string> { cellOne, cellTwo };
		var lpm = LevelPlayModelFactory.Create(level, 2, 1);

		lpm.LockColumns(0, 1);

		Assert.IsTrue(lpm.CurrentState == LevelPlayState.Unstarted);

		lpm.MakeAMove(new Move(0, 0, MoveInput.SwipeDown));

		Assert.IsTrue(lpm.CurrentState == LevelPlayState.Won);
	}

	[Test]
	public void SimpleTwoLaneRowTest()
	{
		var cellOne = "RcWWrwWW";
		var cellTwo = "RcWWrwWW";

		var level = new List<string> { cellOne, cellTwo };
		var lpm = LevelPlayModelFactory.Create(level, 1, 2);

		lpm.LockRows(0, 1);

		Assert.IsTrue(lpm.CurrentState == LevelPlayState.Unstarted);

		lpm.MakeAMove(new Move(0, 1, MoveInput.SwipeRight));

		Assert.IsTrue(lpm.CurrentState == LevelPlayState.Won);
	}

	[Test]
	public void Locked2x2Test()
	{
		var cellOne = "RcWWrwWW";
		var cellTwo = "RcWWrwWW";
		var cellThree = "RcWWrwWW";
		var cellFour = "RcWWrwWW";

		var level = new List<string> { cellOne, cellTwo, cellThree, cellFour };
		var lpm = LevelPlayModelFactory.Create(level, 2, 2);

		lpm.LockRows(0, 1);
		lpm.LockColumns(0, 1);

		Assert.IsTrue(lpm.CurrentState == LevelPlayState.Unstarted);

		lpm.MakeAMove(new Move(0, 1, MoveInput.SwipeRight));

		Assert.IsTrue(lpm.CurrentState == LevelPlayState.Won);
	}
}

[TestFixture]
public class BackgroundModelTest
{
	[Test]
	public void CreateSingleColourBackgroundTest()
	{
		var bg = new BackgroundModel(columns: 1, rows: 1, colours: new Colour[] { Colour.Red });

		Colour colour;
		var tryGet = bg.TryGet(0, 0, out colour);

		Assert.IsTrue(tryGet == true && colour == Colour.Red);
	}
}

[TestFixture]
public class CubeModelTest
{
	[Test]
	public void DefaultCubeModelSetupTest()
	{
		var cube = new CubeModel();
		Assert.IsTrue(cube.Front == Colour.Red);
	}

	[Test]
	public void SimpleCubeParserTest()
	{
		var cube = LevelPlayModelFactory.CreateCubeFromConfig("RcWWrrWW");
		Assert.IsTrue(IsShowing(cube, Colour.White));
	}

	[Test]
	public void LockedCubeParserTest()
	{
		var cube = LevelPlayModelFactory.CreateCubeFromConfig("RcWWrrWW_tbrl");

		Assert.IsTrue(cube.IsLocked(MoveInput.SwipeDown));
	}


	[Test]
	public void HLockedCubeRotationTest()
	{
		var cube = new CubeModel(front: Colour.Red, back: Colour.White, left: Colour.White, 
								 right: Colour.White, top: Colour.White, bottom: Colour.White);

		cube.Lock(MoveInput.SwipeLeft, MoveInput.SwipeRight);

		cube.Update(MoveInput.SwipeLeft);

		Assert.IsTrue(IsShowing(cube, Colour.Red));

		cube.Update(MoveInput.SwipeRight);

		Assert.IsTrue(IsShowing(cube, Colour.Red));

		cube.UnLock(MoveInput.SwipeLeft);
		cube.Update(MoveInput.SwipeLeft);

		Assert.IsTrue(IsShowing(cube, Colour.White));
	}

	[Test]
	public void ComplicatedRotationTest()
	{
		var cube = new CubeModel(front: Colour.Red, back: Colour.Green, left: Colour.Yellow, right: Colour.Blue, top: Colour.White, bottom: Colour.Orange);

		var correct = IsShowing(cube, Colour.Red);

		cube.Update(MoveInput.SwipeRight);	correct &= IsShowing(cube, Colour.Yellow);
		cube.Update(MoveInput.SwipeDown);	correct &= IsShowing(cube, Colour.White);
		cube.Update(MoveInput.SwipeLeft);	correct &= IsShowing(cube, Colour.Red);
		cube.Update(MoveInput.SwipeUp);		correct &= IsShowing(cube, Colour.Yellow);

		Assert.IsTrue(correct);
	}

	[Test]
	public void ComplicatedRotationTestTwo()
	{
		var cube = new CubeModel(front: Colour.Red, back: Colour.Green, left: Colour.Yellow, 
								right: Colour.Blue, top: Colour.White, bottom: Colour.Orange);

		var correct = IsShowing(cube, Colour.Red);

		cube.Update(MoveInput.SwipeUp);		correct &= IsShowing(cube, Colour.Orange);
		cube.Update(MoveInput.SwipeDown);	correct &= IsShowing(cube, Colour.Red);
		cube.Update(MoveInput.SwipeDown);	correct &= IsShowing(cube, Colour.White);
		cube.Update(MoveInput.SwipeLeft);	correct &= IsShowing(cube, Colour.Blue);
		cube.Update(MoveInput.SwipeLeft);	correct &= IsShowing(cube, Colour.Orange);
		cube.Update(MoveInput.SwipeLeft);	correct &= IsShowing(cube, Colour.Yellow);
		cube.Update(MoveInput.SwipeUp);		correct &= IsShowing(cube, Colour.Red);

		Assert.IsTrue(correct);
	}

	private bool IsShowing(CubeModel cube, Colour colour)
	{
		return cube.Front == colour;
	}

	[Test]
	public void SwipeRightTest()
	{
		var cube = new CubeModel();

		Assert.IsTrue(cube.Front == Colour.Red);

		cube.Update(MoveInput.SwipeRight);

		Assert.IsTrue(cube.Front == Colour.Green && 
					  cube.Back == Colour.Yellow &&
					  cube.Right == Colour.Red &&
					  cube.Left == Colour.Red);
	}

	[Test]
	public void SwipeLeftTest()
	{
		var cube = new CubeModel();

		Assert.IsTrue(cube.Front == Colour.Red);

		cube.Update(MoveInput.SwipeLeft);

		Assert.IsTrue(cube.Front == Colour.Yellow &&
					  cube.Back == Colour.Green &&
					  cube.Right == Colour.Red &&
					  cube.Left == Colour.Red);
	}

	[Test]
	public void SwipeUpTest()
	{
		var cube = new CubeModel();

		Assert.IsTrue(cube.Front == Colour.Red);

		cube.Update(MoveInput.SwipeUp);

		Assert.IsTrue(cube.Front == Colour.White &&
					  cube.Back == Colour.Blue &&
					  cube.Top == Colour.Red &&
					  cube.Bottom == Colour.Red);
	}

	[Test]
	public void SwipeDownTest()
	{
		var cube = new CubeModel();

		Assert.IsTrue(cube.Front == Colour.Red);

		cube.Update(MoveInput.SwipeDown);

		Assert.IsTrue(cube.Front == Colour.Blue &&
					  cube.Back == Colour.White &&
					  cube.Top == Colour.Red &&
					  cube.Bottom == Colour.Red);
	}
}
