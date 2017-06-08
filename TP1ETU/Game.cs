using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;

namespace TP1PROF
{
  /// <summary>
  /// Classe représentant le concept de jeu.
  /// S'occupe de la gestion d'une partie.
  /// </summary>
  public class Game
  {
    #region Propriétés
    #region Constantes définissant le jeu
    /// <summary>
    /// Nombre maximal de niveau dans le jeu
    /// </summary>
    public const int NUM_MAX_LEVEL = 3;

    // ppoulin
    // Ajouter les constantes ici    
    // A COMPLÉTER
    public const int BLOCK_SIZE = 100;
    public const int HORIZONTAL_BLOCK_COUNT = 6;
    public const int NB_MAX_VEHICLES = 5;
    public const int VERTICAL_BLOCK_COUNT = 6;
    #endregion

    // ppoulin
    // Déclaration ici du tableau de véhicules
    // A COMPLÉTER
    private Vehicle[] allVehicles;

    /// <summary>
    /// La grille contenant la position de tous les véhicules du jeu
    /// </summary>
    private Grid grid = null;

    /// <summary>
    /// Texture SFML pour l'arrière-plan du jeu
    /// </summary>
    Texture backgroundTexture = null;

    /// <summary>
    /// Propriété SFML pour l'arrière-plan du jeu
    /// </summary>
    Sprite backgroundSprite = null;
    #endregion
    /// <summary>
    /// Constructeur du jeu
    /// </summary>
    public Game()
    {
      grid = new Grid();

      // ppoulin
      // Initialiser le contenu du tableau de véhicules ici
      // A COMPLETER
      allVehicles = new Vehicle[NB_MAX_VEHICLES];

      // Propriétés SFML
      backgroundTexture = new Texture("Arts/background.bmp");
      backgroundSprite = new Sprite(backgroundTexture);
    }


    /// <summary>
    /// Permet de charger un niveau de jeu à partir d'un fichier sur le disque
    /// </summary>
    /// <param name="path">Le chemin d'accès du fichier sur le disque</param>
    /// <returns>true si le chargement s'est bien effectué, false sinon</returns>
    public bool LoadLevel(string path)
    {
      // ppoulin
      // Décommenter le code suivant pour le chargement de la grille.
      // A DÉCOMMENTER
      if (File.Exists(path))
      {
        string content = File.ReadAllText(path);
        bool retval = grid.LoadFromMemory(content);
        if (true == retval)
        {
          for (int i = 0; i < Game.NB_MAX_VEHICLES; i++)
          {
            allVehicles[i] = grid.GetVehicle(i + 1);
          }
        }
        return retval;
      }
      else
        return false;
    }

    /// <summary>
    /// Permet d'afficher tous les éléments du jeu
    /// </summary>
    /// <param name="window">Le contexte de rendu du jeu</param>
    public void Draw(RenderWindow window)
    {
      // Arrière-plan en premier...
      window.Draw(backgroundSprite);

      // ppoulin
      // A compléter lorsque la méthode Draw de la class Vehicle aura été codée
      for (int i = 0; i < allVehicles.Length; i++)
      {
        if (allVehicles[i] != null)
        {
          allVehicles[i].Draw(window);
        }
      }
      // Affichage par-dessus le background de tous les véhicules
      // A COMPLETER	

    }

    /// <summary>
    /// Met à jour le jeu
    /// </summary>
    /// <param name="positionX">Position absolue en X où le joueur a cliqué</param>
    /// <param name="positionY">Position absolue en Y où le joueur a cliqué</param>
    /// <returns>true si la partie n'est pas finie, false sinon</returns>
    public bool Update(int positionX, int positionY)
    {
      bool applicationMustContinue = true;


      // ppoulin
      // A COMPLETER:
      // a) Déplacez les véhicules s'ils ont été cliqués. Utilisez la méthode IsClicked
      //    pour déterminer si un véhicule a été cliqué.  Appelez ensuite la méthode Move
      //    le cas échéant.
      for (int i = 0; i < allVehicles.Length; i++)
      {
        if (allVehicles[i] != null && allVehicles[i].IsClicked(positionX, positionY) == true)
        {
          allVehicles[i].Move(grid,positionX, positionY);
        }
      }
      // b) Vérifier la condition de fin de partie: est-ce que le véhicule rouge (le "0")
      //    peut sortir de la grille?  Si oui, affectez la variable en retour adéquatement.
      if (allVehicles[0].GetBlockOffsetX() == 4)
      {
        applicationMustContinue = false;
      }


      return applicationMustContinue;
    }
  }


}
