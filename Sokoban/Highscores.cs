using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Sokoban
{
    class Highscores : List<Score>
    {
        private List<Score> _highscores;

        public Highscores()
        {
            _highscores = new List<Score>();
        }

        public void ShowHighscores()
        {
            for (int i = 0; i < _highscores.Count; i++)
            {
                Debug.WriteLine("Tijd: " + _highscores[i].Seconden + " Zetten: " + _highscores[i].Zetten);
            }
        }

        public void AddScore(Score score)
        {
            _highscores.Add(score);
        }

    }
}
