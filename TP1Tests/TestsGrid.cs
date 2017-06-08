using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP1PROF;


namespace TP1Tests
{
  [TestClass]
  public class TestsGrid
  {
    // ppoulin
    // A décommenter lorsque la méthode InitGrid aura été codée.
    [TestMethod]
    public void TestGridConstructor_01()
    {


      Grid grid = new Grid();

      for (int i = 0; i < Game.VERTICAL_BLOCK_COUNT; i++)
      {
        for (int j = 0; j < Game.HORIZONTAL_BLOCK_COUNT; j++)
        {
          Assert.AreEqual(0, grid.GetElementAt(i, j));
        }
      }
    }



    // ppoulin
    // A décommenter lorsque la méthode LoadFromMemory aura été codée
    [TestMethod]
    public void TestGridLoadFromMemoryPASOK_01()
    {
      Grid grid = new Grid();
      Assert.IsFalse(grid.LoadFromMemory(""));
    }

    private const string LEVEL_PASOK01 = @"0 0 0 0 0 0;
                                           1 1 0 0 0 0;
                                           0 0 0 0 0 0;
                                           0 0 3 3 3 0;
                                           0 0 0 0 0 0;";
    [TestMethod]
    public void TestGridLoadFromMemoryPASOK_02()
    {
      Grid grid = new Grid();
      Assert.IsFalse(grid.LoadFromMemory(LEVEL_PASOK01));
    }

    private const string LEVEL_PASOK02 = @"0 0 0 0 0 0;
                                           1 1 0 0 0 0;
                                           1 1 0 0 0 0;
                                           0 0 0 0 0 0;
                                           0 0 3 3 3 0;
                                             0 0 0 0 0;";
    [TestMethod]
    public void TestGridLoadFromMemoryPASOK_03()
    {
      Grid grid = new Grid();
      Assert.IsFalse(grid.LoadFromMemory(LEVEL_PASOK02));
    }

    private const string LEVEL01 = @"0 0 0 0 0 0;
                                     1 1 0 0 0 0;
                                     0 0 0 2 2 2;
                                     0 0 0 0 0 0;
                                     0 0 3 3 3 0;
                                     0 0 0 0 0 0;";
    [TestMethod]
    public void TestGridLoadFromMemoryOK_01()
    {
      Grid grid = new Grid();
      Assert.IsTrue(grid.LoadFromMemory(LEVEL01));
    }

    [TestMethod]
    public void TestGridLoadFromMemoryOK_02()
    {
      Grid grid = new Grid();
      Assert.IsTrue(grid.LoadFromMemory(LEVEL01));

      Assert.AreEqual(1, grid.GetElementAt(1, 0));
      Assert.AreEqual(1, grid.GetElementAt(1, 1));

      Assert.AreEqual(2, grid.GetElementAt(2, 3));
      Assert.AreEqual(2, grid.GetElementAt(2, 4));
      Assert.AreEqual(2, grid.GetElementAt(2, 5));

      Assert.AreEqual(3, grid.GetElementAt(4, 2));
      Assert.AreEqual(3, grid.GetElementAt(4, 3));
      Assert.AreEqual(3, grid.GetElementAt(4, 4));
    }



    // ppoulin
    // A décommenter lorsque la méthode GetVehicle aura été codée
    [TestMethod]
    public void TestGridGetVehicleOK_01()
    {
      Grid grid = new Grid();
      Assert.IsTrue(grid.LoadFromMemory(LEVEL01));
      Vehicle vehicle = grid.GetVehicle(1);
      Assert.IsNotNull(vehicle);
      Assert.AreEqual(1, vehicle.GetBlockOffsetY());
      Assert.AreEqual(0, vehicle.GetBlockOffsetX());
      Assert.AreEqual(2, vehicle.GetWidth());
      Assert.AreEqual(1, vehicle.GetHeight());
    }

    private const string LEVEL02 = @"0 0 0 0 0 0;
                                     0 1 0 0 0 0;
                                     0 1 0 2 2 2;
                                     0 0 0 0 0 0;
                                     0 0 3 3 3 0;
                                     0 0 0 0 0 0;";
    [TestMethod]
    public void TestGridGetVehicleOK_02()
    {
      Grid grid = new Grid();
      Assert.IsTrue(grid.LoadFromMemory(LEVEL02));
      Vehicle vehicle = grid.GetVehicle(1);
      Assert.IsNotNull(vehicle);
      Assert.AreEqual(1, vehicle.GetBlockOffsetY());
      Assert.AreEqual(1, vehicle.GetBlockOffsetX());
      Assert.AreEqual(1, vehicle.GetWidth());
      Assert.AreEqual(2, vehicle.GetHeight());
    }

    [TestMethod]
    public void TestGridGetVehiclePasOK_01()
    {
      Grid grid = new Grid();
      Assert.IsTrue(grid.LoadFromMemory(LEVEL02));
      Vehicle vehicle = grid.GetVehicle(4);
      Assert.IsNull(vehicle);
    }
  }
}
