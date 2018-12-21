///**
// * Composite is the base class for all composite nodes. Thus, if you want to
// * create new custom composite nodes, you need to inherit from this class.
// *
// * When creating composite nodes, you will need to propagate the tick signal
// * to the children nodes manually. To do that, override the `tick` method and
// * call the `_execute` method on all nodes. For instance, take a look at how
// * the Sequence node inherit this class and how it call its children:
// *
// *     // Inherit from Composite, using the util function Class.
// *     class Sequence extends Composite {
// *
// *       constructor(){
// *         // Remember to set the name of the node.
// *         super({name: 'Sequence'});
// *       }
// *
// *       // Override the tick function
// *       tick(tick) {
// *
// *         // Iterates over the children
// *         for (var i=0; i<this.children.length; i++) {
// *
// *           //Propagate the tick
// *           var status = this.children[i]._execute(tick);
// *
// *           if (status !== SUCCESS) {
// *               return status;
// *           }
// *         }
// *
// *         return SUCCESS;
// *       }
// *     };
// *
// * @module b3
// * @class Composite
// * @extends BaseNode
// **/

using System;
using System.Collections.Generic;

namespace XIL.AI.Behavior3Sharp
{
    public class Composite : BaseNode
    {
        protected List<BaseNode> children;

        public override void Initialize(Behavior3NodeCfg cfg)
        {
            base.Initialize(cfg);
            this.children = new List<BaseNode>();
            this.category = Constants.COMPOSITE;
        }

        public int GetChildCount()
        {
            return this.children.Count;
        }

        public void AddChild(BaseNode child)
        {
            this.children.Add(child);
        }


    }
}
