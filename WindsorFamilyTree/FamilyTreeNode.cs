using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFamilyTree
{
    public class FamilyTreeNode
    {
        public RoyalFamilyMember Member { get; set; }
        public List<FamilyTreeNode> Children { get; set; }

        public FamilyTreeNode(RoyalFamilyMember member)
        {
            Member = member;
            Children = new List<FamilyTreeNode>();
        }

        public void AddChild(FamilyTreeNode child)
        {
            Children.Add(child);
        }
    }
}