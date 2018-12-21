/**
 * Condition is the base class for all condition nodes. Thus, if you want to
 * create new custom condition nodes, you need to inherit from this class.
 *
 * @class Condition
 * @extends BaseNode
 **/


namespace XIL.AI.Behavior3Sharp
{
    class Condition : BaseNode
    {
        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.category = Constants.CONDITION;
        }

    }
}
