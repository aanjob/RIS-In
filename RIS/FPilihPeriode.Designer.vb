<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPilihPeriode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FPilihPeriode))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.CBOKat = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CBOTipePPn = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.SLUGudang = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BOk = New DevExpress.XtraEditors.SimpleButton()
        Me.DTPAkhir = New DevExpress.XtraEditors.DateEdit()
        Me.DTPAwal = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LCIGudang = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LCIPPn = New DevExpress.XtraLayout.LayoutControlItem()
        Me.ESIPPn = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LCIKat = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem7 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.CBOKat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CBOTipePPn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SLUGudang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPAkhir.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPAkhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPAwal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPAwal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LCIGudang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LCIPPn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ESIPPn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LCIKat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.CBOKat)
        Me.LayoutControl1.Controls.Add(Me.CBOTipePPn)
        Me.LayoutControl1.Controls.Add(Me.SLUGudang)
        Me.LayoutControl1.Controls.Add(Me.BOk)
        Me.LayoutControl1.Controls.Add(Me.DTPAkhir)
        Me.LayoutControl1.Controls.Add(Me.DTPAwal)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(396, 195)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'CBOKat
        '
        Me.CBOKat.EditValue = ""
        Me.CBOKat.Location = New System.Drawing.Point(76, 104)
        Me.CBOKat.Name = "CBOKat"
        Me.CBOKat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CBOKat.Properties.Items.AddRange(New Object() {"Semua", "Export", "Import", "Lokal"})
        Me.CBOKat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.CBOKat.Size = New System.Drawing.Size(112, 20)
        Me.CBOKat.StyleController = Me.LayoutControl1
        Me.CBOKat.TabIndex = 9
        '
        'CBOTipePPn
        '
        Me.CBOTipePPn.EditValue = ""
        Me.CBOTipePPn.Location = New System.Drawing.Point(76, 80)
        Me.CBOTipePPn.Name = "CBOTipePPn"
        Me.CBOTipePPn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CBOTipePPn.Properties.Items.AddRange(New Object() {"Semua", "PPn", "Non PPn"})
        Me.CBOTipePPn.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.CBOTipePPn.Size = New System.Drawing.Size(117, 20)
        Me.CBOTipePPn.StyleController = Me.LayoutControl1
        Me.CBOTipePPn.TabIndex = 8
        '
        'SLUGudang
        '
        Me.SLUGudang.EditValue = ""
        Me.SLUGudang.Location = New System.Drawing.Point(76, 56)
        Me.SLUGudang.Name = "SLUGudang"
        Me.SLUGudang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SLUGudang.Properties.View = Me.SearchLookUpEdit1View
        Me.SLUGudang.Size = New System.Drawing.Size(287, 20)
        Me.SLUGudang.StyleController = Me.LayoutControl1
        Me.SLUGudang.TabIndex = 7
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'BOk
        '
        Me.BOk.Image = Global.RIS.My.Resources.Resources.Preview
        Me.BOk.Location = New System.Drawing.Point(160, 128)
        Me.BOk.Name = "BOk"
        Me.BOk.Size = New System.Drawing.Size(76, 30)
        Me.BOk.StyleController = Me.LayoutControl1
        Me.BOk.TabIndex = 6
        Me.BOk.Text = "Preview"
        '
        'DTPAkhir
        '
        Me.DTPAkhir.EditValue = New Date(2016, 3, 15, 9, 12, 19, 559)
        Me.DTPAkhir.Location = New System.Drawing.Point(232, 32)
        Me.DTPAkhir.Name = "DTPAkhir"
        Me.DTPAkhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPAkhir.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPAkhir.Properties.Mask.EditMask = "dd MMMM yyyy"
        Me.DTPAkhir.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.DTPAkhir.Size = New System.Drawing.Size(131, 20)
        Me.DTPAkhir.StyleController = Me.LayoutControl1
        Me.DTPAkhir.TabIndex = 5
        '
        'DTPAwal
        '
        Me.DTPAwal.EditValue = New Date(2016, 3, 15, 9, 12, 19, 559)
        Me.DTPAwal.Location = New System.Drawing.Point(76, 32)
        Me.DTPAwal.Name = "DTPAwal"
        Me.DTPAwal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPAwal.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPAwal.Properties.Mask.EditMask = "dd MMMM yyyy"
        Me.DTPAwal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.DTPAwal.Size = New System.Drawing.Size(132, 20)
        Me.DTPAwal.StyleController = Me.LayoutControl1
        Me.DTPAwal.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem2, Me.EmptySpaceItem3, Me.EmptySpaceItem4, Me.EmptySpaceItem5, Me.LCIGudang, Me.LCIPPn, Me.ESIPPn, Me.EmptySpaceItem1, Me.EmptySpaceItem6, Me.LCIKat, Me.EmptySpaceItem7})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(396, 195)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.DTPAwal
        Me.LayoutControlItem1.CustomizationFormText = "Periode"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(20, 20)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(180, 24)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(180, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(180, 24)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Periode"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(41, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.DTPAkhir
        Me.LayoutControlItem2.CustomizationFormText = "s/d"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(200, 20)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(155, 24)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(155, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(155, 24)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "s/d"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(15, 13)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.BOk
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(148, 116)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(80, 34)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(80, 34)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(80, 34)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 20)
        Me.EmptySpaceItem2.MaxSize = New System.Drawing.Size(20, 0)
        Me.EmptySpaceItem2.MinSize = New System.Drawing.Size(20, 10)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(20, 130)
        Me.EmptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(355, 20)
        Me.EmptySpaceItem3.MaxSize = New System.Drawing.Size(20, 0)
        Me.EmptySpaceItem3.MinSize = New System.Drawing.Size(20, 10)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(21, 130)
        Me.EmptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(20, 116)
        Me.EmptySpaceItem4.MaxSize = New System.Drawing.Size(128, 34)
        Me.EmptySpaceItem4.MinSize = New System.Drawing.Size(128, 34)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(128, 34)
        Me.EmptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.AllowHotTrack = False
        Me.EmptySpaceItem5.CustomizationFormText = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(228, 116)
        Me.EmptySpaceItem5.MaxSize = New System.Drawing.Size(127, 34)
        Me.EmptySpaceItem5.MinSize = New System.Drawing.Size(127, 34)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(127, 34)
        Me.EmptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem5.Text = "EmptySpaceItem5"
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'LCIGudang
        '
        Me.LCIGudang.Control = Me.SLUGudang
        Me.LCIGudang.CustomizationFormText = "Gudang"
        Me.LCIGudang.Location = New System.Drawing.Point(20, 44)
        Me.LCIGudang.Name = "LCIGudang"
        Me.LCIGudang.Size = New System.Drawing.Size(335, 24)
        Me.LCIGudang.Text = "Gudang"
        Me.LCIGudang.TextSize = New System.Drawing.Size(41, 13)
        Me.LCIGudang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LCIPPn
        '
        Me.LCIPPn.Control = Me.CBOTipePPn
        Me.LCIPPn.CustomizationFormText = "Tipe PPn"
        Me.LCIPPn.Location = New System.Drawing.Point(20, 68)
        Me.LCIPPn.MaxSize = New System.Drawing.Size(165, 24)
        Me.LCIPPn.MinSize = New System.Drawing.Size(165, 24)
        Me.LCIPPn.Name = "LCIPPn"
        Me.LCIPPn.Size = New System.Drawing.Size(165, 24)
        Me.LCIPPn.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LCIPPn.Text = "Tipe PPn"
        Me.LCIPPn.TextSize = New System.Drawing.Size(41, 13)
        Me.LCIPPn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'ESIPPn
        '
        Me.ESIPPn.AllowHotTrack = False
        Me.ESIPPn.CustomizationFormText = "ESIPPn"
        Me.ESIPPn.Location = New System.Drawing.Point(185, 68)
        Me.ESIPPn.MinSize = New System.Drawing.Size(10, 24)
        Me.ESIPPn.Name = "ESIPPn"
        Me.ESIPPn.Size = New System.Drawing.Size(170, 24)
        Me.ESIPPn.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.ESIPPn.Text = "ESIPPn"
        Me.ESIPPn.TextSize = New System.Drawing.Size(0, 0)
        Me.ESIPPn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 150)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(0, 24)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(104, 24)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(376, 25)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.AllowHotTrack = False
        Me.EmptySpaceItem6.CustomizationFormText = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem6.MaxSize = New System.Drawing.Size(0, 20)
        Me.EmptySpaceItem6.MinSize = New System.Drawing.Size(10, 20)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(376, 20)
        Me.EmptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem6.Text = "EmptySpaceItem6"
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'LCIKat
        '
        Me.LCIKat.Control = Me.CBOKat
        Me.LCIKat.CustomizationFormText = "Kategori"
        Me.LCIKat.Location = New System.Drawing.Point(20, 92)
        Me.LCIKat.MaxSize = New System.Drawing.Size(160, 24)
        Me.LCIKat.MinSize = New System.Drawing.Size(160, 24)
        Me.LCIKat.Name = "LCIKat"
        Me.LCIKat.Size = New System.Drawing.Size(160, 24)
        Me.LCIKat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LCIKat.Text = "Kategori"
        Me.LCIKat.TextSize = New System.Drawing.Size(41, 13)
        Me.LCIKat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'EmptySpaceItem7
        '
        Me.EmptySpaceItem7.AllowHotTrack = False
        Me.EmptySpaceItem7.CustomizationFormText = "EmptySpaceItem7"
        Me.EmptySpaceItem7.Location = New System.Drawing.Point(180, 92)
        Me.EmptySpaceItem7.Name = "EmptySpaceItem7"
        Me.EmptySpaceItem7.Size = New System.Drawing.Size(175, 24)
        Me.EmptySpaceItem7.Text = "EmptySpaceItem7"
        Me.EmptySpaceItem7.TextSize = New System.Drawing.Size(0, 0)
        '
        'FPilihPeriode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 195)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Blue"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "FPilihPeriode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".: Pilih Periode :."
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.CBOKat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CBOTipePPn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SLUGudang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPAkhir.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPAkhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPAwal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPAwal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LCIGudang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LCIPPn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ESIPPn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LCIKat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DTPAkhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DTPAwal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents SLUGudang As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LCIGudang As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CBOTipePPn As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LCIPPn As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ESIPPn As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents CBOKat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LCIKat As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem7 As DevExpress.XtraLayout.EmptySpaceItem
End Class
