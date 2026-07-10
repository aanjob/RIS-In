<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLapJualHarian
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FLapJualHarian))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.BExExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.PivotGridControl1 = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField6 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.BProses = New DevExpress.XtraEditors.SimpleButton()
        Me.DTPAkhir = New DevExpress.XtraEditors.DateEdit()
        Me.DTPAwal = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PivotGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPAkhir.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPAkhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPAwal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTPAwal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.BExExcel)
        Me.LayoutControl1.Controls.Add(Me.PivotGridControl1)
        Me.LayoutControl1.Controls.Add(Me.BProses)
        Me.LayoutControl1.Controls.Add(Me.DTPAkhir)
        Me.LayoutControl1.Controls.Add(Me.DTPAwal)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(947, 422)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'BExExcel
        '
        Me.BExExcel.Image = Global.RIS.My.Resources.Resources.ExExcel
        Me.BExExcel.Location = New System.Drawing.Point(837, 380)
        Me.BExExcel.Name = "BExExcel"
        Me.BExExcel.Size = New System.Drawing.Size(98, 30)
        Me.BExExcel.StyleController = Me.LayoutControl1
        Me.BExExcel.TabIndex = 10
        Me.BExExcel.Text = "Export Excel"
        '
        'PivotGridControl1
        '
        Me.PivotGridControl1.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField4, Me.PivotGridField5, Me.PivotGridField6})
        Me.PivotGridControl1.Location = New System.Drawing.Point(12, 46)
        Me.PivotGridControl1.Name = "PivotGridControl1"
        Me.PivotGridControl1.Size = New System.Drawing.Size(923, 330)
        Me.PivotGridControl1.TabIndex = 9
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "Tanggal"
        Me.PivotGridField1.CellFormat.FormatString = "dd MMMM yyyy"
        Me.PivotGridField1.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.PivotGridField1.FieldName = "Tanggal"
        Me.PivotGridField1.Name = "PivotGridField1"
        Me.PivotGridField1.ValueFormat.FormatString = "dd MMMM yyyy"
        Me.PivotGridField1.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 1
        Me.PivotGridField2.Caption = "Cabang"
        Me.PivotGridField2.FieldName = "Cabang"
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.Options.ShowTotals = False
        Me.PivotGridField2.Width = 200
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.Caption = "Merk"
        Me.PivotGridField3.FieldName = "Merk"
        Me.PivotGridField3.Name = "PivotGridField3"
        Me.PivotGridField3.Options.ShowTotals = False
        '
        'PivotGridField4
        '
        Me.PivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField4.AreaIndex = 0
        Me.PivotGridField4.Caption = "Dos"
        Me.PivotGridField4.FieldName = "Dos"
        Me.PivotGridField4.Name = "PivotGridField4"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField5.AreaIndex = 1
        Me.PivotGridField5.Caption = "Psg"
        Me.PivotGridField5.FieldName = "Psg"
        Me.PivotGridField5.Name = "PivotGridField5"
        '
        'PivotGridField6
        '
        Me.PivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField6.AreaIndex = 1
        Me.PivotGridField6.Caption = "Kategori"
        Me.PivotGridField6.FieldName = "Kat"
        Me.PivotGridField6.Name = "PivotGridField6"
        '
        'BProses
        '
        Me.BProses.Image = Global.RIS.My.Resources.Resources.Proses
        Me.BProses.Location = New System.Drawing.Point(372, 12)
        Me.BProses.Name = "BProses"
        Me.BProses.Size = New System.Drawing.Size(71, 30)
        Me.BProses.StyleController = Me.LayoutControl1
        Me.BProses.TabIndex = 8
        Me.BProses.Text = "Proses"
        '
        'DTPAkhir
        '
        Me.DTPAkhir.EditValue = New Date(2022, 3, 29, 15, 28, 24, 779)
        Me.DTPAkhir.Location = New System.Drawing.Point(222, 17)
        Me.DTPAkhir.Name = "DTPAkhir"
        Me.DTPAkhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPAkhir.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPAkhir.Properties.DisplayFormat.FormatString = "dd MMMM yyyy"
        Me.DTPAkhir.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DTPAkhir.Properties.EditFormat.FormatString = "dd MMMM yyyy"
        Me.DTPAkhir.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DTPAkhir.Properties.Mask.EditMask = "dd MMMM yyyy"
        Me.DTPAkhir.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.DTPAkhir.Size = New System.Drawing.Size(146, 20)
        Me.DTPAkhir.StyleController = Me.LayoutControl1
        Me.DTPAkhir.TabIndex = 5
        '
        'DTPAwal
        '
        Me.DTPAwal.EditValue = New Date(2022, 3, 29, 15, 28, 24, 779)
        Me.DTPAwal.Location = New System.Drawing.Point(51, 17)
        Me.DTPAwal.Name = "DTPAwal"
        Me.DTPAwal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPAwal.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTPAwal.Properties.DisplayFormat.FormatString = "dd MMMM yyyy"
        Me.DTPAwal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DTPAwal.Properties.EditFormat.FormatString = "dd MMMM yyyy"
        Me.DTPAwal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DTPAwal.Properties.Mask.EditMask = "dd MMMM yyyy"
        Me.DTPAwal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.DTPAwal.Size = New System.Drawing.Size(147, 20)
        Me.DTPAwal.StyleController = Me.LayoutControl1
        Me.DTPAwal.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem5, Me.EmptySpaceItem1, Me.LayoutControlItem6, Me.LayoutControlItem3, Me.EmptySpaceItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(947, 422)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.DTPAwal
        Me.LayoutControlItem1.CustomizationFormText = "Periode"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(190, 34)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(190, 34)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(190, 34)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Periode"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(36, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.DTPAkhir
        Me.LayoutControlItem2.CustomizationFormText = "s/d"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(190, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(170, 34)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(170, 34)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(170, 34)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "s/d"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(15, 13)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.BProses
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(360, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(75, 34)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(75, 34)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(75, 34)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(435, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(492, 34)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.PivotGridControl1
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 34)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(927, 334)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.BExExcel
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(825, 368)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(102, 34)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(102, 34)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(102, 34)
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
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 368)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(825, 34)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'FLapJualHarian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(947, 422)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Blue"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "FLapJualHarian"
        Me.Text = ".: Laporan Penjualan Harian :."
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PivotGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPAkhir.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPAkhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPAwal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTPAwal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BProses As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DTPAkhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DTPAwal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents PivotGridControl1 As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField6 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents BExExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
End Class
