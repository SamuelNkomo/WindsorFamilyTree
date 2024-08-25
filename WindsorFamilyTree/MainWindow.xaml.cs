using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindsorFamilyTree
{
    public partial class MainWindow : Window
    {
        private FamilyTree familyTree;

        public MainWindow()
        {
            InitializeComponent();
            InitializeFamilyTree();
            FamilyTreeView.ItemsSource = new[] { familyTree.Monarch };
        }

        private void InitializeFamilyTree()
        {
            var monarch = new RoyalFamilyMember("Queen Elizabeth II", new DateTime(1926, 4, 21), true);
            familyTree = new FamilyTree(monarch);

            // Example initialization - add the initial family members
            var charles = new FamilyTreeNode(new RoyalFamilyMember("Prince Charles", new DateTime(1948, 11, 14), true));
            familyTree.Monarch.AddChild(charles);

            var william = new FamilyTreeNode(new RoyalFamilyMember("Prince William", new DateTime(1982, 6, 21), true));
            charles.AddChild(william);

            var george = new FamilyTreeNode(new RoyalFamilyMember("Prince George", new DateTime(2013, 7, 22), true));
            william.AddChild(george);
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if (FamilyTreeView.SelectedItem == null) return;

            var selectedNode = FamilyTreeView.SelectedItem as FamilyTreeNode;
            if (selectedNode == null) return;

            var name = NameTextBox.Text;
            var dob = DobDatePicker.SelectedDate ?? DateTime.Now;
            var isAlive = IsAliveCheckBox.IsChecked ?? true;

            var newMember = new RoyalFamilyMember(name, dob, isAlive);
            var newNode = new FamilyTreeNode(newMember);

            selectedNode.AddChild(newNode);
            FamilyTreeView.ItemsSource = null;
            FamilyTreeView.ItemsSource = new[] { familyTree.Monarch };
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var name = SearchTextBox.Text;
            var node = SearchFamilyTree(familyTree.Monarch, name);
            if (node != null)
            {
                SearchResultTextBlock.Text = $"Found {name} in the family tree.";
                HighlightNode(node);  // Highlight the found node
            }
            else
            {
                SearchResultTextBlock.Text = $"{name} not found in the family tree.";
            }
        }

        private FamilyTreeNode SearchFamilyTree(FamilyTreeNode node, string name)
        {
            if (node.Member.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                return node;

            foreach (var child in node.Children)
            {
                var result = SearchFamilyTree(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }

        private void HighlightNode(FamilyTreeNode node)
        {
            var treeViewItem = FindTreeViewItem(FamilyTreeView, node);
            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                treeViewItem.BringIntoView(); // Ensure the node is visible
            }
        }

        private TreeViewItem FindTreeViewItem(ItemsControl container, object item)
        {
            if (container == null) return null;

            foreach (var child in container.Items)
            {
                var treeViewItem = container.ItemContainerGenerator.ContainerFromItem(child) as TreeViewItem;
                if (treeViewItem == null) continue;

                if (treeViewItem.DataContext == item)
                {
                    return treeViewItem;
                }

                // Recursively search the children
                var foundChild = FindTreeViewItem(treeViewItem, item);
                if (foundChild != null)
                {
                    treeViewItem.IsExpanded = true; // Expand parent nodes
                    return foundChild;
                }
            }

            return null;
        }

        private void FamilyTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedNode = FamilyTreeView.SelectedItem as FamilyTreeNode;
            if (selectedNode != null)
            {
                // Example action: Display member details or perform any other necessary actions
                SearchResultTextBlock.Text = $"Selected: {selectedNode.Member.Name}";
            }
        }
    }
}