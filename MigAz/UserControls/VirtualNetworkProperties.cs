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

namespace MigAz.UserControls
{
    public partial class VirtualNetworkProperties : UserControl
    {
        TreeNode _ARMVirtualNetowrkNode;

        public delegate Task AfterPropertyChanged();
        public event AfterPropertyChanged PropertyChanged;
        private bool _IsInitialBinding = false;

        public VirtualNetworkProperties()
        {
            InitializeComponent();
        }

        public void Bind(TreeNode armVirtualNetworkNode)
        {
            _IsInitialBinding = true;
            _ARMVirtualNetowrkNode = armVirtualNetworkNode;

            TreeNode asmVirtualNetworkNode = (TreeNode)_ARMVirtualNetowrkNode.Tag;
            VirtualNetwork asmVirtualNetwork = (VirtualNetwork)asmVirtualNetworkNode.Tag;

            lblVNetName.Text = asmVirtualNetwork.Name.ToString();
            txtVirtualNetworkName.Text = asmVirtualNetwork.TargetName;
            dgvAddressSpaces.DataSource = asmVirtualNetwork.AddressPrefixes.Select(x => new { AddressPrefix = x }).ToList();

            _IsInitialBinding = false;
        }

        private void txtVirtualNetworkName_TextChanged(object sender, EventArgs e)
        {
            if (!_IsInitialBinding)
            {
                TextBox txtSender = (TextBox)sender;

                TreeNode asmVirtualNetworkNode = (TreeNode)_ARMVirtualNetowrkNode.Tag;
                VirtualNetwork asmVirtualNetwork = (VirtualNetwork)asmVirtualNetworkNode.Tag;

                asmVirtualNetwork.TargetName = txtSender.Text.Trim();
                _ARMVirtualNetowrkNode.Text = asmVirtualNetwork.GetFinalTargetName();

                PropertyChanged();
            }
        }

        private void txtVirtualNetworkName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
