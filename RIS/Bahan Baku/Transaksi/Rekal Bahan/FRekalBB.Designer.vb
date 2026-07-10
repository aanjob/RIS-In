<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRekalBB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRekalBB))
        Me.BackstageViewControl1 = New DevExpress.XtraBars.Ribbon.BackstageViewControl()
        Me.BackstageViewClientControl1 = New DevExpress.XtraBars.Ribbon.BackstageViewClientControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.MarqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.DTPTanggal = New DevExpress.XtraEditors.DateEdit()
        Me.TBPeriode = New DevExpress.XtraEditors.TextEdit()
        Me.TBKode = New DevExpress.XtraEditors.TextEdit()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LCIPB = New DevExpress.XtraLayout.LayoutControlItem()
        Me.BVTRekalBB = New DevExpress.XtraBars.Ribbon.BackstageViewTabItem()
        Me.BackstageViewItemSeparator1 = New DevExpress.XtraBars.Ribbon.BackstageViewItemSeparator()
        Me.BVBProsesSbOp = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.BVBProsesStAdj = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.BVBCancelRekal = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.BVBExit = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackstageViewControl1.SuspendLayout()
        Me.BackstageViewClientControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPTanggal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPTanggal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TBPeriode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TBKode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LCIPB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BackstageViewControl1
        '
        Me.BackstageViewControl1.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Yellow
        Me.BackstageViewControl1.Controls.Add(Me.BackstageViewClientControl1)
        Me.BackstageViewControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BackstageViewControl1.Items.Add(Me.BVTRekalBB)
        Me.BackstageViewControl1.Items.Add(Me.BackstageViewItemSeparator1)
        Me.BackstageViewControl1.Items.Add(Me.BVBProsesSbOp)
        Me.BackstageViewControl1.Items.Add(Me.BVBProsesStAdj)
        Me.BackstageViewControl1.Items.Add(Me.BVBCancelRekal)
        Me.BackstageViewControl1.Items.Add(Me.BVBExit)
        Me.BackstageViewControl1.Location = New System.Drawing.Point(0, 0)
        Me.BackstageViewControl1.Name = "BackstageViewControl1"
        Me.BackstageViewControl1.SelectedTab = Me.BVTRekalBB
        Me.BackstageViewControl1.SelectedTabIndex = 0
        Me.BackstageViewControl1.Size = New System.Drawing.Size(919, 460)
        Me.BackstageViewControl1.TabIndex = 0
        Me.BackstageViewControl1.Text = "BackstageViewControl1"
        '
        'BackstageViewClientControl1
        '
        Me.BackstageViewClientControl1.Controls.Add(Me.LayoutControl1)
        Me.BackstageViewClientControl1.Location = New System.Drawing.Point(242, 0)
        Me.BackstageViewClientControl1.Name = "BackstageViewClientControl1"
        Me.BackstageViewClientControl1.Size = New System.Drawing.Size(677, 460)
        Me.BackstageViewClientControl1.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.MarqueeProgressBarControl1)
        Me.LayoutControl1.Controls.Add(Me.DTPTanggal)
        Me.LayoutControl1.Controls.Add(Me.TBPeriode)
        Me.LayoutControl1.Controls.Add(Me.TBKode)
        Me.LayoutControl1.Controls.Add(Me.GridControl1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(677, 460)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'MarqueeProgressBarControl1
        '
        Me.MarqueeProgressBarControl1.EditValue = 0
        Me.MarqueeProgressBarControl1.Location = New System.Drawing.Point(187, 60)
        Me.MarqueeProgressBarControl1.Name = "MarqueeProgressBarControl1"
        Me.MarqueeProgressBarControl1.Size = New System.Drawing.Size(237, 20)
        Me.MarqueeProgressBarControl1.StyleController = Me.LayoutControl1
        Me.MarqueeProgressBarControl1.TabIndex = 8
        '
        'DTPTanggal
        '
        Me.DTPTanggal.EditValue = New Date(2016, 3, 8, 10, 42, 35, 759)
        Me.DTPTanggal.Location = New System.Drawing.Point(55, 60)
        Me.DTPTanggal.Name = "DTPTanggal"
        Me.DTPTanggal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPTanggal.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPTanggal.Properties.Mask.EditMask = "dd MMMM yyyy"
        Me.DTPTanggal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPTanggal.Size = New System.Drawing.Size(128, 20)
        Me.DTPTanggal.StyleController = Me.LayoutControl1
        Me.DTPTanggal.TabIndex = 7
        '
        'TBPeriode
        '
        Me.TBPeriode.EditValue = ""
        Me.TBPeriode.Location = New System.Drawing.Point(55, 36)
        Me.TBPeriode.Name = "TBPeriode"
        Me.TBPeriode.Properties.ReadOnly = True
        Me.TBPeriode.Size = New System.Drawing.Size(178, 20)
        Me.TBPeriode.StyleController = Me.LayoutControl1
        Me.TBPeriode.TabIndex = 6
        '
        'TBKode
        '
        Me.TBKode.EditValue = ""
        Me.TBKode.Location = New System.Drawing.Point(55, 12)
        Me.TBKode.Name = "TBKode"
        Me.TBKode.Properties.ReadOnly = True
        Me.TBKode.Size = New System.Drawing.Size(128, 20)
        Me.TBKode.StyleController = Me.LayoutControl1
        Me.TBKode.TabIndex = 5
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
        Me.GridControl1.Location = New System.Drawing.Point(12, 84)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(653, 364)
        Me.GridControl1.TabIndex = 4
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "RekalIDD"
        Me.GridColumn1.FieldName = "RekalIDD"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "RekalID"
        Me.GridColumn2.FieldName = "RekalID"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "ID Bahan"
        Me.GridColumn3.FieldName = "BBID"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 129
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Deskripsi Bahan"
        Me.GridColumn4.FieldName = "Bahan"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 292
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Nilai"
        Me.GridColumn5.DisplayFormat.FormatString = "{0:n2}"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "Nilai"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 84
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "ID Gudang"
        Me.GridColumn6.FieldName = "GdID"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        Me.GridColumn6.Width = 62
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Harga Satuan"
        Me.GridColumn7.DisplayFormat.FormatString = "{0:n2}"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "HarSat"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 83
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem1, Me.EmptySpaceItem2, Me.LayoutControlItem4, Me.EmptySpaceItem3, Me.LCIPB})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(677, 460)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GridControl1
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(657, 368)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.TBKode
        Me.LayoutControlItem2.CustomizationFormText = "Rekal ID"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(175, 24)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(175, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(175, 24)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "ID Rekal"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(40, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.TBPeriode
        Me.LayoutControlItem3.CustomizationFormText = "Periode"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Periode"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(40, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(175, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(482, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(225, 24)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(432, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.DTPTanggal
        Me.LayoutControlItem4.CustomizationFormText = "Tanggal"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(175, 24)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(175, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(175, 24)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Tanggal"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(40, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(416, 48)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(241, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LCIPB
        '
        Me.LCIPB.Control = Me.MarqueeProgressBarControl1
        Me.LCIPB.CustomizationFormText = "LCIPB"
        Me.LCIPB.Location = New System.Drawing.Point(175, 48)
        Me.LCIPB.MaxSize = New System.Drawing.Size(241, 24)
        Me.LCIPB.MinSize = New System.Drawing.Size(241, 24)
        Me.LCIPB.Name = "LCIPB"
        Me.LCIPB.Size = New System.Drawing.Size(241, 24)
        Me.LCIPB.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LCIPB.Text = "LCIPB"
        Me.LCIPB.TextSize = New System.Drawing.Size(0, 0)
        Me.LCIPB.TextToControlDistance = 0
        Me.LCIPB.TextVisible = False
        Me.LCIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'BVTRekalBB
        '
        Me.BVTRekalBB.Appearance.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BVTRekalBB.Appearance.Options.UseFont = True
        Me.BVTRekalBB.AppearanceHover.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BVTRekalBB.AppearanceHover.Options.UseFont = True
        Me.BVTRekalBB.AppearanceSelected.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BVTRekalBB.AppearanceSelected.Options.UseFont = True
        Me.BVTRekalBB.Caption = "Rekalkulasi Bahan"
        Me.BVTRekalBB.ContentControl = Me.BackstageViewClientControl1
        Me.BVTRekalBB.Glyph = Global.RIS.My.Resources.Resources.Rekal
        Me.BVTRekalBB.Name = "BVTRekalBB"
        Me.BVTRekalBB.Selected = True
        '
        'BackstageViewItemSeparator1
        '
        Me.BackstageViewItemSeparator1.Name = "BackstageViewItemSeparator1"
        '
        'BVBProsesSbOp
        '
        Me.BVBProsesSbOp.AppearanceHover.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BVBProsesSbOp.AppearanceHover.Options.UseFont = True
        Me.BVBProsesSbOp.AppearancePressed.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BVBProsesSbOp.AppearancePressed.Options.UseFont = True
        Me.BVBProsesSbOp.Caption = "Proses Sebelum Opname"
        Me.BVBProsesSbOp.Glyph = Global.RIS.My.Resources.Resources.ProsesSbOp
        Me.BVBProsesSbOp.GlyphDisabled = Global.RIS.My.Resources.Resources.ProsesSbOpD
        Me.BVBProsesSbOp.Name = "BVBProsesSbOp"
        '
        'BVBProsesStAdj
        '
        Me.BVBProsesStAdj.AppearanceHover.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BVBProsesStAdj.AppearanceHover.Options.UseFont = True
        Me.BVBProsesStAdj.AppearancePressed.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BVBProsesStAdj.AppearancePressed.Options.UseFont = True
        Me.BVBProsesStAdj.Caption = "Proses Setelah Adjustment"
        Me.BVBProsesStAdj.Glyph = Global.RIS.My.Resources.Resources.ProsesStAdj
        Me.BVBProsesStAdj.GlyphDisabled = Global.RIS.My.Resources.Resources.ProsesStAdjD
        Me.BVBProsesStAdj.Name = "BVBProsesStAdj"
        '
        'BVBCancelRekal
        '
        Me.BVBCancelRekal.AppearanceHover.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BVBCancelRekal.AppearanceHover.Options.UseFont = True
        Me.BVBCancelRekal.AppearancePressed.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BVBCancelRekal.AppearancePressed.Options.UseFont = True
        Me.BVBCancelRekal.Caption = "Cancel Rekalkulasi"
        Me.BVBCancelRekal.Glyph = Global.RIS.My.Resources.Resources.CancelRekal
        Me.BVBCancelRekal.GlyphDisabled = Global.RIS.My.Resources.Resources.CancelRekalD
        Me.BVBCancelRekal.Name = "BVBCancelRekal"
        '
        'BVBExit
        '
        Me.BVBExit.AppearanceHover.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BVBExit.AppearanceHover.Options.UseFont = True
        Me.BVBExit.AppearancePressed.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BVBExit.AppearancePressed.Options.UseFont = True
        Me.BVBExit.Caption = "Exit"
        Me.BVBExit.Glyph = Global.RIS.My.Resources.Resources.Keluar
        Me.BVBExit.GlyphDisabled = Global.RIS.My.Resources.Resources.KeluarD
        Me.BVBExit.Name = "BVBExit"
        '
        'BackgroundWorker1
        '
        '
        'FRekalBB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(919, 460)
        Me.Controls.Add(Me.BackstageViewControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Blue"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "FRekalBB"
        Me.Text = ".: Rekalkulasi Bahan Baku :."
        Me.BackstageViewControl1.ResumeLayout(False)
        Me.BackstageViewClientControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPTanggal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPTanggal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TBPeriode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TBKode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LCIPB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BackstageViewControl1 As DevExpress.XtraBars.Ribbon.BackstageViewControl
    Friend WithEvents BackstageViewClientControl1 As DevExpress.XtraBars.Ribbon.BackstageViewClientControl
    Friend WithEvents BVTRekalBB As DevExpress.XtraBars.Ribbon.BackstageViewTabItem
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents TBPeriode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TBKode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents BackstageViewItemSeparator1 As DevExpress.XtraBars.Ribbon.BackstageViewItemSeparator
    Friend WithEvents BVBProsesSbOp As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Friend WithEvents BVBProsesStAdj As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Friend WithEvents BVBCancelRekal As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Friend WithEvents BVBExit As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DTPTanggal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents LCIPB As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
End Class
