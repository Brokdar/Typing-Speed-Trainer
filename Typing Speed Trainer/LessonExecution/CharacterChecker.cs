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
            else if (reference == '\u2423' && target == ' ')
            {
                return true;
            }
            // Check for return character '⏎'
            else if (reference == '\u23ce' && target == '\r')
            {
                return true;
            }
            // Check for tab character '⇒'
            else if (reference == '\u21d2' && target == '\t')
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
