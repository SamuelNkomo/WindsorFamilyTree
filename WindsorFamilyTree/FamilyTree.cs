using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFamilyTree
{
    public class FamilyTree
    {
        public FamilyTreeNode Monarch { get; set; }

        public FamilyTree(RoyalFamilyMember monarch)
        {
            Monarch = new FamilyTreeNode(monarch);
        }
    }
}