/**
 * The Sequence node ticks its children sequentially until one of them
 * returns `FAILURE`, `RUNNING` or `ERROR`. If all children return the
 * success state, the sequence also returns `SUCCESS`.
 *
 * @module b3
 * @class Sequence
 * @extends Composite
 **/


namespace XIL.AI.Behavior3Sharp
{
    public class Sequence : Composite
    {
        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);

            this.name = "Sequence";
            this.title = "Sequence ";
        }

        public override B3Status tick(Tick tick)
        {
            for (int i = 0; i < this.children.Count; i++)
            {
                var status = this.children[i]._execute(tick);
                if (status != B3Status.SUCCESS)
                {
                    return status;
                }
            }

            return B3Status.SUCCESS;
        }

    }

}
