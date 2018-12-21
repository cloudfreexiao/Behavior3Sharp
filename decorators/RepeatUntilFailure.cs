/**
 * RepeatUntilFailure is a decorator that repeats the tick signal until the
 * node child returns `FAILURE`, `RUNNING` or `ERROR`. Optionally, a maximum
 * number of repetitions can be defined.
 *
 * @module b3
 * @class RepeatUntilFailure
 * @extends Decorator
 **/

namespace XIL.AI.Behavior3Sharp
{
    public class RepeatUntilFailure : Decorator
    {
        private int maxLoop;

        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.maxLoop = cfg.GetInt32("maxLoop", -1);
            this.name = "RepeatUntilFailure";
            this.title = "Repeat Until Failure";
        }

        public override void open(Tick tick)
        {
            tick.blackboard.Set("i", 0, tick.tree.id, this.id);
        }

        public override B3Status tick(Tick tick)
        {
            if (this.child == null)
            {
                return B3Status.ERROR;
            }

            var i = tick.blackboard.Get<int>("i", tick.tree.id, this.id, 0);
            var status = B3Status.ERROR;

            while (this.maxLoop < 0 || i < this.maxLoop)
            {
                status = this.child._execute(tick);
                if (status == B3Status.SUCCESS)
                {
                    i++;
                }
                else
                {
                    break;
                }
            }

            tick.blackboard.Set("i", i, tick.tree.id, this.id);
            return status;
        }

    }

}