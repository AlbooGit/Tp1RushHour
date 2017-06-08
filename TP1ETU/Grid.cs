using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TP1PROF
{
  /// <summary>
  /// Représente le concept de grille de jeu logique dans le jeu de Rush hour
  /// Contient les positions des véhicules
  /// </summary>
  public class Grid
  {
    /// <summary>
    /// Le tableau logique contenant les éléments du jeu
    /// Chaque élément du tableau doit être entre 0 (rien) et Game.NB_MAX_VEHICULES.
    /// </summary>

    // ppoulin
    // A décommenter lorsque les constantes de la classe Game auront été codées    
    private int[,] elements = new int[Game.VERTICAL_BLOCK_COUNT, Game.HORIZONTAL_BLOCK_COUNT];

    /// <summary>
    /// Constructeur de la grille de jeu
    /// </summary>
    public Grid()
    {
      InitGrid();
    }

    /// <summary>
    /// Méthode interne permettant de réinitialiser la grille à 0 partout.
    /// </summary>
    private void InitGrid()
    {
      // ppoulin
      // A compléter pour initialiser le contenu du tableau elements à 0 partout.
      for (int i = 0; i < elements.GetLength(0); i++)
      {
        for (int j = 0; j < elements.GetLength(1); j++)
        {
          elements[i, j] = 0;
        }
      }
      // N'oubliez pas de décommenter le test approprié dans le fichier TestsGrid du projet TP1Tests
    }

    /// <summary>
    /// Permet de charger un tableau de jeu à partir d'une chaîne de caractères
    /// en mémoire. La chaine doit contenir 6 lignes se terminant toutes par ;
    /// Chaque élément sur chaque ligne doit être séparé par un espace.
    /// </summary>
    /// <param name="fileContent">La chaîne de caractères contenant la description du tableau de jeu.</param>
    /// <returns>true si le tableau de jeu a été correctement chargé, false sinon</returns>
    public bool LoadFromMemory(string fileContent)
    {
      InitGrid();
      bool dataOK = true;
      fileContent = fileContent.Replace(System.Environment.NewLine, " ");
      string[] valeursFileContent = fileContent.Split(new char[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries); //Stack overflow http://stackoverflow.com/questions/1254577/string-split-by-multiple-character-delimiter
      int x = 0;
      if (valeursFileContent.Length == 36)
      {
        for (int i = 0; i < elements.GetLength(0); i++)
        {
          for (int j = 0; j < elements.GetLength(1); j++)
          {
            if (valeursFileContent[x] == "0" || valeursFileContent[x] == "1"
                   || valeursFileContent[x] == "2" || valeursFileContent[x] == "3"
                          || valeursFileContent[x] == "4" || valeursFileContent[x] == "5")
            {
              elements[i, j] = Convert.ToInt32(valeursFileContent[x]);
              x++;
            }
          }
        }
      }
      else
      {
        dataOK = false;
      }
      return dataOK;      
    }

    /// <summary>
    /// Permet d'accéder à un élément précis de la grille
    /// </summary>
    /// <param name="row">La ligne dans la grille</param>
    /// <param name="column">La colonne dans la grille</param>
    /// <returns>L'élément se trouvant à la position spécifiée. -1 si la position est invalide.</returns>
    public int GetElementAt(int row, int column)
    {
      if (row >= 0 && row < elements.GetLength(0) && column >= 0 && column < elements.GetLength(1))
      {
        return elements[row, column];
      }
      else
        return -1;
    }


    // ppoulin
    // A décommenter lorsque la classe Vehicule aura été codée.

    /// <summary>
    /// Permet d'enlever un véhicule de la grille. Utilisée lorsqu'un véhicule est déplacé. On l'enlève (méthode EraseVehicle) 
    /// d'où il se trouve puis on le déplacer et finalement on le replace (méthode SetVehicle) à la nouvelle place.
    /// </summary>
    /// <param name="vehicle">Le véhicule à enlever</param>
    public void EraseVehicle(Vehicle vehicle)
    {
      InternalSetVehicle(vehicle, 0);
    }

    /// <summary>
    /// Permet de remplacer le contenu de la grille à l'endroit spécifié par le véhicule
    /// par une valeur précise
    /// </summary>
    /// <param name="vehicle">Le véhicule à considérer pour la position</param>
    /// <param name="value">La valeur à mettre dans la grille</param>
    private void InternalSetVehicle(Vehicle vehicle, int value)
    {
      for (int i = vehicle.GetBlockOffsetX(); i < vehicle.GetBlockOffsetX() + vehicle.GetWidth(); i++)
      {
        for (int j = vehicle.GetBlockOffsetY(); j < vehicle.GetBlockOffsetY() + vehicle.GetHeight(); j++)
        {
          elements[j, i] = value;
        }
      }
    }

    /// <summary>
    /// Permet d'ajouter un véhicule de la grille. Utilisée lorsqu'un véhicule est déplacé. On l'enlève (méthode EraseVehicle) 
    /// d'où il se trouve puis on le déplacer et finalement on le replace (méthode SetVehicle) à la nouvelle place.
    /// </summary>
    /// <param name="vehicle">Le véhicule à ajouter</param>
    public void SetVehicle(Vehicle vehicle)
    {
      InternalSetVehicle(vehicle, vehicle.GetValue());
    }



    /// <summary>
    /// Recherche dans la grille le véhicule corrrespondant à l'identifiant spécifié.
    /// </summary>
    /// <param name="num">L'identifiant du véhicule à rechercher. Entre 0 et Game.NB_MAX_VEHICULES</param>
    /// <returns>Le véhicule correspondant s'il a été trouvé, false sinon</returns>
    // ppoulin
    // Écrivez ici le code de la méthode GetVehicle
    public Vehicle GetVehicle(int num)
    {
      int blockOffsetX = 0;
      int blockOffSetY = 0;
      int value = num;
      int width = 0;
      int height = 0;

      for (int i = 0; i < elements.GetLength(0); i++)
      {
        for (int j = 0; j < elements.GetLength(1); j++)
        {
          if (elements[i, j] == num)
          {
            blockOffsetX = j;
            blockOffSetY = i;
            width = 1;
            height = 1;
            break;
          }
        }
        if (width != 0 || height != 0)
        {
          break;
        }
      }
      Vehicle vehicle = null;
      if (elements[blockOffSetY + 1, blockOffsetX] == num && elements[blockOffSetY + 2, blockOffsetX] == num)
      {
        height = height + 2;
      }
      else if (elements[blockOffSetY + 1, blockOffsetX] == num)
      {
        height = height + 1;
      }
      else if (elements[blockOffSetY, blockOffsetX + 1] == num && elements[blockOffSetY, blockOffsetX + 2] == num)
      {
        width = width + 2;
      }
      else if (elements[blockOffSetY, blockOffsetX + 1] == num)
      {
        width = width + 1;
      }
      else
      {
        return vehicle;
      }
      vehicle = new Vehicle(blockOffsetX, blockOffSetY, width, height, num);
      return vehicle;
    }
  }
}
