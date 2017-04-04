﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MigAz.Azure.Asm;
using MigAz.UserControls.Migrators;

namespace MigAz.UserControls
{
    public partial class NetworkSecurityGroupProperties : UserControl
    {
        private TreeNode _NetworkSecurityGroupNode;
        private bool _IsInitialBinding = false;

        public delegate Task AfterPropertyChanged();
        public event AfterPropertyChanged PropertyChanged;

        public NetworkSecurityGroupProperties()
        {
            InitializeComponent();
        }

        internal void Bind(TreeNode networkSecurityGroupNode, AsmToArm asmToArmForm)
        {
            _IsInitialBinding = true;

            _NetworkSecurityGroupNode = networkSecurityGroupNode;

            TreeNode asmNetworkSecurityGroupNode = (TreeNode)_NetworkSecurityGroupNode.Tag;
            NetworkSecurityGroup asmNetworkSecurityGroup = (NetworkSecurityGroup)asmNetworkSecurityGroupNode.Tag;

            lblSourceName.Text = asmNetworkSecurityGroup.Name;
            txtTargetName.Text = asmNetworkSecurityGroup.TargetName;

            _IsInitialBinding = false;
        }

        private void txtTargetName_TextChanged(object sender, EventArgs e)
        {
            TextBox txtSender = (TextBox)sender;
            TreeNode asmNetworkSecurityGroupNode = (TreeNode)_NetworkSecurityGroupNode.Tag;
            NetworkSecurityGroup asmNetworkSecurityGroup = (NetworkSecurityGroup)asmNetworkSecurityGroupNode.Tag;

            asmNetworkSecurityGroup.TargetName = txtSender.Text;
            _NetworkSecurityGroupNode.Text = asmNetworkSecurityGroup.GetFinalTargetName();

            if (!_IsInitialBinding)
                PropertyChanged();
        }
    }
}
