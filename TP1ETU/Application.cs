using System;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Windows.Forms;
namespace TP1PROF
{
    /// <summary>
    /// Classe de base créée par le gabarit de projet SFML
    /// </summary>
    public class Application
    {
      #region Code fourni
        /// <summary>
        /// Contexte de rendu
        /// </summary>
        private RenderWindow window = null;

        /// <summary>
        /// Le jeu de Rush hour
        /// </summary>
        private Game game = null;

        /// <summary>
        /// Dernière position en X où le joueur a cliqué avec la souris
        /// </summary>
        private int lastPositionXPressed = -1;

        /// <summary>
        /// Dernière position en Y où le joueur a cliqué avec la souris
        /// </summary>
        private int lastPositionYPressed = -1;

        /// <summary>
        /// Méthode appelée lorsque la fenêtre principale se ferme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        /// <summary>
        /// Méthode appelée lorsque le joueur appuie sur un bouton de la souris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMousePressed(object sender, MouseButtonEventArgs e)
        {
            lastPositionXPressed = e.X;
            lastPositionYPressed = e.Y;
        }

        /// <summary>
        /// Constructeur de la classe Application
        /// </summary>
        public Application()
        {
            window = new RenderWindow( new SFML.Window.VideoMode(Game.BLOCK_SIZE * Game.HORIZONTAL_BLOCK_COUNT, 
                                                                  Game.BLOCK_SIZE*Game.VERTICAL_BLOCK_COUNT), 
                                       "TP1 Rush hour");
            
            // Enregistrement des événements utiles pour notre jeu                                       
            window.Closed += new EventHandler(OnClose);
            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMousePressed);
            game = new Game();
        }

        /// <summary>
        /// Méthode principale du jeu. Gère tout.
        /// </summary>
        public void Run()
        {
            // Niveau courant
            int levelNum = 1;
            // Chaîne de caractères utilisée pour construire le chemin d'accès sur disque du niveau à charger
            string levelNameBase=  "Levels/Level{0}.txt";

            // Nom concret, sur le disque, du fichier de niveau à charger. Les fichiers de niveau doivent être nommés
            // Level1.txt, Level2.txt, Level3.txt, etc
            string levelName = string.Format(levelNameBase,levelNum);

            // Chargement du niveau
            if(false == game.LoadLevel(levelName))
              return;

            // Boucle principale du jeu
            window.SetActive();
            bool mustContinue = true;
            while (mustContinue && window.IsOpen)
            { 
              // Si l'utilisateur a cliqué sur la fenêtre, les coordonnées suivantes auront
              // été modifiées. Sinon, elles vaudront -1
              if(lastPositionXPressed != -1 && lastPositionYPressed != -1)
              {
                // Mettre le jeu à jour si nécessaire.
                mustContinue = game.Update(lastPositionXPressed, lastPositionYPressed);
                lastPositionXPressed = lastPositionYPressed = -1;
              }
              
              window.Clear(Color.Black);
              window.DispatchEvents();
              game.Draw(window);
              window.Display();
              
              // Gestion de la fin de partie
              if( false == mustContinue)
              { 
                if(levelNum >= Game.NUM_MAX_LEVEL)
                {
                  MessageBox.Show( "Vous avez complété tous les niveaux du jeu", "Félicitations.");
                }
                else if(DialogResult.Yes == MessageBox.Show( "Vous avez réussi ce niveau.\n\nVoulez-vous continuer au prochain niveau?", "Victoire", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                  levelName = string.Format(levelNameBase, ++levelNum);
                  mustContinue = game.LoadLevel(levelName);
                }
              }                                
            }            
        }
        #endregion
    }
}
