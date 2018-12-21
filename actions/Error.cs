/**
 * This action node returns `ERROR` always.
 *
 * @module b3
 * @class Error
 * @extends Action
 **/

namespace XIL.AI.Behavior3Sharp
{
    public class Error : Action
    {
        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.name = "Error";
            this.title = "Error";
        }

        public override B3Status tick(Tick tick) { return B3Status.ERROR; }
    }
}
