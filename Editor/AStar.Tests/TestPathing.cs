using System.Linq;
using NUnit.Framework;

using Assert = UnityEngine.Assertions.Assert;


// Use Window > General > Test Runner to run tests.
namespace AStar.Tests
{
    [TestFixture]
    public class TestPathing
    {
        private byte[,] _grid;

        [SetUp]
        public void SetUp()
        {
            var level = @"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                          XOOOXXXXOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOXXXXOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOXXXXOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOXXXXOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOXXXXOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOXXXXOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOXXXXOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOX
                          XXXXXXXXXXXXXXXXXXXXXXOOOOOOOOOX
                          XXXXXXXXXXXXXXXXXXXXXXOOOOOOOOOX
                          XXXXXXXXXXXXXXXXXXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOXXXXXOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOX
                          XOOOOOOXXXXXXXXXXXXXXXXXXXXXXXXX
                          XOOOOOOXXXXXXXXXXXXXXXXXXXXXXXXX
                          XOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOX
                          XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            _grid = new byte[32, 32];
            var splitLevel = level.Split('\n')
                                  .Select(x => x.Trim())
                                  .ToList();

            for (var x = 0; x < splitLevel.Count; x++)
            {
                for (var y = 0; y < splitLevel[x].Length; y++)
                {
                    if (splitLevel[x][y] != 'X')
                    {
                        _grid[x, y] = 1;
                    }

                }
            }

        }

        [Test]
        public void TestPathingOptions()
        {
            var pathfinderOptions = new PathFinderOptions { PunishChangeDirection = true };

            var pathfinder = new PathFinder(_grid, pathfinderOptions);
            var path = pathfinder.FindPath(new Point(1, 1), new Point(30, 30));
            Helper.Print(_grid, path);
        }

        [Test]
        public void TestPathingEnvironment()
        {
            var pathfinder = new PathFinder(_grid);
            var path = pathfinder.FindPath(new Point(1, 1), new Point(30, 30));
            Helper.Print(_grid, path);

            Assert.AreEqual(path[0].X, 30);
            Assert.AreEqual(path[0].Y, 30);
            Assert.AreEqual(path[1].X, 30);
            Assert.AreEqual(path[1].Y, 29);
            Assert.AreEqual(path[2].X, 30);
            Assert.AreEqual(path[2].Y, 28);
            Assert.AreEqual(path[3].X, 30);
            Assert.AreEqual(path[3].Y, 27);
            Assert.AreEqual(path[4].X, 30);
            Assert.AreEqual(path[4].Y, 26);
            Assert.AreEqual(path[5].X, 30);
            Assert.AreEqual(path[5].Y, 25);
            Assert.AreEqual(path[6].X, 30);
            Assert.AreEqual(path[6].Y, 24);
            Assert.AreEqual(path[7].X, 30);
            Assert.AreEqual(path[7].Y, 23);
            Assert.AreEqual(path[8].X, 30);
            Assert.AreEqual(path[8].Y, 22);
            Assert.AreEqual(path[9].X, 30);
            Assert.AreEqual(path[9].Y, 21);
            Assert.AreEqual(path[10].X, 30);
            Assert.AreEqual(path[10].Y, 20);
            Assert.AreEqual(path[11].X, 30);
            Assert.AreEqual(path[11].Y, 19);
            Assert.AreEqual(path[12].X, 30);
            Assert.AreEqual(path[12].Y, 18);
            Assert.AreEqual(path[13].X, 30);
            Assert.AreEqual(path[13].Y, 17);
            Assert.AreEqual(path[14].X, 30);
            Assert.AreEqual(path[14].Y, 16);
            Assert.AreEqual(path[15].X, 30);
            Assert.AreEqual(path[15].Y, 15);
            Assert.AreEqual(path[16].X, 30);
            Assert.AreEqual(path[16].Y, 14);
            Assert.AreEqual(path[17].X, 30);
            Assert.AreEqual(path[17].Y, 13);
            Assert.AreEqual(path[18].X, 30);
            Assert.AreEqual(path[18].Y, 12);
            Assert.AreEqual(path[19].X, 30);
            Assert.AreEqual(path[19].Y, 11);
            Assert.AreEqual(path[20].X, 30);
            Assert.AreEqual(path[20].Y, 10);
            Assert.AreEqual(path[21].X, 30);
            Assert.AreEqual(path[21].Y, 9);
            Assert.AreEqual(path[22].X, 30);
            Assert.AreEqual(path[22].Y, 8);
            Assert.AreEqual(path[23].X, 30);
            Assert.AreEqual(path[23].Y, 7);
            Assert.AreEqual(path[24].X, 29);
            Assert.AreEqual(path[24].Y, 6);
            Assert.AreEqual(path[25].X, 28);
            Assert.AreEqual(path[25].Y, 6);
            Assert.AreEqual(path[26].X, 27);
            Assert.AreEqual(path[26].Y, 7);
            Assert.AreEqual(path[27].X, 27);
            Assert.AreEqual(path[27].Y, 8);
            Assert.AreEqual(path[28].X, 27);
            Assert.AreEqual(path[28].Y, 9);
            Assert.AreEqual(path[29].X, 27);
            Assert.AreEqual(path[29].Y, 10);
            Assert.AreEqual(path[30].X, 27);
            Assert.AreEqual(path[30].Y, 11);
            Assert.AreEqual(path[31].X, 27);
            Assert.AreEqual(path[31].Y, 12);
            Assert.AreEqual(path[32].X, 27);
            Assert.AreEqual(path[32].Y, 13);
            Assert.AreEqual(path[33].X, 27);
            Assert.AreEqual(path[33].Y, 14);
            Assert.AreEqual(path[34].X, 27);
            Assert.AreEqual(path[34].Y, 15);
            Assert.AreEqual(path[35].X, 27);
            Assert.AreEqual(path[35].Y, 16);
            Assert.AreEqual(path[36].X, 27);
            Assert.AreEqual(path[36].Y, 17);
            Assert.AreEqual(path[37].X, 27);
            Assert.AreEqual(path[37].Y, 18);
            Assert.AreEqual(path[38].X, 27);
            Assert.AreEqual(path[38].Y, 19);
            Assert.AreEqual(path[39].X, 27);
            Assert.AreEqual(path[39].Y, 20);
            Assert.AreEqual(path[40].X, 26);
            Assert.AreEqual(path[40].Y, 21);
            Assert.AreEqual(path[41].X, 25);
            Assert.AreEqual(path[41].Y, 22);
            Assert.AreEqual(path[42].X, 24);
            Assert.AreEqual(path[42].Y, 23);
            Assert.AreEqual(path[43].X, 23);
            Assert.AreEqual(path[43].Y, 24);
            Assert.AreEqual(path[44].X, 22);
            Assert.AreEqual(path[44].Y, 25);
            Assert.AreEqual(path[45].X, 21);
            Assert.AreEqual(path[45].Y, 26);
            Assert.AreEqual(path[46].X, 20);
            Assert.AreEqual(path[46].Y, 27);
            Assert.AreEqual(path[47].X, 19);
            Assert.AreEqual(path[47].Y, 28);
            Assert.AreEqual(path[48].X, 18);
            Assert.AreEqual(path[48].Y, 29);
            Assert.AreEqual(path[49].X, 17);
            Assert.AreEqual(path[49].Y, 28);
            Assert.AreEqual(path[50].X, 16);
            Assert.AreEqual(path[50].Y, 27);
            Assert.AreEqual(path[51].X, 15);
            Assert.AreEqual(path[51].Y, 26);
            Assert.AreEqual(path[52].X, 14);
            Assert.AreEqual(path[52].Y, 25);
            Assert.AreEqual(path[53].X, 13);
            Assert.AreEqual(path[53].Y, 24);
            Assert.AreEqual(path[54].X, 12);
            Assert.AreEqual(path[54].Y, 23);
            Assert.AreEqual(path[55].X, 11);
            Assert.AreEqual(path[55].Y, 22);
            Assert.AreEqual(path[56].X, 10);
            Assert.AreEqual(path[56].Y, 21);
            Assert.AreEqual(path[57].X, 10);
            Assert.AreEqual(path[57].Y, 20);
            Assert.AreEqual(path[58].X, 10);
            Assert.AreEqual(path[58].Y, 19);
            Assert.AreEqual(path[59].X, 10);
            Assert.AreEqual(path[59].Y, 18);
            Assert.AreEqual(path[60].X, 10);
            Assert.AreEqual(path[60].Y, 17);
            Assert.AreEqual(path[61].X, 10);
            Assert.AreEqual(path[61].Y, 16);
            Assert.AreEqual(path[62].X, 10);
            Assert.AreEqual(path[62].Y, 15);
            Assert.AreEqual(path[63].X, 10);
            Assert.AreEqual(path[63].Y, 14);
            Assert.AreEqual(path[64].X, 10);
            Assert.AreEqual(path[64].Y, 13);
            Assert.AreEqual(path[65].X, 10);
            Assert.AreEqual(path[65].Y, 12);
            Assert.AreEqual(path[66].X, 10);
            Assert.AreEqual(path[66].Y, 11);
            Assert.AreEqual(path[67].X, 10);
            Assert.AreEqual(path[67].Y, 10);
            Assert.AreEqual(path[68].X, 10);
            Assert.AreEqual(path[68].Y, 9);
            Assert.AreEqual(path[69].X, 10);
            Assert.AreEqual(path[69].Y, 8);
            Assert.AreEqual(path[70].X, 10);
            Assert.AreEqual(path[70].Y, 7);
            Assert.AreEqual(path[71].X, 10);
            Assert.AreEqual(path[71].Y, 6);
            Assert.AreEqual(path[72].X, 9);
            Assert.AreEqual(path[72].Y, 5);
            Assert.AreEqual(path[73].X, 8);
            Assert.AreEqual(path[73].Y, 4);
            Assert.AreEqual(path[74].X, 7);
            Assert.AreEqual(path[74].Y, 3);
            Assert.AreEqual(path[75].X, 6);
            Assert.AreEqual(path[75].Y, 3);
            Assert.AreEqual(path[76].X, 5);
            Assert.AreEqual(path[76].Y, 3);
            Assert.AreEqual(path[77].X, 4);
            Assert.AreEqual(path[77].Y, 3);
            Assert.AreEqual(path[78].X, 3);
            Assert.AreEqual(path[78].Y, 3);
            Assert.AreEqual(path[79].X, 2);
            Assert.AreEqual(path[79].Y, 2);
            Assert.AreEqual(path[80].X, 1);
            Assert.AreEqual(path[80].Y, 1);
        }
    }
}
