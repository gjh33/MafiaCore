namespace MafiaCore
{
    public abstract class InputEntry
    {
        public abstract Input GetInput();
    }
    
    public class InputEntry<TValue> : InputEntry
    {
        public Input<TValue> Input { get; private set; }
        public TValue Value { get; private set; }

        public override Input GetInput() => Input;

        public InputEntry(Input<TValue> input, TValue value)
        {
            Input = input;
            Value = value;
        }
    }
}