Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO

Public Class FPerusahaan
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String
    Dim Kode As Guid
    Dim Pic(), PicLama() As Byte
    Dim ImageLama As Image
    Dim msLama As New MemoryStream()

    Sub CekAkses()
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("PrsEd"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True

        'Me.TBPerusahaan.Properties.ReadOnly = True
        'Me.MAlamat.Properties.ReadOnly = True
        Me.TBKota.Properties.ReadOnly = True
        'Me.TBTelp.Properties.ReadOnly = True
        'Me.TBFax.Properties.ReadOnly = True
        'Me.TBPemilik.Properties.ReadOnly = True
        'Me.TBNPWP.Properties.ReadOnly = True
        Me.TBTTPOLk.Properties.ReadOnly = True
        Me.TBMM1.Properties.ReadOnly = True
        Me.TBMM2.Properties.ReadOnly = True
        Me.TBOngkir.Properties.ReadOnly = True
        Me.TBApp.Properties.ReadOnly = True
        Me.SPBatasBB.Properties.ReadOnly = True
        Me.TBPersenLbhBhn.Properties.ReadOnly = True
        Me.TBPersenPPn.Properties.ReadOnly = True
        Me.SPPjBBID.Properties.ReadOnly = True
        Me.PGambar.Properties.ReadOnly = True
        Me.CEPrintDate.Properties.ReadOnly = True
        Me.CEAutoBM.Properties.ReadOnly = True

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBEdit.Enabled = False
        Me.BVBExit.Enabled = False

        'Me.TBPerusahaan.Properties.ReadOnly = False
        'Me.MAlamat.Properties.ReadOnly = False
        Me.TBKota.Properties.ReadOnly = False
        'Me.TBTelp.Properties.ReadOnly = False
        'Me.TBFax.Properties.ReadOnly = False
        'Me.TBPemilik.Properties.ReadOnly = False
        'Me.TBNPWP.Properties.ReadOnly = False
        Me.TBTTPOLk.Properties.ReadOnly = False
        Me.TBMM1.Properties.ReadOnly = False
        Me.TBMM2.Properties.ReadOnly = False
        Me.TBOngkir.Properties.ReadOnly = False
        Me.TBApp.Properties.ReadOnly = False
        Me.SPBatasBB.Properties.ReadOnly = False
        Me.TBPersenLbhBhn.Properties.ReadOnly = False
        Me.TBPersenPPn.Properties.ReadOnly = False
        Me.SPPjBBID.Properties.ReadOnly = False
        Me.PGambar.Properties.ReadOnly = False
        Me.CEPrintDate.Properties.ReadOnly = False
        Me.CEAutoBM.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True
    End Sub

    Public Sub FillDt()

        'Me.TBPerusahaan.EditValue = MainModule.NmPerusahaan
        'Me.MAlamat.EditValue = MainModule.Alamat
        Me.TBKota.EditValue = MainModule.Kota
        'Me.TBTelp.EditValue = MainModule.Telp
        'Me.TBFax.EditValue = MainModule.Fax
        'Me.TBPemilik.EditValue = MainModule.NmPemilik
        'Me.TBNPWP.EditValue = MainModule.NPWP
        Me.TBTTPOLk.EditValue = MainModule.TTPOLk
        Me.TBMM1.EditValue = MainModule.MM1
        Me.TBMM2.EditValue = MainModule.MM2
        Me.TBOngkir.EditValue = MainModule.OngkosKirim
        Me.TBApp.EditValue = MainModule.NmApp
        Me.SPBatasBB.EditValue = MainModule.BatasBB
        Me.TBPersenLbhBhn.EditValue = MainModule.PersenLbh
        Me.TBPersenPPn.EditValue = MainModule.PersenPPn
        Me.CEAutoBM.EditValue = MainModule.AutoBM
        Me.CEPrintDate.EditValue = MainModule.PrintDt
        Me.SPPjBBID.EditValue = MainModule.PjBBID
      

        Dim cmd As New SqlCommand("Select PicID,Picture From M_Image Where ID='P01'", koneksi)
        Dim newImage As Image
        Dim Reader As SqlClient.SqlDataReader
        With cmd
            .Connection = koneksi

            With koneksi
                .Open()
                Reader = cmd.ExecuteReader()

                If Reader.HasRows Then
                    While Reader.Read
                        Kode = Reader.Item(0)
                        Pic = Reader.Item(1)
                        PicLama = Reader.Item(1)
                    End While
                Else
                    Pic = Nothing
                    PicLama = Nothing
                End If
                Reader.Close()
                .Close()
            End With
        End With

        If Pic Is Nothing Then
            Me.PGambar.Image = Nothing
        Else
            Using ms As New MemoryStream(Pic, 0, Pic.Length)
                ms.Write(Pic, 0, Pic.Length)
                newImage = Image.FromStream(ms, True)
                msLama = ms
                ImageLama = newImage
            End Using
            Me.PGambar.Image = newImage
        End If
    End Sub

    Private Sub FPerusahaan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Perusahaan"
    End Sub

    Private Sub FPerusahaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        FillDt()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Perusahaan"

        OpenControl()
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        If Me.PGambar.Image IsNot Nothing Then
            Dim ms As New MemoryStream()

            If Object.ReferenceEquals(ImageLama, Me.PGambar.EditValue) Then
                ms = msLama
            Else
                Me.PGambar.Image.Save(ms, Me.PGambar.Image.RawFormat)
                Pic = ms.GetBuffer
                ms.Close()
            End If
        End If

        Dim cmSP As New SqlCommand("SPUpM_Perusahaan")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            '.Parameters.Add("@Perusahaan", SqlDbType.VarChar).Value = Me.TBPerusahaan.EditValue
            '.Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
            .Parameters.Add("@Kota", SqlDbType.VarChar).Value = Me.TBKota.EditValue
            '.Parameters.Add("@Telp", SqlDbType.VarChar).Value = Me.TBTelp.EditValue
            '.Parameters.Add("@Fax", SqlDbType.VarChar).Value = Me.TBFax.EditValue
            '.Parameters.Add("@Pemilik", SqlDbType.VarChar).Value = Me.TBPemilik.EditValue
            '.Parameters.Add("@NPWP", SqlDbType.VarChar).Value = Me.TBNPWP.EditValue
            .Parameters.Add("@TTPOLk", SqlDbType.VarChar).Value = Me.TBTTPOLk.EditValue
            .Parameters.Add("@MM1", SqlDbType.VarChar).Value = Me.TBMM1.EditValue
            .Parameters.Add("@MM2", SqlDbType.VarChar).Value = Me.TBMM2.EditValue
            .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.TBOngkir.EditValue
            .Parameters.Add("@App", SqlDbType.VarChar).Value = Me.TBApp.EditValue
            .Parameters.Add("@Batas", SqlDbType.Int).Value = Me.SPBatasBB.EditValue
            .Parameters.Add("@PjBBID", SqlDbType.Int).Value = Me.SPPjBBID.EditValue
            .Parameters.Add("@PersenLebih", SqlDbType.Int).Value = Me.TBPersenLbhBhn.EditValue
            .Parameters.Add("@PersenPPn", SqlDbType.Int).Value = Me.TBPersenPPn.EditValue
            .Parameters.Add("@PrintDate", SqlDbType.Bit).Value = Me.CEPrintDate.EditValue
            .Parameters.Add("@AutoBM", SqlDbType.Bit).Value = Me.CEAutoBM.EditValue
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            Try
                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    .Close()
                End With

                If Me.PGambar.Image IsNot Nothing Then
                    If PicLama Is Nothing Then
                        InsImage("P01", "Perusahaan", Pic)
                    Else
                        UpImage(Kode, "P01", "Perusahaan", Pic)
                    End If
                End If

                If x = 0 Then
                    XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                'MainModule.NmPerusahaan = Me.TBPerusahaan.EditValue
                'MainModule.Alamat = Me.MAlamat.EditValue
                MainModule.Kota = Me.TBKota.EditValue
                'MainModule.Telp = Me.TBTelp.EditValue
                'MainModule.Fax = Me.TBFax.EditValue
                'MainModule.NmPemilik = Me.TBPemilik.EditValue
                'MainModule.NPWP = Me.TBNPWP.EditValue
                MainModule.TTPOLk = Me.TBTTPOLk.EditValue
                MainModule.MM1 = Me.TBMM1.EditValue
                MainModule.MM2 = Me.TBMM2.EditValue
                MainModule.OngkosKirim = Me.TBOngkir.EditValue
                MainModule.NmApp = Me.TBApp.EditValue
                MainModule.BatasBB = Me.SPBatasBB.EditValue
                MainModule.PersenLbh = Me.TBPersenLbhBhn.EditValue
                MainModule.PersenPPn = Me.TBPersenPPn.EditValue
                MainModule.PrintDt = Me.CEPrintDate.EditValue
                MainModule.AutoBM = Me.CEAutoBM.EditValue
                MainModule.PjBBID = Me.SPPjBBID.EditValue

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End With

        LockControl()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub
End Class