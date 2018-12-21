/**
 * Priority ticks its children sequentially until one of them returns
 * `SUCCESS`, `RUNNING` or `ERROR`. If all children return the failure state,
 * the priority also returns `FAILURE`.
 *
 * @module b3
 * @class Priority
 * @extends Composite
 **/

namespace XIL.AI.Behavior3Sharp
{
    public class Priority : Composite
    {
        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);

            this.name = "Priority";
            this.title = "Priority ";
        }

        public override B3Status tick(Tick tick)
        {
            for (int i = 0; i < this.children.Count; i++)
            {
                var status = this.children[i]._execute(tick);
                if (status != B3Status.FAILURE)
                {
                    return status;
                }
            }

            return B3Status.FAILURE;
        }

    }

}