

namespace SecondDesktopDll
{
    public class MsgArgs
    {
        public object Sender { get; private set; }

        public MsgArgs(object sender = null)
        {
            Sender = sender;
        }
    }
}
