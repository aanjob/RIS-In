<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRestoreDB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRestoreDB))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.CEDeleteDB = New DevExpress.XtraEditors.CheckEdit()
        Me.ProgressPanel1 = New DevExpress.XtraWaitForm.ProgressPanel()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.BCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.BProses = New DevExpress.XtraEditors.SimpleButton()
        Me.BSearchLoc = New DevExpress.XtraEditors.SimpleButton()
        Me.TBPathSave = New DevExpress.XtraEditors.TextEdit()
        Me.TBNamaDB = New DevExpress.XtraEditors.TextEdit()
        Me.BSearchDB = New DevExpress.XtraEditors.SimpleButton()
        Me.TBPath = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem8 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LCIPB = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.CEDeleteDB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TBPathSave.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TBNamaDB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TBPath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LCIPB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.CEDeleteDB)
        Me.LayoutControl1.Controls.Add(Me.ProgressPanel1)
        Me.LayoutControl1.Controls.Add(Me.PictureEdit1)
        Me.LayoutControl1.Controls.Add(Me.BCancel)
        Me.LayoutControl1.Controls.Add(Me.BProses)
        Me.LayoutControl1.Controls.Add(Me.BSearchLoc)
        Me.LayoutControl1.Controls.Add(Me.TBPathSave)
        Me.LayoutControl1.Controls.Add(Me.TBNamaDB)
        Me.LayoutControl1.Controls.Add(Me.BSearchDB)
        Me.LayoutControl1.Controls.Add(Me.TBPath)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(653, 209)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'CEDeleteDB
        '
        Me.CEDeleteDB.Location = New System.Drawing.Point(157, 12)
        Me.CEDeleteDB.Name = "CEDeleteDB"
        Me.CEDeleteDB.Properties.Caption = "Delete Current Database"
        Me.CEDeleteDB.Size = New System.Drawing.Size(484, 19)
        Me.CEDeleteDB.StyleController = Me.LayoutControl1
        Me.CEDeleteDB.TabIndex = 13
        '
        'ProgressPanel1
        '
        Me.ProgressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ProgressPanel1.Appearance.Options.UseBackColor = True
        Me.ProgressPanel1.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.ProgressPanel1.AppearanceCaption.Options.UseFont = True
        Me.ProgressPanel1.AppearanceDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ProgressPanel1.AppearanceDescription.Options.UseFont = True
        Me.ProgressPanel1.Caption = "Waiting Process Restore Database"
        Me.ProgressPanel1.Description = "Process ..."
        Me.ProgressPanel1.Location = New System.Drawing.Point(157, 161)
        Me.ProgressPanel1.Name = "ProgressPanel1"
        Me.ProgressPanel1.Size = New System.Drawing.Size(484, 36)
        Me.ProgressPanel1.StyleController = Me.LayoutControl1
        Me.ProgressPanel1.TabIndex = 12
        Me.ProgressPanel1.Text = "ProgressPanel1"
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = Global.RIS.My.Resources.Resources.Restore_b
        Me.PictureEdit1.Location = New System.Drawing.Point(12, 22)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.ZoomPercent = 54
        Me.PictureEdit1.Size = New System.Drawing.Size(141, 163)
        Me.PictureEdit1.StyleController = Me.LayoutControl1
        Me.PictureEdit1.TabIndex = 11
        '
        'BCancel
        '
        Me.BCancel.Image = Global.RIS.My.Resources.Resources.Batal
        Me.BCancel.Location = New System.Drawing.Point(401, 127)
        Me.BCancel.Name = "BCancel"
        Me.BCancel.Size = New System.Drawing.Size(71, 30)
        Me.BCancel.StyleController = Me.LayoutControl1
        Me.BCancel.TabIndex = 10
        Me.BCancel.Text = "Cancel"
        '
        'BProses
        '
        Me.BProses.Image = Global.RIS.My.Resources.Resources.Proses
        Me.BProses.Location = New System.Drawing.Point(326, 127)
        Me.BProses.Name = "BProses"
        Me.BProses.Size = New System.Drawing.Size(71, 30)
        Me.BProses.StyleController = Me.LayoutControl1
        Me.BProses.TabIndex = 9
        Me.BProses.Text = "Proses"
        '
        'BSearchLoc
        '
        Me.BSearchLoc.Image = Global.RIS.My.Resources.Resources.Find
        Me.BSearchLoc.Location = New System.Drawing.Point(607, 93)
        Me.BSearchLoc.Name = "BSearchLoc"
        Me.BSearchLoc.Size = New System.Drawing.Size(34, 30)
        Me.BSearchLoc.StyleController = Me.LayoutControl1
        Me.BSearchLoc.TabIndex = 8
        '
        'TBPathSave
        '
        Me.TBPathSave.EditValue = ""
        Me.TBPathSave.Location = New System.Drawing.Point(244, 97)
        Me.TBPathSave.Name = "TBPathSave"
        Me.TBPathSave.Size = New System.Drawing.Size(359, 20)
        Me.TBPathSave.StyleController = Me.LayoutControl1
        Me.TBPathSave.TabIndex = 7
        '
        'TBNamaDB
        '
        Me.TBNamaDB.EditValue = ""
        Me.TBNamaDB.Location = New System.Drawing.Point(244, 69)
        Me.TBNamaDB.Name = "TBNamaDB"
        Me.TBNamaDB.Size = New System.Drawing.Size(184, 20)
        Me.TBNamaDB.StyleController = Me.LayoutControl1
        Me.TBNamaDB.TabIndex = 6
        '
        'BSearchDB
        '
        Me.BSearchDB.Image = Global.RIS.My.Resources.Resources.Find
        Me.BSearchDB.Location = New System.Drawing.Point(607, 35)
        Me.BSearchDB.Name = "BSearchDB"
        Me.BSearchDB.Size = New System.Drawing.Size(34, 30)
        Me.BSearchDB.StyleController = Me.LayoutControl1
        Me.BSearchDB.TabIndex = 5
        '
        'TBPath
        '
        Me.TBPath.EditValue = ""
        Me.TBPath.Location = New System.Drawing.Point(244, 39)
        Me.TBPath.Name = "TBPath"
        Me.TBPath.Size = New System.Drawing.Size(359, 20)
        Me.TBPath.StyleController = Me.LayoutControl1
        Me.TBPath.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.EmptySpaceItem1, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem2, Me.LayoutControlItem5, Me.EmptySpaceItem6, Me.EmptySpaceItem8, Me.LayoutControlItem8, Me.LCIPB, Me.LayoutControlItem9, Me.EmptySpaceItem2, Me.EmptySpaceItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(653, 209)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.TBPath
        Me.LayoutControlItem1.CustomizationFormText = "Source File DB"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(145, 23)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(450, 24)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(450, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 6, 2)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Source File DB"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(84, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.TBNamaDB
        Me.LayoutControlItem3.CustomizationFormText = "Nama DB Restore"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(145, 57)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(275, 24)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(275, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(275, 24)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Nama DB Restore"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(84, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.TBPathSave
        Me.LayoutControlItem4.CustomizationFormText = "Letak Restore DB"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(145, 81)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(450, 24)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(450, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 6, 2)
        Me.LayoutControlItem4.Size = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Letak Restore DB"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(84, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(420, 57)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(213, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.BProses
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(314, 115)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(75, 34)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(75, 34)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(75, 34)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.BCancel
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(389, 115)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(75, 34)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(75, 34)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(75, 34)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.BSearchDB
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(595, 23)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.BSearchLoc
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(595, 81)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.AllowHotTrack = False
        Me.EmptySpaceItem6.CustomizationFormText = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(145, 115)
        Me.EmptySpaceItem6.MaxSize = New System.Drawing.Size(169, 34)
        Me.EmptySpaceItem6.MinSize = New System.Drawing.Size(169, 34)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(169, 34)
        Me.EmptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem6.Text = "EmptySpaceItem6"
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem8
        '
        Me.EmptySpaceItem8.AllowHotTrack = False
        Me.EmptySpaceItem8.CustomizationFormText = "EmptySpaceItem8"
        Me.EmptySpaceItem8.Location = New System.Drawing.Point(464, 115)
        Me.EmptySpaceItem8.Name = "EmptySpaceItem8"
        Me.EmptySpaceItem8.Size = New System.Drawing.Size(169, 34)
        Me.EmptySpaceItem8.Text = "EmptySpaceItem8"
        Me.EmptySpaceItem8.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.PictureEdit1
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 10)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(145, 167)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(145, 167)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(145, 167)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "LayoutControlItem8"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextToControlDistance = 0
        Me.LayoutControlItem8.TextVisible = False
        '
        'LCIPB
        '
        Me.LCIPB.Control = Me.ProgressPanel1
        Me.LCIPB.CustomizationFormText = "LCIPB"
        Me.LCIPB.Location = New System.Drawing.Point(145, 149)
        Me.LCIPB.MinSize = New System.Drawing.Size(54, 20)
        Me.LCIPB.Name = "LCIPB"
        Me.LCIPB.Size = New System.Drawing.Size(488, 40)
        Me.LCIPB.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LCIPB.Text = "LCIPB"
        Me.LCIPB.TextSize = New System.Drawing.Size(0, 0)
        Me.LCIPB.TextToControlDistance = 0
        Me.LCIPB.TextVisible = False
        Me.LCIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.CEDeleteDB
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(145, 0)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(488, 23)
        Me.LayoutControlItem9.Text = "LayoutControlItem9"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextToControlDistance = 0
        Me.LayoutControlItem9.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(145, 10)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 177)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(145, 12)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'BackgroundWorker1
        '
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'FRestoreDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(653, 209)
        Me.ControlBox = False
        Me.Controls.Add(Me.LayoutControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Blue"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "FRestoreDB"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".: Restore Database :."
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.CEDeleteDB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TBPathSave.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TBNamaDB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TBPath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LCIPB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BSearchLoc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TBPathSave As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TBNamaDB As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BSearchDB As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TBPath As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents BCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BProses As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem8 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ProgressPanel1 As DevExpress.XtraWaitForm.ProgressPanel
    Friend WithEvents LCIPB As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents CEDeleteDB As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
End Class
