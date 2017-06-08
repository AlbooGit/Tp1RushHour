using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP1PROF;
using SFML;
using SFML.Graphics;

namespace TP1Tests
{
  [TestClass]
  public class TestsVehicle
  {
    [TestMethod]
    public void TestVehicleConstructorVerticalOK_1()
    {
      Vehicle vehicle = new Vehicle(0, 0, 1, 2, 1);

      Assert.AreEqual(0, vehicle.GetBlockOffsetX());
      Assert.AreEqual(0, vehicle.GetBlockOffsetY());
      Assert.AreEqual(1, vehicle.GetWidth());
      Assert.AreEqual(2, vehicle.GetHeight());
      Assert.AreEqual(1, vehicle.GetValue());
    }

    [TestMethod]
    public void TestVehicleConstructorHorizontalOK_1()
    {
      Vehicle vehicle = new Vehicle(2, 3, 2, 1, 1);

      Assert.AreEqual(2, vehicle.GetBlockOffsetX());
      Assert.AreEqual(3, vehicle.GetBlockOffsetY());
      Assert.AreEqual(2, vehicle.GetWidth());
      Assert.AreEqual(1, vehicle.GetHeight());
      Assert.AreEqual(1, vehicle.GetValue());
    }



    // ppoulin
    // A décommenter lorsque mentionné dans l'énoncé
    [TestMethod]
    public void TestVehicleIsClickedOK_1()
    {
      Vehicle vehicle = new Vehicle(0, 0, 1, 2, 1);
      Assert.IsTrue(vehicle.IsClicked(Game.BLOCK_SIZE / 2, Game.BLOCK_SIZE / 2));
    }

    [TestMethod]
    public void TestVehicleIsClickedOK_2()
    {
      Vehicle vehicle = new Vehicle(3, 2, 1, 2, 1);
      Assert.IsTrue(vehicle.IsClicked(3 * Game.BLOCK_SIZE + 1, 2 * Game.BLOCK_SIZE + 2));
    }
    [TestMethod]
    public void TestVehicleIsClickedOK_3()
    {
      Vehicle vehicle = new Vehicle(3, 2, 2, 1, 1);
      Assert.IsTrue(vehicle.IsClicked(4 * Game.BLOCK_SIZE + 1, 2 * Game.BLOCK_SIZE + 2));
    }
    [TestMethod]
    public void TestVehicleIsClickedOK_4()
    {
      Vehicle vehicle = new Vehicle(3, 2, 2, 1, 1);
      Assert.IsFalse(vehicle.IsClicked(7 * Game.BLOCK_SIZE + 1, 2 * Game.BLOCK_SIZE + 2));
    }
    [TestMethod]
    public void TestVehicleIsClickedOK_5()
    {
      Vehicle vehicle = new Vehicle(0, 0, 2, 1, 1);
      Assert.IsTrue(vehicle.IsClicked(0, 0));
    }
    [TestMethod]
    public void TestVehicleIsClickedOK_6()
    {
      Vehicle vehicle = new Vehicle(3, 2, 2, 1, 1);
      Assert.IsFalse(vehicle.IsClicked(-1, -1));
    }

    // ppoulin
    // A décommenter lorsque spécifié dans l'énoncé      
    private const string LEVEL01 = @"0 0 0 0 0 0;
                                     1 1 0 0 0 0;
                                     0 0 0 2 2 2;
                                     0 0 0 0 0 0;
                                     0 0 3 3 3 0;
                                     0 0 0 0 0 0;";
    
    private const string LEVELTEST = @"0 0 0 0 0 0;
                                       0 1 0 0 0 0;
                                       0 1 0 2 2 2;
                                       0 0 0 0 0 0;
                                       0 0 3 3 3 0;
                                       0 0 0 0 0 0;";
    [TestMethod]
    public void TestVehicleMoveRight_01()
    {
      Grid grid = new Grid();
      Assert.IsTrue(grid.LoadFromMemory(LEVEL01));
      Vehicle vehicle = grid.GetVehicle(1);
      // On simule un clic juste à droite du centre
      int posX = (vehicle.GetBlockOffsetX() * Game.BLOCK_SIZE) + (vehicle.GetWidth() * Game.BLOCK_SIZE / 2) + 1;
      // En plein centre verticalement
      int posY = (vehicle.GetBlockOffsetY() * Game.BLOCK_SIZE) + (vehicle.GetHeight() * Game.BLOCK_SIZE / 2);

      // On s'assure que le vehicule peut bouger...
      Assert.IsTrue(vehicle.Move(grid, posX, posY));
      // ... puis on valide que la position est correct après le déplacement.
      Assert.AreEqual(1, vehicle.GetBlockOffsetX());
      Assert.AreEqual(1, vehicle.GetBlockOffsetY());
    }


    // ppoulin
    // Écrivez les tests TestVehicleMoveLeft_01, TestVehicleMoveUp_01 et TestVehicleMoveDown_01 ici
    [TestMethod]
    public void TestVehicleMoveLeft_01()
    {
      Grid grid = new Grid();
      Assert.IsTrue(grid.LoadFromMemory(LEVEL01));
      Vehicle vehicle = grid.GetVehicle(1);
      // On simule un clic juste à gauche du centre
      int posX = (vehicle.GetBlockOffsetX() * Game.BLOCK_SIZE) + (vehicle.GetWidth() * Game.BLOCK_SIZE / 2) - 1;
      // En plein centre verticalement
      int posY = (vehicle.GetBlockOffsetY() * Game.BLOCK_SIZE) + (vehicle.GetHeight() * Game.BLOCK_SIZE / 2);

      // On s'assure que le vehicule ne peut bouger...
      Assert.IsFalse (vehicle.Move(grid, posX, posY));
      // ... puis on valide que la position est correct après le déplacement.
      Assert.AreEqual(0, vehicle.GetBlockOffsetX());
      Assert.AreEqual(1, vehicle.GetBlockOffsetY());
    }


    [TestMethod]
    public void TestVehicleMoveDown_01()
    {
      Grid grid = new Grid();
      Assert.IsTrue(grid.LoadFromMemory(LEVELTEST));
      Vehicle vehicle = grid.GetVehicle(1);
      // On simule un clic juste en bas du centre
      int posX = (vehicle.GetBlockOffsetX() * Game.BLOCK_SIZE) + (vehicle.GetWidth() * Game.BLOCK_SIZE / 2);
      // En plein centre horizontalement
      int posY = (vehicle.GetBlockOffsetY() * Game.BLOCK_SIZE) + (vehicle.GetHeight() * Game.BLOCK_SIZE / 2) + 1;

      // On s'assure que le vehicule peut bouger...
      Assert.IsTrue(vehicle.Move(grid, posX, posY));
      // ... puis on valide que la position est correct après le déplacement.
      Assert.AreEqual(1, vehicle.GetBlockOffsetX());
      Assert.AreEqual(2, vehicle.GetBlockOffsetY());
    }
    [TestMethod]
    public void TestVehicleMoveUp_01()
    {
      Grid grid = new Grid();
      Assert.IsTrue(grid.LoadFromMemory(LEVELTEST));
      Vehicle vehicle = grid.GetVehicle(1);
      // On simule un clic juste en haut du centre
      int posX = (vehicle.GetBlockOffsetX() * Game.BLOCK_SIZE) + (vehicle.GetWidth() * Game.BLOCK_SIZE / 2);
      // En plein centre horizontalement
      int posY = (vehicle.GetBlockOffsetY() * Game.BLOCK_SIZE) + (vehicle.GetHeight() * Game.BLOCK_SIZE / 2) - 1;

      // On s'assure que le vehicule peut bouger...
      Assert.IsTrue(vehicle.Move(grid, posX, posY));
      // ... puis on valide que la position est correct après le déplacement.
      Assert.AreEqual(1, vehicle.GetBlockOffsetX());
      Assert.AreEqual(0, vehicle.GetBlockOffsetY());
    }
  }
}
