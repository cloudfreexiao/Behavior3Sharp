/**
 * Action is the base class for all action nodes. Thus, if you want to create
 * new custom action nodes, you need to inherit from this class. For example,
 * take a look at the Runner action:
 *
 *     class Runner extends b3.Action {
 *       constructor(){
 *         super({name: 'Runner'});
 *       }
 *       tick(tick) {
 *         return b3.RUNNING;
 *       }
 *     };
 *
 * @module b3
 * @class Action
 * @extends BaseNode
 **/

using System.Collections.Generic;

namespace XIL.AI.Behavior3Sharp
{
    public class Action: BaseNode
    {
        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.parameters = new Dictionary<string, string>();
	        this.properties = new Dictionary<string, string>();
            this.category = Constants.ACTION;
        }

    }
}
