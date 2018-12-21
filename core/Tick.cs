using System;
using System.Collections.Generic;


namespace XIL.AI.Behavior3Sharp
{
    public class Tick
    {
        /**
        * The tree reference.
        * @property {b3.BehaviorTree} tree
        * @readOnly
        **/
        public BehaviorTree tree = null;
        /**
         * The debug reference.
         * @property {Object} debug
         * @readOnly
         */
        public object debug = null;

        /**
        * The target object reference.
        * @property {Object} target
        * @readOnly
        **/
        public object target = null;


        /**
         * The blackboard reference.
         * @property {b3.Blackboard} blackboard
         * @readOnly
         **/
        public Blackboard blackboard = null;

        /**
        * The list of open nodes. Update during the tree traversal.
        * @property {Array} _openNodes
        * @protected
        * @readOnly
        **/
        public List<BaseNode> _openNodes = null;

        /**
         * The number of nodes entered during the tick. Update during the tree
         * traversal.
         *
         * @property {Integer} _nodeCount
         * @protected
         * @readOnly
         **/
        public int _nodeCount = 0;


        public void Initialize(BehaviorTree tree, Blackboard blackboard, object debug, object target)
        {
            this.debug = debug;
            this.target = target;
            this.blackboard = blackboard;
            this.tree = tree;
            this._openNodes = new List<BaseNode>();
        }

        /**
        * Called when entering a node (called by BaseNode).
        * @method _enterNode
        * @param {Object} node The node that called this method.
        * @protected
        **/
        public void _enterNode(BaseNode node)
        {
            this._nodeCount++;
            this._openNodes.Add(node);

            // TODO: call debug here
        }

        /**
         * Callback when opening a node (called by BaseNode).
         * @method _openNode
         * @param {Object} node The node that called this method.
         * @protected
         **/
        public void _openNode(BaseNode node)
        {
            // TODO: call debug here
        }

        /**
        * Callback when ticking a node (called by BaseNode).
        * @method _tickNode
        * @param {Object} node The node that called this method.
        * @protected
        **/
        public void _tickNode(BaseNode node)
        {
            // TODO: call debug here
        }

        /**
         * Callback when closing a node (called by BaseNode).
         * @method _closeNode
         * @param {Object} node The node that called this method.
         * @protected
         **/
        public void _closeNode(BaseNode node)
        {
            // TODO: call debug here
            if(this._openNodes.Count > 0)
            {
                this._openNodes.Remove(this._openNodes[this._openNodes.Count - 1]);
            }
        }

        /**
         * Callback when exiting a node (called by BaseNode).
         * @method _exitNode
         * @param {Object} node The node that called this method.
         * @protected
         **/
        public void _exitNode(BaseNode node)
        {
            // TODO: call debug here
        }

    }
}
