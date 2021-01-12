using System;
using System.Collections.Generic;
using NUnit.Framework;

using Assert = UnityEngine.Assertions.Assert;


// Use Window > General > Test Runner to run tests.
namespace AStar.Tests
{
    [TestFixture]
    public class TestPathfinder
    {
        private byte[,] _grid;
        private PathFinder _pathFinder;

        [SetUp]
        public void SetUp()
        {
            _grid = CreateMatrix(8, 8);
            _pathFinder = new PathFinder(_grid);
        }

        [Test]
        public void ShouldPathRectangleGrid()
        {
            var grid = CreateMatrix(3, 5);
            var pathfinder = new PathFinder(grid);

            var path = pathfinder.FindPath(new Point(0, 0), new Point(2, 4));
            Console.WriteLine(Helper.PrintGrid(grid));
            Console.WriteLine(Helper.PrintPath(grid, path));

            Assert.AreEqual(path[0].X, 2);
            Assert.AreEqual(path[0].Y, 4);
            Assert.AreEqual(path[1].X, 2);
            Assert.AreEqual(path[1].Y, 3);
            Assert.AreEqual(path[2].X, 2);
            Assert.AreEqual(path[2].Y, 2);
            Assert.AreEqual(path[3].X, 1);
            Assert.AreEqual(path[3].Y, 1);
            Assert.AreEqual(path[4].X, 0);
            Assert.AreEqual(path[4].Y, 0);
        }

        [Test]
        public void ShouldPathToSelf()
        {
            var path = _pathFinder.FindPath(new Point(1, 1), new Point(1, 1));
            Assert.AreEqual(path.Count, 1);

            var node = path[0];
            Assert.AreEqual(node.X, 1);
            Assert.AreEqual(node.Y, 1);
        }

        [Test]
        public void ShouldPathToAdjacent()
        {
            var path = _pathFinder.FindPath(new Point(1, 1), new Point(2, 1));

            Assert.AreEqual(path.Count, 2);

            var node = path[0];

            Assert.AreEqual(node.X, 2);
            Assert.AreEqual(node.Y, 1);


            node = path[1];
            Assert.AreEqual(node.X, 1);
            Assert.AreEqual(node.Y, 1);


            Console.WriteLine(Helper.PrintGrid(_grid));
            Console.WriteLine(Helper.PrintPath(_grid, path));
        }

        [Test]
        public void ShouldDoSimplePath()
        {
            var path = _pathFinder.FindPath(new Point(1, 1), new Point(4, 2));
            Helper.Print(_grid, path);
            Assert.AreEqual(path.Count, 4);

            var item = path[3];
            Assert.AreEqual(item.X, 1);
            Assert.AreEqual(item.Y, 1);

            item = path[2];
            Assert.AreEqual(item.X, 2);
            Assert.AreEqual(item.Y, 2);

            item = path[1];
            Assert.AreEqual(item.X, 3);
            Assert.AreEqual(item.Y, 2);

            item = path[0];
            Assert.AreEqual(item.X, 4);
            Assert.AreEqual(item.Y, 2);
        }

        [Test]
        public void ShouldDoSimplePathWithNoDiagonal()
        {
            var pathfinderOptions = new PathFinderOptions { Diagonals = false };
            _pathFinder = new PathFinder(_grid, pathfinderOptions);

            var path = _pathFinder.FindPath(new Point(1, 1), new Point(4, 2));
            Helper.Print(_grid, path);
            PrintCoordinates(path);

            Assert.AreEqual(path.Count, 5);

            var item = path[4];
            Assert.AreEqual(item.X, 1);
            Assert.AreEqual(item.Y, 1);

            item = path[3];
            Assert.AreEqual(item.X, 2);
            Assert.AreEqual(item.Y, 1);

            item = path[2];
            Assert.AreEqual(item.X, 3);
            Assert.AreEqual(item.Y, 1);

            item = path[1];
            Assert.AreEqual(item.X, 4);
            Assert.AreEqual(item.Y, 1);

            item = path[0];
            Assert.AreEqual(item.X, 4);
            Assert.AreEqual(item.Y, 2);
        }

        [Test]
        public void ShouldDoSimplePathWithNoDiagonalAroundObstacle()
        {
            var pathfinderOptions = new PathFinderOptions { Diagonals = false };
            _pathFinder = new PathFinder(_grid, pathfinderOptions);

            _grid[2, 0] = 0;
            _grid[2, 1] = 0;
            _grid[2, 2] = 0;

            var path = _pathFinder.FindPath(new Point(1, 1), new Point(4, 2));
            Helper.Print(_grid, path);
            PrintCoordinates(path);

            Assert.AreEqual(path.Count, 7);

            var item = path[6];
            Assert.AreEqual(item.X, 1);
            Assert.AreEqual(item.Y, 1);

            item = path[5];
            Assert.AreEqual(item.X, 1);
            Assert.AreEqual(item.Y, 2);

            item = path[4];
            Assert.AreEqual(item.X, 1);
            Assert.AreEqual(item.Y, 3);

            item = path[3];
            Assert.AreEqual(item.X, 2);
            Assert.AreEqual(item.Y, 3);

            item = path[2];
            Assert.AreEqual(item.X, 3);
            Assert.AreEqual(item.Y, 3);

            item = path[1];
            Assert.AreEqual(item.X, 3);
            Assert.AreEqual(item.Y, 2);

            item = path[0];
            Assert.AreEqual(item.X, 4);
            Assert.AreEqual(item.Y, 2);

        }
        [Test]
        public void ShouldPathAroundObstacle()
        {
            _grid[2, 0] = 0;
            _grid[2, 1] = 0;
            _grid[2, 2] = 0;
            _grid[2, 3] = 0;
            var path = _pathFinder.FindPath(new Point(1, 1), new Point(4, 2));
            Helper.Print(_grid, path);
            Assert.AreEqual(path.Count, 6);

            var item = path[5];
            Assert.AreEqual(item.X, 1);
            Assert.AreEqual(item.Y, 1);

            item = path[4];
            Assert.AreEqual(item.X, 1);
            Assert.AreEqual(item.Y, 2);

            item = path[3];
            Assert.AreEqual(item.X, 1);
            Assert.AreEqual(item.Y, 3);

            item = path[2];
            Assert.AreEqual(item.X, 2);
            Assert.AreEqual(item.Y, 4);

            item = path[1];
            Assert.AreEqual(item.X, 3);
            Assert.AreEqual(item.Y, 3);

            item = path[0];
            Assert.AreEqual(item.X, 4);
            Assert.AreEqual(item.Y, 2);
        }

        [Test]
        public void ShouldFindNoPath()
        {
            _grid[2, 0] = 0;
            _grid[2, 1] = 0;
            _grid[2, 2] = 0;
            _grid[2, 3] = 0;
            _grid[2, 4] = 0;
            _grid[2, 5] = 0;
            _grid[2, 6] = 0;
            _grid[2, 7] = 0;
            var path = _pathFinder.FindPath(new Point(1, 1), new Point(4, 2));
            Assert.AreEqual(path, null);
        }

        private static byte[,] CreateMatrix(int height, int width)
        {
            var mMatrix = new byte[height, width];

            for (var row = 0; row < mMatrix.GetLength(0); row++)
            {

                for (var column = 0; column < mMatrix.GetLength(1); column++)
                {
                    mMatrix[row, column] = 1;
                }
            }

            return mMatrix;
        }

        private static void PrintCoordinates(List<PathFinderNode> path)
        {
            foreach (var node in path)
            {
                Console.WriteLine(node.X);
                Console.WriteLine(node.Y);
                Console.WriteLine(Environment.NewLine);
            }
        }

    }
}
