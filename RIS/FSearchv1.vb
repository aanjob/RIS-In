Imports System.Data.SqlClient

Public Class FSearchv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsSearch As System.Data.DataSet
    Public Indicator As Int16
    Dim Master, F1, F2, F3, F5 As String
    Dim F4 As Date

    Public Sub New(ByVal IsiMaster As String, ByVal Filter1 As String, ByVal Filter2 As String, ByVal Filter3 As String, ByVal Filter4 As Date, Filter5 As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Indicator = "100"

        Master = IsiMaster
        F1 = Filter1
        F2 = Filter2
        F3 = Filter3
        F4 = Filter4
        F5 = Filter5

        If Filter5 = "Tampil All" Then
            Me.LCICek.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCICek.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If Master = "Gudang" Then
            If IO.File.Exists("SrGudang.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrGudang.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select GdID,L.Nama,Alamat,K.Nama As Kota From M_Gudang L Inner Join M_Kota K On L.KotaID=K.KotaID Where Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_GudangSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_GudangSr")

                DsSearch.WriteXml("SrGudang.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_GudangSr"

            Me.GridView1.Columns("Nama").Width = 100
            Me.GridView1.Columns("Alamat").Width = 150
            Me.GridView1.Columns("Kota").Width = 100

            Me.GridView1.Columns("GdID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Posisi" Then
            If IO.File.Exists("SrPosisi.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrPosisi.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select PosisiID From M_Posisi Where Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_PosisiSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_PosisiSr")

                DsSearch.WriteXml("SrPosisi.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_PosisiSr"

            Me.GridView1.Columns("PosisiID").Width = 300

            Me.GridView1.Columns("PosisiID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Jenis Cust" Then
            If IO.File.Exists("SrJnsCust.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrJnsCust.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct Jenis From M_JnsCust Where Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_JnsCustSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_JnsCustSr")
                DsSearch.Tables("M_JnsCustSr").Rows.Add("%")

                DsSearch.WriteXml("SrJnsCust.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_JnsCustSr"

            Me.GridView1.Columns("Jenis").Width = 300
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Sts Harga" Then
            If IO.File.Exists("SrStsHarga.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrStsHarga.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct stsHarga From M_BrgHarga Where Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_BrgHargaSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_BrgHargaSr")
                DsSearch.Tables("M_BrgHargaSr").Rows.Add("%")

                DsSearch.WriteXml("SrStsHarga.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_BrgHargaSr"

            Me.GridView1.Columns("stsHarga").Width = 300
            Me.GridView1.Columns("stsHarga").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Cabang" Then
            If IO.File.Exists("SrCabang.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrCabang.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select CabID,Cabang From M_Cab Where Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_CabSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_CabSr")
                DsSearch.Tables("M_CabSr").Rows.Add("%", "Semua Cabang")

                DsSearch.WriteXml("SrCabang.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_CabSr"

            Me.GridView1.Columns("Cabang").Width = 100

            Me.GridView1.Columns("CabID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Cabang").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Proses" Then
            If IO.File.Exists("SrProses.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrProses.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Proses From M_Proses Where Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_ProsSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_ProsSr")
                DsSearch.Tables("M_ProsSr").Rows.Add("%")

                DsSearch.WriteXml("SrProses.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_ProsSr"

            Me.GridView1.Columns("Proses").Width = 100

            Me.GridView1.Columns("Proses").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Sales" Then
            If IO.File.Exists("SrSales.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrSales.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select SalID,Area,Nama From M_Sales Where Aktif='True' and Gol<>'Bahan'", koneksi)

                cmsl.TableMappings.Add("Table", "M_SalesSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_SalesSr")

                DsSearch.WriteXml("SrSales.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_SalesSr"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("SalID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Area").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "SalesCabang" Then
            'Filter1=CabID

            If IO.File.Exists("SrSalesCab.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrSalesCab.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select S.SalID,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='Lokal' and CabID='" & Filter1 & "' and CS.Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_SalesSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_SalesSr")

                DsSearch.WriteXml("SrSalesCab.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_SalesSr"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("SalID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "Supplier" Then
            If IO.File.Exists("SrSupplier.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrSupplier.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select SuppID,S.Nama,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_Supp")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_Supp")

                DsSearch.WriteXml("SrSupplier.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Supp"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("SuppID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Kota").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Style BJ" Then
            If IO.File.Exists("SrStyle.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrStyle.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select StyleID,Nama From M_Style", koneksi)

                cmsl.TableMappings.Add("Table", "M_Style")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_Style")

                DsSearch.WriteXml("SrStyle.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Style"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("StyleID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "M_Brg" Then
            'Filter1=Gol
            If IO.File.Exists("SrBrg" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBrg" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select License,Luxury,ArtCode,HSCode,ArtName,StyleID,MerkID,KatID,JnsID,SubJns,Urut,B.WrnID,W.Nama as Warna, AssID,SatID,Isi,SubGrup,Grup From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where B.Aktif='True' and Gol='" & Filter1 & "'", koneksi)

                cmsl.TableMappings.Add("Table", "M_Brg")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_Brg")

                DsSearch.WriteXml("SrBrg" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Brg"

            Me.GridView1.Columns("License").Width = 75
            Me.GridView1.Columns("Luxury").Width = 75
            Me.GridView1.Columns("ArtCode").Width = 150
            Me.GridView1.Columns("HSCode").Width = 100
            Me.GridView1.Columns("ArtName").Width = 200
            Me.GridView1.Columns("StyleID").Width = 100
            Me.GridView1.Columns("Warna").Width = 150

            Me.GridView1.Columns("MerkID").Visible = False
            Me.GridView1.Columns("KatID").Visible = False
            Me.GridView1.Columns("JnsID").Visible = False
            Me.GridView1.Columns("SubJns").Visible = False
            Me.GridView1.Columns("Urut").Visible = False
            Me.GridView1.Columns("WrnID").Visible = False
            Me.GridView1.Columns("AssID").Visible = False
            Me.GridView1.Columns("SubGrup").Visible = False
            Me.GridView1.Columns("Grup").Visible = False


            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("HSCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "M_BrgJO" Then
            If IO.File.Exists("SrBrgJO.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBrgJO.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select B.ArtCode,HSCode,ArtName, StyleID, B.KatID, K.Nama As Kategori, B.WrnID, W.Nama As Warna, Ass, SatID, Isi From M_Brg B Inner Join M_BrgKat K On B.KatID=K.KatID Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where B.Aktif='True' and Gol='Job Order'", koneksi)

                cmsl.TableMappings.Add("Table", "M_Brg")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_Brg")

                DsSearch.WriteXml("SrBrgJO.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select B.ArtCode,HSCode,ArtName, StyleID, B.KatID, K.Nama As Kategori, B.WrnID, W.Nama As Warna, Ass, ArtCust, Brand, SatID, Isi From M_Brg B Inner Join M_BrgKat K On B.KatID=K.KatID Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where B.Aktif='True' and Gol='Job Order'", koneksi)

            'cmsl.TableMappings.Add("Table", "M_Brg")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "M_Brg")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Brg"

            Me.GridView1.OptionsView.ColumnAutoWidth = False

            Me.GridView1.Columns("WrnID").Visible = False
            Me.GridView1.Columns("KatID").Visible = False


            Me.GridView1.Columns("ArtCode").Width = 100
            Me.GridView1.Columns("HSCode").Width = 100
            Me.GridView1.Columns("ArtName").Width = 200
            Me.GridView1.Columns("StyleID").Width = 100
            Me.GridView1.Columns("Kategori").Width = 120
            Me.GridView1.Columns("SatID").Width = 100
            Me.GridView1.Columns("Warna").Width = 100

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("HSCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Brg Model" Then
            'Filter1=ArtName
            'Filter2= Warna

            If IO.File.Exists("SrBrgModel" & Filter1 & Filter2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBrgModel" & Filter1 & Filter2 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select ArtCode,Ass As Uk From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where ArtName='" & Filter1 & "' And W.Nama='" & Filter2 & "' And SatID In ('P','PCS')", koneksi)

                cmsl.TableMappings.Add("Table", "M_Brg")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_Brg")

                DsSearch.WriteXml("SrBrgModel" & Filter1 & Filter2 & ".xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select ArtCode,Ass As Uk From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where ArtName='" & Filter1 & "' And W.Nama='" & Filter2 & "' And SatID='P'", koneksi)

            'cmsl.TableMappings.Add("Table", "M_Brg")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "M_Brg")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Brg"

            Me.GridView1.Columns("ArtCode").Width = 120
            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Master Mesin" Then

            If IO.File.Exists("SrMesin.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrMesin.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select BBID,B.Nama,B.JnsID,J.Nama as Jenis,Merk,SubJns,Kode,Uk,Jasa,ThnProd,Sat From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True' and J.JnsPers ='Mesin' Order By B.Nama", koneksi)

                cmsl.TableMappings.Add("Table", "M_Mesin")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_Mesin")

                DsSearch.WriteXml("SrMesin.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Mesin"

            Me.GridView1.Columns("BBID").Width = 100
            Me.GridView1.Columns("Nama").Width = 350

            Me.GridView1.Columns("JnsID").Visible = False
            Me.GridView1.Columns("Merk").Visible = False
            Me.GridView1.Columns("SubJns").Visible = False
            Me.GridView1.Columns("Kode").Visible = False
            Me.GridView1.Columns("Uk").Visible = False
            Me.GridView1.Columns("Jasa").Visible = False
            Me.GridView1.Columns("ThnProd").Visible = False

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "M_BB" Then
            'Filter1=InisialBC
            'Filter2=JnsPers


            If IO.File.Exists("SrBB" & Filter2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBB" & Filter2 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select '" & Filter1 & "'+BBID as BBID,HSCode,B.Nama,DivPO,B.JnsID,J.Nama as Jenis,Merk,SubJns,Tbl,Gram, Wrn,Kode,Hard,Uk,Jasa,ThnProd,Sat,Ket,stsJasa From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True' and J.Gol Like'" & Filter2 & "%' Order By B.Nama", koneksi)

                cmsl.TableMappings.Add("Table", "M_BB" & Filter2)
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_BB" & Filter2)

                DsSearch.WriteXml("SrBB" & Filter2 & ".xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select BBID,HSCode,B.Nama,B.JnsID, J.Nama as Jenis,SubJns,Tbl,Gram,Wrn,Kode,Hard,Uk,Jasa,Sat,Ket From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True'", koneksi)

            'cmsl.TableMappings.Add("Table", "M_BB")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "M_BB")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_BB" & Filter2

            Me.GridView1.Columns("BBID").Width = 100
            Me.GridView1.Columns("HSCode").Width = 100

            Me.GridView1.Columns("Nama").Width = 350
            'Me.GridView1.Columns("Jenis").Width = 100
            Me.GridView1.Columns("DivPO").Visible = False
            Me.GridView1.Columns("JnsID").Visible = False
            Me.GridView1.Columns("Merk").Visible = False
            Me.GridView1.Columns("SubJns").Visible = False
            Me.GridView1.Columns("Tbl").Visible = False
            Me.GridView1.Columns("Gram").Visible = False
            Me.GridView1.Columns("Wrn").Visible = False
            Me.GridView1.Columns("Kode").Visible = False
            Me.GridView1.Columns("Hard").Visible = False
            Me.GridView1.Columns("Uk").Visible = False
            Me.GridView1.Columns("Jasa").Visible = False
            Me.GridView1.Columns("Ket").Visible = False
            Me.GridView1.Columns("stsJasa").Visible = False
            Me.GridView1.Columns("ThnProd").Visible = False

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("HSCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Proses Produksi" Then
            If IO.File.Exists("SrProsProd.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrProsProd.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Proses From M_Proses Where Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_ProsesSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_ProsesSr")

                DsSearch.WriteXml("SrProsProd.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_ProsesSr"

            Me.GridView1.Columns("Proses").Width = 300

            Me.GridView1.Columns("Proses").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "Bahan BtNum" Then
            'Filter1=InisialBC
            'Filter2=JnsPers
            'Filter3=GdID
            'Filter4=Tanggal

            If IO.File.Exists("SrBBBtNum" & Filter2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBBtNum" & Filter2 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Jenis,BtNum As BatchNum,'" & Filter1 & "'+BBID As BBID,Nama,Sat From(Select J.Nama As Jenis,B.BBID as BBID,B.Nama,BtNum,B.Sat From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Inner Join T_StokBB S On B.BBID=S.BBID Where B.Aktif='True' and J.Gol Like'" & Filter2 & "%' and GdID='" & Filter3 & "' and Tanggal<='" & Filter4 & "' Group By B.BBID,B.Nama,J.Nama,Sat,BtNum Having sum(Masuk - Keluar) > 0) as x Order By Nama", koneksi)

                cmsl.TableMappings.Add("Table", "BahanBtNum" & Filter2)
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BahanBtNum" & Filter2)

                DsSearch.WriteXml("SrBBBtNum" & Filter2 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BahanBtNum" & Filter2

            Me.GridView1.Columns("Jenis").Width = 150
            Me.GridView1.Columns("BBID").Width = 120
            Me.GridView1.Columns("Nama").Width = 400
            Me.GridView1.Columns("BatchNum").Width = 150
            Me.GridView1.Columns("Sat").Width = 80

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Bahan BtNum No Stok" Then
            'Filter1=InisialBC
            'Filter2=JnsPers
            'Filter3=GdID
            'Filter4=Tanggal

            If IO.File.Exists("SrBBBtNumNS" & Filter2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBBtNumNS" & Filter2 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct J.Nama As Jenis,'" & Filter1 & "'+B.BBID as BBID,B.Nama,BtNum As BatchNum,B.Sat From M_BB B Left Outer Join M_BBJns J On B.JnsID=J.JnsID Left Outer Join T_StokBB S On B.BBID=S.BBID Where B.Aktif='True' and J.Gol Like'" & Filter2 & "%' Order By Nama", koneksi)

                cmsl.TableMappings.Add("Table", "BahanBtNumNS" & Filter2)
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BahanBtNumNS" & Filter2)

                DsSearch.WriteXml("SrBBBtNumNS" & Filter2 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BahanBtNumNS" & Filter2

            Me.GridView1.Columns("Jenis").Width = 150
            Me.GridView1.Columns("BBID").Width = 120
            Me.GridView1.Columns("Nama").Width = 400
            Me.GridView1.Columns("BatchNum").Width = 150
            Me.GridView1.Columns("Sat").Width = 80

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Batch Number" Then
            'Filter1=InisialBC
            'Filter2=JnsPers
            'Filter3=GdID
            'Filter4=Tanggal

            If IO.File.Exists("BatchNumber" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("BatchNumber" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct BtNum As BatchNum From T_StokBB where BBID='" & Filter1 & "'", koneksi)

                cmsl.TableMappings.Add("Table", "BtNum" & Filter1)
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BtNum" & Filter1)

                DsSearch.WriteXml("BatchNumber" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BtNum" & Filter1

            Me.GridView1.Columns("BatchNum").Width = 150

            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Collect PO" Then
            'Filter1=POID
            'Filter2=ArtCode

            If IO.File.Exists("SrCollPO.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrCollPO.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select CollPOIDD,H.CollPOID,C.Nama As Cust,ArtCode,Qty-(Select Isnull((Select Sum(Qty) From T_POBJCollPO where CollPOIDD=D.CollPOIDD and POID<>'" & Filter1 & "'),0)) As Qty From T_CollPO H Inner Join T_CollPODtl D On H.CollPOID=D.CollPOID Inner Join M_Cust C On C.CustID=H.CustID Where ArtCode='" & Filter2 & "'", koneksi)

                cmsl.TableMappings.Add("Table", "T_CollPO")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_CollPO")

                DsSearch.WriteXml("SrCollPO.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_CollPO"

            Me.GridView1.Columns("Cust").Width = 200

            Me.GridView1.Columns("CollPOID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Cust").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "JualBJ" Then
            'Filter1=Jns Cust
            'Filter2=Gol
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrHarga.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrHarga.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi,stsHarga,Harga,DiscOB,Gol From M_Brg B Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & Filter1 & "' and Gol='" & Filter2 & "' and B.Aktif='True' and H.Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "Harga")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "Harga")

                DsSearch.WriteXml("SrHarga.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "Harga"

            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("DiscOB").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscOB").DisplayFormat.FormatString = "{0:n3}"
            'Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            'Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "JualBJ Manual" Then
            'Filter2=Gol

            If IO.File.Exists("JualBJMn.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("JualBJMn.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi,'' as stsHarga, 0.00 As Harga, 0.000 as DiscOB, 0 As Stok From M_Brg B Where B.Aktif='True' and Gol='" & Filter2 & "'", koneksi)

                cmsl.TableMappings.Add("Table", "JualBJMn")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "JualBJMn")

                DsSearch.WriteXml("JualBJMn.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridView1.OptionsBehavior.Editable = True

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "JualBJMn"

            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("DiscOB").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscOB").DisplayFormat.FormatString = "{0:n3}"
            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Harga").OptionsColumn.AllowEdit = True
            'Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("DiscOB").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("ArtCode").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("SatID").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Isi").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("stsHarga").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Stok").OptionsColumn.AllowEdit = False

        ElseIf Master = "JualPromo" Then
            'Filter1=Promo
            'Filter2=Paket
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=DocID
            If IO.File.Exists("SrPromo.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrPromo.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select * from(Select D.ArtCode,ArtName,B.SatID,B.Isi,dbo.fcStokBJ(B.ArtCode,'" & Filter3 & "','" & Filter4 & "','" & Filter5 & "') As Stok From T_PromoFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode  Where PromoID='" & Filter1 & "' and Paket='" & Filter2 & "' and B.Aktif='True') As x Where Stok >0", koneksi)

                cmsl.TableMappings.Add("Table", "Promo")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "Promo")

                DsSearch.WriteXml("SrPromo.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select * from(Select D.ArtCode,ArtName,B.SatID,B.Isi,dbo.fcStokBJ(B.ArtCode,'" & Filter3 & "','" & Filter4 & "','" & Filter5 & "') As Stok From T_PromoFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode  Where PromoID='" & Filter1 & "' and Paket='" & Filter2 & "' and B.Aktif='True') As x Where Stok >0", koneksi)

            'cmsl.TableMappings.Add("Table", "Harga")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "Harga")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "Promo"

            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "StokBJ" Then
            'Filter1=Gol
            'Filter2=
            'Filter3=
            'Filter4=
            'Filter5=

            If IO.File.Exists("SrStokBJ.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrStokBJ.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi From M_Brg B Where B.Aktif='True' and Gol='" & Filter1 & "'", koneksi)

                cmsl.TableMappings.Add("Table", "StokBJ")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "StokBJ")

                DsSearch.WriteXml("SrStokBJ.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBJ"

            'Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            'Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "StokBB" Then
            'Filter1=
            'Filter2=
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=DocID
            If IO.File.Exists("SrStokBB.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrStokBB.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select * from(Select B.BBID,Nama As Bahan,B.Sat,dbo.fcStokBB(B.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter5 & "') As Stok From M_BB B Where B.Aktif='True') As x Where Stok >0", koneksi)

                cmsl.TableMappings.Add("Table", "StokBB")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "StokBB")

                DsSearch.WriteXml("SrStokBB.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select * from(Select B.BBID,Nama As Bahan,B.Sat,dbo.fcStokBB(B.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter5 & "') As Stok From M_BB B Where B.Aktif='True') As x Where Stok >0", koneksi)

            'cmsl.TableMappings.Add("Table", "StokBB")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "StokBB")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBB"

            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Stok BJ Gol" Then
            'Filter1=SatID
            'Filter2=Gol
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=DocID
            If IO.File.Exists("SrStokBJGol.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrStokBJGol.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi From M_Brg B Where B.Aktif='True' and SatID like '" & Filter1 & "' and Gol Like '" & Filter2 & "'", koneksi)

                cmsl.TableMappings.Add("Table", "StokBJ")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "StokBJ")

                DsSearch.WriteXml("SrStokBJGol.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBJ"

            'Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            'Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BJ Gol" Then
            'Filter1=SatID
            'Filter2=Gol
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBJGol" & Filter1 & Filter2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBJGol" & Filter1 & Filter2 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi From M_Brg B Where B.Aktif='True' and SatID like '" & Filter1 & "' and Gol In ('" & Filter2 & "','Promosi')", koneksi)

                cmsl.TableMappings.Add("Table", "StokBJ")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "StokBJ")

                DsSearch.WriteXml("SrBJGol" & Filter1 & Filter2 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBJ"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Stok BJ Op" Then
            'Filter1=SatID
            'Filter2=Gol
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=DocID
            If IO.File.Exists("SrStokBJOp.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrStokBJOp.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select * from(Select B.ArtCode,ArtName,B.SatID,B.Isi,0 As Stok From M_Brg B Where B.Aktif='True' and SatID like '" & Filter1 & "' and Gol='" & Filter2 & "') As x Where ArtCode Not In (Select ArtCode From T_StokBJ Where GdID='" & Filter3 & "' and PeriodID=" & MainModule.periodAktif & ")", koneksi)

                cmsl.TableMappings.Add("Table", "StokBJ")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "StokBJ")

                DsSearch.WriteXml("SrStokBJOp.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBJ"

            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Stok BB Op" Then
            'Filter1=
            'Filter2=
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=DocID

            If IO.File.Exists("SrStokBBOp.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrStokBBOp.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select * from(Select B.JnsID, J.Nama As Jenis,B.BBID,B.Nama As Bahan,B.Sat,0 As Stok From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True') As x Where BBID Not In (Select BBID From T_StokBB Where GdID='" & Filter3 & "' and PeriodID=" & MainModule.periodAktif & ")", koneksi)
                'cmsl = New SqlDataAdapter("Select * from(Select B.JnsID, J.Nama As Jenis,B.BBID,B.Nama As Bahan,B.Sat,dbo.fcStokBB(B.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter5 & "') As Stok From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True') As x Where BBID Not In (Select BBID From T_StokBB Where GdID='" & Filter3 & "')", koneksi)

                cmsl.TableMappings.Add("Table", "StokBB")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "StokBB")

                DsSearch.WriteXml("SrStokBBOp.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select * from(Select B.JnsID, J.Nama As Jenis,B.BBID,B.Nama As Bahan,B.Sat,dbo.fcStokBB(B.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter5 & "') As Stok From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True') As x Where BBID Not In (Select BBID From T_StokBB Where GdID='" & Filter3 & "')", koneksi)

            'cmsl.TableMappings.Add("Table", "StokBB")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "StokBB")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBB"

            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Divisi" Then
            'Filter1 : Data

            If IO.File.Exists("SrDivisi.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrDivisi.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select DivID,Nama,PrsPol From M_Div Where Data Like '" & Filter1 & "' and Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_Div")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_Div")

                DsSearch.WriteXml("SrDivisi.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select DivID,Nama From M_Div Where Aktif='True'", koneksi)

            'cmsl.TableMappings.Add("Table", "M_Div")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "M_Div")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Div"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("PrsPol").Visible = False

            Me.GridView1.Columns("DivID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Komponen" Then
            If IO.File.Exists("SrKomponen.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrKomponen.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select KompID,Nama From M_Komp Where Aktif='True'", koneksi)

                cmsl.TableMappings.Add("Table", "M_Komp")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_Komp")

                DsSearch.WriteXml("SrKomponen.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select KompID,Nama From M_Komp Where Aktif='True'", koneksi)

            'cmsl.TableMappings.Add("Table", "M_Komp")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "M_Komp")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Komp"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("KompID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Spec" Then
            If IO.File.Exists("SrSpec.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrSpec.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select SpecID,H.CustID,C.Nama As Customer,StyleID,Brand,ShoeLast,ArtName,Warna,RangeSize,SampleSize, H.Ket,Dibuat,Pattern,Chaser,PPC,PembKulit,PembNonKulit,Teknik,Mengetahui From M_Spec H Inner Join M_Cust C On H.CustID=C.CustID", koneksi)

                cmsl.TableMappings.Add("Table", "M_Spec")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_Spec")

                DsSearch.WriteXml("SrSpec.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select SpecID,H.CustID,C.Nama As Customer,StyleID,Brand,ArtName,Warna, RangeSize,SampleSize,H.Ket, Pattern,Dibuat,Teknik,PembKulit,PembNonKulit,Menyetujui From M_Spec H Inner Join M_Cust C On H.CustID=C.CustID", koneksi)

            'cmsl.TableMappings.Add("Table", "M_Spec")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "M_Spec")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Spec"

            Me.GridView1.Columns("SpecID").Width = 120
            Me.GridView1.Columns("Customer").Width = 150
            Me.GridView1.Columns("ArtName").Width = 120
            Me.GridView1.Columns("Warna").Width = 100
            'Me.GridView1.Columns("Ket").Width = 150
            'Me.GridView1.Columns("Pattern").Width = 120
            'Me.GridView1.Columns("Dibuat").Width = 120
            'Me.GridView1.Columns("Teknik").Width = 120
            'Me.GridView1.Columns("PembKulit").Width = 120
            'Me.GridView1.Columns("PembNonKulit").Width = 120
            'Me.GridView1.Columns("Menyetujui").Width = 120

            Me.GridView1.Columns("Ket").Visible = False
            Me.GridView1.Columns("Pattern").Visible = False
            Me.GridView1.Columns("Dibuat").Visible = False
            Me.GridView1.Columns("Teknik").Visible = False
            Me.GridView1.Columns("Chaser").Visible = False
            Me.GridView1.Columns("PPC").Visible = False
            Me.GridView1.Columns("PembKulit").Visible = False
            Me.GridView1.Columns("PembNonKulit").Visible = False
            Me.GridView1.Columns("Mengetahui").Visible = False
            Me.GridView1.Columns("RangeSize").Visible = False
            Me.GridView1.Columns("SampleSize").Visible = False

            Me.GridView1.Columns("SpecID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Customer").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Model" Then
            If IO.File.Exists("SrModel.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrModel.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select MdlID,M.SpecID,M.StyleID,S.Brand,M.ArtName,M.Warna,M.RangeSize,M.SampleSize,PersenGenerate, M.Ket From M_Model M Inner Join M_Spec S On M.SpecId=S.SpecID ", koneksi)

                cmsl.TableMappings.Add("Table", "M_ModelSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_ModelSr")

                DsSearch.WriteXml("SrModel.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_ModelSr"

            Me.GridView1.Columns("MdlID").Width = 120
            Me.GridView1.Columns("SpecID").Width = 120
            Me.GridView1.Columns("Brand").Width = 150
            Me.GridView1.Columns("ArtName").Width = 120
            Me.GridView1.Columns("Warna").Width = 100

            Me.GridView1.Columns("RangeSize").Visible = False
            Me.GridView1.Columns("SampleSize").Visible = False
            Me.GridView1.Columns("PersenGenerate").Visible = False
            Me.GridView1.Columns("Ket").Visible = False

            Me.GridView1.Columns("MdlID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("SpecID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Brand").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Purchase Request Tooling" Then
            'Filter1=SuppID
            'Filter2=CustID
            'Filter3=
            'Filter4=
            'Filter5=

            If IO.File.Exists("SrPRTool" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrPRTool" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select * From (Select H.PRTID,D.PRTIDD,H.Tanggal,StyleID,Style,D.BBID,Case When D.Ket='' Then B.Nama Else B.Nama +' ('+ D.Ket +')' End As Bahan,Qty-BtlOrder-(Select Isnull((Select Sum(Qty) From T_POBBDtl Where BOMID=H.PRTID and DocIDD=D.PRTIDD),0)) as Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=H.SuppID Order By Tanggal desc),0)) As Harga,'False' as stsJasa,D.Sat From T_PRTool H Inner Join T_PRToolDtl D On H.PRTID=D.PRTID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB SB On SB.BBID=D.BBID Where H.SuppID='" & Filter1 & "' and SB.SuppID='" & Filter1 & "' and H.CustID='" & Filter2 & "') As x Where Qty>0", koneksi)

                cmsl.TableMappings.Add("Table", "T_PRToolSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_PRToolSr")

                DsSearch.WriteXml("SrPRTool" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_PRToolSr"

            Me.GridView1.Columns("stsJasa").Visible = False
            Me.GridView1.Columns("PRTIDD").Visible = False

            Me.GridView1.Columns("PRTID").Width = 120
            Me.GridView1.Columns("Tanggal").Width = 150
            Me.GridView1.Columns("StyleID").Width = 120
            Me.GridView1.Columns("Style").Width = 150
            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

            Me.GridView1.Columns("PRTID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Tanggal").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("StyleID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Style").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Purchase Request Sparepart" Then
            'Filter1=SuppID
            'Filter2=Tipe
            'Filter3=Jenis
            'Filter4=
            'Filter5=

            If IO.File.Exists("SrPRSpM" & Filter1 & Filter2 & Filter3 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrPRSpM" & Filter1 & Filter2 & Filter3 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select * From (Select H.PRSMID,PRSMIDD,Tanggal,D.BBID,Case When D.Ket='' Then B.Nama Else B.Nama +' ('+ D.Ket +')' End As Bahan,Qty-BtlOrder-(Select Isnull((Select Sum(Qty) From T_POBBDtl Where BOMID=H.PRSMID and DocIDD=D.PRSMIDD),0)) as Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID='" & Filter1 & "' Order By Tanggal desc),0)) As Harga,'False' as stsJasa,D.Sat From T_PRSpM H Inner Join T_PRSpMDtl D On H.PRSMID=D.PRSMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where Tipe='" & Filter2 & "' and Jenis='" & Filter3 & "' and S.SuppID='" & Filter1 & "') as x Where Qty>0", koneksi)

                cmsl.TableMappings.Add("Table", "T_PRSpMSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_PRSpMSr")

                DsSearch.WriteXml("SrPRSpM" & Filter1 & Filter2 & Filter3 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_PRSpMSr"

            Me.GridView1.Columns("stsJasa").Visible = False
            Me.GridView1.Columns("PRSMIDD").Visible = False

            Me.GridView1.Columns("PRSMID").Width = 120
            Me.GridView1.Columns("Tanggal").Width = 150
            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

            Me.GridView1.Columns("PRSMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Tanggal").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Request" Then
            'Filter1=Jenis
            'Filter2=
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrReq" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrReq" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                'cmsl = New SqlDataAdapter("Select BOMID, ArtName,Warna From T_BOM Where stsLunas = 'False' OR Jenis='" & Filter1 & "'", koneksi)
                cmsl = New SqlDataAdapter("Select ReqPID,Tanggal,Jenis From T_ReqP where stsApp='True' and stsLunas='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_ReqSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_ReqSr")

                DsSearch.WriteXml("SrReq" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_ReqSr"

            Me.GridView1.Columns("ReqPID").Width = 120
            Me.GridView1.Columns("Tanggal").Width = 250
            Me.GridView1.Columns("Jenis").Width = 150

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

            Me.GridView1.Columns("ReqPID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Tanggal").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BOM" Then
            'Filter1=Jenis
            'Filter2=
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBOM" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBOM" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct H.BOMID,MerkID,JnsID,H.ArtName,Warna,TotPsg+TotPsgPol As TotPsg From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner join M_Brg Br On P.ArtCode=Br.ArtCode Where stsApp='True' and stsLunas='False' and stsBatal='False' and stsBtlProd='False' Union All Select Distinct H.TambahanID,MerkID,JnsID, BH.ArtName,Warna,TotPsg+TotPsgPol As TotPsg From T_BOMTam H Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner join M_Brg Br On P.ArtCode=Br.ArtCode Inner Join T_BOM BH On H.BOMID=BH.BOMID Where H.stsApp='True' and stsLunas='False' and stsBatal='False' and stsBtlProd='False' Order By BOMID", koneksi)

                'cmsl = New SqlDataAdapter("Select Distinct H.BOMID,MerkID,JnsID,H.ArtName,Warna,TotPsg+TotPsgPol As TotPsg From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner join M_Brg Br On P.ArtCode=Br.ArtCode Where stsApp='True' and stsLunas='False' and stsBatal='False' and stsBtlProd='False' Order By BOMID", koneksi)

                cmsl.TableMappings.Add("Table", "T_BOMSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_BOMSr")

                DsSearch.WriteXml("SrBOM" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If
            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_BOMSr"

            Me.GridView1.Columns("MerkID").Visible = False
            Me.GridView1.Columns("JnsID").Visible = False

            Me.GridView1.Columns("BOMID").Width = 120
            Me.GridView1.Columns("ArtName").Width = 250
            Me.GridView1.Columns("Warna").Width = 150

            Me.GridView1.Columns("BOMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BOM Jasa" Then
            'Filter1=Jenis
            'Filter2=
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBOMJs" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBOMJs" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct BOMID,ArtName,Warna,TotPsg From T_HProdJamDtl Where stsJasa='True' and TotPsg-(Select Sum(Pack) From T_HProdJamDtl as x where x.BOMID=T_HProdJamDtl.BOMID)>0", koneksi)

                cmsl.TableMappings.Add("Table", "T_BOMJsSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_BOMJsSr")

                DsSearch.WriteXml("SrBOMJs" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If
            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_BOMJsSr"

            Me.GridView1.Columns("BOMID").Width = 120
            Me.GridView1.Columns("ArtName").Width = 250
            Me.GridView1.Columns("Warna").Width = 150

            Me.GridView1.Columns("BOMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB With BOM" Then
            'Filter1=BOMID
            'Filter2=POID
            'Filter3=SuppID
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBBWBOM.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBWBOM.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select * From (Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.BOMID and BBID=D.BBID and POID<>'" & Filter2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where BOMID='" & Filter1 & "' and S.SuppID='" & Filter3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID,SuppID,D.stsJasa Union All Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.TambahanID and BBID=D.BBID and POID<>'" & Filter2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOMTamDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where TambahanID='" & Filter1 & "' and S.SuppID='" & Filter3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,TambahanID,SuppID,D.stsJasa) as x Order By Bahan", koneksi)

                'cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.BOMID and BBID=D.BBID and POID<>'" & Filter2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where BOMID='" & Filter1 & "' and S.SuppID='" & Filter3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID,SuppID,D.stsJasa  Order By Bahan", koneksi)

                cmsl.TableMappings.Add("Table", "BBBOM")
                DsSearch = New System.Data.DataSet
                cmsl.SelectCommand.CommandTimeout = 9000
                cmsl.Fill(DsSearch, "BBBOM")
                DsSearch.WriteXml("SrBBWBOM.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOM"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("stsJasa").Visible = False

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"
            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BOM Sch" Then
            'Filter1=Jenis
            'Filter2=
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBOMSch" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBOMSch" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                'cmsl = New SqlDataAdapter("Select BOMID, ArtName,Warna From T_BOM Where stsLunas = 'False' OR Jenis='" & Filter1 & "'", koneksi)
                cmsl = New SqlDataAdapter("Select Distinct H.BOMID,H.ArtName,Warna,TotPsg+TotPsgPol As TotPsg From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner join M_Brg Br On P.ArtCode=Br.ArtCode Inner Join T_SchPrPPIC  SC On H.BOMID=SC.BOMID Where stsApp='True' and stsLunas='False' and stsBatal='False' Order By BOMID", koneksi)

                cmsl.TableMappings.Add("Table", "T_BOMSchSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_BOMSchSr")

                DsSearch.WriteXml("SrBOMSch" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If
            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_BOMSchSr"

            Me.GridView1.Columns("BOMID").Width = 120
            Me.GridView1.Columns("ArtName").Width = 250
            Me.GridView1.Columns("Warna").Width = 150

            Me.GridView1.Columns("BOMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB With Request" Then
            'Filter1=BOMID
            'Filter2=POID
            'Filter3=SuppID
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBBWReq.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBWReq.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Sum(Req) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.ReqPID and BBID=D.BBID and POID<>'" & Filter2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_ReqPDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where ReqPID='" & Filter1 & "' and S.SuppID='" & Filter3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,ReqPID,SuppID,D.stsJasa Order By Bahan", koneksi)

                cmsl.TableMappings.Add("Table", "BBBOM")
                DsSearch = New System.Data.DataSet
                cmsl.SelectCommand.CommandTimeout = 9000
                cmsl.Fill(DsSearch, "BBBOM")
                DsSearch.WriteXml("SrBBWBOM.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOM"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("stsJasa").Visible = False

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB Kanan With BOM" Then
            'Filter1=BOMID
            'Filter2=POID
            'Filter3=SuppID
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBBKnWBOM.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBKnWBOM.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) As Qty From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where BOMID='" & Filter1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID Union All Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) As Qty,D.stsJasa From T_BOMTamDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where TambahanID='" & Filter1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,TambahanID,D.stsJasa", koneksi)

                'cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) As Qty From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where BOMID='" & Filter1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID", koneksi)

                cmsl.TableMappings.Add("Table", "BBBOM")
                DsSearch = New System.Data.DataSet
                cmsl.SelectCommand.CommandTimeout = 9000
                cmsl.Fill(DsSearch, "BBBOM")
                DsSearch.WriteXml("SrBBKnWBOM.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.BOMID and BBID=D.BBID and POID<>'" & Filter2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where BOMID='" & Filter1 & "' and S.SuppID='" & Filter3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID,SuppID", koneksi)

            'cmsl.TableMappings.Add("Table", "BBBOM")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "BBBOM")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOM"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB Kanan With Request" Then
            'Filter1=BOMID
            'Filter2=POID
            'Filter3=SuppID
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBBKnWReq.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBKnWReq.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Sum(Req) As Qty From T_ReqDtl D Inner Join M_BB B On D.BBID=B.BBID Where ReqID='" & Filter1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,ReqID", koneksi)

                cmsl.TableMappings.Add("Table", "BBReq")
                DsSearch = New System.Data.DataSet
                cmsl.SelectCommand.CommandTimeout = 9000
                cmsl.Fill(DsSearch, "BBReq")
                DsSearch.WriteXml("SrBBKnWReq.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.BOMID and BBID=D.BBID and POID<>'" & Filter2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where BOMID='" & Filter1 & "' and S.SuppID='" & Filter3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID,SuppID", koneksi)

            'cmsl.TableMappings.Add("Table", "BBBOM")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "BBBOM")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOM"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB No BOM" Then
            'Filter1=SuppID
            'Filter2=JnsPers
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBBNBOM" & Filter2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBNBOM" & Filter2 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct B.BBID,B.Nama As Bahan,B.Sat,(Select Isnull((Select Top 1 (HargaBeli*NilTukarRp)+HargaBahan From M_BBHarga Where BBID=B.BBID and SuppID=S.SuppID Order By Tanggal desc,HargaIDD desc),0)) As Harga,Uk,stsJasa From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Inner Join M_SuppBB S On B.BBID=S.BBID Where S.SuppID Like '" & Filter1 & "' and J.Gol Like'" & Filter2 & "%' and B.Aktif='True' Order By B.Nama", koneksi)

                cmsl.TableMappings.Add("Table", "BB")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BB")

                DsSearch.WriteXml("SrBBNBOM" & Filter2 & ".xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select B.BBID,B.Nama As Bahan,B.Sat,0 As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=B.BBID Order By Tanggal desc,HargaIDD desc),0)) As Harga From M_BB B Inner Join M_SuppBB S On B.BBID=S.BBID Where S.SuppID Like '" & Filter1 & "' and B.Aktif='True'", koneksi)

            'cmsl.TableMappings.Add("Table", "BB")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "BB")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BB"

            Me.GridView1.Columns("Harga").Visible = False
            Me.GridView1.Columns("stsJasa").Visible = False

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB No BOM BtNum" Then
            'Filter1=SuppID
            'Filter2=JnsPers
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBBNBOMBtNum" & Filter2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBNBOMBtNum" & Filter2 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct B.BBID,SB.BtNum As BatchNum,B.Nama As Bahan,B.Sat,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=B.BBID and SuppID=S.SuppID Order By Tanggal desc,HargaIDD desc),0)) As Harga,Uk,stsJasa From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Inner Join M_SuppBB S On B.BBID=S.BBID Inner Join T_StokBB SB On B.BBID=SB.BBID Where S.SuppID Like '" & Filter1 & "' and J.Gol Like'" & Filter2 & "%' and B.Aktif='True' Order By B.Nama", koneksi)

                cmsl.TableMappings.Add("Table", "BB")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BB")

                DsSearch.WriteXml("SrBBNBOMBtNum" & Filter2 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BB"

            Me.GridView1.Columns("Harga").Visible = False
            Me.GridView1.Columns("stsJasa").Visible = False

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB BOM" Then
            'Filter1=BOMID
            'Filter2=BPBID
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=Inisial BC

            If IO.File.Exists("SrBBBOM.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBBOM.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select '" & Filter5 & "'+D.BBID as BBID,B.Nama As Bahan,D.Sat,Case When Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and BtNum=x.BtNum and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID and BtNum=x.BtNum),0)) > dbo.fcStokBB(D.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter2 & "') Then dbo.fcStokBB(D.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter2 & "') Else Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and BtNum=x.BtNum and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID and BtNum=x.BtNum),0)) End  As Qty  From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where BOMID='" & Filter1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID", koneksi)

                cmsl.TableMappings.Add("Table", "BBBOM")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BBBOM")

                DsSearch.WriteXml("SrBBBOM.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Case When Round(Sum(Keb),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) > dbo.fcStokBB(D.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter2 & "') Then dbo.fcStokBB(D.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter2 & "') Else Round(Sum(Keb),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) End  As Qty  From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where BOMID='" & Filter1 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID", koneksi)

            'cmsl.TableMappings.Add("Table", "BBBOM")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "BBBOM")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOM"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB BOM BtNum" Then
            'Filter1=BOMID
            'Filter2=BPBID
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=Inisial BC

            If IO.File.Exists("SrBBBOMBtNum.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBBOMBtNum.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select '" & Filter5 & "'+BBID As BBID,BtNum As BatchNum,Bahan,Sat,Case When Qty>Stok Then Stok Else Qty End As Qty From (Select x.*,tbl.BtNum, tbl.Stok From (Select BOMID,D.BBID as BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) As Qty From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where BOMID='" & Filter1 & "' and B.Aktif='True' Group By BOMID,D.BBID,B.Nama,D.Sat) as x Inner Join (Select BBID,BtNum,Sum(Masuk)-Sum(Keluar) As Stok From T_StokBB Where GdID='" & Filter3 & "' and Tanggal<='" & Filter4 & "' Group By BBID,BtNum) As tbl On x.BBID=tbl.BBID Union All Select x.*,tbl.BtNum, tbl.Stok From (Select TambahanID,D.BBID as BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.TambahanID and BBID=D.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.TambahanID and BBID=D.BBID),0)) As Qty From T_BOMTamDtl D Inner Join M_BB B On D.BBID=B.BBID Where TambahanID='" & Filter1 & "' and B.Aktif='True' Group By TambahanID,D.BBID,B.Nama,D.Sat) as x Inner Join (Select BBID,BtNum,Sum(Masuk)-Sum(Keluar) As Stok From T_StokBB Where GdID='" & Filter3 & "' and Tanggal<='" & Filter4 & "' Group By BBID,BtNum) As tbl On x.BBID=tbl.BBID) as y Order By BBID", koneksi)

                'cmsl = New SqlDataAdapter("Select '" & Filter5 & "'+BBID As BBID,BtNum As BatchNum,Bahan,Sat,Case When Qty>Stok Then Stok Else Qty End As Qty From (Select x.*,tbl.BtNum, tbl.Stok From (Select BOMID,D.BBID as BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) As Qty From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where BOMID='" & Filter1 & "' and B.Aktif='True' Group By BOMID,D.BBID,B.Nama,D.Sat) as x Inner Join (Select BBID,BtNum,Sum(Masuk)-Sum(Keluar) As Stok From T_StokBB Where GdID='" & Filter3 & "' and Tanggal<='" & Filter4 & "' Group By BBID,BtNum) As tbl On x.BBID=tbl.BBID) as y Order By BBID", koneksi)

                cmsl.TableMappings.Add("Table", "BBBOMBtNum")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BBBOMBtNum")

                DsSearch.WriteXml("SrBBBOMBtNum.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOMBtNum"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "BB Request" Then
            'Filter1=ReqID
            'Filter2=BPBID
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=Inisial BC

            If IO.File.Exists("SrBBReq.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBReq.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select '" & Filter5 & "'+D.BBID as BBID,B.Nama As Bahan,D.Sat,Case When Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.ReqPID and BBID=D.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.ReqPID and BBID=D.BBID),0))) > dbo.fcStokBB(D.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter2 & "') Then dbo.fcStokBB(D.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter2 & "') Else Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.ReqPID and BBID=D.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.ReqPID and BBID=D.BBID),0))) End  As Qty From T_ReqPDtl D Inner Join M_BB B On D.BBID=B.BBID Where ReqPID='" & Filter1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,ReqPID", koneksi)

                cmsl.TableMappings.Add("Table", "BBReq")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BBReq")

                DsSearch.WriteXml("SrBBReq.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBReq"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB Request BtNum" Then
            'Filter1=ReqID
            'Filter2=BPBID
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=Inisial BC

            If IO.File.Exists("SrBBReqBtNum.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBReqBtNum.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select '" & Filter5 & "'+BBID As BBID,BtNum As BatchNum,Bahan,Sat,Case When Qty>Stok Then Stok Else Qty End As Qty From (Select x.*,tbl.BtNum, tbl.Stok From (Select ReqPID,D.BBID as BBID,B.Nama As Bahan,D.Sat,Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.ReqPID and BBID=D.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.ReqPID and BBID=D.BBID),0))) As Qty From T_ReqPDtl D Inner Join M_BB B On D.BBID=B.BBID Where ReqPID='" & Filter1 & "' and B.Aktif='True' Group By ReqPID,D.BBID,B.Nama,D.Sat) as x Inner Join (Select BBID,BtNum,Sum(Masuk)-Sum(Keluar) As Stok From T_StokBB Where GdID='" & Filter3 & "' and Tanggal<='" & Filter4 & "' Group By BBID,BtNum) As tbl On x.BBID=tbl.BBID) as y Order By BBID", koneksi)

                'cmsl = New SqlDataAdapter("Select '" & Filter5 & "'+BBID As BBID,BtNum As BatchNum,Bahan,Sat,Case When Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=x.ReqPID and BBID=x.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=x.ReqPID and BBID=x.BBID),0))) > dbo.fcStokBB(x.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter2 & "',x.BtNum) Then dbo.fcStokBB(x.BBID,'" & Filter3 & "','" & Filter4 & "','" & Filter2 & "',x.BtNum) Else Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=x.ReqPID and BBID=x.BBID and H.BPBID<>'" & Filter2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=x.ReqPID and BBID=x.BBID),0))) End As Qty From(Select Distinct ReqPID, D.BBID, BtNum,B.Nama As Bahan, D.Sat,Req From T_ReqPDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join T_StokBB S on D.BBID=S.BBID Where ReqPID='" & Filter1 & "' and B.Aktif='True'and GdID='" & Filter3 & "')as x Group By BBID,Bahan,Sat,ReqPID,BtNum", koneksi)

                cmsl.TableMappings.Add("Table", "BBReqBtNum")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BBReqBtNum")

                DsSearch.WriteXml("SrBBReqBtNum.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBReqBtNum"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB RPB" Then
            'Filter1=BOMID
            'Filter2=RPBID
            'Filter3=Inisial BC
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBBRPB.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBRPB.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select '" & Filter3 & "'+D.BBID As BBID,BB.Nama As Bahan,D.Sat,(Sum(Qty)-(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=B.DocID and BBID=D.BBID and H.RPBID<>'" & Filter2 & "'),0))) As Qty From T_BPB B Inner Join T_BPBDtl D On B.BPBID=D.BPBID Inner Join M_BB BB On D.BBID=BB.BBID Where B.DocID='" & Filter1 & "' Group By B.DocID,D.BBID,BB.Nama,D.Sat", koneksi)

                cmsl.TableMappings.Add("Table", "BBRPB")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BBRPB")

                DsSearch.WriteXml("SrBBRPB.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select D.BBID,BB.Nama As Bahan,D.Sat,(Sum(Qty)-(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=B.DocID and BBID=D.BBID and H.RPBID<>'" & Filter2 & "'),0))) As Qty From T_BPB B Inner Join T_BPBDtl D On B.BPBID=D.BPBID Inner Join M_BB BB On D.BBID=BB.BBID Where B.DocID='" & Filter1 & "' Group By B.DocID,D.BBID,BB.Nama,D.Sat", koneksi)

            'cmsl.TableMappings.Add("Table", "BBRPB")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "BBRPB")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBRPB"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB RPB BtNum" Then
            'Filter1=BOMID
            'Filter2=RPBID
            'Filter3=Inisial BC
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrBBRPBBtNum.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBRPBBtNum.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select '" & Filter3 & "'+D.BBID As BBID,BtNum As BatchNum,BB.Nama As Bahan,D.Sat,(Sum(Qty)-(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=B.DocID and BBID=D.BBID and H.RPBID<>'" & Filter2 & "' and BtNum=D.BtNum),0))) As Qty From T_BPB B Inner Join T_BPBDtl D On B.BPBID=D.BPBID Inner Join M_BB BB On D.BBID=BB.BBID Where B.DocID='" & Filter1 & "' Group By B.DocID,D.BBID,BB.Nama,D.Sat,BtNum", koneksi)

                cmsl.TableMappings.Add("Table", "BBRPBBtNum")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BBRPBBtNum")

                DsSearch.WriteXml("SrBBRPBBtNum.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBRPBBtNum"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB Model" Then
            'Filter1=MdlID
            'Filter2=BOMID
            'Filter3=DivID
            'Filter4=
            'Filter5=KompID

            If IO.File.Exists("SrBBModel" & Filter1 & Filter3 & Filter5 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBModel" & Filter1 & Filter3 & Filter5 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select BBID,Bahan,UkBB,Sat,Sum(Keb) As Keb From(Select BP.ArtCode,MD.DivID,Md.KompID,MD.BBID, B.Nama As Bahan,UkBB,MD.Sat,Std,Bp.Qty+BP.QtyPol-Bp.BtlOrder As Qty, Case when KaliQty='True' Then Round(Std*(Bp.Qty+BP.QtyPol-Bp.BtlOrder),2) Else Case When Std=0 Then 0 Else Round((Bp.Qty+BP.QtyPol-Bp.BtlOrder)/Std,2) End End As Keb From M_ModelDtl MD Inner Join T_BOM BM On MD.MdlID=BM.MdlID Inner Join T_BOMPO BP On BM.BOMID=BP.BOMID and MD.ArtCode=BP.ArtCode Inner Join M_BB B On MD.BBID=B.BBID Where MD.MdlID='" & Filter1 & "' and BM.BOMID='" & Filter2 & "' and MD.DivID='" & Filter3 & "' and MD.KompID='" & Filter5 & "') as x Group By DivID,KompID,BBID,Bahan,UkBB,Sat", koneksi)

                cmsl.TableMappings.Add("Table", "BBModel")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "BBModel")

                DsSearch.WriteXml("SrBBModel" & Filter1 & Filter3 & Filter5 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBModel"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Keb").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Keb").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "LPB" Then
            'Filter1=SuppID
            'Filter2=Tipe PPn
            'Filter3=Mt Uang
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrLPB.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrLPB.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select POID,TrmID,Tanggal,GdID,DiscP,DiscRp,DiscRpSat From T_TrmBB Where SuppID='" & Filter1 & "' and TipePPn='" & Filter2 & "' and MtUang='" & Filter3 & "' and Tanggal<='" & Filter4 & "' and stsTagih='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_TrmBB")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_TrmBB")

                DsSearch.WriteXml("SrLPB.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select TrmID,Tanggal,GdID,DiscP,DiscRp,DiscRpSat From T_TrmBB Where SuppID='" & Filter1 & "' and stsTagih='False'", koneksi)

            'cmsl.TableMappings.Add("Table", "T_TrmBB")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "T_TrmBB")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_TrmBB"

            Me.GridView1.Columns("GdID").Visible = False
            Me.GridView1.Columns("DiscP").Visible = False
            Me.GridView1.Columns("DiscRp").Visible = False
            Me.GridView1.Columns("DiscRpSat").Visible = False

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("DiscP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscP").DisplayFormat.FormatString = "{0:n3}"
            Me.GridView1.Columns("DiscRp").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscRp").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("DiscRpSat").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscRpSat").DisplayFormat.FormatString = "{0:n3}"

            Me.GridView1.Columns("TrmID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("POID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "LPB BJ" Then
            'Filter1=SuppID
            'Filter2=Tipe PPn
            'Filter3=Mt Uang
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrLPBBJ.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrLPBBJ.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select POID,H.TrmID,Tanggal,GdID From T_TrmBJ H Where JnsDoc='PO' and SuppID='" & Filter1 & "' and TipePPn='" & Filter2 & "' and MtUang='" & Filter3 & "' and Tanggal<='" & Filter4 & "' and stsTagih='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_TrmBJ")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_TrmBJ")

                DsSearch.WriteXml("SrLPBBJ.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_TrmBJ"

            Me.GridView1.Columns("GdID").Visible = False

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

            Me.GridView1.Columns("TrmID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("POID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Retur BB" Then
            'Filter1=SuppID
            'Filter2=Tipe PPn
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrRBL.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrRBL.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select RtrID,Tanggal,GdID,DiscP,DiscRp,DiscRpSat From T_RtrBB Where SuppID='" & Filter1 & "' and TipePPn='" & Filter2 & "' and stsNotaRtr='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_RtrBB")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_RtrBB")

                DsSearch.WriteXml("SrRBL.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select RtrID,Tanggal,GdID,DiscP,DiscRp,DiscRpSat From T_RtrBB Where SuppID='" & Filter1 & "' and stsNotaRtr='False'", koneksi)

            'cmsl.TableMappings.Add("Table", "T_RtrBB")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "T_RtrBB")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_RtrBB"

            Me.GridView1.Columns("GdID").Visible = False
            Me.GridView1.Columns("DiscP").Visible = False
            Me.GridView1.Columns("DiscRp").Visible = False
            Me.GridView1.Columns("DiscRpSat").Visible = False

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("DiscP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscP").DisplayFormat.FormatString = "{0:n3}"
            Me.GridView1.Columns("DiscRp").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscRp").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("DiscRpSat").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscRpSat").DisplayFormat.FormatString = "{0:n3}"

            Me.GridView1.Columns("RtrID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Tagihan" Then
            'Filter1=SuppID
            'Filter2=MtUang
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrTagihan.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrTagihan.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select TagihID,Tanggal,SisaBayar From T_Tagihan Where SuppID='" & Filter1 & "' and MtUang='" & Filter2 & "' and stsLunas='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_TagihanSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_TagihanSr")

                DsSearch.WriteXml("SrTagihan.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_TagihanSr"


            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("TagihID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrHut Retur" Then
            'Filter1=SuppID
            'Filter2=MtUang
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrByrHutRtr.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrByrHutRtr.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select RtrTagihID,Tanggal,SisaPakai From T_RtrTagih Where SuppID='" & Filter1 & "' and MtUang='" & Filter2 & "' and stsPakai='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_RtrTagih")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_RtrTagih")

                DsSearch.WriteXml("SrByrHutRtr.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select RtrTagihID,Tanggal,SisaPakai From T_RtrTagih Where SuppID='" & Filter1 & "' and stsPakai='False'", koneksi)

            'cmsl.TableMappings.Add("Table", "T_RtrTagih")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "T_RtrTagih")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_RtrTagih"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("RtrTagihID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrHut Non Retur" Then
            'Filter1=SuppID
            'Filter2=MtUang
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrByrHutNRtr.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrByrHutNRtr.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select MetodeID,Tanggal,SisaPakai From T_MetodeByr Where SuppID='" & Filter1 & "' and MtUang='" & Filter2 & "' and stsPakai='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_MetodeByr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_MetodeByr")

                DsSearch.WriteXml("SrByrHutNRtr.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select MetodeID,Tanggal,TotBayar From T_MetodeByr Where SuppID='" & Filter1 & "' and stsPakai='False'", koneksi)

            'cmsl.TableMappings.Add("Table", "T_MetodeByr")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "T_MetodeByr")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_MetodeByr"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("MetodeID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrHut JM" Then
            'Filter1=SuppID
            'Filter2=MtUang
            'Filter3=
            'Filter4=
            'Filter5=
            If IO.File.Exists("SrByrHutJM.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrByrHutJM.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select JMID,Tanggal,SisaPakai From T_JMHut Where SuppID='" & Filter1 & "' and MtUang='" & Filter2 & "' and stsPakai='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_JM")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_JM")

                DsSearch.WriteXml("SrByrHutJM.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select RtrTagihID,Tanggal,SisaPakai From T_RtrTagih Where SuppID='" & Filter1 & "' and stsPakai='False'", koneksi)

            'cmsl.TableMappings.Add("Table", "T_RtrTagih")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "T_RtrTagih")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JM"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "Penjualan Giro" Then
            'Filter1=CustID
            'Filter2=Gol
            'Filter3=BGID
            'Filter4=Tanggal
            'Filter5=

            If IO.File.Exists("SrJualGiro.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrJualGiro.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Isnull(DPPBJID,'') as DPPBJID,J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BG H1 Inner Join T_BGDtl D1 On H1.BGID=D1.BGID Where JualID=J.JualID and H1.BGID<>'" & Filter3 & "' and H1.BGID Not In (Select Distinct DocID From T_ByrPiutDtl)),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID=J.JualID and DocID<> '" & Filter3 & "'),0)) As SisaBayar From T_JualBJ J Left Outer Join T_DPPBJDtl D On J.JualID= D.JualID Where J.CustID='" & Filter1 & "' and J.Gol='" & Filter2 & "' and stsLunas='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_JualBJ")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_JualBJ")

                DsSearch.WriteXml("SrJualGiro.xml", XmlWriteMode.WriteSchema)
            End If

            'Sebelum Direvisi
            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select Isnull(DPPBJID,'') as DPPBJID,J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BG H1 Inner Join T_BGDtl D1 On H1.BGID=D1.BGID Where JualID=J.JualID and H1.BGID<>'" & Filter3 & "' and H1.BGID Not In (Select Distinct DocID From T_ByrPiutDtl) and H1.TglCair >='" & Filter4 & "'),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID=J.JualID and DocID<> '" & Filter3 & "'),0)) As SisaBayar From T_JualBJ J Left Outer Join T_DPPBJDtl D On J.JualID= D.JualID Where J.CustID='" & Filter1 & "' and J.Gol='" & Filter2 & "' and stsLunas='False'", koneksi)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualBJ"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("DPPBJID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Penjualan Lain2" Then
            'Filter1=CustID
            'Filter2=Gol
            'Filter3=BGID
            'Filter4=Tanggal
            'Filter5=

            If IO.File.Exists("SrJualLain2.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrJualLain2.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BG H1 Inner Join T_BGDtl D1 On H1.BGID=D1.BGID Where JualID=J.JualID and H1.BGID<>'" & Filter3 & "' and H1.BGID Not In (Select Distinct DocID From T_ByrPiutDtl)),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID=J.JualID and DocID<> '" & Filter3 & "'),0)) As SisaBayar From T_JualBebas J Where J.CustID='" & Filter1 & "' and stsLunas='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_JualLain2")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_JualLain2")

                DsSearch.WriteXml("SrJualLain2.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualLain2"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Penjualan Bahan" Then
            'Filter1=CustID
            'Filter2=Gol
            'Filter3=BGID
            'Filter4=Tanggal
            'Filter5=

            If IO.File.Exists("SrJualBahan.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrJualBahan.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BG H1 Inner Join T_BGDtl D1 On H1.BGID=D1.BGID Where JualID=J.JualID and H1.BGID<>'" & Filter3 & "' and H1.BGID Not In (Select Distinct DocID From T_ByrPiutDtl)),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID=J.JualID and DocID<> '" & Filter3 & "'),0)) As SisaBayar From T_JualBB J Where J.CustID='" & Filter1 & "' and stsLunas='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_JualBahan")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_JualBahan")

                DsSearch.WriteXml("SrJualBahan.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualBahan"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "Penjualan Bayar" Then
            'Filter1=CustID
            'Filter2=Gol
            'Filter3=ByrPiutID
            'Filter4=
            'Filter5=MtUang

            If IO.File.Exists("SrJualByr.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrJualByr.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select DPPBJID,J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & Filter3 & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & Filter3 & "'),0)) As SisaBayar From T_JualBJ J Left Outer Join T_DPPBJDtl D On J.JualID= D.JualID Where J.CustID='" & Filter1 & "' and J.Gol='" & Filter2 & "' and MtUang='" & Filter5 & "' and stsLunas='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_JualBJ")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_JualBJ")

                DsSearch.WriteXml("SrJualByr.xml", XmlWriteMode.WriteSchema)
            End If

            'Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select DPPBJID,J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & Filter3 & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & Filter3 & "'),0)) As SisaBayar From T_JualBJ J Left Outer Join T_DPPBJDtl D On J.JualID= D.JualID Where J.CustID='" & Filter1 & "' and J.Gol='" & Filter2 & "' and stsLunas='False'", koneksi)

            'cmsl.TableMappings.Add("Table", "T_JualBJ")
            'DsSearch = New System.Data.DataSet
            'cmsl.Fill(DsSearch, "T_JualBJ")

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualBJ"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("DPPBJID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Penjualan Lain2 Bayar" Then
            'F1=CustID
            'F2=Gol
            'F3=ByrPiutID
            'F4=
            'F5=MtUang
            If IO.File.Exists("SrJualLain2Byr.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrJualLain2Byr.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & Filter3 & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & Filter3 & "'),0)) As SisaBayar From T_JualBebas J Where J.CustID='" & Filter1 & "' and MtUang='" & Filter5 & "' and stsLunas='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_JualLain2")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_JualLain2")

                DsSearch.WriteXml("SrJualLain2Byr.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualLain2"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Penjualan Bahan Bayar" Then
            'F1=CustID
            'F2=Gol
            'F3=ByrPiutID
            'F4=
            'F5=MtUang
            If IO.File.Exists("SrJualBahanByr.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrJualBahanByr.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & Filter3 & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & Filter3 & "'),0)) As SisaBayar From T_JualBB J Where J.CustID='" & Filter1 & "' and MtUang='" & Filter5 & "' and stsLunas='False'", koneksi)

                cmsl.TableMappings.Add("Table", "T_JualBahan")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_JualBahan")

                DsSearch.WriteXml("SrJualBahanByr.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualBahan"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "ByrPiut" Then
            'Filter1=CustID
            'Filter2=Gol
            'Filter3=MtUang
            'Filter4=Tgl
            'Filter5=
            If IO.File.Exists("SrByrPiut.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrByrPiut.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select CashID As DocID,'Cash' As CaraBayar,'' As Deskripsi,SisaPakai From T_Cash Where CustID='" & Filter1 & "' and stsPakai='False'  and Gol='" & Filter2 & "' and MtUang='" & Filter3 & "' UNION ALL Select BGID As DocID,'BG' As CaraBayar,'' As Deskripsi,SisaPakai From T_BG Where CustID='" & Filter1 & "' and stsPakai='False' and Gol='" & Filter2 & "' and MtUang='" & Filter3 & "' and '" & Filter4 & "'>= TglCair UNION ALL Select PotID As DocID,'DbCr Note' As CaraBayar,Nama As Deskripsi,0 As SisaPakai From M_JnsPot Where Aktif='True' Union All Select VcrID As DocID,'Voucher' As CaraBayar,Header As Deskripsi,SisaPakai From T_Voucher Where CustID='" & Filter1 & "' and stsPakai='False'  and Gol='" & Filter2 & "' and MtUang='" & Filter3 & "'", koneksi)

                cmsl.TableMappings.Add("Table", "CaraBayar")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "CaraBayar")

                DsSearch.WriteXml("SrByrPiut.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrPiut Retur" Then
            'Filter1=CustID
            'Filter2=Gol tidak dipakai filter bisa cross
            'Filter3=MtUang
            'Filter4=Tgl
            'Filter5=
            If IO.File.Exists("SrByrPiutRtr.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrByrPiutRtr.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select RtrID As DocID,'Retur' As CaraBayar,'' As Deskripsi,SisaPakai From T_RtrBJ Where CustID='" & Filter1 & "' and MtUang='" & Filter3 & "' and stsPakai='False'", koneksi)

                cmsl.TableMappings.Add("Table", "CaraBayar")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "CaraBayar")

                DsSearch.WriteXml("SrByrPiutRtr.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrPiut Retur Jual Bahan" Then
            'Filter1=CustID
            'Filter2=Gol tidak dipakai filter bisa cross
            'Filter3=MtUang
            'Filter4=Tgl
            'Filter5=
            If IO.File.Exists("SrByrPiutRtrJualBB.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrByrPiutRtrJualBB.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select RtrID As DocID,'Retur' As CaraBayar,'' As Deskripsi,SisaPakai From T_RtrPenjBB Where CustID='" & Filter1 & "' and MtUang='" & Filter3 & "' and stsPakai='False'", koneksi)

                cmsl.TableMappings.Add("Table", "CaraBayar")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "CaraBayar")

                DsSearch.WriteXml("SrByrPiutRtrJualBB.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrPiut Retur Lain-Lain" Then
            'Filter1=CustID
            'Filter2=Gol tidak dipakai filter bisa cross
            'Filter3=MtUang
            'Filter4=Tgl
            'Filter5=
            If IO.File.Exists("SrByrPiutRtrJualL2.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrByrPiutRtrJualL2.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select RtrID As DocID,'Retur' As CaraBayar,'' As Deskripsi,SisaPakai From T_RtrPenjBebas Where CustID='" & Filter1 & "' and MtUang='" & Filter3 & "' and stsPakai='False'", koneksi)

                cmsl.TableMappings.Add("Table", "CaraBayar")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "CaraBayar")

                DsSearch.WriteXml("SrByrPiutRtrJualL2.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrPiut JM" Then
            'Filter1=CustID
            'Filter2=Gol
            'Filter3=MtUang
            'Filter4=Tgl
            'Filter5=
            If IO.File.Exists("SrByrPiutJM.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrByrPiutJM.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select JMID As DocID,'JM' As CaraBayar,'' As Deskripsi,SisaPakai From T_JMPiut Where CustID='" & Filter1 & "' and Gol='" & Filter2 & "' and MtUang='" & Filter3 & "' and stsPakai='False'", koneksi)

                cmsl.TableMappings.Add("Table", "CaraBayar")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "CaraBayar")

                DsSearch.WriteXml("SrByrPiutJM.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Tagihan Waste" Then
            'Filter1=
            'Filter2=
            'Filter3=
            'Filter4=Tanggal
            'Filter5=
            If IO.File.Exists("SrWaste.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrWaste.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select H.TagihID,H.PEN,TagihIDD,'" & Filter1 & "'+D.BBID As BBID,B.Nama,D.Sat,D.Qty From T_Tagihan H Inner Join T_TagihanDtl2 D On H.TagihID=D.TagihID Inner Join M_BB B On D.BBID=B.BBID Where Year(Tanggal)>=Year(dateadd(mm,-24,'" & Filter4 & "'))", koneksi)

                cmsl.TableMappings.Add("Table", "T_TagihanSr2")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_TagihanSr2")

                DsSearch.WriteXml("SrWaste.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_TagihanSr2"

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("TagihIDD").Visible = False

            Me.GridView1.Columns("TagihID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("PEN").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Kategori TKL" Then
            If IO.File.Exists("SrKatTKL.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrKatTKL.xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select KatID,Kategori,GaJam,OTN,OTL From M_KatTKL Order By KatID Asc", koneksi)

                cmsl.TableMappings.Add("Table", "M_KatTKLSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "M_KatTKLSr")

                DsSearch.WriteXml("SrKatTKL.xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_KatTKLSr"

            Me.GridView1.Columns("KatID").Width = 200
            Me.GridView1.Columns("Kategori").Width = 350

            Me.GridView1.Columns("GaJam").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("GaJam").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("OTN").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("OTN").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("OTL").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("OTL").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("KatID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Kategori").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Hasil Produksi" Then
            'Filter1=Unit

            If IO.File.Exists("SrHProd" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrHProd" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Tanggal From T_HProd Where Unit='" & Filter1 & "'", koneksi)

                cmsl.TableMappings.Add("Table", "T_HProdSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_HProdSr")

                DsSearch.WriteXml("SrHProd" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_HProdSr"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

        ElseIf Master = "Hasil Produksi Target" Then
            'Filter1=Unit

            If IO.File.Exists("SrHProdTarget" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrHProdTarget" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Tanggal,CuttUpp,CuttBott,Seri,SabUpp,SabIns,JhtKomp,JhtUpp,FinishUpp,Insock,Insole,Outsole, Insertt,Inject,Ass,Finish,Pack,Phylon From T_HProdTarget Where Unit='" & Filter1 & "' Order By Tanggal", koneksi)

                cmsl.TableMappings.Add("Table", "T_HProdTargetSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_HProdTargetSr")

                DsSearch.WriteXml("SrHProdTarget" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_HProdTargetSr"

            Me.GridView1.Columns("CuttUpp").Visible = False
            Me.GridView1.Columns("CuttBott").Visible = False
            Me.GridView1.Columns("Seri").Visible = False
            Me.GridView1.Columns("SabUpp").Visible = False
            Me.GridView1.Columns("SabIns").Visible = False
            Me.GridView1.Columns("JhtKomp").Visible = False
            Me.GridView1.Columns("JhtUpp").Visible = False
            Me.GridView1.Columns("FinishUpp").Visible = False
            Me.GridView1.Columns("Insock").Visible = False
            Me.GridView1.Columns("Insole").Visible = False
            Me.GridView1.Columns("Outsole").Visible = False
            Me.GridView1.Columns("Insertt").Visible = False
            Me.GridView1.Columns("Inject").Visible = False
            Me.GridView1.Columns("Ass").Visible = False
            Me.GridView1.Columns("Finish").Visible = False
            Me.GridView1.Columns("Pack").Visible = False
            Me.GridView1.Columns("Phylon").Visible = False

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

        ElseIf Master = "Hasil Produksi Jam" Then
            'Filter1=Unit

            If IO.File.Exists("SrHProdJam" & Filter1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrHProdJam" & Filter1 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Tanggal,Jam From T_HProdJam Where Unit='" & Filter1 & "'", koneksi)

                cmsl.TableMappings.Add("Table", "T_HProdJamSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_HProdJamSr")

                DsSearch.WriteXml("SrHProdJam" & Filter1 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_HProdJamSr"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

        ElseIf Master = "Samp Req" Then
            'Filter1=Mkt

            If IO.File.Exists("SrSampReq" & Filter1 & Filter2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrSampReq" & Filter1 & Filter2 & ".xml")
            Else
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct H.ReqID,Rev,H.Tanggal,TglKirim,H.CustID,C.Nama As Cust,H.ChaserID,(Select Nama From M_User Where UserID=H.ChaserID) As Chaser,H.PtMkID,(Select Nama From M_User Where UserID=H.PtMkID) As PtMk,(Select StlName + '- '  From (Select Distinct SD.StlName From T_SampReqDtl SD Inner Join T_TrmSRDtl TD1 On SD.ReqID=TD1.ReqID and SD.ReqIDD=TD1.ReqIDD Where SD.ReqID=H.ReqID and QtyRj>0) as q FOR XML PATH('')) As StlName,(Select Warna + '- '  From (Select Distinct SD.Warna From T_SampReqDtl SD Inner Join T_TrmSRDtl TD1 On SD.ReqID=TD1.ReqID and SD.ReqIDD=TD1.ReqIDD Where SD.ReqID=H.ReqID and QtyRj>0) as q FOR XML PATH('')) As Warna From T_SampReq H Inner Join T_SampReqDtl D On H.ReqID=D.ReqID Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_TrmSRDtl TD On H.ReqID=TD.ReqID Where H.MktID='" & Filter1 & "' and QtyRj>0", koneksi)

                cmsl.TableMappings.Add("Table", "T_SampReqSr")
                DsSearch = New System.Data.DataSet
                cmsl.Fill(DsSearch, "T_SampReqSr")

                DsSearch.WriteXml("SrSampReq" & Filter1 & Filter2 & ".xml", XmlWriteMode.WriteSchema)
            End If

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_SampReqSr"

            Me.GridView1.Columns("CustID").Visible = False
            Me.GridView1.Columns("ChaserID").Visible = False
            Me.GridView1.Columns("PtMkID").Visible = False

            Me.GridView1.Columns("ReqID").Width = 120
            Me.GridView1.Columns("Cust").Width = 150
            Me.GridView1.Columns("Chaser").Width = 150
            Me.GridView1.Columns("PtMk").Width = 150
            Me.GridView1.Columns("StlName").Width = 150

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
        End If

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BRefresh_Click(sender As Object, e As EventArgs) Handles BRefresh.Click
        If F5 = "Tampil All" Then
            Me.LCICek.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCICek.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If Master = "Gudang" Then
            If IO.File.Exists("SrGudang.xml") Then
                System.IO.File.Delete("SrGudang.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select GdID,L.Nama,Alamat,K.Nama As Kota From M_Gudang L Inner Join M_Kota K On L.KotaID=K.KotaID Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_GudangSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_GudangSr")

            DsSearch.WriteXml("SrGudang.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_GudangSr"

            Me.GridView1.Columns("Nama").Width = 100
            Me.GridView1.Columns("Alamat").Width = 150
            Me.GridView1.Columns("Kota").Width = 100

            Me.GridView1.Columns("GdID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Posisi" Then
            If IO.File.Exists("SrPosisi.xml") Then
                DsSearch.ReadXml("SrPosisi.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select PosisiID From M_Posisi Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_PosisiSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_PosisiSr")

            DsSearch.WriteXml("SrPosisi.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_PosisiSr"

            Me.GridView1.Columns("PosisiID").Width = 300

            Me.GridView1.Columns("PosisiID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Jenis Cust" Then
            If IO.File.Exists("SrJnsCust.xml") Then
                System.IO.File.Delete("SrJnsCust.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct Jenis From M_JnsCust Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_JnsCustSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_JnsCustSr")
            DsSearch.Tables("M_JnsCustSr").Rows.Add("%")

            DsSearch.WriteXml("SrJnsCust.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_JnsCustSr"

            Me.GridView1.Columns("Jenis").Width = 300
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Sts Harga" Then
            If IO.File.Exists("SrStsHarga.xml") Then
                System.IO.File.Delete("SrStsHarga.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct stsHarga From M_BrgHarga Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_BrgHargaSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_BrgHargaSr")
            DsSearch.Tables("M_BrgHargaSr").Rows.Add("%")

            DsSearch.WriteXml("SrStsHarga.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_BrgHargaSr"

            Me.GridView1.Columns("stsHarga").Width = 300
            Me.GridView1.Columns("stsHarga").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Cabang" Then
            If IO.File.Exists("SrCabang.xml") Then
                System.IO.File.Delete("SrCabang.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select CabID,Cabang From M_Cab Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_CabSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_CabSr")
            DsSearch.Tables("M_CabSr").Rows.Add("%", "Semua Cabang")

            DsSearch.WriteXml("SrCabang.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_CabSr"

            Me.GridView1.Columns("Cabang").Width = 100
            Me.GridView1.Columns("CabID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Cabang").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Proses" Then
            If IO.File.Exists("SrProses.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrProses.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Proses From M_Proses Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_ProsSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_ProsSr")
            DsSearch.Tables("M_ProsSr").Rows.Add("%")

            DsSearch.WriteXml("SrProses.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_ProsSr"

            Me.GridView1.Columns("Proses").Width = 100
            Me.GridView1.Columns("Proses").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Sales" Then
            If IO.File.Exists("SrSales.xml") Then
                System.IO.File.Delete("SrSales.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select SalID,Area,Nama From M_Sales Where Aktif='True' and Gol<>'Bahan'", koneksi)

            cmsl.TableMappings.Add("Table", "M_SalesSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_SalesSr")

            DsSearch.WriteXml("SrSales.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_SalesSr"

            Me.GridView1.Columns("Nama").Width = 100
            Me.GridView1.Columns("SalID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Area").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "SalesCabang" Then
            'Filter1=CabID
            If IO.File.Exists("SrSalesCab.xml") Then
                System.IO.File.Delete("SrSalesCab.xml")
            End If


            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select S.SalID,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='Lokal' and CabID='" & F1 & "' and CS.Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_SalesSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_SalesSr")

            DsSearch.WriteXml("SrSalesCab.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_SalesSr"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("SalID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Supplier" Then
            If IO.File.Exists("SrSupplier.xml") Then
                System.IO.File.Delete("SrSupplier.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select SuppID,S.Nama,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_Supp")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_Supp")

            DsSearch.WriteXml("SrSupplier.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Supp"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("SuppID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Kota").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Style BJ" Then
            If IO.File.Exists("SrStyle.xml") Then
                System.IO.File.Delete("SrStyle.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select StyleID,Nama From M_Style", koneksi)

            cmsl.TableMappings.Add("Table", "M_Style")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_Style")

            DsSearch.WriteXml("SrStyle.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Style"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("StyleID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "M_Brg" Then
            'F1=Gol
            If IO.File.Exists("SrBrg" & F1 & ".xml") Then
                System.IO.File.Delete("SrBrg" & F1 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select License,Luxury,ArtCode,HSCode,ArtName,StyleID,MerkID,KatID,JnsID,SubJns,Urut,B.WrnID,W.Nama as Warna, AssID,SatID,Isi,SubGrup,Grup From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where B.Aktif='True' and Gol='" & F1 & "'", koneksi)

            cmsl.TableMappings.Add("Table", "M_Brg")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_Brg")

            DsSearch.WriteXml("SrBrg" & F1 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Brg"

            Me.GridView1.Columns("License").Width = 75
            Me.GridView1.Columns("Luxury").Width = 75
            Me.GridView1.Columns("ArtCode").Width = 150
            Me.GridView1.Columns("HSCode").Width = 100
            Me.GridView1.Columns("ArtName").Width = 200
            Me.GridView1.Columns("StyleID").Width = 100

            Me.GridView1.Columns("MerkID").Visible = False
            Me.GridView1.Columns("KatID").Visible = False
            Me.GridView1.Columns("JnsID").Visible = False
            Me.GridView1.Columns("SubJns").Visible = False
            Me.GridView1.Columns("Urut").Visible = False
            Me.GridView1.Columns("WrnID").Visible = False
            Me.GridView1.Columns("AssID").Visible = False
            Me.GridView1.Columns("SubGrup").Visible = False
            Me.GridView1.Columns("Grup").Visible = False

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("HSCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "M_BrgJO" Then
            If IO.File.Exists("SrBrgJO.xml") Then
                System.IO.File.Delete("SrBrgJO.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select B.ArtCode,HSCode,ArtName, StyleID, B.KatID, K.Nama As Kategori, B.WrnID, W.Nama As Warna, Ass, SatID, Isi From M_Brg B Inner Join M_BrgKat K On B.KatID=K.KatID Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where B.Aktif='True' and Gol='Job Order'", koneksi)

            cmsl.TableMappings.Add("Table", "M_Brg")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_Brg")

            DsSearch.WriteXml("SrBrgJO.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Brg"

            Me.GridView1.OptionsView.ColumnAutoWidth = False

            Me.GridView1.Columns("WrnID").Visible = False
            Me.GridView1.Columns("KatID").Visible = False

            Me.GridView1.Columns("ArtCode").Width = 100
            Me.GridView1.Columns("HSCode").Width = 100
            Me.GridView1.Columns("ArtName").Width = 200
            Me.GridView1.Columns("StyleID").Width = 100
            Me.GridView1.Columns("Kategori").Width = 120
            Me.GridView1.Columns("SatID").Width = 100
            Me.GridView1.Columns("Warna").Width = 100

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("HSCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Brg Model" Then
            'F1=ArtName
            'F2= Warna
            If IO.File.Exists("SrBrgModel" & F1 & F2 & ".xml") Then
                System.IO.File.Delete("SrBrgModel" & F1 & F2 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select ArtCode,Ass As Uk From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where ArtName='" & F1 & "' And W.Nama='" & F2 & "' And SatID In ('P','PCS')", koneksi)

            cmsl.TableMappings.Add("Table", "M_Brg")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_Brg")

            DsSearch.WriteXml("SrBrgModel" & F1 & F2 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Brg"

            Me.GridView1.Columns("ArtCode").Width = 120
            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Master Mesin" Then

            If IO.File.Exists("SrMesin.xml") Then
                System.IO.File.Delete("SrMesin.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select BBID,B.Nama,B.JnsID,J.Nama as Jenis,Merk,SubJns,Kode,Uk,Jasa,ThnProd,Sat From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True' and J.JnsPers ='Mesin' Order By B.Nama", koneksi)

            cmsl.TableMappings.Add("Table", "M_Mesin")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_Mesin")

            DsSearch.WriteXml("SrMesin.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Mesin"

            Me.GridView1.Columns("BBID").Width = 100
            Me.GridView1.Columns("Nama").Width = 350

            Me.GridView1.Columns("JnsID").Visible = False
            Me.GridView1.Columns("Merk").Visible = False
            Me.GridView1.Columns("SubJns").Visible = False
            Me.GridView1.Columns("Kode").Visible = False
            Me.GridView1.Columns("Uk").Visible = False
            Me.GridView1.Columns("Jasa").Visible = False
            Me.GridView1.Columns("ThnProd").Visible = False

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "M_BB" Then
            'F1=InisialBC
            'F2=JnsPers

            If IO.File.Exists("SrBB" & F2 & ".xml") Then
                System.IO.File.Delete("SrBB" & F2 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '" & F1 & "'+BBID as BBID,HSCode,B.Nama,DivPO,B.JnsID,J.Nama as Jenis,Merk,SubJns,Tbl,Gram,Wrn,Kode,Hard, Uk,Jasa,ThnProd,Sat,Ket,stsJasa From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True' and J.Gol Like'" & F2 & "%'  Order By B.Nama", koneksi)

            cmsl.TableMappings.Add("Table", "M_BB" & F2)
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_BB" & F2)

            DsSearch.WriteXml("SrBB" & F2 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_BB" & F2

            Me.GridView1.Columns("BBID").Width = 100
            Me.GridView1.Columns("HSCode").Width = 100

            Me.GridView1.Columns("Nama").Width = 350
            'Me.GridView1.Columns("Jenis").Width = 100
            Me.GridView1.Columns("DivPO").Visible = False
            Me.GridView1.Columns("JnsID").Visible = False
            Me.GridView1.Columns("Merk").Visible = False
            Me.GridView1.Columns("SubJns").Visible = False
            Me.GridView1.Columns("Tbl").Visible = False
            Me.GridView1.Columns("Gram").Visible = False
            Me.GridView1.Columns("Wrn").Visible = False
            Me.GridView1.Columns("Kode").Visible = False
            Me.GridView1.Columns("Hard").Visible = False
            Me.GridView1.Columns("Uk").Visible = False
            Me.GridView1.Columns("Jasa").Visible = False
            Me.GridView1.Columns("Ket").Visible = False
            Me.GridView1.Columns("stsJasa").Visible = False
            Me.GridView1.Columns("ThnProd").Visible = False

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("HSCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Proses Produksi" Then

            If IO.File.Exists("SrProsProd.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrProsProd.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Proses From M_Proses Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_ProsesSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_ProsesSr")

            DsSearch.WriteXml("SrProsProd.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_ProsesSr"

            Me.GridView1.Columns("Proses").Width = 300

            Me.GridView1.Columns("Proses").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "Bahan BtNum" Then
            'F1=InisialBC
            'F2=JnsPers
            'F3=GdID
            'F4=Tanggal

            If IO.File.Exists("SrBBBtNum" & F2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBBtNum" & F2 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Jenis,BtNum As BatchNum,'" & F1 & "'+BBID As BBID,Nama,Sat From(Select J.Nama As Jenis,B.BBID as BBID,B.Nama,BtNum,B.Sat From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Inner Join T_StokBB S On B.BBID=S.BBID Where B.Aktif='True' and J.Gol Like'" & F2 & "%' and GdID='" & F3 & "' and Tanggal<='" & F4 & "' Group By B.BBID,B.Nama,J.Nama,Sat,BtNum Having sum(Masuk - Keluar) > 0) as x Order By Nama", koneksi)

            cmsl.TableMappings.Add("Table", "BahanBtNum" & F2)
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BahanBtNum" & F2)

            DsSearch.WriteXml("SrBBBtNum" & F2 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BahanBtNum" & F2

            Me.GridView1.Columns("Jenis").Width = 150
            Me.GridView1.Columns("BBID").Width = 120
            Me.GridView1.Columns("Nama").Width = 400
            Me.GridView1.Columns("BatchNum").Width = 150
            Me.GridView1.Columns("Sat").Width = 80

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Bahan BtNum No Stok" Then
            'F1=InisialBC
            'F2=JnsPers
            'F3=GdID
            'F4=Tanggal

            If IO.File.Exists("SrBBBtNumNS" & F2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBBtNumNS" & F2 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct J.Nama As Jenis,'" & F1 & "'+B.BBID as BBID,B.Nama,BtNum As BatchNum,B.Sat From M_BB B Left Outer Join M_BBJns J On B.JnsID=J.JnsID Left Outer Join T_StokBB S On B.BBID=S.BBID Where B.Aktif='True' and J.Gol Like'" & F2 & "%' Order By Nama", koneksi)

            cmsl.TableMappings.Add("Table", "BahanBtNumNS" & F2)
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BahanBtNumNS" & F2)

            DsSearch.WriteXml("SrBBBtNumNS" & F2 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BahanBtNumNS" & F2

            Me.GridView1.Columns("Jenis").Width = 150
            Me.GridView1.Columns("BBID").Width = 120
            Me.GridView1.Columns("Nama").Width = 400
            Me.GridView1.Columns("BatchNum").Width = 150
            Me.GridView1.Columns("Sat").Width = 80

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Batch Number" Then
            'F1=InisialBC
            'F2=JnsPers
            'F3=GdID
            'F4=Tanggal

            If IO.File.Exists("BatchNumber" & F1 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("BatchNumber" & F1 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct BtNum As BatchNum From T_StokBB where BBID='" & F1 & "'", koneksi)

            cmsl.TableMappings.Add("Table", "BtNum" & F1)
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BtNum" & F1)

            DsSearch.WriteXml("BatchNumber" & F1 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BtNum" & F1

            Me.GridView1.Columns("BatchNum").Width = 150

            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Collect PO" Then
            'Filter1=POID
            'Filter2=ArtCode

            If IO.File.Exists("SrCollPO.xml") Then
                System.IO.File.Delete("SrCollPO.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select CollPOIDD,H.CollPOID,C.Nama As Cust,ArtCode,Qty-(Select Isnull((Select Sum(Qty) From T_POBJCollPO where CollPOIDD=D.CollPOIDD and POID<>'" & F1 & "'),0)) As Qty From T_CollPO H Inner Join T_CollPODtl D On H.CollPOID=D.CollPOID Inner Join M_Cust C On C.CustID=H.CustID Where ArtCode='" & F2 & "'", koneksi)

            cmsl.TableMappings.Add("Table", "T_CollPO")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_CollPO")

            DsSearch.WriteXml("SrCollPO.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_CollPO"

            Me.GridView1.Columns("Cust").Width = 200

            Me.GridView1.Columns("CollPOID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Cust").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "JualBJ" Then
            'F1=Jns Cust
            'F2=Gol
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrHarga.xml") Then
                System.IO.File.Delete("SrHarga.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi,stsHarga,Harga,DiscOB,Gol From M_Brg B Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & F1 & "' and Gol='" & F2 & "' and B.Aktif='True' and H.Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "Harga")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "Harga")

            DsSearch.WriteXml("SrHarga.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "Harga"

            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("DiscOB").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscOB").DisplayFormat.FormatString = "{0:n3}"
            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "JualBJ Manual" Then
            'F2=Gol

            If IO.File.Exists("JualBJMn.xml") Then
                System.IO.File.Delete("JualBJMn.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi,'' as stsHarga,0.00 As Harga,0.000 as DiscOB,0 As Stok From M_Brg B Where B.Aktif='True' and Gol='" & F2 & "'", koneksi)

            cmsl.TableMappings.Add("Table", "JualBJMn")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "JualBJMn")

            DsSearch.WriteXml("JualBJMn.xml", XmlWriteMode.WriteSchema)

            Me.GridView1.OptionsBehavior.Editable = True

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "JualBJMn"

            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("DiscOB").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscOB").DisplayFormat.FormatString = "{0:n3}"
            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Harga").OptionsColumn.AllowEdit = True
            'Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("DiscOB").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("ArtCode").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("SatID").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Isi").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("stsHarga").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Stok").OptionsColumn.AllowEdit = False

        ElseIf Master = "JualPromo" Then
            'F1=Promo
            'F2=Paket
            'F3=Gudang
            'F4=Tgl
            'F5=DocID
            If IO.File.Exists("SrPromo.xml") Then
                System.IO.File.Delete("SrPromo.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select * from(Select D.ArtCode,ArtName,B.SatID,B.Isi,dbo.fcStokBJ(B.ArtCode,'" & F3 & "','" & F4 & "','" & F5 & "') As Stok From T_PromoFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode  Where PromoID='" & F1 & "' and Paket='" & F2 & "' and B.Aktif='True') As x Where Stok >0", koneksi)

            cmsl.TableMappings.Add("Table", "Promo")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "Promo")

            DsSearch.WriteXml("SrPromo.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "Promo"

            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "StokBJ" Then
            'F1=Gol
            'F2=
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrStokBJ.xml") Then
                System.IO.File.Delete("SrStokBJ.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi From M_Brg B Where B.Aktif='True' and Gol='" & F1 & "'", koneksi)

            cmsl.TableMappings.Add("Table", "StokBJ")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "StokBJ")

            DsSearch.WriteXml("SrStokBJ.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBJ"

            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "StokBB" Then
            'F1=
            'F2=
            'F3=Gudang
            'F4=Tgl
            'F5=DocID
            If IO.File.Exists("SrStokBB.xml") Then
                System.IO.File.Delete("SrStokBB.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select * from(Select B.BBID,Nama As Bahan,B.Sat,dbo.fcStokBB(B.BBID,'" & F3 & "','" & F4 & "','" & F5 & "') As Stok From M_BB B Where B.Aktif='True') As x Where Stok >0", koneksi)

            cmsl.TableMappings.Add("Table", "StokBB")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "StokBB")

            DsSearch.WriteXml("SrStokBB.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBB"

            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Stok BJ Gol" Then
            'F1=SatID
            'F2=Gol
            'F3=Gudang
            'F4=Tgl
            'F5=DocID
            If IO.File.Exists("SrStokBJGol.xml") Then
                System.IO.File.Delete("SrStokBJGol.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi From M_Brg B Where B.Aktif='True' and SatID like '" & F1 & "' and Gol Like '" & F2 & "'", koneksi)

            cmsl.TableMappings.Add("Table", "StokBJ")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "StokBJ")
            DsSearch.WriteXml("SrStokBJGol.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBJ"

            'Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            'Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BJ Gol" Then
            'F1=SatID
            'F2=Gol
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrBJGol" & F1 & F2 & ".xml") Then
                System.IO.File.Delete("SrBJGol" & F1 & F2 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi From M_Brg B Where B.Aktif='True' and SatID like '" & F1 & "' and Gol In ('" & F2 & "','Promosi')", koneksi)

            cmsl.TableMappings.Add("Table", "StokBJ")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "StokBJ")
            DsSearch.WriteXml("SrBJGol" & F1 & F2 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBJ"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Stok BJ Op" Then
            'F1=SatID
            'F2=Gol
            'F3=Gudang
            'F4=Tgl
            'F5=DocID
            If IO.File.Exists("SrStokBJOp.xml") Then
                System.IO.File.Delete("SrStokBJOp.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select * from(Select B.ArtCode,ArtName,B.SatID,B.Isi,0 As Stok From M_Brg B Where B.Aktif='True' and SatID like '" & F1 & "' and Gol='" & F2 & "') As x Where ArtCode Not In (Select ArtCode From T_StokBJ Where GdID='" & F3 & "' and PeriodID=" & MainModule.periodAktif & ")", koneksi)

            cmsl.TableMappings.Add("Table", "StokBJ")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "StokBJ")

            DsSearch.WriteXml("SrStokBJOp.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBJ"

            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("ArtCode").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Stok BB Op" Then
            'F1=
            'F2=
            'F3=Gudang
            'F4=Tgl
            'F5=DocID
            If IO.File.Exists("SrStokBBOp.xml") Then
                System.IO.File.Delete("SrStokBBOp.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select * from(Select B.JnsID, J.Nama As Jenis,B.BBID,B.Nama As Bahan,B.Sat,0 As Stok From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True') As x Where BBID Not In (Select BBID From T_StokBB Where GdID='" & F3 & "' and PeriodID=" & MainModule.periodAktif & ")", koneksi)
            'cmsl = New SqlDataAdapter("Select * from(Select B.JnsID, J.Nama As Jenis,B.BBID,B.Nama As Bahan,B.Sat,dbo.fcStokBB(B.BBID,'" & F3 & "','" & F4 & "','" & F5 & "') As Stok From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True') As x Where BBID Not In (Select BBID From T_StokBB Where GdID='" & F3 & "')", koneksi)

            cmsl.TableMappings.Add("Table", "StokBB")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "StokBB")

            DsSearch.WriteXml("SrStokBBOp.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "StokBB"

            Me.GridView1.Columns("Stok").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Stok").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Divisi" Then
            'F1: Data

            If IO.File.Exists("SrDivisi.xml") Then
                System.IO.File.Delete("SrDivisi.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select DivID,Nama,PrsPol From M_Div Where Data Like '" & F1 & "' and Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_Div")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_Div")

            DsSearch.WriteXml("SrDivisi.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Div"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("PrsPol").Visible = False

            Me.GridView1.Columns("DivID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Komponen" Then
            If IO.File.Exists("SrKomponen.xml") Then
                System.IO.File.Delete("SrKomponen.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select KompID,Nama From M_Komp Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "M_Komp")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_Komp")

            DsSearch.WriteXml("SrKomponen.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Komp"

            Me.GridView1.Columns("Nama").Width = 100

            Me.GridView1.Columns("KompID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Spec" Then
            If IO.File.Exists("SrSpec.xml") Then
                System.IO.File.Delete("SrSpec.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select SpecID,H.CustID,C.Nama As Customer,StyleID,Brand,ShoeLast,ArtName,Warna,RangeSize,SampleSize,H.Ket, Dibuat,Pattern,Chaser,PPC,PembKulit,PembNonKulit,Teknik,Mengetahui From M_Spec H Inner Join M_Cust C On H.CustID=C.CustID", koneksi)

            cmsl.TableMappings.Add("Table", "M_Spec")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_Spec")

            DsSearch.WriteXml("SrSpec.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_Spec"

            Me.GridView1.Columns("SpecID").Width = 120
            Me.GridView1.Columns("Customer").Width = 150
            Me.GridView1.Columns("ArtName").Width = 120
            Me.GridView1.Columns("Warna").Width = 100
            'Me.GridView1.Columns("Ket").Width = 150
            'Me.GridView1.Columns("Pattern").Width = 120
            'Me.GridView1.Columns("Dibuat").Width = 120
            'Me.GridView1.Columns("Teknik").Width = 120
            'Me.GridView1.Columns("PembKulit").Width = 120
            'Me.GridView1.Columns("PembNonKulit").Width = 120
            'Me.GridView1.Columns("Menyetujui").Width = 120

            Me.GridView1.Columns("Ket").Visible = False
            Me.GridView1.Columns("Pattern").Visible = False
            Me.GridView1.Columns("Dibuat").Visible = False
            Me.GridView1.Columns("Teknik").Visible = False
            Me.GridView1.Columns("Chaser").Visible = False
            Me.GridView1.Columns("PPC").Visible = False
            Me.GridView1.Columns("PembKulit").Visible = False
            Me.GridView1.Columns("PembNonKulit").Visible = False
            Me.GridView1.Columns("Mengetahui").Visible = False
            Me.GridView1.Columns("RangeSize").Visible = False
            Me.GridView1.Columns("SampleSize").Visible = False

            Me.GridView1.Columns("SpecID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Customer").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Model" Then
            If IO.File.Exists("SrModel.xml") Then
                System.IO.File.Delete("SrModel.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select MdlID,M.SpecID,M.StyleID,S.Brand,M.ArtName,M.Warna,M.RangeSize,M.SampleSize,PersenGenerate, M.Ket From M_Model M Inner Join M_Spec S On M.SpecId=S.SpecID ", koneksi)

            cmsl.TableMappings.Add("Table", "M_ModelSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_ModelSr")

            DsSearch.WriteXml("SrModel.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_ModelSr"

            Me.GridView1.Columns("MdlID").Width = 120
            Me.GridView1.Columns("SpecID").Width = 120
            Me.GridView1.Columns("Brand").Width = 150
            Me.GridView1.Columns("ArtName").Width = 120
            Me.GridView1.Columns("Warna").Width = 100

            Me.GridView1.Columns("RangeSize").Visible = False
            Me.GridView1.Columns("SampleSize").Visible = False
            Me.GridView1.Columns("PersenGenerate").Visible = False
            Me.GridView1.Columns("Ket").Visible = False

            Me.GridView1.Columns("MdlID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("SpecID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Brand").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Purchase Request Tooling" Then
            'F1=SuppID
            'F2=CustID
            'F3=
            'F4=
            'F5=

            If IO.File.Exists("SrPRTool" & F1 & ".xml") Then
                System.IO.File.Delete("SrPRTool" & F1 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select * From (Select H.PRTID,D.PRTIDD,H.Tanggal,StyleID,Style,D.BBID,Case When D.Ket='' Then B.Nama Else B.Nama +' ('+ D.Ket +')' End As Bahan,Qty-BtlOrder-(Select Isnull((Select Sum(Qty) From T_POBBDtl Where BOMID=H.PRTID and DocIDD=D.PRTIDD),0)) as Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=H.SuppID Order By Tanggal desc),0)) As Harga,'False' as stsJasa,D.Sat From T_PRTool H Inner Join T_PRToolDtl D On H.PRTID=D.PRTID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB SB On SB.BBID=D.BBID Where H.SuppID='" & F1 & "' and SB.SuppID='" & F1 & "' and H.CustID='" & F2 & "') As x Where Qty>0", koneksi)

            cmsl.TableMappings.Add("Table", "T_PRToolSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_PRToolSr")

            DsSearch.WriteXml("SrPRTool" & F1 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_PRToolSr"

            Me.GridView1.Columns("stsJasa").Visible = False
            Me.GridView1.Columns("PRTIDD").Visible = False

            Me.GridView1.Columns("PRTID").Width = 120
            Me.GridView1.Columns("Tanggal").Width = 150
            Me.GridView1.Columns("StyleID").Width = 120
            Me.GridView1.Columns("Style").Width = 150
            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

            Me.GridView1.Columns("PRTID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Tanggal").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("StyleID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Style").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Purchase Request Sparepart" Then
            'F1=SuppID
            'F2=Tipe
            'F3=Jenis
            'F4=
            'F5=

            If IO.File.Exists("SrPRSpM" & F1 & F2 & F3 & ".xml") Then
                System.IO.File.Delete("SrPRSpM" & F1 & F2 & F3 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select * From (Select H.PRSMID,PRSMIDD,Tanggal,D.BBID,Case When D.Ket='' Then B.Nama Else B.Nama +' ('+ D.Ket +')' End As Bahan,Qty-BtlOrder-(Select Isnull((Select Sum(Qty) From T_POBBDtl Where BOMID=H.PRSMID and DocIDD=D.PRSMIDD),0)) as Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID='" & F1 & "' Order By Tanggal desc),0)) As Harga,'False' as stsJasa,D.Sat From T_PRSpM H Inner Join T_PRSpMDtl D On H.PRSMID=D.PRSMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where Tipe='" & F2 & "' and Jenis='" & F3 & "' and S.SuppID='" & F1 & "') as x Where Qty>0", koneksi)

            cmsl.TableMappings.Add("Table", "T_PRSpMSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_PRSpMSr")

            DsSearch.WriteXml("SrPRSpM" & F1 & F2 & F3 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_PRSpMSr"

            Me.GridView1.Columns("stsJasa").Visible = False
            Me.GridView1.Columns("PRSMIDD").Visible = False

            Me.GridView1.Columns("PRSMID").Width = 120
            Me.GridView1.Columns("Tanggal").Width = 150
            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

            Me.GridView1.Columns("PRSMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Tanggal").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Request" Then
            'F1=Jenis
            'F2=
            'F3=
            'F4=
            'F5=

            If IO.File.Exists("SrReq" & F1 & ".xml") Then
                System.IO.File.Delete("SrReq" & F1 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select BOMID, ArtName,Warna From T_BOM Where stsLunas = 'False' OR Jenis='" & Filter1 & "'", koneksi)
            cmsl = New SqlDataAdapter("Select ReqPID,Tanggal,Jenis From T_ReqP where stsApp='True' and stsLunas='False'", koneksi)


            cmsl.TableMappings.Add("Table", "T_ReqSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_ReqSr")

            DsSearch.WriteXml("SrReq" & F1 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_ReqSr"

            Me.GridView1.Columns("ReqPID").Width = 120
            Me.GridView1.Columns("Tanggal").Width = 250
            Me.GridView1.Columns("Jenis").Width = 150

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

            Me.GridView1.Columns("ReqPID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Tanggal").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Jenis").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "BOM" Then
            'F1=Jenis
            'F2=
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrBOM" & F1 & ".xml") Then
                System.IO.File.Delete("SrBOM" & F1 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct H.BOMID,MerkID,JnsID,H.ArtName,Warna,TotPsg+TotPsgPol As TotPsg From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner join M_Brg Br On P.ArtCode=Br.ArtCode Where stsApp='True' and stsLunas='False' and stsBatal='False' and stsBtlProd='False' Union All Select Distinct H.TambahanID,MerkID,JnsID, BH.ArtName,Warna,TotPsg+TotPsgPol As TotPsg From T_BOMTam H Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner join M_Brg Br On P.ArtCode=Br.ArtCode Inner Join T_BOM BH On H.BOMID=BH.BOMID Where H.stsApp='True' and stsLunas='False' and stsBatal='False' and stsBtlProd='False' Order By BOMID", koneksi)
            'cmsl = New SqlDataAdapter("Select Distinct H.BOMID,MerkID,JnsID,H.ArtName,Warna,TotPsg+TotPsgPol As TotPsg From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner join M_Brg Br On P.ArtCode=Br.ArtCode Where stsApp='True' and stsLunas='False' and stsBatal='False' and stsBtlProd='False' Order By BOMID", koneksi)


            cmsl.TableMappings.Add("Table", "T_BOMSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_BOMSr")

            DsSearch.WriteXml("SrBOM" & F1 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_BOMSr"

            Me.GridView1.Columns("MerkID").Visible = False
            Me.GridView1.Columns("JnsID").Visible = False

            Me.GridView1.Columns("BOMID").Width = 120
            Me.GridView1.Columns("ArtName").Width = 250
            Me.GridView1.Columns("Warna").Width = 150

            Me.GridView1.Columns("BOMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BOM Jasa" Then
            'F1=Jenis
            'F2=
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrBOMJs" & F1 & ".xml") Then
                System.IO.File.Delete("SrBOMJs" & F1 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct BOMID,ArtName,Warna,TotPsg From T_HProdJamDtl Where stsJasa='True' and TotPsg-(Select Sum(Pack) From T_HProdJamDtl as x where x.BOMID=T_HProdJamDtl.BOMID)>0", koneksi)
            cmsl.TableMappings.Add("Table", "T_BOMJsSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_BOMJsSr")

            DsSearch.WriteXml("SrBOMJs" & F1 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_BOMJsSr"

            Me.GridView1.Columns("BOMID").Width = 120
            Me.GridView1.Columns("ArtName").Width = 250
            Me.GridView1.Columns("Warna").Width = 150

            Me.GridView1.Columns("BOMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB With BOM" Then
            'F1=BOMID
            'F2=POID
            'F3=SuppID
            'F4=
            'F5=
            If IO.File.Exists("SrBBWBOM.xml") Then
                System.IO.File.Delete("SrBBWBOM.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select * From (Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.BOMID and BBID=D.BBID and POID<>'" & F2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where BOMID='" & F1 & "' and S.SuppID='" & F3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID,SuppID,D.stsJasa Union All Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.TambahanID and BBID=D.BBID and POID<>'" & F2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOMTamDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where TambahanID='" & F1 & "' and S.SuppID='" & F3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,TambahanID,SuppID,D.stsJasa ) as x Order By Bahan", koneksi)

            'cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.BOMID and BBID=D.BBID and POID<>'" & F2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where BOMID='" & F1 & "' and S.SuppID='" & F3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID,SuppID,D.stsJasa Order By Bahan", koneksi)


            cmsl.TableMappings.Add("Table", "BBBOM")
            DsSearch = New System.Data.DataSet
            cmsl.SelectCommand.CommandTimeout = 9000
            cmsl.Fill(DsSearch, "BBBOM")

            DsSearch.WriteXml("SrBBWBOM.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOM"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"
            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BOM Sch" Then
            'F1=
            'F2=
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrBOMSch.xml") Then
                System.IO.File.Delete("SrBOMSch.xml")
            End If


            Dim cmsl As SqlDataAdapter
            'cmsl = New SqlDataAdapter("Select BOMID, ArtName,Warna From T_BOM Where stsLunas = 'False' OR Jenis='" & Filter1 & "'", koneksi)
            cmsl = New SqlDataAdapter("Select Distinct H.BOMID,H.ArtName,Warna,TotPsg+TotPsgPol As TotPsg From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner join M_Brg Br On P.ArtCode=Br.ArtCode Inner Join T_SchPrPPIC  SC On H.BOMID=SC.BOMID Where stsApp='True' and stsLunas='False' and stsBatal='False' Order By BOMID", koneksi)

            cmsl.TableMappings.Add("Table", "T_BOMSchSr")
            DsSearch = New System.Data.DataSet
            cmsl.SelectCommand.CommandTimeout = 9000
            cmsl.Fill(DsSearch, "T_BOMSchSr")

            DsSearch.WriteXml("SrBOMSch" & F1 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_BOMSchSr"

            Me.GridView1.Columns("BOMID").Width = 120
            Me.GridView1.Columns("ArtName").Width = 250
            Me.GridView1.Columns("Warna").Width = 150

            Me.GridView1.Columns("BOMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("ArtName").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Warna").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB With Request" Then
            'F1=BOMID
            'F2=POID
            'F3=SuppID
            'F4=
            'F5=
            If IO.File.Exists("SrBBWReq.xml") Then
                System.IO.File.Delete("SrBBWReq.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Sum(Req) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.ReqPID and BBID=D.BBID and POID<>'" & F2 & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_ReqPDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where ReqPID='" & F1 & "' and S.SuppID='" & F3 & "' And S.Aktif='True' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,ReqPID,SuppID,D.stsJasa Order By Bahan", koneksi)

            cmsl.TableMappings.Add("Table", "BBBOM")
            DsSearch = New System.Data.DataSet
            cmsl.SelectCommand.CommandTimeout = 9000
            cmsl.Fill(DsSearch, "BBBOM")

            DsSearch.WriteXml("SrBBWBOM.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOM"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB Kanan With BOM" Then
            'F1=BOMID
            'F2=POID
            'F3=SuppID
            'F4=
            'F5=
            If IO.File.Exists("SrBBKnWBOM.xml") Then
                System.IO.File.Delete("SrBBKnWBOM.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) As Qty,D.stsJasa From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where BOMID='" & F1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID Union All Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) As Qty,D.stsJasa From T_BOMTamDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where TambahanID='" & F1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,TambahanID,D.stsJasa", koneksi)

            'cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) As Qty,D.stsJasa From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where BOMID='" & F1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID", koneksi)

            cmsl.TableMappings.Add("Table", "BBBOM")
            DsSearch = New System.Data.DataSet
            cmsl.SelectCommand.CommandTimeout = 9000
            cmsl.Fill(DsSearch, "BBBOM")

            DsSearch.WriteXml("SrBBKnWBOM.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOM"

            Me.GridView1.Columns("Bahan").Width = 250
            Me.GridView1.Columns("stsJasa").Visible = False

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB Kanan With Request" Then
            'F1=BOMID
            'F2=POID
            'F3=SuppID
            'F4=
            'F5=
            If IO.File.Exists("SrBBKnWReq.xml") Then
                System.IO.File.Delete("SrBBKnWReq.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,D.Sat,Sum(Req) As Qty,D.stsJasa From T_ReqDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where ReqID='" & F1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,ReqID", koneksi)

            cmsl.TableMappings.Add("Table", "BBReq")
            DsSearch = New System.Data.DataSet
            cmsl.SelectCommand.CommandTimeout = 9000
            cmsl.Fill(DsSearch, "BBReq")

            DsSearch.WriteXml("SrBBKnWReq.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBReq"

            Me.GridView1.Columns("Bahan").Width = 250
            Me.GridView1.Columns("stsJasa").Visible = False

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB No BOM" Then
            'F1=SuppID
            'F2=JnsPers
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrBBNBOM" & F2 & ".xml") Then
                System.IO.File.Delete("SrBBNBOM" & F2 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct B.BBID,B.Nama As Bahan,B.Sat,(Select Isnull((Select Top 1 (HargaBeli*NilTukarRp)+HargaBahan From M_BBHarga Where BBID=B.BBID and SuppID=S.SuppID Order By Tanggal desc,HargaIDD desc),0)) As Harga,Uk,stsJasa From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Inner Join M_SuppBB S On B.BBID=S.BBID Where S.SuppID Like '" & F1 & "' and J.Gol Like'" & F2 & "%' and B.Aktif='True' Order By B.Nama", koneksi)

            cmsl.TableMappings.Add("Table", "BB")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BB")

            DsSearch.WriteXml("SrBBNBOM" & F2 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BB"

            Me.GridView1.Columns("Harga").Visible = False
            Me.GridView1.Columns("stsJasa").Visible = False

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB No BOM BtNum" Then
            'F1=SuppID
            'F2=JnsPers
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrBBNBOMBtNum" & F2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBNBOMBtNum" & F2 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct B.BBID,SB.BtNum As BatchNum,B.Nama As Bahan,B.Sat,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=B.BBID and SuppID=S.SuppID Order By Tanggal desc,HargaIDD desc),0)) As Harga,Uk,stsJasa From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Inner Join M_SuppBB S On B.BBID=S.BBID Inner Join T_StokBB SB On B.BBID=SB.BBID Where S.SuppID Like '" & F1 & "' and J.Gol Like'" & F2 & "%' and B.Aktif='True' Order By B.Nama", koneksi)

            cmsl.TableMappings.Add("Table", "BB")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BB")

            DsSearch.WriteXml("SrBBNBOMBtNum" & F2 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BB"

            Me.GridView1.Columns("Harga").Visible = False
            Me.GridView1.Columns("stsJasa").Visible = False

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Harga").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Harga").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB BOM" Then
            'F1=BOMID
            'F2=BPBID
            'F3=Gudang
            'F4=Tgl
            'F5=Inisial BC
            If IO.File.Exists("SrBBBOM.xml") Then
                System.IO.File.Delete("SrBBBOM.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '" & F5 & "'+D.BBID as BBID,B.Nama As Bahan,D.Sat,Case When Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & F2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) > dbo.fcStokBB(D.BBID,'" & F3 & "','" & F4 & "','" & F2 & "') Then dbo.fcStokBB(D.BBID,'" & F3 & "','" & F4 & "','" & F2 & "') Else Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & F2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) End  As Qty  From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where BOMID='" & F1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,BOMID", koneksi)

            cmsl.TableMappings.Add("Table", "BBBOM")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BBBOM")

            DsSearch.WriteXml("SrBBBOM.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOM"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB BOM BtNum" Then
            'F1=BOMID
            'F2=BPBID
            'F3=Gudang
            'F4=Tgl
            'F5=Inisial BC
            If IO.File.Exists("SrBBBOMBtNum.xml") Then
                System.IO.File.Delete("SrBBBOMBtNum.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '" & F5 & "'+BBID As BBID,BtNum As BatchNum,Bahan,Sat,Case When Qty>Stok Then Stok Else Qty End As Qty From (Select x.*,tbl.BtNum, tbl.Stok From (Select BOMID,D.BBID as BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & F2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) As Qty From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where BOMID='" & F1 & "' and B.Aktif='True' Group By BOMID,D.BBID,B.Nama,D.Sat) as x Inner Join (Select BBID,BtNum,Sum(Masuk)-Sum(Keluar) As Stok From T_StokBB Where GdID='" & F3 & "' and Tanggal<='" & F4 & "' Group By BBID,BtNum) As tbl On x.BBID=tbl.BBID Union All Select x.*,tbl.BtNum, tbl.Stok From (Select TambahanID,D.BBID as BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.TambahanID and BBID=D.BBID and H.BPBID<>'" & F2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.TambahanID and BBID=D.BBID),0)) As Qty From T_BOMTamDtl D Inner Join M_BB B On D.BBID=B.BBID Where TambahanID='" & F1 & "' and B.Aktif='True' Group By TambahanID,D.BBID,B.Nama,D.Sat) as x Inner Join (Select BBID,BtNum,Sum(Masuk)-Sum(Keluar) As Stok From T_StokBB Where GdID='" & F3 & "' and Tanggal<='" & F4 & "' Group By BBID,BtNum) As tbl On x.BBID=tbl.BBID) as y Order By BBID", koneksi)

            'cmsl = New SqlDataAdapter("Select '" & F5 & "'+BBID As BBID,BtNum As BatchNum,Bahan,Sat,Case When Qty>Stok Then Stok Else Qty End As Qty From (Select x.*,tbl.BtNum, tbl.Stok From (Select BOMID,D.BBID as BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & F2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) As Qty From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where BOMID='" & F1 & "' and B.Aktif='True' Group By BOMID,D.BBID,B.Nama,D.Sat) as x Inner Join (Select BBID,BtNum,Sum(Masuk)-Sum(Keluar) As Stok From T_StokBB Where GdID='" & F3 & "' and Tanggal<='" & F4 & "' Group By BBID,BtNum) As tbl On x.BBID=tbl.BBID) as y Order By BBID", koneksi

            cmsl.TableMappings.Add("Table", "BBBOMBtNum")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BBBOMBtNum")

            DsSearch.WriteXml("SrBBBOMBtNum.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBBOMBtNum"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB Request" Then
            'Filter1=ReqID
            'Filter2=BPBID
            'Filter3=Gudang
            'Filter4=Tgl
            'Filter5=Inisial BC

            If IO.File.Exists("SrBBReq.xml") Then
                System.IO.File.Delete("SrBBReq.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '" & F5 & "'+D.BBID as BBID,B.Nama As Bahan,D.Sat,Case When Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.ReqPID and BBID=D.BBID and H.BPBID<>'" & F2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.ReqPID and BBID=D.BBID),0))) > dbo.fcStokBB(D.BBID,'" & F3 & "','" & F4 & "','" & F2 & "') Then dbo.fcStokBB(D.BBID,'" & F3 & "','" & F4 & "','" & F2 & "') Else Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.ReqPID and BBID=D.BBID and H.BPBID<>'" & F2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.ReqPID and BBID=D.BBID),0))) End  As Qty From T_ReqPDtl D Inner Join M_BB B On D.BBID=B.BBID Where ReqPID='" & F1 & "' and B.Aktif='True' Group By D.BBID,B.Nama,D.Sat,ReqPID", koneksi)

            cmsl.TableMappings.Add("Table", "BBReq")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BBReq")

            DsSearch.WriteXml("SrBBReq.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBReq"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB Request BtNum" Then
            'F1=ReqID
            'F2=BPBID
            'F3=Gudang
            'F4=Tgl
            'F5=Inisial BC

            If IO.File.Exists("SrBBReqBtNum.xml") Then
                System.IO.File.Delete("SrBBReqBtNum.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '" & F5 & "'+BBID As BBID,BtNum As BatchNum,Bahan,Sat,Case When Qty>Stok Then Stok Else Qty End As Qty From (Select x.*,tbl.BtNum, tbl.Stok From (Select ReqPID,D.BBID as BBID,B.Nama As Bahan,D.Sat,Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.ReqPID and BBID=D.BBID and H.BPBID<>'" & F2 & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.ReqPID and BBID=D.BBID),0))) As Qty From T_ReqPDtl D Inner Join M_BB B On D.BBID=B.BBID Where ReqPID='" & F1 & "' and B.Aktif='True' Group By ReqPID,D.BBID,B.Nama,D.Sat) as x Inner Join (Select BBID,BtNum,Sum(Masuk)-Sum(Keluar) As Stok From T_StokBB Where GdID='" & F3 & "' and Tanggal<='" & F4 & "' Group By BBID,BtNum) As tbl On x.BBID=tbl.BBID) as y Order By BBID", koneksi)

            cmsl.TableMappings.Add("Table", "BBReqBtNum")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BBReqBtNum")

            DsSearch.WriteXml("SrBBReqBtNum.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBReqBtNum"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB RPB" Then
            'F1=BOMID
            'F2=RPBID
            'F3=Inisial BC
            'F4=
            'F5=
            If IO.File.Exists("SrBBRPB.xml") Then
                System.IO.File.Delete("SrBBRPB.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '" & F3 & "'+D.BBID As BBID,BB.Nama As Bahan,D.Sat,(Sum(Qty)-(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=B.DocID and BBID=D.BBID and H.RPBID<>'" & F2 & "'),0))) As Qty From T_BPB B Inner Join T_BPBDtl D On B.BPBID=D.BPBID Inner Join M_BB BB On D.BBID=BB.BBID Where B.DocID='" & F1 & "' Group By B.DocID,D.BBID,BB.Nama,D.Sat", koneksi)

            cmsl.TableMappings.Add("Table", "BBRPB")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BBRPB")

            DsSearch.WriteXml("SrBBRPB.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBRPB"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB RPB BtNum" Then
            'F1=BOMID
            'F2=RPBID
            'F3=Inisial BC
            'F4=
            'F5=
            If IO.File.Exists("SrBBRPBBtNum.xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBRPBBtNum.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '" & F3 & "'+D.BBID As BBID,BtNum As BatchNum,BB.Nama As Bahan,D.Sat,(Sum(Qty)-(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=B.DocID and BBID=D.BBID and H.RPBID<>'" & F2 & "' and BtNum=D.BtNum),0))) As Qty From T_BPB B Inner Join T_BPBDtl D On B.BPBID=D.BPBID Inner Join M_BB BB On D.BBID=BB.BBID Where B.DocID='" & F1 & "' Group By B.DocID,D.BBID,BB.Nama,D.Sat,BtNum", koneksi)

            cmsl.TableMappings.Add("Table", "BBRPBBtNum")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BBRPBBtNum")

            DsSearch.WriteXml("SrBBRPBBtNum.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBRPBBtNum"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n0}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("BatchNum").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "BB Model" Then
            'F1=MdlID
            'F2=BOMID
            'F3=DivID
            'F4=
            'F5=KompID

            If IO.File.Exists("SrBBModel" & F1 & F3 & F5 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrBBModel" & F1 & F3 & F5 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select BBID,Bahan,UkBB,Sat,Sum(Keb) As Keb From(Select BP.ArtCode,MD.DivID,Md.KompID,MD.BBID, B.Nama As Bahan,UkBB,MD.Sat,Std,Bp.Qty+BP.QtyPol-Bp.BtlOrder As Qty, Case when KaliQty='True' Then Round(Std*(Bp.Qty+BP.QtyPol-Bp.BtlOrder),2) Else Case When Std=0 Then 0 Else Round((Bp.Qty+BP.QtyPol-Bp.BtlOrder)/Std,2) End End As Keb From M_ModelDtl MD Inner Join T_BOM BM On MD.MdlID=BM.MdlID Inner Join T_BOMPO BP On BM.BOMID=BP.BOMID and MD.ArtCode=BP.ArtCode Inner Join M_BB B On MD.BBID=B.BBID Where MD.MdlID='" & F1 & "' and BM.BOMID='" & F2 & "' and MD.DivID='" & F3 & "' and MD.KompID='" & F5 & "') as x Group By DivID,KompID,BBID,Bahan,UkBB,Sat", koneksi)

            cmsl.TableMappings.Add("Table", "BBModel")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "BBModel")

            DsSearch.WriteXml("SrBBModel" & F1 & F3 & F5 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "BBModel"

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("Keb").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Keb").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "LPB" Then
            'F1=SuppID
            'F2=Tipe PPn
            'F3=Mt Uang
            'F4=
            'F5=
            If IO.File.Exists("SrLPB.xml") Then
                System.IO.File.Delete("SrLPB.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select POID,TrmID,Tanggal,GdID,DiscP,DiscRp,DiscRpSat From T_TrmBB Where SuppID='" & F1 & "' and TipePPn='" & F2 & "' and MtUang='" & F3 & "' and Tanggal<='" & F4 & "' and stsTagih='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_TrmBB")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_TrmBB")

            DsSearch.WriteXml("SrLPB.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_TrmBB"

            Me.GridView1.Columns("GdID").Visible = False
            Me.GridView1.Columns("DiscP").Visible = False
            Me.GridView1.Columns("DiscRp").Visible = False
            Me.GridView1.Columns("DiscRpSat").Visible = False

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("DiscP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscP").DisplayFormat.FormatString = "{0:n3}"
            Me.GridView1.Columns("DiscRp").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscRp").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("DiscRpSat").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscRpSat").DisplayFormat.FormatString = "{0:n3}"

            Me.GridView1.Columns("TrmID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("POID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "LPB BJ" Then
            'F1=SuppID
            'F2=Tipe PPn
            'F3=Mt Uang
            'F4=
            'F5=

            If IO.File.Exists("SrLPBBJ.xml") Then
                System.IO.File.Delete("SrLPBBJ.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select POID,H.TrmID,Tanggal,GdID From T_TrmBJ H Where JnsDoc='PO' and SuppID='" & F1 & "' and TipePPn='" & F2 & "' and MtUang='" & F3 & "' and Tanggal<='" & F4 & "' and stsTagih='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_TrmBJ")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_TrmBJ")

            DsSearch.WriteXml("SrLPBBJ.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_TrmBJ"

            Me.GridView1.Columns("GdID").Visible = False

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

            Me.GridView1.Columns("TrmID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("POID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Retur BB" Then
            'F1=SuppID
            'F2=Tipe PPn
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrRBL.xml") Then
                System.IO.File.Delete("SrRBL.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select RtrID,Tanggal,GdID,DiscP,DiscRp,DiscRpSat From T_RtrBB Where SuppID='" & F1 & "' and TipePPn='" & F2 & "' and stsNotaRtr='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_RtrBB")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_RtrBB")

            DsSearch.WriteXml("SrRBL.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_RtrBB"

            Me.GridView1.Columns("GdID").Visible = False
            Me.GridView1.Columns("DiscP").Visible = False
            Me.GridView1.Columns("DiscRp").Visible = False
            Me.GridView1.Columns("DiscRpSat").Visible = False

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("DiscP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscP").DisplayFormat.FormatString = "{0:n3}"
            Me.GridView1.Columns("DiscRp").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscRp").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("DiscRpSat").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("DiscRpSat").DisplayFormat.FormatString = "{0:n3}"

            Me.GridView1.Columns("RtrID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Tagihan" Then
            'F1=SuppID
            'F2=MtUang
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrTagihan.xml") Then
                System.IO.File.Delete("SrTagihan.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select TagihID,Tanggal,SisaBayar From T_Tagihan Where SuppID='" & F1 & "' and MtUang='" & F2 & "' and stsLunas='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_TagihanSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_TagihanSr")

            DsSearch.WriteXml("SrTagihan.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_TagihanSr"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("TagihID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrHut Retur" Then
            'F1=SuppID
            'F2=MtUang
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrByrHutRtr.xml") Then
                System.IO.File.Delete("SrByrHutRtr.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select RtrTagihID,Tanggal,SisaPakai From T_RtrTagih Where SuppID='" & F1 & "' and MtUang='" & F2 & "' and stsPakai='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_RtrTagih")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_RtrTagih")

            DsSearch.WriteXml("SrByrHutRtr.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_RtrTagih"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("RtrTagihID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrHut Non Retur" Then
            'F1=SuppID
            'F2=MtUang
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrByrHutNRtr.xml") Then
                System.IO.File.Delete("SrByrHutNRtr.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select MetodeID,Tanggal,SisaPakai From T_MetodeByr Where SuppID='" & F1 & "' and MtUang='" & F2 & "' and stsPakai='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_MetodeByr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_MetodeByr")

            DsSearch.WriteXml("SrByrHutNRtr.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_MetodeByr"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("MetodeID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrHut JM" Then
            'F1=SuppID
            'F2=MtUang
            'F3=
            'F4=
            'F5=
            If IO.File.Exists("SrByrHutJM.xml") Then
                System.IO.File.Delete("SrByrHutJM.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select JMID,Tanggal,SisaPakai From T_JMHut Where SuppID='" & F1 & "' and MtUang='" & F2 & "' and stsPakai='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_JM")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_JM")

            DsSearch.WriteXml("SrByrHutJM.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JM"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JMID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Penjualan Giro" Then
            'F1=CustID
            'F2=Gol
            'F3=BGID
            'F4=Tanggal
            'F5=
            If IO.File.Exists("SrJualGiro.xml") Then
                System.IO.File.Delete("SrJualGiro.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select DPPBJID,J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BG H1 Inner Join T_BGDtl D1 On H1.BGID=D1.BGID Where JualID=J.JualID and H1.BGID<>'" & F3 & "' and H1.BGID Not In (Select Distinct DocID From T_ByrPiutDtl)),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID=J.JualID and DocID<> '" & F3 & "'),0)) As SisaBayar From T_JualBJ J Left Outer Join T_DPPBJDtl D On J.JualID= D.JualID Where J.CustID='" & F1 & "' and J.Gol='" & F2 & "' and stsLunas='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_JualBJ")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_JualBJ")

            DsSearch.WriteXml("SrJualGiro.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualBJ"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("DPPBJID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Penjualan Lain2" Then
            'F1=CustID
            'F2=Gol
            'F3=BGID
            'F4=Tanggal
            'F5=

            If IO.File.Exists("SrJualLain2.xml") Then
                System.IO.File.Delete("SrJualLain2.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BG H1 Inner Join T_BGDtl D1 On H1.BGID=D1.BGID Where JualID=J.JualID and H1.BGID<>'" & F3 & "' and H1.BGID Not In (Select Distinct DocID From T_ByrPiutDtl)),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID=J.JualID and DocID<> '" & F3 & "'),0)) As SisaBayar From T_JualBebas J Where J.CustID='" & F1 & "' and stsLunas='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_JualLain2")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_JualLain2")

            DsSearch.WriteXml("SrJualLain2.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualLain2"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Penjualan Bahan" Then
            'F1=CustID
            'F2=Gol
            'F3=BGID
            'F4=Tanggal
            'F5=

            If IO.File.Exists("SrJualBahan.xml") Then
                System.IO.File.Delete("SrJualBahan.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BG H1 Inner Join T_BGDtl D1 On H1.BGID=D1.BGID Where JualID=J.JualID and H1.BGID<>'" & F3 & "' and H1.BGID Not In (Select Distinct DocID From T_ByrPiutDtl)),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID=J.JualID and DocID<> '" & F3 & "'),0)) As SisaBayar From T_JualBB J Where J.CustID='" & F1 & "' and stsLunas='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_JualBahan")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_JualBahan")

            DsSearch.WriteXml("SrJualBahan.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualBahan"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "Penjualan Bayar" Then
            'F1=CustID
            'F2=Gol
            'F3=ByrPiutID
            'F4=
            'F5=MtUang
            If IO.File.Exists("SrJualByr.xml") Then
                System.IO.File.Delete("SrJualByr.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select DPPBJID,J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & F3 & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & F3 & "'),0)) As SisaBayar From T_JualBJ J Left Outer Join T_DPPBJDtl D On J.JualID= D.JualID Where J.CustID='" & F1 & "' and J.Gol='" & F2 & "'  and MtUang='" & F5 & "' and stsLunas='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_JualBJ")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_JualBJ")

            DsSearch.WriteXml("SrJualByr.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualBJ"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("DPPBJID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "Penjualan Lain2 Bayar" Then
            'F1=CustID
            'F2=Gol
            'F3=ByrPiutID
            'F4=
            'F5=MtUang
            If IO.File.Exists("SrJualLain2Byr.xml") Then
                System.IO.File.Delete("SrJualLain2Byr.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & F3 & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & F3 & "'),0)) As SisaBayar From T_JualBebas J Where J.CustID='" & F1 & "' and MtUang='" & F5 & "' and stsLunas='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_JualLain2")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_JualLain2")

            DsSearch.WriteXml("SrJualLain2Byr.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualLain2"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Penjualan Bahan Bayar" Then
            'F1=CustID
            'F2=Gol
            'F3=ByrPiutID
            'F4=
            'F5=MtUang
            If IO.File.Exists("SrJualBahanByr.xml") Then
                System.IO.File.Delete("SrJualBahanByr.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select J.JualID,J.Tanggal,J.Ket,J.MtUang As MataUang,J.TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & F3 & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & F3 & "'),0)) As SisaBayar From T_JualBB J Where J.CustID='" & F1 & "' and MtUang='" & F5 & "' and stsLunas='False'", koneksi)

            cmsl.TableMappings.Add("Table", "T_JualBahan")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_JualBahan")

            DsSearch.WriteXml("SrJualBahanByr.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_JualBahan"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaBayar").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("JualID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "ByrPiut" Then
            'F1=CustID
            'F2=Gol
            'F3=MtUang
            'F4=Tgl
            'F5=
            If IO.File.Exists("SrByrPiut.xml") Then
                System.IO.File.Delete("SrByrPiut.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select CashID As DocID,'Cash' As CaraBayar,'' As Deskripsi,SisaPakai From T_Cash Where CustID='" & F1 & "' and stsPakai='False' and Gol='" & F2 & "' and MtUang='" & F3 & "' UNION ALL Select BGID As DocID,'BG' As CaraBayar,'' As Deskripsi,SisaPakai From T_BG Where CustID='" & F1 & "' and stsPakai='False' and Gol='" & F2 & "' and MtUang='" & F3 & "' and '" & F4 & "'>= TglCair UNION ALL Select PotID As DocID,'DbCr Note' As CaraBayar,Nama As Deskripsi,0 As SisaPakai From M_JnsPot Where Aktif='True' Union All Select VcrID As DocID,'Voucher' As CaraBayar,Header As Deskripsi,SisaPakai From T_Voucher Where CustID='" & F1 & "' and stsPakai='False'  and Gol='" & F2 & "' and MtUang='" & F3 & "'", koneksi)

            cmsl.TableMappings.Add("Table", "CaraBayar")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "CaraBayar")

            DsSearch.WriteXml("SrByrPiut.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrPiut Retur" Then
            'F1=CustID
            'F2=Gol tidak dipakai filter bisa cross
            'F3=MtUang
            'F4=Tgl
            'F5=
            If IO.File.Exists("SrByrPiutRtr.xml") Then
                System.IO.File.Delete("SrByrPiutRtr.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select RtrID As DocID,'Retur' As CaraBayar,'' As Deskripsi,SisaPakai From T_RtrBJ Where CustID='" & F1 & "' and MtUang='" & F3 & "' and stsPakai='False'", koneksi)

            cmsl.TableMappings.Add("Table", "CaraBayar")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "CaraBayar")

            DsSearch.WriteXml("SrByrPiutRtr.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrPiut Retur Jual Bahan" Then
            'F1=CustID
            'F2=Gol tidak dipakai filter bisa cross
            'F3=MtUang
            'F4=Tgl
            'F5=

            If IO.File.Exists("SrByrPiutRtrJualBB.xml") Then
                System.IO.File.Delete("SrByrPiutRtrJualBB.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select RtrID As DocID,'Retur' As CaraBayar,'' As Deskripsi,SisaPakai From T_RtrPenjBB Where CustID='" & F1 & "' and MtUang='" & F3 & "' and stsPakai='False'", koneksi)

            cmsl.TableMappings.Add("Table", "CaraBayar")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "CaraBayar")

            DsSearch.WriteXml("SrByrPiutRtrJualBB.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "ByrPiut Retur Lain-Lain" Then
            'F1=CustID
            'F2=Gol tidak dipakai F bisa cross
            'F3=MtUang
            'F4=Tgl
            'F5=

            If IO.File.Exists("SrByrPiutRtrJualL2.xml") Then
                System.IO.File.Delete("SrByrPiutRtrJualL2.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select RtrID As DocID,'Retur' As CaraBayar,'' As Deskripsi,SisaPakai From T_RtrPenjBebas Where CustID='" & F1 & "' and MtUang='" & F3 & "' and stsPakai='False'", koneksi)

            cmsl.TableMappings.Add("Table", "CaraBayar")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "CaraBayar")

            DsSearch.WriteXml("SrByrPiutRtrJualL2.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


        ElseIf Master = "ByrPiut JM" Then
            'F1=CustID
            'F2=Gol
            'F3=MtUang
            'F4=Tgl
            'F5=
            If IO.File.Exists("SrByrPiutJM.xml") Then
                System.IO.File.Delete("SrByrPiutJM.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select JMID As DocID,'JM' As CaraBayar,'' As Deskripsi,SisaPakai From T_JMPiut Where CustID='" & F1 & "' and MtUang='" & F3 & "' and Gol='" & F2 & "' and stsPakai='False'", koneksi)

            cmsl.TableMappings.Add("Table", "CaraBayar")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "CaraBayar")

            DsSearch.WriteXml("SrByrPiutJM.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "CaraBayar"

            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("SisaPakai").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("DocID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("CaraBayar").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Tagihan Waste" Then
            'F1=
            'F2=
            'F3=
            'F4=Tanggal
            'F5=
            If IO.File.Exists("SrWaste.xml") Then
                System.IO.File.Delete("SrWaste.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select H.TagihID,H.PEN,TagihIDD,'" & F1 & "'+D.BBID As BBID,B.Nama,D.Sat,D.Qty From T_Tagihan H Inner Join T_TagihanDtl2 D On H.TagihID=D.TagihID Inner Join M_BB B On D.BBID=B.BBID Where Year(Tanggal)>=Year(dateadd(mm,-24,'" & F4 & "'))", koneksi)

            cmsl.TableMappings.Add("Table", "T_TagihanSr2")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_TagihanSr2")

            DsSearch.WriteXml("SrWaste.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_TagihanSr2"

            Me.GridView1.Columns("Qty").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("Qty").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("TagihIDD").Visible = False

            Me.GridView1.Columns("TagihID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("PEN").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Kategori TKL" Then
            If IO.File.Exists("SrKatTKL.xml") Then
                System.IO.File.Delete("SrKatTKL.xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select KatID,Kategori,GaJam,OTN,OTL From M_KatTKL Order By KatID Asc", koneksi)

            cmsl.TableMappings.Add("Table", "M_KatTKLSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_KatTKLSr")

            DsSearch.WriteXml("SrKatTKL.xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_KatTKLSr"

            Me.GridView1.Columns("KatID").Width = 200
            Me.GridView1.Columns("Kategori").Width = 350

            Me.GridView1.Columns("GaJam").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("GaJam").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("OTN").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("OTN").DisplayFormat.FormatString = "{0:n2}"
            Me.GridView1.Columns("OTL").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridView1.Columns("OTL").DisplayFormat.FormatString = "{0:n2}"

            Me.GridView1.Columns("KatID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Kategori").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        ElseIf Master = "Hasil Produksi" Then
            'F1=Unit
            If IO.File.Exists("SrHProd" & F1 & ".xml") Then
                System.IO.File.Delete("SrHProd" & F1 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Tanggal From T_HProd Where Unit='" & F1 & "'", koneksi)

            cmsl.TableMappings.Add("Table", "T_HProdSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_HProdSr")

            DsSearch.WriteXml("SrHProd" & F1 & ".xml", XmlWriteMode.WriteSchema)


            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_HProdSr"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

        ElseIf Master = "Hasil Produksi Target" Then
            'Filter1=Unit
            If IO.File.Exists("SrHProdTarget" & F1 & ".xml") Then
                System.IO.File.Delete("SrHProdTarget" & F1 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Tanggal,CuttUpp,CuttBott,Seri,SabUpp,SabIns,JhtKomp,JhtUpp,FinishUpp,Insock,Insole,Outsole, Insertt,Inject,Ass,Finish,Pack,Phylon From T_HProdTarget Where Unit='" & F1 & "' Order By Tanggal", koneksi)

            cmsl.TableMappings.Add("Table", "T_HProdTargetSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_HProdTargetSr")

            DsSearch.WriteXml("SrHProdTarget" & F1 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_HProdTargetSr"

            Me.GridView1.Columns("CuttUpp").Visible = False
            Me.GridView1.Columns("CuttBott").Visible = False
            Me.GridView1.Columns("Seri").Visible = False
            Me.GridView1.Columns("SabUpp").Visible = False
            Me.GridView1.Columns("SabIns").Visible = False
            Me.GridView1.Columns("JhtKomp").Visible = False
            Me.GridView1.Columns("JhtUpp").Visible = False
            Me.GridView1.Columns("FinishUpp").Visible = False
            Me.GridView1.Columns("Insock").Visible = False
            Me.GridView1.Columns("Insole").Visible = False
            Me.GridView1.Columns("Outsole").Visible = False
            Me.GridView1.Columns("Insertt").Visible = False
            Me.GridView1.Columns("Inject").Visible = False
            Me.GridView1.Columns("Ass").Visible = False
            Me.GridView1.Columns("Finish").Visible = False
            Me.GridView1.Columns("Pack").Visible = False
            Me.GridView1.Columns("Phylon").Visible = False

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

        ElseIf Master = "Hasil Produksi Jam" Then
            'F1=Unit
            If IO.File.Exists("SrHProdJam" & F1 & ".xml") Then
                System.IO.File.Delete("SrHProdJam" & F1 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Tanggal,Jam From T_HProdJam Where Unit='" & F1 & "'", koneksi)

            cmsl.TableMappings.Add("Table", "T_HProdJamSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_HProdJamSr")

            DsSearch.WriteXml("SrHProdJam" & F1 & ".xml", XmlWriteMode.WriteSchema)


            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_HProdJamSr"

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"

        ElseIf Master = "Samp Req" Then
            'Filter1=Mkt

            If IO.File.Exists("SrSampReq" & F1 & F2 & ".xml") Then
                DsSearch = New System.Data.DataSet
                DsSearch.ReadXml("SrSampReq" & F1 & F2 & ".xml")
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct H.ReqID,Rev,H.Tanggal,TglKirim,H.CustID,C.Nama As Cust,H.ChaserID,(Select Nama From M_User Where UserID=H.ChaserID) As Chaser,H.PtMkID,(Select Nama From M_User Where UserID=H.PtMkID) As PtMk,(Select StlName + '- '  From (Select Distinct SD.StlName From T_SampReqDtl SD Inner Join T_TrmSRDtl TD1 On SD.ReqID=TD1.ReqID and SD.ReqIDD=TD1.ReqIDD Where SD.ReqID=H.ReqID and QtyRj>0) as q FOR XML PATH('')) As StlName,(Select Warna + '- '  From (Select Distinct SD.Warna From T_SampReqDtl SD Inner Join T_TrmSRDtl TD1 On SD.ReqID=TD1.ReqID and SD.ReqIDD=TD1.ReqIDD Where SD.ReqID=H.ReqID and QtyRj>0) as q FOR XML PATH('')) As Warna From T_SampReq H Inner Join T_SampReqDtl D On H.ReqID=D.ReqID Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_TrmSRDtl TD On H.ReqID=TD.ReqID Where H.MktID='" & F1 & "' and QtyRj>0", koneksi)

            cmsl.TableMappings.Add("Table", "T_SampReqSr")
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "T_SampReqSr")

            DsSearch.WriteXml("SrSampReq" & F1 & F2 & ".xml", XmlWriteMode.WriteSchema)

            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "T_SampReqSr"

            Me.GridView1.Columns("CustID").Visible = False
            Me.GridView1.Columns("ChaserID").Visible = False
            Me.GridView1.Columns("PtMkID").Visible = False

            Me.GridView1.Columns("ReqID").Width = 120
            Me.GridView1.Columns("Cust").Width = 150
            Me.GridView1.Columns("Chaser").Width = 150
            Me.GridView1.Columns("PtMk").Width = 150
            Me.GridView1.Columns("StlName").Width = 150

            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            Me.GridView1.Columns("Tanggal").DisplayFormat.FormatString = "dd MMMM yyyy"
        End If
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        dataTrans = New Collection
        dataTrans.Clear()

        If Master = "Gudang" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("GdID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")

        ElseIf Master = "Posisi" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PosisiID"), "Kode")

        ElseIf Master = "Jenis Cust" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Jenis"), "Kode")

        ElseIf Master = "Sts Harga" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsHarga"), "Kode")

        ElseIf Master = "Cabang" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CabID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Cabang"), "Nama")

        ElseIf Master = "Proses" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Proses"), "Kode")

        ElseIf Master = "Sales" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SalId"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")

        ElseIf Master = "SalesCabang" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SalId"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")

        ElseIf Master = "Supplier" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SuppId"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")

        ElseIf Master = "Style BJ" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("StyleID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")

        ElseIf Master = "M_Brg" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("StyleId"), "StyleID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("MerkID"), "MerkID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("KatId"), "KatId")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JnsID"), "JnsID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SubJns"), "SubJns")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Urut"), "Urut")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("WrnID"), "WrnID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("AssID"), "AssID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SatID"), "SatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Isi"), "Isi")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("License"), "License")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Luxury"), "Luxury")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SubGrup"), "SubGrup")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Grup"), "Grup")

        ElseIf Master = "M_BrgJO" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("StyleId"), "StyleID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("KatID"), "KatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("WrnID"), "WrnID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Ass"), "Ass")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SatID"), "SatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Isi"), "Isi")

        ElseIf Master = "Brg Model" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Uk"), "Uk")

        ElseIf Master = "Master Mesin" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")

        ElseIf Master = "M_BB" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("HSCode"), "HSCode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DivPO"), "DivPO")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JnsID"), "JnsID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Merk"), "Merk")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SubJns"), "SubJns")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Tbl"), "Tbl")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Gram"), "Gram")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Wrn"), "Wrn")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Kode"), "SubKode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Hard"), "Hard")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Uk"), "Uk")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Jasa"), "Jasa")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Ket"), "Ket")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsJasa"), "stsJasa")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ThnProd"), "ThnProd")

        ElseIf Master = "Proses Produksi" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Proses"), "Kode")

        ElseIf Master = "Bahan BtNum" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BatchNum"), "BtNum")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")

        ElseIf Master = "Bahan BtNum No Stok" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BatchNum"), "BtNum")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")

        ElseIf Master = "Batch Number" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BatchNum"), "BtNum")

        ElseIf Master = "Collect PO" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CollPOID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CollPOIDD"), "CollPOIDD")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Cust"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "ArtCode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "BBSpecMdl" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Warna"), "Warna")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")

        ElseIf Master = "JualBJ" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SatID"), "SatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Isi"), "Isi")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsHarga"), "stsHarga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Harga"), "Harga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DiscOB"), "DiscOB")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Gol"), "Gol")

        ElseIf Master = "JualBJ Manual" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SatID"), "SatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Isi"), "Isi")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsHarga"), "stsHarga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Harga"), "Harga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DiscOB"), "DiscOB")
            'dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "JualPromo" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SatID"), "SatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Isi"), "Isi")

        ElseIf Master = "StokBJ" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SatID"), "SatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Isi"), "Isi")

        ElseIf Master = "StokBB" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")

        ElseIf Master = "Stok BJ Gol" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SatID"), "SatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Isi"), "Isi")

        ElseIf Master = "Stok BB Gol" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")

        ElseIf Master = "BJ Gol" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SatID"), "SatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Isi"), "Isi")

        ElseIf Master = "Stok BJ Op" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SatID"), "SatID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Isi"), "Isi")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Stok"), "Qty")

        ElseIf Master = "Stok BB Op" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JnsID"), "JnsID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Jenis"), "Jenis")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Stok"), "Qty")

        ElseIf Master = "Divisi" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DivID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PrsPol"), "PrsPol")

        ElseIf Master = "Komponen" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("KompID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")

        ElseIf Master = "Spec" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SpecID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CustID"), "CustID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("StyleID"), "StyleID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Brand"), "Brand")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ShoeLast"), "ShoeLast")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "ArtName")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Warna"), "Warna")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("RangeSize"), "RangeSize")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SampleSize"), "SampleSize")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Ket"), "Ket")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Dibuat"), "Dibuat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Pattern"), "Pattern")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Chaser"), "Chaser")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PPC"), "PPC")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PembKulit"), "PembKulit")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PembNonKulit"), "PembNonKulit")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Teknik"), "Teknik")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Mengetahui"), "Mengetahui")

        ElseIf Master = "Model" Then

            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("MdlID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SpecID"), "SpecID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("StyleID"), "StyleID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Brand"), "Brand")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "ArtName")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Warna"), "Warna")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("RangeSize"), "RangeSize")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SampleSize"), "SampleSize")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PersenGenerate"), "PersenGenerate")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Ket"), "Ket")

        ElseIf Master = "Purchase Request Tooling" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PRTID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PRTIDD"), "DocIDD")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "BBID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Harga"), "Harga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsJasa"), "stsJasa")

        ElseIf Master = "Purchase Request Sparepart" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PRSMID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PRSMIDD"), "DocIDD")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "BBID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Harga"), "Harga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsJasa"), "stsJasa")

        ElseIf Master = "Request" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ReqPID"), "Kode")

        ElseIf Master = "BOM" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BOMID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("MerkID"), "MerkID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JnsID"), "JnsID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "ArtName")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Warna"), "Warna")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("TotPsg"), "TotPsg")

        ElseIf Master = "BOM Jasa" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BOMID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "ArtName")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Warna"), "Warna")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("TotPsg"), "TotPsg")

        ElseIf Master = "BB With BOM" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Harga"), "Harga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsJasa"), "stsJasa")

        ElseIf Master = "BOM Sch" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BOMID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ArtName"), "ArtName")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Warna"), "Warna")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("TotPsg"), "TotPsg")

        ElseIf Master = "BB With Request" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Harga"), "Harga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsJasa"), "stsJasa")

        ElseIf Master = "BB Kanan With BOM" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "BB Kanan With Request" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "BB No BOM" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Harga"), "Harga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsJasa"), "stsJasa")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Uk"), "Uk")

        ElseIf Master = "BB No BOM BtNum" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BatchNum"), "BtNum")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Harga"), "Harga")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("stsJasa"), "stsJasa")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Uk"), "Uk")

        ElseIf Master = "BB BOM" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "BB BOM BtNum" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BatchNum"), "BtNum")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "BB Request" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "BB Request BtNum" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BatchNum"), "BtNum")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "BB RPB" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "BB RPB BtNum" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BatchNum"), "BtNum")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Qty"), "Qty")

        ElseIf Master = "BB Model" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Bahan"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("UkBB"), "Uk")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Keb"), "Keb")

        ElseIf Master = "LPB" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("TrmID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("POID"), "POID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Tanggal"), "Tgl")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("GdID"), "GdID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DiscP"), "DiscP")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DiscRp"), "DiscRp")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DiscRpSat"), "DiscRpSat")

        ElseIf Master = "LPB BJ" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("TrmID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("POID"), "POID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Tanggal"), "Tgl")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("GdID"), "GdID")

        ElseIf Master = "Retur BB" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("RtrID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Tanggal"), "Tgl")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("GdID"), "GdID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DiscP"), "DiscP")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DiscRp"), "DiscRp")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DiscRpSat"), "DiscRpSat")

        ElseIf Master = "Tagihan" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("TagihID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaBayar"), "SisaBayar")

        ElseIf Master = "ByrHut Retur" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("RtrTagihID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaPakai"), "SisaPakai")

        ElseIf Master = "ByrHut Non Retur" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("MetodeID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaPakai"), "SisaPakai")

        ElseIf Master = "ByrHut JM" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JMID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaPakai"), "SisaPakai")

        ElseIf Master = "Penjualan Giro" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JualID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaBayar"), "SisaBayar")

        ElseIf Master = "Penjualan Lain2" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JualID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaBayar"), "SisaBayar")

        ElseIf Master = "Penjualan Bahan" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JualID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaBayar"), "SisaBayar")

        ElseIf Master = "Penjualan Bayar" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JualID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaBayar"), "SisaBayar")

        ElseIf Master = "Penjualan Lain2 Bayar" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JualID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaBayar"), "SisaBayar")

        ElseIf Master = "Penjualan Bahan Bayar" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JualID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaBayar"), "SisaBayar")

        ElseIf Master = "ByrPiut" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DocID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CaraBayar"), "Cara")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Deskripsi"), "Deskripsi")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaPakai"), "SisaPakai")

        ElseIf Master = "ByrPiut Retur" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DocID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CaraBayar"), "Cara")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Deskripsi"), "Deskripsi")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaPakai"), "SisaPakai")

        ElseIf Master = "ByrPiut Retur Jual Bahan" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DocID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CaraBayar"), "Cara")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Deskripsi"), "Deskripsi")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaPakai"), "SisaPakai")

        ElseIf Master = "ByrPiut Retur Lain-Lain" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DocID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CaraBayar"), "Cara")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Deskripsi"), "Deskripsi")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaPakai"), "SisaPakai")

        ElseIf Master = "ByrPiut JM" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("DocID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CaraBayar"), "Cara")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Deskripsi"), "Deskripsi")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SisaPakai"), "SisaPakai")

        ElseIf Master = "Tagihan Waste" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("TagihID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PEN"), "PEN")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("TagihIDD"), "TagihIDD")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("BBID"), "BBID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Nama"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Sat"), "Sat")

        ElseIf Master = "Kategori TKL" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("KatID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Kategori"), "Nama")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("GaJam"), "GaJam")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("OTN"), "OTN")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("OTL"), "OTL")

        ElseIf Master = "Hasil Produksi" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Tanggal"), "Tanggal")

        ElseIf Master = "Hasil Produksi Target" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CuttUpp"), "CuttUpp")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CuttBott"), "CuttBott")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Seri"), "Seri")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SabUpp"), "SabUpp")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("SabIns"), "SabIns")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JhtKomp"), "JhtKomp")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("JhtUpp"), "JhtUpp")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("FinishUpp"), "FinishUpp")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Insock"), "Insock")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Insole"), "Insole")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Outsole"), "Outsole")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Insertt"), "Insertt")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Inject"), "Inject")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Ass"), "Ass")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Finish"), "Finish")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Pack"), "Pack")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Phylon"), "Phylon")

        ElseIf Master = "Hasil Produksi Jam" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Jam"), "Jam")

        ElseIf Master = "Samp Req" Then
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ReqID"), "Kode")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Rev"), "Rev")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("CustID"), "CustID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("ChaserID"), "ChaserID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("PtMkID"), "PtMkID")
            dataTrans.Add(Me.GridView1.GetFocusedDataRow.Item("Pattern"), "Pattern")

        End If

        Timer1.Enabled = True

        'Me.Close()
    End Sub

    Private Sub FSearch_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.09
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub BRefresh_KeyDown(sender As Object, e As KeyEventArgs) Handles BRefresh.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub CEAll_EditValueChanged(sender As Object, e As EventArgs) Handles CEAll.EditValueChanged
        If Me.CEAll.EditValue = "True" Then
            If IO.File.Exists("SrBB" & F2 & ".xml") Then
                System.IO.File.Delete("SrBB" & F2 & ".xml")
            End If

            If IO.File.Exists("SrBBWReq.xml") Then
                System.IO.File.Delete("SrBBWReq.xml")
            End If

            If IO.File.Exists("SrBBWBOM.xml") Then
                System.IO.File.Delete("SrBBWBOM.xml")
            End If

            If IO.File.Exists("SrBBNBOM.xml") Then
                System.IO.File.Delete("SrBBNBOM.xml")
            End If

            dataTrans = New Collection
            dataTrans.Clear()

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct BBID,Nama As Bahan,Sat, 0 as Qty From M_BB Inner Join M_BBJns J On B.JnsID=J.JnsID Where B.Aktif='True' and J.Gol Like'" & F2 & "%' Order By B.Nama", koneksi)

            cmsl.TableMappings.Add("Table", "M_BB" & F2)
            DsSearch = New System.Data.DataSet
            cmsl.Fill(DsSearch, "M_BB" & F2)


            Me.GridControl1.DataSource = DsSearch
            Me.GridControl1.DataMember = "M_BB" & F2

            Me.GridView1.Columns("Bahan").Width = 250

            Me.GridView1.Columns("BBID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Me.GridView1.Columns("Bahan").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        End If

    End Sub
End Class