using UnityEngine;
using UnityEditor;
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
