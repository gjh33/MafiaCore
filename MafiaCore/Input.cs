namespace MafiaCore
{
    public abstract class Input
    {
        public string Name;
        public string Description;
    }

    public class Input<T> : Input
    {
        public InputEntry<T> CreateEntry(T value)
        {
            return new InputEntry<T>(this, value);
        }
    }
}