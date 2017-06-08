using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace TP1PROF
{
  /// <summary>
  /// Classe représentant le concept de véhicule dans le jeu de Rush hour
  /// </summary>
  public class Vehicle
  {
    //  #region Propriétés
    // ppoulin
    // Ajoutez ici les déclarations des propriétés manquantes selon le diagramme de classes.
    private int blockOffsetX = 0;
    private int blockOffsetY = 0;
    private int value;


    // A décommenter lorsque la constante NB_MAX_VEHICULES aura été déclarée dans la classe Game
    /// <summary>
    /// Toutes les couleurs possibles pour les véhicules
    /// </summary>
    public static readonly Color[] VEHICLE_COLORS = new Color[Game.NB_MAX_VEHICLES + 1] { Color.Black, Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Magenta };



    #region Propriétés SFML pour l'affichage
    private readonly RectangleShape vehicleShape = null;
    #endregion
    //  #endregion

    /// <summary>
    /// Constructeur de la class Vehicle
    /// </summary>
    /// <param name="blockOffsetX">Décalage en largeur du coin supérieur gauche du véhicule par rapport au coin
    /// supérieur gauche de la grille de jeu</param>
    /// <param name="blockOffsetY">Décalage en hauteur du coin supérieur gauche du véhicule par rapport au coin
    /// supérieur gauche de la grille de jeu</param>
    /// <param name="width">Largeur du véhicule. Entre 1 (min) et 3 (max)</param>
    /// <param name="height">Hauteur du véhicule. Entre 1 (min) et 3 (max)</param>
    /// <param name="value">Identifiant associé au véhicule. Sert (entre autres) à déterminer la couleur du véhicule</param>
    public Vehicle(int blockOffsetX, int blockOffsetY, int width, int height, int value)
    {
      // ppoulin
      // A compléter
      // Assignation de toutes les propriétés
      this.blockOffsetY = blockOffsetY;
      this.blockOffsetX = blockOffsetX;
      this.value = value;

      // ppoulin
      // A décommenter lorsque les constantes auront été déclarées dans la classe Game.
      // Incluant les propriétés SFML
      vehicleShape = new RectangleShape(new Vector2f(width * Game.BLOCK_SIZE, height * Game.BLOCK_SIZE));
      vehicleShape.FillColor = VEHICLE_COLORS[value];
    }

    /// <summary>
    /// Retourne l'identifiant associé au véhicule
    /// </summary>
    /// <returns>L'identifiant associé au véhicule</returns>
    // ppoulin
    // Écrivez ici le code de la méthode GetValue
    public int GetValue()
    {
      return value;
    }
    /// <summary>
    /// Retourne le décalage en X par rapport au coin supérieur gauche de la planche de jeu.
    /// </summary>
    /// <returns>Le décalage en X par rapport au coin supérieur gauche de la planche de jeu</returns>
    // ppoulin
    // Écrivez ici le code de la méthode GetBlockOffsetX
    public int GetBlockOffsetX()
    {
      return blockOffsetX;
    }



    /// <summary>
    /// Permet de modifier la position en X du véhicule. Usage interne seulement.
    /// </summary>
    /// <param name="newOffset">La nouvelle position en X.</param>
    // ppoulin
    // Écrivez ici le code de la méthode SetBlockOffsetX
    private int SetBlockOffsetX(int newOffset)
    {
      blockOffsetX = newOffset;
      return blockOffsetX;
    }



    /// <summary>
    /// Retourne le décalage en Y par rapport au coin supérieur gauche de la planche de jeu.
    /// </summary>
    /// <returns>Le décalage en Y par rapport au coin supérieur gauche de la planche de jeu</returns>
    // ppoulin
    // Écrivez ici le code de la méthode GetBlockOffsetY
    public int GetBlockOffsetY()
    {
      return blockOffsetY;
    }



    /// <summary>
    /// Permet de modifier la position en Y du véhicule. Usage interne seulement.
    /// </summary>
    /// <param name="newOffset">La nouvelle position en Y.</param>
    // ppoulin
    // Écrivez ici le code de la méthode SetBlockOffsetY
    private int SetBlockOffsetY(int newOffset)
    {
      blockOffsetY = newOffset;
      return blockOffsetY;
    }



    /// <summary>
    /// Retourne la largeur du véhicule. Entre 0 et Game.HORIZONTAL_BLOCK_COUNT
    /// </summary>
    /// <returns>La largeur du véhicule</returns>
    // ppoulin
    // Écrivez ici le code de la méthode GetWidth
    public int GetWidth()
    {
      return (int)vehicleShape.Size.X / Game.BLOCK_SIZE;
    }



    /// <summary>
    /// Retourne la hauteur du véhicule. Entre 0 et Game.VERTICAL_BLOCK_COUNT
    /// </summary>
    /// <returns>La hauteur du véhicule</returns>
    // ppoulin
    // Écrivez ici le code de la méthode GetHeight
    public int GetHeight()
    {
      return (int)vehicleShape.Size.Y / Game.BLOCK_SIZE;
    }




    /// <summary>
    /// Affiche un véhicule à l'écran
    /// </summary>
    /// <param name="window">Le contexte de rendu du jeu</param>
    public void Draw(RenderWindow window)
    {
      // ppoulin
      // A décommenter lorsque les propriétés auront été déclarées

      vehicleShape.Position = new Vector2f(blockOffsetX * Game.BLOCK_SIZE, blockOffsetY * Game.BLOCK_SIZE);
      window.Draw(vehicleShape);
    }


    /// <summary>
    /// Vérifie si le véhicule recouvre la position spécifiée en paramètres
    /// </summary>
    /// <param name="absolutePositionX">La position en X absolue dans l'écran où le joueur a cliqué</param>
    /// <param name="absolutePositionY">La position en X absolue dans l'écran où le joueur a cliqué</param>
    /// <returns>true si la position est par-dessus le véhicule, false sinon</returns>
    // ppoulin
    // Écrivez ici le code de la méthode IsClicked
    public bool IsClicked(int absolutePositionX, int absolutePositionY)
    {
      int vehicleWidth = GetWidth();
      int vehicleHeight = GetHeight();

      if (vehicleWidth == 2)
      {
        if ((blockOffsetX == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY == absolutePositionY / Game.BLOCK_SIZE) || (blockOffsetX + 1 == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY == absolutePositionY / Game.BLOCK_SIZE))
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else if (vehicleWidth == 3)
      {
        if ((blockOffsetX == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY == absolutePositionY / Game.BLOCK_SIZE) ||
                           (blockOffsetX + 1 == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY == absolutePositionY / Game.BLOCK_SIZE) ||
                           blockOffsetX + 2 == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY == absolutePositionY / Game.BLOCK_SIZE)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else if (vehicleHeight == 2)
      {
        if ((blockOffsetX == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY == absolutePositionY / Game.BLOCK_SIZE) || (blockOffsetX == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY + 1 == absolutePositionY / Game.BLOCK_SIZE))
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else if (vehicleHeight == 3)
      {
        if ((blockOffsetX == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY == absolutePositionY / Game.BLOCK_SIZE) ||
                           (blockOffsetX == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY + 1 == absolutePositionY / Game.BLOCK_SIZE) ||
                           blockOffsetX == absolutePositionX / Game.BLOCK_SIZE && blockOffsetY + 2 == absolutePositionY / Game.BLOCK_SIZE)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Déplace un véhicule
    /// </summary>
    /// <param name="grid">La grille de jeu à utiliser pour déterminer si le déplacement est possible</param>
    /// <param name="absolutePositionX">La position en X absolue dans l'écran où le joueur a cliqué</param>
    /// <param name="absolutePositionY">La position en Y absolue dans l'écran où le joueur a cliqué</param>
    /// <returns>true si le déplacement était possible, false sinon.</returns>
    public bool Move(Grid grid, int absolutePositionX, int absolutePositionY)
    {
      bool canMove = false;

      // Essayer de bouger dans la direction du vehicule
      // ppoulin
      // A décommenter
      if (GetWidth() == 1)
      {
        // Le vehicule est vertical
        // Trouver si l'on doit bouger en haut ou en bas

        // Centre du vehicule
        // ppoulin
        // A décommenter
        int posCentre = (GetHeight() / 2 + GetBlockOffsetY()) * Game.BLOCK_SIZE;
        if (absolutePositionY < posCentre)
        {
          // Par en haut
          // S'il n'y a rien là où on veut aller...
          if (grid.GetElementAt(GetBlockOffsetY() - 1, GetBlockOffsetX()) == 0)
          {
            // On bouge vers le haut!
            // Effacer l'ancien dans la grid
            grid.EraseVehicle(this);
            // Décalage vers le haut
            SetBlockOffsetY(GetBlockOffsetY() - 1);
            // Specifier la nouvelle position
            grid.SetVehicle(this);
            canMove = true;
          }
        }
        else if (absolutePositionY > posCentre && GetHeight() == 3)
        {
          if (grid.GetElementAt(GetBlockOffsetY() + 3, GetBlockOffsetX()) == 0)
          {
            // On bouge vers le bas
            // Effacer l'ancien dans la grid
            grid.EraseVehicle(this);
            // Décalage vers le bas
            SetBlockOffsetY(GetBlockOffsetY() + 1);
            // Specifier la nouvelle position
            grid.SetVehicle(this);
            canMove = true;
          }

        }
        else
        {
          if (grid.GetElementAt(GetBlockOffsetY() + 2, GetBlockOffsetX()) == 0)
          {
            // On bouge vers le bas
            // Effacer l'ancien dans la grid
            grid.EraseVehicle(this);
            // Décalage vers le bas
            SetBlockOffsetY(GetBlockOffsetY() + 1);
            // Specifier la nouvelle position
            grid.SetVehicle(this);
            canMove = true;
          }
        }
        // A compléter vers le bas, vers la gauche et vers la droite.  Les instructions requises sont très similaires à 
        // celles ci-haut.
      }

      else
      {

        int posCentre = (GetWidth() / 2 + GetBlockOffsetX()) * Game.BLOCK_SIZE;
        if (absolutePositionX > posCentre && GetWidth() == 3)
        {
          // Par droite
          // S'il n'y a rien là où on veut aller...
          if (grid.GetElementAt(GetBlockOffsetY(), GetBlockOffsetX() + 3) == 0)
          {
            // On bouge vers la droite
            // Effacer l'ancien dans la grid
            grid.EraseVehicle(this);
            // Décalage vers le haut
            SetBlockOffsetX(GetBlockOffsetX() + 1);
            // Specifier la nouvelle position
            grid.SetVehicle(this);
            canMove = true;
          }
        }
        else if (absolutePositionX > posCentre)
        {
          if (grid.GetElementAt(GetBlockOffsetY(), GetBlockOffsetX() + 2) == 0)
          {
            // On bouge vers la droite
            // Effacer l'ancien dans la grid
            grid.EraseVehicle(this);
            // Décalage vers le haut
            SetBlockOffsetX(GetBlockOffsetX() + 1);
            // Specifier la nouvelle position
            grid.SetVehicle(this);
            canMove = true;
          }
        }
        else
        {
          if (grid.GetElementAt(GetBlockOffsetY(), GetBlockOffsetX() - 1) == 0)
          {
            // On bouge vers la gauche
            // Effacer l'ancien dans la grid
            grid.EraseVehicle(this);
            // Décalage vers la gauche
            SetBlockOffsetX(GetBlockOffsetX() - 1);
            // Specifier la nouvelle position
            grid.SetVehicle(this);
            canMove = true;
          }
        }

      }
      return canMove;
    }

  }
}

