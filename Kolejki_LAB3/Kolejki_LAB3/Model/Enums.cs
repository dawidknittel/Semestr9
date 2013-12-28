namespace Kolejki_LAB3.Model
{
    public enum Algorithm
    {
        FIFO,
        LIFO,
        RSS,
        NONE
    }

    public enum AddMachineStateWindow
    {
        ADD,
        CANCEL
    }

    public enum WaitingTimeOption
    {
        Constant,
        Uniform,
        Exponential,
        Normal
    }

    public enum InputOutputEnum
    {
        Strumień_wejściowy,
        Strumień_wyjściowy
    }

    public enum SystemState
    {
        Start,
        End,
        None
    }

    public enum RelationType
    {
        Input,
        Output
    }
}
