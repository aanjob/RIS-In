<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPeriode
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FPeriode))
        Me.BackstageViewControl1 = New DevExpress.XtraBars.Ribbon.BackstageViewControl()
        Me.BackstageViewClientControl1 = New DevExpress.XtraBars.Ribbon.BackstageViewClientControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.BProses = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SPTahun = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.BackstageViewTabItem1 = New DevExpress.XtraBars.Ribbon.BackstageViewTabItem()
        Me.BackstageViewItemSeparator1 = New DevExpress.XtraBars.Ribbon.BackstageViewItemSeparator()
        Me.BVBClose = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.BVBOpen = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.BVBExit = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BackstageViewControl1.SuspendLayout()
        Me.BackstageViewClientControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SPTahun.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BackstageViewControl1
        '
        Me.BackstageViewControl1.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Yellow
        Me.BackstageViewControl1.Controls.Add(Me.BackstageViewClientControl1)
        Me.BackstageViewControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BackstageViewControl1.Items.Add(Me.BackstageViewTabItem1)
        Me.BackstageViewControl1.Items.Add(Me.BackstageViewItemSeparator1)
        Me.BackstageViewControl1.Items.Add(Me.BVBClose)
        Me.BackstageViewControl1.Items.Add(Me.BVBOpen)
        Me.BackstageViewControl1.Items.Add(Me.BVBExit)
        Me.BackstageViewControl1.Location = New System.Drawing.Point(0, 0)
        Me.BackstageViewControl1.Name = "BackstageViewControl1"
        Me.BackstageViewControl1.SelectedTab = Me.BackstageViewTabItem1
        Me.BackstageViewControl1.SelectedTabIndex = 0
        Me.BackstageViewControl1.Size = New System.Drawing.Size(1088, 430)
        Me.BackstageViewControl1.TabIndex = 0
        Me.BackstageViewControl1.Text = "BackstageViewControl1"
        '
        'BackstageViewClientControl1
        '
        Me.BackstageViewClientControl1.Controls.Add(Me.LayoutControl1)
        Me.BackstageViewClientControl1.Location = New System.Drawing.Point(162, 0)
        Me.BackstageViewClientControl1.Name = "BackstageViewClientControl1"
        Me.BackstageViewClientControl1.Size = New System.Drawing.Size(926, 430)
        Me.BackstageViewClientControl1.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.BProses)
        Me.LayoutControl1.Controls.Add(Me.GridControl1)
        Me.LayoutControl1.Controls.Add(Me.SPTahun)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(926, 430)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'BProses
        '
        Me.BProses.Image = Global.RIS.My.Resources.Resources.Proses
        Me.BProses.Location = New System.Drawing.Point(132, 12)
        Me.BProses.Name = "BProses"
        Me.BProses.Size = New System.Drawing.Size(71, 28)
        Me.BProses.StyleController = Me.LayoutControl1
        Me.BProses.TabIndex = 6
        Me.BProses.Text = "Proses"
        '
        'GridControl1
        '
        Me.GridControl1.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.First.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.Last.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.Next.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.Prev.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.GridControl1.Location = New System.Drawing.Point(12, 44)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(902, 374)
        Me.GridControl1.TabIndex = 5
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn8, Me.GridColumn7, Me.GridColumn9, Me.GridColumn10})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "ID Period"
        Me.GridColumn1.FieldName = "PeriodID"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 60
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Bulan"
        Me.GridColumn2.FieldName = "Bulan"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 110
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Tahun"
        Me.GridColumn3.FieldName = "Tahun"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 73
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Tgl Awal"
        Me.GridColumn4.DisplayFormat.FormatString = "dd MMMM yyyy"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn4.FieldName = "TglAwal"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 115
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Tgl Akhir"
        Me.GridColumn5.DisplayFormat.FormatString = "dd MMMM yyyy"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn5.FieldName = "TglAkhir"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 5
        Me.GridColumn5.Width = 104
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Inserted Date"
        Me.GridColumn6.DisplayFormat.FormatString = "dd MMMM yyyy HH:mm:ss"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn6.FieldName = "InsDate"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 129
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Close"
        Me.GridColumn8.FieldName = "stsClose"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        Me.GridColumn8.Width = 45
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Insert By"
        Me.GridColumn7.FieldName = "InsBy"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 7
        Me.GridColumn7.Width = 52
        '
        'SPTahun
        '
        Me.SPTahun.EditValue = New Decimal(New Integer() {2015, 0, 0, 0})
        Me.SPTahun.Location = New System.Drawing.Point(45, 17)
        Me.SPTahun.Name = "SPTahun"
        Me.SPTahun.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SPTahun.Properties.MaxValue = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.SPTahun.Properties.MinValue = New Decimal(New Integer() {2015, 0, 0, 0})
        Me.SPTahun.Size = New System.Drawing.Size(83, 20)
        Me.SPTahun.StyleController = Me.LayoutControl1
        Me.SPTahun.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(926, 430)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.SPTahun
        Me.LayoutControlItem1.CustomizationFormText = "Tahun"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(120, 26)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(120, 26)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(120, 32)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Tahun"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(30, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.GridControl1
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(906, 378)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.BProses
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(120, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(75, 32)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(75, 32)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(75, 32)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(195, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(711, 32)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'BackstageViewTabItem1
        '
        Me.BackstageViewTabItem1.Appearance.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BackstageViewTabItem1.Appearance.Options.UseFont = True
        Me.BackstageViewTabItem1.AppearanceHover.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BackstageViewTabItem1.AppearanceHover.Options.UseFont = True
        Me.BackstageViewTabItem1.AppearanceSelected.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BackstageViewTabItem1.AppearanceSelected.Options.UseFont = True
        Me.BackstageViewTabItem1.Caption = "Periode"
        Me.BackstageViewTabItem1.ContentControl = Me.BackstageViewClientControl1
        Me.BackstageViewTabItem1.Glyph = Global.RIS.My.Resources.Resources.Period
        Me.BackstageViewTabItem1.Name = "BackstageViewTabItem1"
        Me.BackstageViewTabItem1.Selected = True
        '
        'BackstageViewItemSeparator1
        '
        Me.BackstageViewItemSeparator1.Name = "BackstageViewItemSeparator1"
        '
        'BVBClose
        '
        Me.BVBClose.Caption = "Close Periode"
        Me.BVBClose.Glyph = Global.RIS.My.Resources.Resources.ClPeriod
        Me.BVBClose.GlyphDisabled = Global.RIS.My.Resources.Resources.ClPeriodD
        Me.BVBClose.Name = "BVBClose"
        '
        'BVBOpen
        '
        Me.BVBOpen.Caption = "Open Periode"
        Me.BVBOpen.Glyph = Global.RIS.My.Resources.Resources.OpPeriod
        Me.BVBOpen.GlyphDisabled = Global.RIS.My.Resources.Resources.OpPeriodD
        Me.BVBOpen.Name = "BVBOpen"
        '
        'BVBExit
        '
        Me.BVBExit.Caption = "Exit"
        Me.BVBExit.Glyph = Global.RIS.My.Resources.Resources.Keluar
        Me.BVBExit.GlyphDisabled = Global.RIS.My.Resources.Resources.KeluarD
        Me.BVBExit.Name = "BVBExit"
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Closed Date"
        Me.GridColumn9.DisplayFormat.FormatString = "dd MMMM yyyy HH:mm:ss"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn9.FieldName = "ClsDate"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 8
        Me.GridColumn9.Width = 118
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Closed By"
        Me.GridColumn10.FieldName = "ClsBy"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 9
        Me.GridColumn10.Width = 77
        '
        'FPeriode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1088, 430)
        Me.Controls.Add(Me.BackstageViewControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Blue"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "FPeriode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".: Periode :."
        Me.BackstageViewControl1.ResumeLayout(False)
        Me.BackstageViewClientControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SPTahun.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BackstageViewControl1 As DevExpress.XtraBars.Ribbon.BackstageViewControl
    Friend WithEvents BackstageViewClientControl1 As DevExpress.XtraBars.Ribbon.BackstageViewClientControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents BProses As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SPTahun As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents BackstageViewTabItem1 As DevExpress.XtraBars.Ribbon.BackstageViewTabItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BackstageViewItemSeparator1 As DevExpress.XtraBars.Ribbon.BackstageViewItemSeparator
    Friend WithEvents BVBClose As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Friend WithEvents BVBOpen As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Friend WithEvents BVBExit As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
End Class
