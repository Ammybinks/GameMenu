using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using SpriteLibrary;

namespace ExperimentalFloss
{
    public partial class PrintString
    {
        // Prints a string on screen instantly
        int full;

        float letterDistance;

        char[] letters;

        public LinkedList<Sprite> sentenceList;
        public LinkedList<Sprite> currentList;

        public Sprite fontSheetSprite;
        public Texture2D fontSheetTexture;

        public void Do(Game core, Vector2 sentenceCorner, string sentence) // Overload Methods
        {
            Do(core, sentenceCorner, sentence, 0.1f, 35, core.fontSheetSprite, core.fontSheetTexture);
        }
        public void Do(Game core, Vector2 sentenceCorner, string sentence, float size)
        {
            Do(core, sentenceCorner, sentence, size, 35, core.fontSheetSprite, core.fontSheetTexture);
        }
        public void Do(Game core, Vector2 sentenceCorner, string sentence, float size, int letterWidth)
        {
            Do(core, sentenceCorner, sentence, size, letterWidth, core.fontSheetSprite, core.fontSheetTexture);
        }
        public void Do(Game core, Vector2 sentenceCorner, string sentence, float size, int letterWidth, Sprite spriteSheet)
        {
            Do(core, sentenceCorner, sentence, size, letterWidth, spriteSheet, core.fontSheetTexture);
        }
        public void Do(Game core, Vector2 sentenceCorner, string sentence, float size, int letterWidth, Sprite spriteSheet, Texture2D spriteSheetTexture)
        {
            letterDistance = 0;

            letters = sentence.ToCharArray(); // Set all the letters in the sentence string to individual characters for alphabetical serialisation

            foreach (char character in letters)
            {
                full = char.ToUpper(character) - 65; // Used to get a 0 - 24 code for all letters in the array

                spriteSheet = new Sprite();

                spriteSheet.SetTexture(spriteSheetTexture, 26, 2); // Set up the spriteSheet and add the character to the array to be drawn later
                spriteSheet.setCurrentFrame(full);
                spriteSheet.UpperLeft = new Vector2(sentenceCorner.X + letterDistance, sentenceCorner.Y);
                spriteSheet.Scale = new Vector2(size, size);

                core.sentenceList.AddFirst(spriteSheet);

                letterDistance = letterDistance + letterWidth; // Adds spacing between letters

                full = -1;
            }
        }
    }
}
