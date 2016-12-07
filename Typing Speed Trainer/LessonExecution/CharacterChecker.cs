using System.Windows.Input;

namespace Typing_Speed_Trainer.LessonExecution
{
    public class CharacterChecker
    {
        public static bool Check(char reference, char target)
        {
            if (reference == target)
            {
                return true;
            }
            // Check for blank box character '␣'
            else if (reference == '\u2423' && target == ' ' && Keyboard.IsKeyDown(Key.Space))
            {
                return true;
            }
            // Check for return character '⏎'
            else if (reference == '\u23ce' && target == ' ' && Keyboard.IsKeyDown(Key.Enter))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
