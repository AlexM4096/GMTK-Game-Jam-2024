using NPBehave;

namespace Alex
{
    public class Empty : Task
    {
        public Empty() : base("Empty")
        {
        }

        protected override void DoStart() => Stopped(true);
    }
}