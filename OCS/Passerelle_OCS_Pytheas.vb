
'////////////////////////////////////////////////////////////
'////////////////////////////////////////////////////////////
'Directives d'exécution PAM
'#PAM# RULE IS CONNECTED
'#PAM# RULE EXTERNAL
'#PAM# USE PAMSCRIPTTOOLS
'////////////////////////////////////////////////////////////
Dim opvErrorGlobal			'Traitement global des erreurs
Dim optLastError			'Dernière erreur
Dim optLastErrorMessage	'Dernier message d'erreur
Dim optLastNodeError		'Dernier noeud d'erreur
Dim otApplicationName		'Nom de l'application
Dim otApplicationDescription	'Description de l'application
Dim ovLog
Dim ovCreateLog
Dim ovTypeLog
Dim opvFormatLog
Dim opvSendMail
Dim optRecipient
Dim optSubject
Dim optBody
Dim otFileLog
Dim ovpImport
Dim opColLog
Dim TemplateRoot		'Répertoire de base du modèle
Dim PyEvent                'PyParam(0) PyEvent

Dim CompanyModel
Dim FichierINI	'Fichier INI de dernière date de synchro
Dim odDateNow	'Date du jour
Dim odLastDate	'Dernière date d'inventaire
Dim otrequeteUC	'requête UC
Dim DATASOURCE1_FormatDate
Dim RecPoste	'Enregistrement Liste des postes OCS
Dim RecSoft	'Enregistrement Logiciels flagués pytheas
Dim RecPartition	'Enregistrement Partitions logiques
Dim RecBios	'Enregistrement Bios
Dim RecMemory	'Enregistrement Mémoire
Dim RecProc	'Enregistrement Processeur
Dim RecBoard	'Enregistrement Carte réseau
Dim RecHardDisk	'Enregistrement Disque dur
Dim RecTAG	'Enregistrement TAG ocs
Dim RecRegistry	'Enregistrement Registry OCS
Dim TRANSFORMATION1	'Transformation de données
Dim TRANSFORMATION1_OutputDirectory
Dim TRANSFORMATION1_MasterElement
Dim TRANSFORMATION1_Tatouage
Dim TRANSFORMATION1_Count
Dim RRecPoste
Dim onlCount_RRecPoste
Dim Rec_RRecPoste
Dim Cmd_RRecPoste
Dim otMacAdd	'Mac Adress
Dim otSerialUC	'N° série UC
Dim REGROUPEMENT13
Dim onlCount_REGROUPEMENT13
Dim Rec_REGROUPEMENT13
Dim Cmd_REGROUPEMENT13
Dim REGROUPEMENT18
Dim onlCount_REGROUPEMENT18
Dim Rec_REGROUPEMENT18
Dim Cmd_REGROUPEMENT18
Dim opIf		'Dernier if évalué
Dim RRecMemory
Dim onlCount_RRecMemory
Dim Rec_RRecMemory
Dim Cmd_RRecMemory
Dim RRecBoard2
Dim onlCount_RRecBoard2
Dim Rec_RRecBoard2
Dim Cmd_RRecBoard2
Dim opProperty
Dim PosteNumero
Dim REGROUPEMENT16
Dim onlCount_REGROUPEMENT16
Dim Rec_REGROUPEMENT16
Dim Cmd_REGROUPEMENT16
Dim RRecBios
Dim onlCount_RRecBios
Dim Rec_RRecBios
Dim Cmd_RRecBios
Dim RRecHardDisk
Dim onlCount_RRecHardDisk
Dim Rec_RRecHardDisk
Dim Cmd_RRecHardDisk
Dim RRecSoft
Dim onlCount_RRecSoft
Dim Rec_RRecSoft
Dim Cmd_RRecSoft
Dim RRecPartition
Dim onlCount_RRecPartition
Dim Rec_RRecPartition
Dim Cmd_RRecPartition
Dim RRecProc
Dim onlCount_RRecProc
Dim Rec_RRecProc
Dim Cmd_RRecProc
Dim PAMTASK1

'////////////////////////////////////////////////////////////
'Passerelle OCS Inventory
Function Main(PyParam)

'////////////////////////////////////////////////////////////
	Dim SaveStateInfo
	Dim oTemp
	Dim oTemp1
	Dim oTemp2
	Dim otTemp
	Dim otTemp1
	Dim otTemp2
	Dim onlTemp
	Dim onlTemp1
	Dim onlTemp2
	Dim DATASOURCE1	'Source de données
	Dim Rec_DATASOURCE1
	Dim Sql_RecPoste'Chaîne SQLListe des postes OCS
	Dim Sql_RecSoft'Chaîne SQLLogiciels flagués pytheas
	Dim Sql_RecPartition'Chaîne SQLPartitions logiques
	Dim Sql_RecBios'Chaîne SQLBios
	Dim Sql_RecMemory'Chaîne SQLMémoire
	Dim Sql_RecProc'Chaîne SQLProcesseur
	Dim Sql_RecBoard'Chaîne SQLCarte réseau
	Dim Sql_RecHardDisk'Chaîne SQLDisque dur
	Dim Sql_RecTAG'Chaîne SQLTAG ocs
	Dim Sql_RecRegistry'Chaîne SQLRegistry OCS
	Dim oGrp_RRecPoste
	Dim oTmp_RRecPoste
	Dim oFilter_RRecPoste
	Dim oni_RRecPoste
	Dim LoginUser	'Login utilisateur
	Dim odDateInv	'Date inventaire
	Dim otOSname	'OS poste
	Dim otTagOcs	'TAG ocs
	Dim otDesignation	'Modèle de l'UC
	Dim otTypePoste	'Type de poste à créer
	Dim ottypeOCS	'type OCS
	Dim otComputerName	'ComputerName
	Dim onlMemory	'Mémoire (Mo)
	Dim otMemory	'Mémoire (Composition)
	Dim oGrp_REGROUPEMENT13
	Dim oTmp_REGROUPEMENT13
	Dim oFilter_REGROUPEMENT13
	Dim oni_REGROUPEMENT13
	Dim oGrp_REGROUPEMENT18
	Dim oTmp_REGROUPEMENT18
	Dim oFilter_REGROUPEMENT18
	Dim oni_REGROUPEMENT18
	Dim oGrp_RRecMemory
	Dim oTmp_RRecMemory
	Dim oFilter_RRecMemory
	Dim oni_RRecMemory
	Dim oGrp_RRecBoard2
	Dim oTmp_RRecBoard2
	Dim oFilter_RRecBoard2
	Dim oni_RRecBoard2
	Dim OBJECTTRANS13
	Dim Temp_OBJECTTRANS13
	Dim Key_OBJECTTRANS13
	Dim ObjPoste
	Dim Temp_ObjPoste
	Dim Key_ObjPoste
	Dim UC
	Dim Temp_UC
	Dim Key_UC
	Dim oGrp_REGROUPEMENT16
	Dim oTmp_REGROUPEMENT16
	Dim oFilter_REGROUPEMENT16
	Dim oni_REGROUPEMENT16
	Dim OBJECTTRANS10
	Dim Temp_OBJECTTRANS10
	Dim Key_OBJECTTRANS10
	Dim oGrp_RRecBios
	Dim oTmp_RRecBios
	Dim oFilter_RRecBios
	Dim oni_RRecBios
	Dim oGrp_RRecHardDisk
	Dim oTmp_RRecHardDisk
	Dim oFilter_RRecHardDisk
	Dim oni_RRecHardDisk
	Dim DisqueDur
	Dim Temp_DisqueDur
	Dim Key_DisqueDur
	Dim oGrp_RRecSoft
	Dim oTmp_RRecSoft
	Dim oFilter_RRecSoft
	Dim oni_RRecSoft
	Dim Software
	Dim Temp_Software
	Dim Key_Software
	Dim oGrp_RRecPartition
	Dim oTmp_RRecPartition
	Dim oFilter_RRecPartition
	Dim oni_RRecPartition
	Dim Partition
	Dim Temp_Partition
	Dim Key_Partition
	Dim oGrp_RRecProc
	Dim oTmp_RRecProc
	Dim oFilter_RRecProc
	Dim oni_RRecProc
	Dim FOREACH1	'Pour chaque nombre de processeur
	Dim Processeur
	Dim Temp_Processeur
	Dim Key_Processeur
	Dim Network
	Dim Temp_Network
	Dim Key_Network
	Dim OBJECTTRANS12
	Dim Temp_OBJECTTRANS12
	Dim Key_OBJECTTRANS12
	Dim OBJECTTRANS14
	Dim Temp_OBJECTTRANS14
	Dim Key_OBJECTTRANS14
	Dim objRES
	Dim Temp_objRES
	Dim Key_objRES
	Dim oParam_PAMTASK1
	Dim oLog_PAMTASK1

'////////////////////////////////////////////////////////////
	opvErrorGlobal = True
	On error resume next
	otApplicationName = "ImportationOCS"
	otApplicationDescription = "Passerelle OCS Inventory"
	optLastError = ""
	optLastErrorMessage = ""
	Set opColLog = CreateObject("Scripting.Dictionary")
	SaveStateInfo = PyPam.Objects.ovModeStatic
	PyPam.Objects.ovModeStatic = False
	'PyParam(0) PyEvent
	If PyParam(0)>0 then
		Set PyEvent = PYPAM.Script.Events.oLoadEvent(PyParam(0))
	Else
		Set PyEvent = nothing
	End If
	If ubound(PyParam)>0 then
		SET PYOBJECT = PYPAM.onlNewInfoObject("", clng(PyParam(1)))
	Else
		SET PYOBJECT = nothing
	End if
	'Répertoire de base du modèle
	TemplateRoot = "C:\PAMRoot\Import\OCS"

	'PyParam(1) CompanyModel 
	Err.Clear
	If uBound(PyParam)>=1 Then
		If IsObject(PyParam(1)) = True Then
			If err <> 0 Then
				err.clear
				CompanyModel = "SDIS13"
			Else
				Set CompanyModel = PyParam(1)
			End If
		Else
			CompanyModel = PyParam(1)
		End If
	Else
		CompanyModel = "SDIS13"
	End If
	Err.Clear

	Key_OBJECTTRANS13 = ""
	Key_ObjPoste = ""
	Key_UC = ""
	Key_OBJECTTRANS10 = ""
	Key_DisqueDur = ""
	Key_Software = ""
	Key_Partition = ""
	Key_Processeur = ""
	Key_Network = ""
	Key_OBJECTTRANS12 = ""
	Key_OBJECTTRANS14 = ""
	Key_objRES = ""

'////////////////////////////////////////////////////////////

	'Création de la structure du répertoire de base du modèle
	If Len(CreateTemplateRoot(TemplateRoot))=0 Then
		Main = "Error : Unable to create TemplateRoot directories"
		Exit Function
	End If

	otFileLog=TemplateRoot & "\log.txt"
	ovLog = -1
	ovCreateLog = -1
	opvFormatLog = -1
	CreateLog

	ovTypeLog = -1
	opvSendMail = 0
	optRecipient = otEmail
	optSubject = ""

	err.Clear
	FichierINI = "d:\pytheas\ocs\ocs.ini"

	err.Clear
	odDateNow = cstr(now)

	err.Clear
	odLastDate = ""

	'Recherche dernière date d'inventaire
	odLastDate = odSearchDate("m")
	err.Clear
	otrequeteUC = generereqUC(odLastDate)

	'Ouverture de la source de donnée
	Err.clear
	Set DATASOURCE1 = CreateObject("ADODB.Connection")
	DATASOURCE1.CommandTimeout = 30
	DATASOURCE1.Open "Provider=MSDASQL.1;Password=sco;Persist Security Info=True;User ID=ocs;Data Source=OCS"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "DATASOURCE1"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
		Exit Function
	End if
	Set Rec_DATASOURCE1 = CreateObject("ADODB.Recordset")
	Rec_DATASOURCE1.CursorLocation = 3
	Err.clear
	Rec_DATASOURCE1.Open "select dateformat from master.dbo.syslanguages where langid = @@langid",DATASOURCE1,3,1
	If Err=0 then 
		If Not Rec_DATASOURCE1.EOF Then
			DATASOURCE1_FormatDate = CStr(Trim(Rec_DATASOURCE1(0).Value))
		Else
			DATASOURCE1_FormatDate = ""
		End If
		Rec_DATASOURCE1.Close
	End If
	Set Rec_DATASOURCE1 = nothing
	Err.clear
	If Err=0 then 
	'Chaine de la requête SQL de Liste des postes OCS
	Sql_RecPoste = otrequeteUC
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecPoste"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	'Chaine de la requête SQL de Logiciels flagués pytheas
	Sql_RecSoft = "SELECT ID, HARDWARE_ID, publisher as PUBLISHER, name as NAME, version as VERSION, folder as FOLDER, comments  as COMMENTS, filename as FILENAME FROM softwares, dico_soft "
	Sql_RecSoft = Sql_RecSoft & " where name = extracted and formatted='pytheas'"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecSoft"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	'Chaine de la requête SQL de Partitions logiques
	Sql_RecPartition = "SELECT ID, HARDWARE_ID, LETTER, convert(convert(volumn using binary) using utf8"
	Sql_RecPartition = Sql_RecPartition & ") as VOLUMN, convert(convert(filesystem using binary) using utf8) as FILESYSTEM,"
	Sql_RecPartition = Sql_RecPartition & " TOTAL, FREE, convert(convert(type using binary) using utf8) as TYPE FROM drives"
	Sql_RecPartition = Sql_RecPartition & " where ifnull(type,'') not like '%removable%' and ifnull(type,'') not like '%cd%"
	Sql_RecPartition = Sql_RecPartition & "rom%'"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecPartition"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	'Chaine de la requête SQL de Bios
	Sql_RecBios = "select type as TYPEPOSTE, HARDWARE_ID, convert(convert(smanufacturer using bina"
	Sql_RecBios = Sql_RecBios & "ry) using utf8) as SMANUFACTURER, convert(convert(smodel using binary) using utf"
	Sql_RecBios = Sql_RecBios & "8) as SMODEL, convert(convert(ssn using binary) using utf8) as SSN, BDATE, conve"
	Sql_RecBios = Sql_RecBios & "rt(convert(bmanufacturer using binary) using utf8) as BMANUFACTURER, convert(con"
	Sql_RecBios = Sql_RecBios & "vert(bversion using binary) using utf8) as BVERSION from bios"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecBios"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	'Chaine de la requête SQL de Mémoire
	Sql_RecMemory = "SELECT ID, HARDWARE_ID, concat(count(id),'x',replace(convert(convert(type using"
	Sql_RecMemory = Sql_RecMemory & " binary) using utf8),'<OUT OF SPEC>','Unknown'),'-',capacity,'Mo ') as Label, su"
	Sql_RecMemory = Sql_RecMemory & "m(capacity) as Total FROM memories where replace(capacity,'No',0) > 0 group by h"
	Sql_RecMemory = Sql_RecMemory & "ardware_id, type, capacity"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecMemory"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	'Chaine de la requête SQL de Processeur
	Sql_RecProc = "SELECT ID, convert(convert(processort using binary) using utf8) as TYPE, proces"
	Sql_RecProc = Sql_RecProc & "sors as FREQUENCY, processorn as NB FROM hardware"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecProc"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	'Chaine de la requête SQL de Carte réseau
	Sql_RecBoard = "SELECT ID, status, HARDWARE_ID, convert(convert(description using binary) using"
	Sql_RecBoard = Sql_RecBoard & " utf8) as DESCRIPTION, MACADDR FROM networks"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecBoard"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	'Chaine de la requête SQL de Disque dur
	Sql_RecHardDisk = "SELECT ID, HARDWARE_ID, convert(convert(manufacturer using binary) using utf8) "
	Sql_RecHardDisk = Sql_RecHardDisk & "as MANUFACTURER, convert(convert(model using binary) using utf8) as MODEL, conve"
	Sql_RecHardDisk = Sql_RecHardDisk & "rt(convert(name using binary) using utf8) as NAME, DISKSIZE FROM storages where "
	Sql_RecHardDisk = Sql_RecHardDisk & "ifnull(disksize,0) > 0 and concat_ws('',manufacturer,name,description,type) not "
	Sql_RecHardDisk = Sql_RecHardDisk & "like '%floppy%' and concat_ws('',manufacturer,name,description,type) not like '%"
	Sql_RecHardDisk = Sql_RecHardDisk & "disquette%' and concat_ws('',manufacturer,name,description,type) not like '%cd%r"
	Sql_RecHardDisk = Sql_RecHardDisk & "om%' and concat_ws('',manufacturer,name,description,type) not like '%removable%'"
	Sql_RecHardDisk = Sql_RecHardDisk & " and concat_ws('',manufacturer,name,description,type) not like '%usb%' and conca"
	Sql_RecHardDisk = Sql_RecHardDisk & "t_ws('',manufacturer,name,description,type) not like '%floppy%' and concat_ws(''"
	Sql_RecHardDisk = Sql_RecHardDisk & ",manufacturer,name,description,type) not like '%tape%'"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecHardDisk"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	'Chaine de la requête SQL de TAG ocs
	Sql_RecTAG = "SELECT * FROM accountinfo"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecTAG"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	'Chaine de la requête SQL de Registry OCS
	Sql_RecRegistry = "select * from registry"
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RecRegistry"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError
		End if
	End if
	End If
	'Transformation de données
	TRANSFORMATION1_OutputDirectory = "D:\temp\txt"
	TRANSFORMATION1_MasterElement = ObjPoste
	TRANSFORMATION1_Tatouage = otComputerName
	TRANSFORMATION1_Count = 0
	Err.clear
	Set TRANSFORMATION1 = CreateObject("Scripting.Dictionary")
	Set oTemp = CreateObject("Scripting.Dictionary")
	If Err=0 then
		TRANSFORMATION1.Add "Name","TRANSFORMATION1"
		TRANSFORMATION1.Add "Description","Transformation de données"
		TRANSFORMATION1.Add "Properties",oTemp
		TRANSFORMATION1.Add "DeleteObject","0"
		TRANSFORMATION1.Add "UpdateObject","1"
		TRANSFORMATION1.Add "ObjectTypes",""
		Set oTemp = nothing
		Set oTemp = CreateObject("Scripting.Dictionary")
		TRANSFORMATION1.Add "Elements",oTemp
		Set oTemp = nothing
		Set oTemp = CreateObject("Scripting.Dictionary")
		TRANSFORMATION1.Add "OrderLocal",oTemp
		Set oTemp = nothing
		Set oTemp = CreateObject("Scripting.Dictionary")
		TRANSFORMATION1.Add "Resumed",oTemp
		Set oTemp = nothing
	End If
	oTmp_RRecPoste = Sql_RecPoste
	oTmp_RRecPoste = Sql_RecPoste
	If (instr(1,oTmp_RRecPoste,"PARAMETERS ",1)>0) AND (instr(1,oTmp_RRecPoste,"SELECT ",1)>0) Then
		oTmp_RRecPoste = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_RRecPoste)
	End if
	Set RecPoste = CreateObject("ADODB.Recordset")
	RecPoste.CursorLocation = 3
	RecPoste.CommandTimeout = DATASOURCE1.CommandTimeout
	Err.clear
	RecPoste.Open oTmp_RRecPoste, DATASOURCE1,3,1
	If Err<>0 then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "RRecPoste"
		if ovLog = -1 Then
			WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_RRecPoste
		End if
	End if
	If RecPoste.State = 1 Then
		If Not RecPoste.EOF Then
			onlCount_RRecPoste = 1
			Do Until RecPoste.EOF
				If Err<>0 then exit do
				On error resume next
				Key_OBJECTTRANS13 = ""
				Key_ObjPoste = ""
				Key_UC = ""
				Key_OBJECTTRANS10 = ""
				Key_DisqueDur = ""
				Key_Software = ""
				Key_Partition = ""
				Key_Processeur = ""
				Key_Network = ""
				Key_OBJECTTRANS12 = ""
				Key_OBJECTTRANS14 = ""
				Key_objRES = ""
				Err.clear

				err.Clear
				LoginUser = Nz(RecPoste("USERID").value,"")

				err.Clear
				odDateInv = Nz(RecPoste("LASTDATE").value,"")

				err.Clear
				otOSname = ""

				err.Clear
				otTagOcs = ""

				err.Clear
				otMacAdd = ""

				err.Clear
				otSerialUC = ""

				err.Clear
				otDesignation = ""

				err.Clear
				otTypePoste = "408"

				err.Clear
				ottypeOCS = ""

				err.Clear
				otComputerName = otDecodeUTF8(Nz(RecPoste("NAME").value,""))

				err.Clear
				onlMemory = 0

				err.Clear
				otMemory = ""

				oTemp = Sql_RecTAG
				'Synchronisation sur le regroupement supérieur
				oFilter_REGROUPEMENT13 = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecTAG,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_REGROUPEMENT13 = oFilter_REGROUPEMENT13 & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_REGROUPEMENT13 = oFilter_REGROUPEMENT13 & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_REGROUPEMENT13)>0 Then
					oTmp_REGROUPEMENT13 = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecTAG, "WHERE", "(" & oFilter_REGROUPEMENT13 & ")","AND")
				Else
					oTmp_REGROUPEMENT13 = Sql_RecTAG
				End If
				If (instr(1,oTmp_REGROUPEMENT13,"PARAMETERS ",1)>0) AND (instr(1,oTmp_REGROUPEMENT13,"SELECT ",1)>0) Then
					oTmp_REGROUPEMENT13 = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_REGROUPEMENT13)
				End if
				Set RecTAG = CreateObject("ADODB.Recordset")
				RecTAG.CursorLocation = 3
				RecTAG.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecTAG.Open oTmp_REGROUPEMENT13, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "REGROUPEMENT13"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_REGROUPEMENT13
					End if
				End if
				If RecTAG.State = 1 Then
					If Not RecTAG.EOF Then
						onlCount_REGROUPEMENT13 = 1
						Do Until RecTAG.EOF
							If Err<>0 then exit do
							On error resume next
							Err.clear

							'Récupération du TAG ocs
							otTagOcs = RecTAG("TAG").value
							Err.Clear
							RecTAG.MoveNext
							onlCount_REGROUPEMENT13 = onlCount_REGROUPEMENT13 + 1 
						Loop
						err.Clear
						RecTAG.close
					End If
				End If
				Set RecTAG= Nothing
				oTemp = Sql_RecBios
				'Synchronisation sur le regroupement supérieur
				oFilter_REGROUPEMENT18 = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecBios,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_REGROUPEMENT18 = oFilter_REGROUPEMENT18 & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_REGROUPEMENT18 = oFilter_REGROUPEMENT18 & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_REGROUPEMENT18)>0 Then
					oTmp_REGROUPEMENT18 = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecBios, "WHERE", "(" & oFilter_REGROUPEMENT18 & ")","AND")
				Else
					oTmp_REGROUPEMENT18 = Sql_RecBios
				End If
				If (instr(1,oTmp_REGROUPEMENT18,"PARAMETERS ",1)>0) AND (instr(1,oTmp_REGROUPEMENT18,"SELECT ",1)>0) Then
					oTmp_REGROUPEMENT18 = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_REGROUPEMENT18)
				End if
				Set RecBios = CreateObject("ADODB.Recordset")
				RecBios.CursorLocation = 3
				RecBios.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecBios.Open oTmp_REGROUPEMENT18, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "REGROUPEMENT18"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_REGROUPEMENT18
					End if
				End if
				If RecBios.State = 1 Then
					If Not RecBios.EOF Then
						onlCount_REGROUPEMENT18 = 1
						Do Until RecBios.EOF
							If Err<>0 then exit do
							On error resume next
							Err.clear

							'Récupération du modèle de l'UC
							otDesignation = RecBios("SMODEL").value
							'Récupération serial UC
							otSerialUC = RecBios("SSN").value
							'Récupère type OCS
							ottypeOCS = "," & lcase(RecBios("TYPEPOSTE").value) & ","
							Err.Clear
							RecBios.MoveNext
							onlCount_REGROUPEMENT18 = onlCount_REGROUPEMENT18 + 1 
						Loop
						err.Clear
						RecBios.close
					End If
				End If
				Set RecBios= Nothing
				err.Clear
				opIf = (instr(",docking station,notebook,peripheral chassis,portable,", ottypeOCS) > 0)
				If Err = 0 then
					If (opIf = True) then 
						'type poste = 437
						otTypePoste = "437"
					End If
				End If
				err.Clear
				opIf = (lcase(left(otComputerName,3)) = "srv")
				If Err = 0 then
					If (opIf = True) then 
						'type poste = 423
						otTypePoste = "423"
					End If
				End If
				oTemp = Sql_RecMemory
				'Synchronisation sur le regroupement supérieur
				oFilter_RRecMemory = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecMemory,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_RRecMemory = oFilter_RRecMemory & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").DataType
					oFilter_RRecMemory = oFilter_RRecMemory & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_RRecMemory)>0 Then
					oTmp_RRecMemory = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecMemory, "WHERE", "(" & oFilter_RRecMemory & ")","AND")
				Else
					oTmp_RRecMemory = Sql_RecMemory
				End If
				If (instr(1,oTmp_RRecMemory,"PARAMETERS ",1)>0) AND (instr(1,oTmp_RRecMemory,"SELECT ",1)>0) Then
					oTmp_RRecMemory = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_RRecMemory)
				End if
				Set RecMemory = CreateObject("PSDScriptTools.oclQueriesPAM")
				RecMemory.otConnectString = "Provider=MSDASQL.1;Password=sco;Persist Security Info=True;User ID=ocs;Data Source=OCS"
				If len(oTmp_RRecMemory) = 0 then
					oTmp_RRecMemory = Sql_RecMemory
				End If
				RecMemory.otSql = "GROUP GROUPFIELD hardware_id, CALCULATEFIELD Total FUNCTION SUM CAPTION Total, CALCULATEFIELD Label FUNCTION CONCAT CAPTION Label" & ";" & oTmp_RRecMemory
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "RRecMemory"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_RRecMemory
					End if
				End if
				If RecMemory.State = 1 Then
					If Not RecMemory.EOF Then
						onlCount_RRecMemory = 1
						Do Until RecMemory.EOF
							If Err<>0 then exit do
							On error resume next
							Err.clear

							'Récupération Mémoire (Mo)
							onlMemory = Nz(RecMemory("SUM_Total").value,0)
							'Récupération Mémoire (Composition)
							otMemory = Trim(Nz(RecMemory("CONCAT_Label").value,""))
							Err.Clear
							RecMemory.MoveNext
							onlCount_RRecMemory = onlCount_RRecMemory + 1 
						Loop
						err.Clear
						RecMemory.close
					End If
				End If
				Set RecMemory= Nothing
				oTemp = Sql_RecBoard
				'Synchronisation sur le regroupement supérieur
				oFilter_RRecBoard2 = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecBoard,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_RRecBoard2 = oFilter_RRecBoard2 & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_RRecBoard2 = oFilter_RRecBoard2 & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_RRecBoard2)>0 Then
					oTmp_RRecBoard2 = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecBoard, "WHERE", "(" & oFilter_RRecBoard2 & ")","AND")
				Else
					oTmp_RRecBoard2 = Sql_RecBoard
				End If
				If (instr(1,oTmp_RRecBoard2,"PARAMETERS ",1)>0) AND (instr(1,oTmp_RRecBoard2,"SELECT ",1)>0) Then
					oTmp_RRecBoard2 = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_RRecBoard2)
				End if
				Set RecBoard = CreateObject("ADODB.Recordset")
				RecBoard.CursorLocation = 3
				RecBoard.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecBoard.Open oTmp_RRecBoard2, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "RRecBoard2"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_RRecBoard2
					End if
				End if
				If RecBoard.State = 1 Then
					If Not RecBoard.EOF Then
						onlCount_RRecBoard2 = 1
						Do Until RecBoard.EOF
							If Err<>0 then exit do
							On error resume next
							Err.clear

							err.Clear
							opIf = (RecBoard("status").value = "Up")
							If Err = 0 then
								If (opIf = True) then 
									'Récupération mac adress
									otMacAdd = RecBoard("MACADDR").value
								End If
							End If
							Err.Clear
							RecBoard.MoveNext
							onlCount_RRecBoard2 = onlCount_RRecBoard2 + 1 
						Loop
						err.Clear
						RecBoard.close
					End If
				End If
				Set RecBoard= Nothing
				Err.Clear
				If Len(Key_OBJECTTRANS13)=0 Then
					Key_OBJECTTRANS13 = otGUID()
					Err.clear
					TRANSFORMATION1("Elements").Add Key_OBJECTTRANS13, CreateObject("Scripting.Dictionary")
					Set OBJECTTRANS13 = TRANSFORMATION1("Elements")(Key_OBJECTTRANS13)
					If Err=0 then 
						OBJECTTRANS13.Add "Type",301
						OBJECTTRANS13.Add "Key",Key_OBJECTTRANS13
						OBJECTTRANS13.Add "Name","OBJECTTRANS13"
						OBJECTTRANS13.Add "Action","1"
						OBJECTTRANS13.Add "Axis",""
						OBJECTTRANS13.Add "Identification","##common_otModel## AND ##common_oidObjectType##"
						OBJECTTRANS13.Add "Parent","Néant"
						TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
						OBJECTTRANS13.Add "Count",TRANSFORMATION1_Count
						Set oTemp = CreateObject("Scripting.Dictionary")
						OBJECTTRANS13.Add "Properties",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						OBJECTTRANS13.Add "Relations",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						OBJECTTRANS13.Add "OrderLocal",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						OBJECTTRANS13.Add "Elements",oTemp
						Set oTemp = nothing
					End If
				Else
					Set OBJECTTRANS13 = Nothing
					Set OBJECTTRANS13 = TRANSFORMATION1("Elements")(Key_OBJECTTRANS13)
				End If

				If Not OBJECTTRANS13 is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz("SDIS13", ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						OBJECTTRANS13("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Nom", 10, 255, 1, "")
					End if
				End If
				err.Clear
				Err.Clear
				If Len(Key_ObjPoste)=0 Then
					Key_ObjPoste = otGUID()
					Err.clear
					OBJECTTRANS13("Elements").Add Key_ObjPoste, CreateObject("Scripting.Dictionary")
					Set ObjPoste = OBJECTTRANS13("Elements")(Key_ObjPoste)
					If Err=0 then 
						ObjPoste.Add "Type",clng(otTypePoste)
						ObjPoste.Add "Key",Key_ObjPoste
						ObjPoste.Add "Name","ObjPoste"
						ObjPoste.Add "Action","0"
						ObjPoste.Add "Axis",""
						ObjPoste.Add "Identification","##private_otProperty1## AND ##common_oidObjectType##"
						ObjPoste.Add "Parent",OBJECTTRANS13("Key")
						TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
						ObjPoste.Add "Count",TRANSFORMATION1_Count
						Set oTemp = CreateObject("Scripting.Dictionary")
						ObjPoste.Add "Properties",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						ObjPoste.Add "Relations",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						ObjPoste.Add "OrderLocal",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						ObjPoste.Add "Elements",oTemp
						Set oTemp = nothing
					End If
				Else
					Set ObjPoste = Nothing
					Set ObjPoste = OBJECTTRANS13("Elements")(Key_ObjPoste)
				End If

				If Not ObjPoste is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(odDateInv, ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						ObjPoste("Properties").Add "C_odDateScan",Array("C_odDateScan", opProperty, "Inventorié(e)", 8, 8, 1, "")
					End if
				End If
				err.Clear
				If Not ObjPoste is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(otComputerName, ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					PosteNumero = opProperty
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						ObjPoste("Properties").Add "P_otProperty1",Array("P_otProperty1", opProperty, "PC name = computername", 10, 30, 1, "")
					End if
				End If
				err.Clear
				Err.Clear
				If Len(Key_UC)=0 Then
					Key_UC = otGUID()
					Err.clear
					ObjPoste("Elements").Add Key_UC, CreateObject("Scripting.Dictionary")
					Set UC = ObjPoste("Elements")(Key_UC)
					If Err=0 then 
						UC.Add "Type",416
						UC.Add "Key",Key_UC
						UC.Add "Name","UC"
						UC.Add "Action","0"
						UC.Add "Axis",""
						UC.Add "Identification","##private_otIOtype2## AND ##common_oidObjectType##"
						UC.Add "Parent",ObjPoste("Key")
						TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
						UC.Add "Count",TRANSFORMATION1_Count
						Set oTemp = CreateObject("Scripting.Dictionary")
						UC.Add "Properties",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						UC.Add "Relations",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						UC.Add "OrderLocal",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						UC.Add "Elements",oTemp
						Set oTemp = nothing
					End If
				Else
					Set UC = Nothing
					Set UC = ObjPoste("Elements")(Key_UC)
				End If

				If Not UC is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(otgetrezo(pytools.nz(RecPoste("ipaddr").value, "")), ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						UC("Properties").Add "P_otIoType1",Array("P_otIoType1", opProperty, "Réseau SDIS", 10, 25, 1, "")
					End if
				End If
				err.Clear
				If Not UC is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(cstr(pytools.nz(RecPoste("userID").value, "")) & "/" & cstr(pytools.nz(RecPoste("nom_reseau").value, "")) & "/" &  replace(left(odDateNow, 5) & mid(odDateNow, 9,2), "/", ""), ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						UC("Properties").Add "P_otProperty1",Array("P_otProperty1", opProperty, "Propriété 1 = userid + nom reseau", 10, 30, 1, "")
					End if
				End If
				err.Clear
				If Not UC is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(cstr(RecPoste("ID").value), ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						UC("Properties").Add "P_otHostId",Array("P_otHostId", opProperty, "Identifiant OCS", 10, 15, 1, "")
					End if
				End If
				err.Clear
				If Not UC is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(odDateInv, ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						UC("Properties").Add "C_odDateScan",Array("C_odDateScan", opProperty, "Inventorié(e)", 8, 8, 1, "")
					End if
				End If
				err.Clear
				If Not UC is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(otComputerName, ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						UC("Properties").Add "P_otIoType2",Array("P_otIoType2", opProperty, "UC name = computername", 10, 25, 1, "")
					End if
				End If
				err.Clear
				If Not UC is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(clng(Nz(RecPoste("MEMORY").value,"")), ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						UC("Properties").Add "P_oniTotalMemorySize",Array("P_oniTotalMemorySize", opProperty, "Taille mémoire RAM de l'UC", 3, 2, 1, "")
					End if
				End If
				err.Clear
				If Not UC is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(otMemory, ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						UC("Properties").Add "P_otMemoryLayout",Array("P_otMemoryLayout", opProperty, "Disposition physique de la mémoire RAM", 10, 50, 1, "")
					End if
				End If
				err.Clear
				If Not UC is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(otSerialUC, ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						UC("Properties").Add "P_otIdSms",Array("P_otIdSms", opProperty, "serial OCS", 10, 50, 1, "")
					End if
				End If
				err.Clear
				oTemp = Sql_RecRegistry
				'Synchronisation sur le regroupement supérieur
				oFilter_REGROUPEMENT16 = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecRegistry,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_REGROUPEMENT16 = oFilter_REGROUPEMENT16 & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_REGROUPEMENT16 = oFilter_REGROUPEMENT16 & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_REGROUPEMENT16)>0 Then
					oTmp_REGROUPEMENT16 = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecRegistry, "WHERE", "(" & oFilter_REGROUPEMENT16 & ")","AND")
				Else
					oTmp_REGROUPEMENT16 = Sql_RecRegistry
				End If
				If (instr(1,oTmp_REGROUPEMENT16,"PARAMETERS ",1)>0) AND (instr(1,oTmp_REGROUPEMENT16,"SELECT ",1)>0) Then
					oTmp_REGROUPEMENT16 = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_REGROUPEMENT16)
				End if
				Set RecRegistry = CreateObject("ADODB.Recordset")
				RecRegistry.CursorLocation = 3
				RecRegistry.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecRegistry.Open oTmp_REGROUPEMENT16, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "REGROUPEMENT16"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_REGROUPEMENT16
					End if
				End if
				If RecRegistry.State = 1 Then
					If Not RecRegistry.EOF Then
						onlCount_REGROUPEMENT16 = 1
						Do Until RecRegistry.EOF
							If Err<>0 then exit do
							On error resume next
							Key_OBJECTTRANS10 = ""
							Err.clear

							err.Clear
							opIf = (lcase(left(recregistry("NAME").value, 7)) = "version")
							If Err = 0 then
								If (opIf = True) then 
									Err.Clear
									If Len(Key_OBJECTTRANS10)=0 Then
										Key_OBJECTTRANS10 = otGUID()
										Err.clear
										UC("Elements").Add Key_OBJECTTRANS10, CreateObject("Scripting.Dictionary")
										Set OBJECTTRANS10 = UC("Elements")(Key_OBJECTTRANS10)
										If Err=0 then 
											OBJECTTRANS10.Add "Type",404
											OBJECTTRANS10.Add "Key",Key_OBJECTTRANS10
											OBJECTTRANS10.Add "Name","OBJECTTRANS10"
											OBJECTTRANS10.Add "Action","0"
											OBJECTTRANS10.Add "Axis",""
											OBJECTTRANS10.Add "Identification","##common_otModel## AND ##common_oidObjectType## AND ##common_oidObjectFather##"
											OBJECTTRANS10.Add "Parent",UC("Key")
											TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
											OBJECTTRANS10.Add "Count",TRANSFORMATION1_Count
											Set oTemp = CreateObject("Scripting.Dictionary")
											OBJECTTRANS10.Add "Properties",oTemp
											Set oTemp = nothing
											Set oTemp = CreateObject("Scripting.Dictionary")
											OBJECTTRANS10.Add "Relations",oTemp
											Set oTemp = nothing
											Set oTemp = CreateObject("Scripting.Dictionary")
											OBJECTTRANS10.Add "OrderLocal",oTemp
											Set oTemp = nothing
											Set oTemp = CreateObject("Scripting.Dictionary")
											OBJECTTRANS10.Add "Elements",oTemp
											Set oTemp = nothing
										End If
									Else
										Set OBJECTTRANS10 = Nothing
										Set OBJECTTRANS10 = UC("Elements")(Key_OBJECTTRANS10)
									End If

									If Not OBJECTTRANS10 is nothing then
										err.Clear
										oTemp = ""
										if err<>0 then oTemp=""
										err.Clear
										opProperty = ""
										opProperty = Trim(nz(odDateInv, ""))
										If Len(opProperty)=0 Then opProperty = oTemp
										Err.Clear
										oTemp = (Len(Trim(nz(opProperty,"")))>0)
										If (err = 0) And (oTemp = True) then
											OBJECTTRANS10("Properties").Add "C_odDateScan",Array("C_odDateScan", opProperty, "Inventorié(e)", 8, 8, 1, "")
										End if
									End If
									err.Clear
									If Not OBJECTTRANS10 is nothing then
										err.Clear
										oTemp = ""
										if err<>0 then oTemp=""
										err.Clear
										opProperty = ""
										opProperty = Trim(nz(recregistry("NAME").value & " " & recregistry("REGVALUE").value, ""))
										If Len(opProperty)=0 Then opProperty = oTemp
										Err.Clear
										oTemp = (Len(Trim(nz(opProperty,"")))>0)
										If (err = 0) And (oTemp = True) then
											OBJECTTRANS10("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Nom du logiciel", 10, 255, 1, "")
										End if
									End If
									err.Clear
									If Not OBJECTTRANS10 is nothing then
										err.Clear
										oTemp = ""
										if err<>0 then oTemp=""
										err.Clear
										opProperty = ""
										opProperty = Trim(nz("Registry", ""))
										If Len(opProperty)=0 Then opProperty = oTemp
										Err.Clear
										oTemp = (Len(Trim(nz(opProperty,"")))>0)
										If (err = 0) And (oTemp = True) then
											OBJECTTRANS10("Properties").Add "P_otProperty2",Array("P_otProperty2", opProperty, "Propriété 2 = registry", 10, 30, 1, "")
										End if
									End If
									err.Clear
									If Not OBJECTTRANS10 is nothing then
										err.Clear
										oTemp = ""
										if err<>0 then oTemp=""
										err.Clear
										opProperty = ""
										opProperty = Trim(nz("En service", ""))
										If Len(opProperty)=0 Then opProperty = oTemp
										Err.Clear
										oTemp = (Len(Trim(nz(opProperty,"")))>0)
										If (err = 0) And (oTemp = True) then
											OBJECTTRANS10("Properties").Add "C_otStatus",Array("C_otStatus", opProperty, "Etat", 10, 25, 1, "")
										End if
									End If
									err.Clear
								End If
							End If
							Err.Clear
							RecRegistry.MoveNext
							onlCount_REGROUPEMENT16 = onlCount_REGROUPEMENT16 + 1 
						Loop
						err.Clear
						RecRegistry.close
					End If
				End If
				Set RecRegistry= Nothing
				oTemp = Sql_RecBios
				'Synchronisation sur le regroupement supérieur
				oFilter_RRecBios = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecBios,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_RRecBios = oFilter_RRecBios & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_RRecBios = oFilter_RRecBios & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_RRecBios)>0 Then
					oTmp_RRecBios = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecBios, "WHERE", "(" & oFilter_RRecBios & ")","AND")
				Else
					oTmp_RRecBios = Sql_RecBios
				End If
				If (instr(1,oTmp_RRecBios,"PARAMETERS ",1)>0) AND (instr(1,oTmp_RRecBios,"SELECT ",1)>0) Then
					oTmp_RRecBios = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_RRecBios)
				End if
				Set RecBios = CreateObject("ADODB.Recordset")
				RecBios.CursorLocation = 3
				RecBios.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecBios.Open oTmp_RRecBios, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "RRecBios"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_RRecBios
					End if
				End if
				If RecBios.State = 1 Then
					If Not RecBios.EOF Then
						onlCount_RRecBios = 1
						Do Until RecBios.EOF
							If Err<>0 then exit do
							On error resume next
							Err.clear

							If Not UC is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecBios("BDATE").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Nz(RecBios("BDATE").value,"")<>"N/A")
								If (err = 0) And (oTemp = True) then
									UC("Properties").Add "P_odDateBios",Array("P_odDateBios", opProperty, "Date du bios", 8, 8, 1, "")
								End if
							End If
							err.Clear
							If Not UC is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(Trim(Nz(RecBios("BMANUFACTURER").value,"") & " " & Nz(RecBios("BVERSION").value,"")), ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									UC("Properties").Add "P_otBios",Array("P_otBios", opProperty, "Bios Installé", 10, 80, 1, "")
								End if
							End If
							err.Clear
							Err.Clear
							RecBios.MoveNext
							onlCount_RRecBios = onlCount_RRecBios + 1 
						Loop
						err.Clear
						RecBios.close
					End If
				End If
				Set RecBios= Nothing
				oTemp = Sql_RecHardDisk
				'Synchronisation sur le regroupement supérieur
				oFilter_RRecHardDisk = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecHardDisk,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_RRecHardDisk = oFilter_RRecHardDisk & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_RRecHardDisk = oFilter_RRecHardDisk & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_RRecHardDisk)>0 Then
					oTmp_RRecHardDisk = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecHardDisk, "WHERE", "(" & oFilter_RRecHardDisk & ")","AND")
				Else
					oTmp_RRecHardDisk = Sql_RecHardDisk
				End If
				If (instr(1,oTmp_RRecHardDisk,"PARAMETERS ",1)>0) AND (instr(1,oTmp_RRecHardDisk,"SELECT ",1)>0) Then
					oTmp_RRecHardDisk = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_RRecHardDisk)
				End if
				Set RecHardDisk = CreateObject("ADODB.Recordset")
				RecHardDisk.CursorLocation = 3
				RecHardDisk.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecHardDisk.Open oTmp_RRecHardDisk, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "RRecHardDisk"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_RRecHardDisk
					End if
				End if
				If RecHardDisk.State = 1 Then
					If Not RecHardDisk.EOF Then
						onlCount_RRecHardDisk = 1
						Do Until RecHardDisk.EOF
							If Err<>0 then exit do
							On error resume next
							Key_DisqueDur = ""
							Err.clear

							Err.Clear
							If Len(Key_DisqueDur)=0 Then
								Key_DisqueDur = otGUID()
								Err.clear
								UC("Elements").Add Key_DisqueDur, CreateObject("Scripting.Dictionary")
								Set DisqueDur = UC("Elements")(Key_DisqueDur)
								If Err=0 then 
									DisqueDur.Add "Type",418
									DisqueDur.Add "Key",Key_DisqueDur
									DisqueDur.Add "Name","DisqueDur"
									DisqueDur.Add "Action","0"
									DisqueDur.Add "Axis",""
									DisqueDur.Add "Identification","##common_otModel## AND ##common_otInternalNumber isnull## AND ##common_oidObjectType## AND ##common_oidObjectFather##"
									DisqueDur.Add "Parent",UC("Key")
									TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
									DisqueDur.Add "Count",TRANSFORMATION1_Count
									Set oTemp = CreateObject("Scripting.Dictionary")
									DisqueDur.Add "Properties",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									DisqueDur.Add "Relations",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									DisqueDur.Add "OrderLocal",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									DisqueDur.Add "Elements",oTemp
									Set oTemp = nothing
								End If
							Else
								Set DisqueDur = Nothing
								Set DisqueDur = UC("Elements")(Key_DisqueDur)
							End If

							If Not DisqueDur is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(odDateInv, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									DisqueDur("Properties").Add "C_odDateScan",Array("C_odDateScan", opProperty, "Inventorié(e)", 8, 8, 1, "")
								End if
							End If
							err.Clear
							If Not DisqueDur is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecHardDisk("DESCRIPTION").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									DisqueDur("Properties").Add "P_otDiskInterface",Array("P_otDiskInterface", opProperty, "Type d'interface", 10, 25, 1, "")
								End if
							End If
							err.Clear
							If Not DisqueDur is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecHardDisk("DISKSIZE").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									DisqueDur("Properties").Add "P_oniSize",Array("P_oniSize", opProperty, "Espace disque total", 4, 4, 1, "")
								End if
							End If
							err.Clear
							If Not DisqueDur is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(Nz(RecHardDisk("NAME").value, RecHardDisk("MODEL").value), ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									DisqueDur("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Désignation", 10, 255, 1, "")
								End if
							End If
							err.Clear
							If Not DisqueDur is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecHardDisk("MANUFACTURER").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									DisqueDur("Properties").Add "C_otManufactor",Array("C_otManufactor", opProperty, "Constructeur", 10, 50, 1, "")
								End if
							End If
							err.Clear
							If Not DisqueDur is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz("En service", ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									DisqueDur("Properties").Add "C_otStatus",Array("C_otStatus", opProperty, "Etat", 10, 25, 1, "")
								End if
							End If
							err.Clear
							Err.Clear
							RecHardDisk.MoveNext
							onlCount_RRecHardDisk = onlCount_RRecHardDisk + 1 
						Loop
						err.Clear
						RecHardDisk.close
					End If
				End If
				Set RecHardDisk= Nothing
				oTemp = Sql_RecSoft
				'Synchronisation sur le regroupement supérieur
				oFilter_RRecSoft = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecSoft,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_RRecSoft = oFilter_RRecSoft & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_RRecSoft = oFilter_RRecSoft & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_RRecSoft)>0 Then
					oTmp_RRecSoft = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecSoft, "WHERE", "(" & oFilter_RRecSoft & ")","AND")
				Else
					oTmp_RRecSoft = Sql_RecSoft
				End If
				If (instr(1,oTmp_RRecSoft,"PARAMETERS ",1)>0) AND (instr(1,oTmp_RRecSoft,"SELECT ",1)>0) Then
					oTmp_RRecSoft = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_RRecSoft)
				End if
				Set RecSoft = CreateObject("ADODB.Recordset")
				RecSoft.CursorLocation = 3
				RecSoft.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecSoft.Open oTmp_RRecSoft, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "RRecSoft"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_RRecSoft
					End if
				End if
				If RecSoft.State = 1 Then
					If Not RecSoft.EOF Then
						onlCount_RRecSoft = 1
						Do Until RecSoft.EOF
							If Err<>0 then exit do
							On error resume next
							Key_Software = ""
							Err.clear

							Err.Clear
							If Len(Key_Software)=0 Then
								Key_Software = otGUID()
								Err.clear
								UC("Elements").Add Key_Software, CreateObject("Scripting.Dictionary")
								Set Software = UC("Elements")(Key_Software)
								If Err=0 then 
									Software.Add "Type",404
									Software.Add "Key",Key_Software
									Software.Add "Name","Software"
									Software.Add "Action","0"
									Software.Add "Axis",""
									Software.Add "Identification","##Default##"
									Software.Add "Parent",UC("Key")
									TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
									Software.Add "Count",TRANSFORMATION1_Count
									Set oTemp = CreateObject("Scripting.Dictionary")
									Software.Add "Properties",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									Software.Add "Relations",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									Software.Add "OrderLocal",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									Software.Add "Elements",oTemp
									Set oTemp = nothing
								End If
							Else
								Set Software = Nothing
								Set Software = UC("Elements")(Key_Software)
							End If

							If Not Software is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz("En service", ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Software("Properties").Add "C_otStatus",Array("C_otStatus", opProperty, "Etat", 10, 25, 1, "")
								End if
							End If
							err.Clear
							If Not Software is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecSoft("PUBLISHER").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Nz(RecSoft("PUBLISHER").value, "") <> "")
								If (err = 0) And (oTemp = True) then
									Software("Properties").Add "C_otManufactor",Array("C_otManufactor", opProperty, "Editeur", 10, 50, 1, "")
								End if
							End If
							err.Clear
							If Not Software is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecSoft("NAME").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Software("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Nom du logiciel", 10, 255, 1, "")
								End if
							End If
							err.Clear
							If Not Software is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecSoft("VERSION").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Software("Properties").Add "P_otProperty1",Array("P_otProperty1", opProperty, "Propriété 1 = version", 10, 30, 1, "")
								End if
							End If
							err.Clear
							If Not Software is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecSoft("FOLDER").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Software("Properties").Add "P_otPath",Array("P_otPath", opProperty, "Chemin", 10, 125, 1, "")
								End if
							End If
							err.Clear
							If Not Software is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecSoft("COMMENTS").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Software("Properties").Add "C_ocMemo",Array("C_ocMemo", opProperty, "Remarques", 12, 0, 1, "")
								End if
							End If
							err.Clear
							If Not Software is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz("Flag pytheas", ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Software("Properties").Add "P_otProperty2",Array("P_otProperty2", opProperty, "Propriété 2 = flag pytheas", 10, 30, 1, "")
								End if
							End If
							err.Clear
							oTemp = (Instr(1, Nz(RecPoste("OSNAME").value,""), "Microsoft", 1) <> 0)
							If (err = 0) And (oTemp = False) then
								Exit do
							End If
							Err.Clear
							RecSoft.MoveNext
							onlCount_RRecSoft = onlCount_RRecSoft + 1 
						Loop
						err.Clear
						RecSoft.close
					End If
				End If
				Set RecSoft= Nothing
				oTemp = Sql_RecPartition
				'Synchronisation sur le regroupement supérieur
				oFilter_RRecPartition = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecPartition,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_RRecPartition = oFilter_RRecPartition & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_RRecPartition = oFilter_RRecPartition & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_RRecPartition)>0 Then
					oTmp_RRecPartition = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecPartition, "WHERE", "(" & oFilter_RRecPartition & ")","AND")
				Else
					oTmp_RRecPartition = Sql_RecPartition
				End If
				If (instr(1,oTmp_RRecPartition,"PARAMETERS ",1)>0) AND (instr(1,oTmp_RRecPartition,"SELECT ",1)>0) Then
					oTmp_RRecPartition = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_RRecPartition)
				End if
				Set RecPartition = CreateObject("ADODB.Recordset")
				RecPartition.CursorLocation = 3
				RecPartition.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecPartition.Open oTmp_RRecPartition, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "RRecPartition"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_RRecPartition
					End if
				End if
				If RecPartition.State = 1 Then
					If Not RecPartition.EOF Then
						onlCount_RRecPartition = 1
						Do Until RecPartition.EOF
							If Err<>0 then exit do
							On error resume next
							Key_Partition = ""
							Err.clear

							Err.Clear
							If Len(Key_Partition)=0 Then
								Key_Partition = otGUID()
								Err.clear
								UC("Elements").Add Key_Partition, CreateObject("Scripting.Dictionary")
								Set Partition = UC("Elements")(Key_Partition)
								If Err=0 then 
									Partition.Add "Type",414
									Partition.Add "Key",Key_Partition
									Partition.Add "Name","Partition"
									Partition.Add "Action","0"
									Partition.Add "Axis",""
									Partition.Add "Identification","##Default##"
									Partition.Add "Parent",UC("Key")
									TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
									Partition.Add "Count",TRANSFORMATION1_Count
									Set oTemp = CreateObject("Scripting.Dictionary")
									Partition.Add "Properties",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									Partition.Add "Relations",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									Partition.Add "OrderLocal",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									Partition.Add "Elements",oTemp
									Set oTemp = nothing
								End If
							Else
								Set Partition = Nothing
								Set Partition = UC("Elements")(Key_Partition)
							End If

							If Not Partition is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz("En service", ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Partition("Properties").Add "C_otStatus",Array("C_otStatus", opProperty, "Etat", 10, 25, 1, "")
								End if
							End If
							err.Clear
							If Not Partition is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(odDateInv, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Partition("Properties").Add "C_odDateScan",Array("C_odDateScan", opProperty, "Inventorié(e)", 8, 8, 1, "")
								End if
							End If
							err.Clear
							If Not Partition is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecPartition("FREE").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Instr(1, Nz(RecPartition("TYPE").value,""), "Network", 1)=0)
								If (err = 0) And (oTemp = True) then
									Partition("Properties").Add "P_onrFreeStorage",Array("P_onrFreeStorage", opProperty, "Espace partition libre", 6, 4, 0, "")
								End if
							End If
							err.Clear
							If Not Partition is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecPartition("TOTAL").value-RecPartition("FREE").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Instr(1, Nz(RecPartition("TYPE").value,""), "Network", 1)=0)
								If (err = 0) And (oTemp = True) then
									Partition("Properties").Add "P_onrUsedStorage",Array("P_onrUsedStorage", opProperty, "Espace partition utilisé", 6, 4, 0, "")
								End if
							End If
							err.Clear
							If Not Partition is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(round(RecPartition("FREE").value*100/RecPartition("TOTAL").value,2), ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Instr(1, Nz(RecPartition("TYPE").value,""), "Network", 1)=0)
								If (err = 0) And (oTemp = True) then
									Partition("Properties").Add "P_onrFreePercentage",Array("P_onrFreePercentage", opProperty, "% Libre", 6, 4, 0, "")
								End if
							End If
							err.Clear
							If Not Partition is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecPartition("TOTAL").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Instr(1, Nz(RecPartition("TYPE").value,""), "Network", 1)=0)
								If (err = 0) And (oTemp = True) then
									Partition("Properties").Add "P_onrStorageSize",Array("P_onrStorageSize", opProperty, "Espace partition total", 6, 4, 0, "")
								End if
							End If
							err.Clear
							If Not Partition is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecPartition("FILESYSTEM").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Partition("Properties").Add "P_otSystemFile",Array("P_otSystemFile", opProperty, "Système de fichier utilisé", 10, 25, 1, "")
								End if
							End If
							err.Clear
							If Not Partition is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(-1, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Instr(1, Nz(RecPartition("TYPE").value,""), "Network", 1)<>0)
								If (err = 0) And (oTemp = True) then
									Partition("Properties").Add "P_ovNetwork",Array("P_ovNetwork", opProperty, "Disque réseau", 1, 1, 1, "")
								End if
							End If
							err.Clear
							If Not Partition is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(Trim(Nz(RecPartition("LETTER").value,"") & " " & Nz(RecPartition("VOLUMN").value,"")), ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Partition("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Unité", 10, 255, 1, "")
								End if
							End If
							err.Clear
							Err.Clear
							RecPartition.MoveNext
							onlCount_RRecPartition = onlCount_RRecPartition + 1 
						Loop
						err.Clear
						RecPartition.close
					End If
				End If
				Set RecPartition= Nothing
				oTemp = Sql_RecProc
				'Synchronisation sur le regroupement supérieur
				oFilter_RRecProc = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecProc,True,DATASOURCE1.ConnectionString)("ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_RRecProc = oFilter_RRecProc & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_RRecProc = oFilter_RRecProc & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_RRecProc)>0 Then
					oTmp_RRecProc = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecProc, "WHERE", "(" & oFilter_RRecProc & ")","AND")
				Else
					oTmp_RRecProc = Sql_RecProc
				End If
				If (instr(1,oTmp_RRecProc,"PARAMETERS ",1)>0) AND (instr(1,oTmp_RRecProc,"SELECT ",1)>0) Then
					oTmp_RRecProc = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_RRecProc)
				End if
				Set RecProc = CreateObject("ADODB.Recordset")
				RecProc.CursorLocation = 3
				RecProc.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecProc.Open oTmp_RRecProc, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "RRecProc"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_RRecProc
					End if
				End if
				If RecProc.State = 1 Then
					If Not RecProc.EOF Then
						onlCount_RRecProc = 1
						Do Until RecProc.EOF
							If Err<>0 then exit do
							On error resume next
							Key_Processeur = ""
							Err.clear

							on error resume next
							For FOREACH1 = 1 To Nz(RecProc("nb").value,0) Step 1
								If err<>0 then exit for
								On error resume next
								Key_Processeur = ""
								Err.clear

								Err.Clear
								If Len(Key_Processeur)=0 Then
									Key_Processeur = otGUID()
									Err.clear
									UC("Elements").Add Key_Processeur, CreateObject("Scripting.Dictionary")
									Set Processeur = UC("Elements")(Key_Processeur)
									If Err=0 then 
										Processeur.Add "Type",412
										Processeur.Add "Key",Key_Processeur
										Processeur.Add "Name","Processeur"
										Processeur.Add "Action","0"
										Processeur.Add "Axis",""
										Processeur.Add "Identification","##Default##"
										Processeur.Add "Parent",UC("Key")
										TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
										Processeur.Add "Count",TRANSFORMATION1_Count
										Set oTemp = CreateObject("Scripting.Dictionary")
										Processeur.Add "Properties",oTemp
										Set oTemp = nothing
										Set oTemp = CreateObject("Scripting.Dictionary")
										Processeur.Add "Relations",oTemp
										Set oTemp = nothing
										Set oTemp = CreateObject("Scripting.Dictionary")
										Processeur.Add "OrderLocal",oTemp
										Set oTemp = nothing
										Set oTemp = CreateObject("Scripting.Dictionary")
										Processeur.Add "Elements",oTemp
										Set oTemp = nothing
									End If
								Else
									Set Processeur = Nothing
									Set Processeur = UC("Elements")(Key_Processeur)
								End If

								If Not Processeur is nothing then
									err.Clear
									oTemp = ""
									if err<>0 then oTemp=""
									err.Clear
									opProperty = ""
									opProperty = Trim(nz("En service", ""))
									If Len(opProperty)=0 Then opProperty = oTemp
									Err.Clear
									oTemp = (Len(Trim(nz(opProperty,"")))>0)
									If (err = 0) And (oTemp = True) then
										Processeur("Properties").Add "C_otStatus",Array("C_otStatus", opProperty, "Etat", 10, 25, 1, "")
									End if
								End If
								err.Clear
								If Not Processeur is nothing then
									err.Clear
									oTemp = ""
									if err<>0 then oTemp=""
									err.Clear
									opProperty = ""
									opProperty = Trim(nz(odDateInv, ""))
									If Len(opProperty)=0 Then opProperty = oTemp
									Err.Clear
									oTemp = (Len(Trim(nz(opProperty,"")))>0)
									If (err = 0) And (oTemp = True) then
										Processeur("Properties").Add "C_odDateScan",Array("C_odDateScan", opProperty, "Inventorié(e)", 8, 8, 1, "")
									End if
								End If
								err.Clear
								If Not Processeur is nothing then
									err.Clear
									oTemp = ""
									if err<>0 then oTemp=""
									err.Clear
									opProperty = ""
									opProperty = Trim(nz(Nz(RecProc("TYPE").value, "Unknow"), ""))
									If Len(opProperty)=0 Then opProperty = oTemp
									Err.Clear
									oTemp = (Len(Trim(nz(opProperty,"")))>0)
									If (err = 0) And (oTemp = True) then
										Processeur("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Désignation du processeur", 10, 255, 1, "")
									End if
								End If
								err.Clear
								If Not Processeur is nothing then
									err.Clear
									oTemp = ""
									if err<>0 then oTemp=""
									err.Clear
									opProperty = ""
									opProperty = Trim(nz(RecProc("FREQUENCY").value, ""))
									If Len(opProperty)=0 Then opProperty = oTemp
									Err.Clear
									oTemp = (Len(Trim(nz(opProperty,"")))>0)
									If (err = 0) And (oTemp = True) then
										Processeur("Properties").Add "P_oniPowerRating",Array("P_oniPowerRating", opProperty, "Indice de puissance", 3, 2, 1, "")
									End if
								End If
								err.Clear
								Err.clear
							Next
							Err.Clear
							RecProc.MoveNext
							onlCount_RRecProc = onlCount_RRecProc + 1 
						Loop
						err.Clear
						RecProc.close
					End If
				End If
				Set RecProc= Nothing
				oTemp = Sql_RecBoard
				'Synchronisation sur le regroupement supérieur
				oFilter_RRecBoard2 = ""
				otTemp2 = PYSCRIPTTOOLS.oSqlAnalyser.oAnalyseSelect(Sql_RecBoard,True,DATASOURCE1.ConnectionString)("HARDWARE_ID")(0)
				if (IsNull(RecPoste("ID").value)) then
					oFilter_RRecBoard2 = oFilter_RRecBoard2 & "(" & otTemp2 & " is null)"
				Else
					otTemp = 0
					otTemp = RecPoste("ID").Type
					oFilter_RRecBoard2 = oFilter_RRecBoard2 & otConvertADOToClause(otTemp, RecPoste("ID").value, 0,otTemp2,"=")
				End If
				If len(oFilter_RRecBoard2)>0 Then
					oTmp_RRecBoard2 = PYSCRIPTTOOLS.oSqlAnalyser.otAddElement(Sql_RecBoard, "WHERE", "(" & oFilter_RRecBoard2 & ")","AND")
				Else
					oTmp_RRecBoard2 = Sql_RecBoard
				End If
				If (instr(1,oTmp_RRecBoard2,"PARAMETERS ",1)>0) AND (instr(1,oTmp_RRecBoard2,"SELECT ",1)>0) Then
					oTmp_RRecBoard2 = PYSCRIPTTOOLS.oSqlAnalyser.otEvaluationParameter(oTmp_RRecBoard2)
				End if
				Set RecBoard = CreateObject("ADODB.Recordset")
				RecBoard.CursorLocation = 3
				RecBoard.CommandTimeout = DATASOURCE1.CommandTimeout
				Err.clear
				RecBoard.Open oTmp_RRecBoard2, DATASOURCE1,3,1
				If Err<>0 then 
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "RRecBoard2"
					if ovLog = -1 Then
						WriteLog optLastError & chr(9) & optLastErrorMessage & chr(9) & optLastNodeError & chr(9) & oTmp_RRecBoard2
					End if
				End if
				If RecBoard.State = 1 Then
					If Not RecBoard.EOF Then
						onlCount_RRecBoard2 = 1
						Do Until RecBoard.EOF
							If Err<>0 then exit do
							On error resume next
							Key_Network = ""
							Err.clear

							Err.Clear
							If Len(Key_Network)=0 Then
								Key_Network = otGUID()
								Err.clear
								UC("Elements").Add Key_Network, CreateObject("Scripting.Dictionary")
								Set Network = UC("Elements")(Key_Network)
								If Err=0 then 
									Network.Add "Type",409
									Network.Add "Key",Key_Network
									Network.Add "Name","Network"
									Network.Add "Action","0"
									Network.Add "Axis",""
									Network.Add "Identification","##Default##"
									Network.Add "Parent",UC("Key")
									TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
									Network.Add "Count",TRANSFORMATION1_Count
									Set oTemp = CreateObject("Scripting.Dictionary")
									Network.Add "Properties",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									Network.Add "Relations",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									Network.Add "OrderLocal",oTemp
									Set oTemp = nothing
									Set oTemp = CreateObject("Scripting.Dictionary")
									Network.Add "Elements",oTemp
									Set oTemp = nothing
								End If
							Else
								Set Network = Nothing
								Set Network = UC("Elements")(Key_Network)
							End If

							If Not Network is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz("En service", ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Network("Properties").Add "C_otStatus",Array("C_otStatus", opProperty, "Etat", 10, 25, 1, "")
								End if
							End If
							err.Clear
							If Not Network is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(odDateInv, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Network("Properties").Add "C_odDateScan",Array("C_odDateScan", opProperty, "Inventorié(e)", 8, 8, 1, "")
								End if
							End If
							err.Clear
							If Not Network is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecBoard("DESCRIPTION").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Network("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Désignation de la carte", 10, 255, 1, "")
								End if
							End If
							err.Clear
							If Not Network is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz(RecBoard("MACADDR").value, ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Network("Properties").Add "P_otMacAddress",Array("P_otMacAddress", opProperty, "Mac Adresse", 10, 18, 1, "")
								End if
							End If
							err.Clear
							If Not Network is nothing then
								err.Clear
								oTemp = ""
								if err<>0 then oTemp=""
								err.Clear
								opProperty = ""
								opProperty = Trim(nz("Réseau", ""))
								If Len(opProperty)=0 Then opProperty = oTemp
								Err.Clear
								oTemp = (Len(Trim(nz(opProperty,"")))>0)
								If (err = 0) And (oTemp = True) then
									Network("Properties").Add "P_otBoardType",Array("P_otBoardType", opProperty, "Type de carte", 10, 25, 1, "")
								End if
							End If
							err.Clear
							Err.Clear
							RecBoard.MoveNext
							onlCount_RRecBoard2 = onlCount_RRecBoard2 + 1 
						Loop
						err.Clear
						RecBoard.close
					End If
				End If
				Set RecBoard= Nothing
				err.Clear
				opIf = (pytools.nz(RecPoste("OSNAME").value, "") <> "")
				If Err = 0 then
					If (opIf = True) then 
						Err.Clear
						If Len(Key_OBJECTTRANS12)=0 Then
							Key_OBJECTTRANS12 = otGUID()
							Err.clear
							UC("Elements").Add Key_OBJECTTRANS12, CreateObject("Scripting.Dictionary")
							Set OBJECTTRANS12 = UC("Elements")(Key_OBJECTTRANS12)
							If Err=0 then 
								OBJECTTRANS12.Add "Type",404
								OBJECTTRANS12.Add "Key",Key_OBJECTTRANS12
								OBJECTTRANS12.Add "Name","OBJECTTRANS12"
								OBJECTTRANS12.Add "Action","0"
								OBJECTTRANS12.Add "Axis",""
								OBJECTTRANS12.Add "Identification","##Default##"
								OBJECTTRANS12.Add "Parent",UC("Key")
								TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
								OBJECTTRANS12.Add "Count",TRANSFORMATION1_Count
								Set oTemp = CreateObject("Scripting.Dictionary")
								OBJECTTRANS12.Add "Properties",oTemp
								Set oTemp = nothing
								Set oTemp = CreateObject("Scripting.Dictionary")
								OBJECTTRANS12.Add "Relations",oTemp
								Set oTemp = nothing
								Set oTemp = CreateObject("Scripting.Dictionary")
								OBJECTTRANS12.Add "OrderLocal",oTemp
								Set oTemp = nothing
								Set oTemp = CreateObject("Scripting.Dictionary")
								OBJECTTRANS12.Add "Elements",oTemp
								Set oTemp = nothing
							End If
						Else
							Set OBJECTTRANS12 = Nothing
							Set OBJECTTRANS12 = UC("Elements")(Key_OBJECTTRANS12)
						End If

						If Not OBJECTTRANS12 is nothing then
							err.Clear
							oTemp = ""
							if err<>0 then oTemp=""
							err.Clear
							opProperty = ""
							opProperty = Trim(nz("En service", ""))
							If Len(opProperty)=0 Then opProperty = oTemp
							Err.Clear
							oTemp = (Len(Trim(nz(opProperty,"")))>0)
							If (err = 0) And (oTemp = True) then
								OBJECTTRANS12("Properties").Add "C_otStatus",Array("C_otStatus", opProperty, "Etat", 10, 25, 1, "")
							End if
						End If
						err.Clear
						If Not OBJECTTRANS12 is nothing then
							err.Clear
							oTemp = ""
							if err<>0 then oTemp=""
							err.Clear
							opProperty = ""
							opProperty = Trim(nz(RecPoste("OSNAME").value, ""))
							If Len(opProperty)=0 Then opProperty = oTemp
							Err.Clear
							oTemp = (Len(Trim(nz(opProperty,"")))>0)
							If (err = 0) And (oTemp = True) then
								OBJECTTRANS12("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Nom du logiciel", 10, 255, 1, "")
							End if
						End If
						err.Clear
						If Not OBJECTTRANS12 is nothing then
							err.Clear
							oTemp = ""
							if err<>0 then oTemp=""
							err.Clear
							opProperty = ""
							opProperty = Trim(nz("OS", ""))
							If Len(opProperty)=0 Then opProperty = oTemp
							Err.Clear
							oTemp = (Len(Trim(nz(opProperty,"")))>0)
							If (err = 0) And (oTemp = True) then
								OBJECTTRANS12("Properties").Add "P_otProperty2",Array("P_otProperty2", opProperty, "Propriété 2 = OS", 10, 30, 1, "")
							End if
						End If
						err.Clear
					End If
				End If
				err.Clear
				opIf = (pytools.nz(RecPoste("OSCOMMENTS").value, "") <> "")
				If Err = 0 then
					If (opIf = True) then 
						Err.Clear
						If Len(Key_OBJECTTRANS14)=0 Then
							Key_OBJECTTRANS14 = otGUID()
							Err.clear
							UC("Elements").Add Key_OBJECTTRANS14, CreateObject("Scripting.Dictionary")
							Set OBJECTTRANS14 = UC("Elements")(Key_OBJECTTRANS14)
							If Err=0 then 
								OBJECTTRANS14.Add "Type",404
								OBJECTTRANS14.Add "Key",Key_OBJECTTRANS14
								OBJECTTRANS14.Add "Name","OBJECTTRANS14"
								OBJECTTRANS14.Add "Action","0"
								OBJECTTRANS14.Add "Axis",""
								OBJECTTRANS14.Add "Identification","##Default##"
								OBJECTTRANS14.Add "Parent",UC("Key")
								TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
								OBJECTTRANS14.Add "Count",TRANSFORMATION1_Count
								Set oTemp = CreateObject("Scripting.Dictionary")
								OBJECTTRANS14.Add "Properties",oTemp
								Set oTemp = nothing
								Set oTemp = CreateObject("Scripting.Dictionary")
								OBJECTTRANS14.Add "Relations",oTemp
								Set oTemp = nothing
								Set oTemp = CreateObject("Scripting.Dictionary")
								OBJECTTRANS14.Add "OrderLocal",oTemp
								Set oTemp = nothing
								Set oTemp = CreateObject("Scripting.Dictionary")
								OBJECTTRANS14.Add "Elements",oTemp
								Set oTemp = nothing
							End If
						Else
							Set OBJECTTRANS14 = Nothing
							Set OBJECTTRANS14 = UC("Elements")(Key_OBJECTTRANS14)
						End If

						If Not OBJECTTRANS14 is nothing then
							err.Clear
							oTemp = ""
							if err<>0 then oTemp=""
							err.Clear
							opProperty = ""
							opProperty = Trim(nz("En service", ""))
							If Len(opProperty)=0 Then opProperty = oTemp
							Err.Clear
							oTemp = (Len(Trim(nz(opProperty,"")))>0)
							If (err = 0) And (oTemp = True) then
								OBJECTTRANS14("Properties").Add "C_otStatus",Array("C_otStatus", opProperty, "Etat", 10, 25, 1, "")
							End if
						End If
						err.Clear
						If Not OBJECTTRANS14 is nothing then
							err.Clear
							oTemp = ""
							if err<>0 then oTemp=""
							err.Clear
							opProperty = ""
							opProperty = Trim(nz(RecPoste("OSCOMMENTS").value, ""))
							If Len(opProperty)=0 Then opProperty = oTemp
							Err.Clear
							oTemp = (Len(Trim(nz(opProperty,"")))>0)
							If (err = 0) And (oTemp = True) then
								OBJECTTRANS14("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Nom du logiciel", 10, 255, 1, "")
							End if
						End If
						err.Clear
						If Not OBJECTTRANS14 is nothing then
							err.Clear
							oTemp = ""
							if err<>0 then oTemp=""
							err.Clear
							opProperty = ""
							opProperty = Trim(nz("SP", ""))
							If Len(opProperty)=0 Then opProperty = oTemp
							Err.Clear
							oTemp = (Len(Trim(nz(opProperty,"")))>0)
							If (err = 0) And (oTemp = True) then
								OBJECTTRANS14("Properties").Add "P_otProperty2",Array("P_otProperty2", opProperty, "Propriété 2 = SP", 10, 30, 1, "")
							End if
						End If
						err.Clear
					End If
				End If
				Err.Clear
				If Len(Key_objRES)=0 Then
					Key_objRES = otGUID()
					Err.clear
					ObjPoste("Elements").Add Key_objRES, CreateObject("Scripting.Dictionary")
					Set objRES = ObjPoste("Elements")(Key_objRES)
					If Err=0 then 
						objRES.Add "Type",417
						objRES.Add "Key",Key_objRES
						objRES.Add "Name","objRES"
						objRES.Add "Action","0"
						objRES.Add "Axis",""
						objRES.Add "Identification","##Private_otComputerName## AND ##common_oidObjectType##"
						objRES.Add "Parent",ObjPoste("Key")
						TRANSFORMATION1_Count = TRANSFORMATION1_Count + 1
						objRES.Add "Count",TRANSFORMATION1_Count
						Set oTemp = CreateObject("Scripting.Dictionary")
						objRES.Add "Properties",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						objRES.Add "Relations",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						objRES.Add "OrderLocal",oTemp
						Set oTemp = nothing
						Set oTemp = CreateObject("Scripting.Dictionary")
						objRES.Add "Elements",oTemp
						Set oTemp = nothing
					End If
				Else
					Set objRES = Nothing
					Set objRES = ObjPoste("Elements")(Key_objRES)
				End If

				If Not objRES is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(otComputerName, ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						objRES("Properties").Add "P_otComputerName",Array("P_otComputerName", opProperty, "Nom d'ordinateur", 10, 25, 1, "")
					End if
				End If
				err.Clear
				If Not objRES is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz("Active", ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						objRES("Properties").Add "C_otStatus",Array("C_otStatus", opProperty, "Etat = Active", 10, 25, 1, "")
					End if
				End If
				err.Clear
				If Not objRES is nothing then
					err.Clear
					oTemp = ""
					if err<>0 then oTemp=""
					err.Clear
					opProperty = ""
					opProperty = Trim(nz(pytools.nz(RecPoste("ipaddr").value, ""), ""))
					If Len(opProperty)=0 Then opProperty = oTemp
					Err.Clear
					oTemp = (Len(Trim(nz(opProperty,"")))>0)
					If (err = 0) And (oTemp = True) then
						objRES("Properties").Add "C_otModel",Array("C_otModel", opProperty, "Nom = adr IP", 10, 255, 1, "")
					End if
				End If
				err.Clear
				TRANSFORMATION1_Code TRANSFORMATION1, ObjPoste,"D:\temp\txt",otComputerName,1,""
				Set ObjPoste = Nothing
				ObjPoste = Empty
				Key_ObjPoste = ""
				Err.Clear
				RecPoste.MoveNext
				onlCount_RRecPoste = onlCount_RRecPoste + 1 
			Loop
			call DeleteObjectsHistory(TRANSFORMATION1, ovLog, opvFormatLog)
			call UpdateObjectsHistory(TRANSFORMATION1, ovLog, opvFormatLog)
			call GenereObjectsHistory(TRANSFORMATION1, ovLog, opvFormatLog)
			call CreateObjectsHistory(TRANSFORMATION1, ovLog, opvFormatLog)
			err.Clear
			RecPoste.close
		End If
	End If
	Set RecPoste= Nothing
	'Ecrit date d'inventaire
	WriteDate odDateNow 
	err.Clear
	'Import des TXT
	err.Clear
	Set oParam_PAMTASK1 = CreateObject("Scripting.Dictionary")
	If (err = 0) Then 
		oParam_PAMTASK1.Add "otImpScanProfil","V4"
		oParam_PAMTASK1.Add "ovImpScanClearLog",True
		oParam_PAMTASK1.Add "ovImpModeIntegration",False
		oParam_PAMTASK1.Add "ovImpScanZip",False
		oParam_PAMTASK1.Add "otImpScanFilter","*.TXT"
		oParam_PAMTASK1.Add "ovImpScanForcingChange",False
		oParam_PAMTASK1.Add "ovImpScanHistoryPam",True
		oParam_PAMTASK1.Add "ovImpScanCreate",False
		oParam_PAMTASK1.Add "ovImpScanInventoryDate",False
		oParam_PAMTASK1.Add "ovImpScanIgnoreCreate",False
		oParam_PAMTASK1.Add "ovImpScanWithoutInternal",False
		oParam_PAMTASK1.Add "ovImpScanGlobalRelation",False
		oParam_PAMTASK1.Add "ovImpScanUpdateList",False
		oParam_PAMTASK1.Add "otImpScanDeleteObject","404"
		oParam_PAMTASK1.Add "ovImpScanPhysicalDelete",True
		oParam_PAMTASK1.Add "ovImpScanNoDelFinancial",True
		oParam_PAMTASK1.Add "otImpScanStatusDelete","Non trouvé"
		oParam_PAMTASK1.Add "ovImpScanSerial",False
		oParam_PAMTASK1.Add "ovImpScanImmo",False
		oParam_PAMTASK1.Add "ovImpUserMatricule",False
		oParam_PAMTASK1.Add "ovImpScanNoLoc",False
		oParam_PAMTASK1.Add "ovImpScanNoLocUser",False
		oParam_PAMTASK1.Add "ovImpOrgMat",True
		oParam_PAMTASK1.Add "ovImpLocMat",True
		oParam_PAMTASK1.Add "ovImpScanNoSupplier",False
		oParam_PAMTASK1.Add "ovImpScanArticle",False
		oParam_PAMTASK1.Add "ovImpScanHistory",False
		oParam_PAMTASK1.Add "ovImpScanWriteTable",False
		oParam_PAMTASK1.Add "ovImpScanSoftWithArticle",True
		oParam_PAMTASK1.Add "ovImpScanArticleSoft",False
		oParam_PAMTASK1.Add "ovImpScanCatalog",False
		oParam_PAMTASK1.Add "ovImpScanDelSoft",False
		oParam_PAMTASK1.Add "ovImpScanCreateSoft",True
		oParam_PAMTASK1.Add "otImpScanImportMode",Cint(0)
		oParam_PAMTASK1.Add "ovImpScanLicGroup",True
		oParam_PAMTASK1.Add "ovImpScanLicCreateLic",False
		oParam_PAMTASK1.Add "ovImpScanLicCreateArt",False
		oParam_PAMTASK1.Add "ovImpScanLicHistory",False
		oParam_PAMTASK1.Add "otImpScanLicDelay","0"
		oParam_PAMTASK1.Add "otImpScanLicExName","*(KB*,*HOTFIX*)"
	End If
	err.Clear
	oTemp = PYSCRIPTTOOLS.oExecutePamTask("PamScanImport", oParam_PAMTASK1,-1)
	PAMTASK1 = oTemp(0)
	opColLog.Add oTemp(0),Array("Import des TXT", "1", oTemp(2))

	If ovLog = -1 Then
		optBody=PYSCRIPTTOOLS.otReadFile(otFileLog)
		otTemp1=opColLog.Keys
		For onlTemp1 = 0 To opColLog.Count -1
			If Not IsEmpty(opColLog(otTemp1(onlTemp1))) = True Then
				otTemp2 = opColLog(otTemp1(onlTemp1))
				If StrComp(otTemp2(1),"1")=0 then
					optBody=optBody & iif(opvFormatLog=-1,otTemp2(0),"<BR><HR><BR><table width=""100%""><tr><td class=Titre>" & otTemp2(0) & "</td></tr></table>")  & PYSCRIPTTOOLS.otReadFile(otTemp2(2))
				Else
					optBody=optBody & iif(opvFormatLog=-1,otTemp2(0),"<BR><HR><BR><table width=""100%""><tr><td class=Titre>" & otTemp2(0) & "</td></tr></table>") & otTemp2(2)
				End if
			End if
		Next
		PYSCRIPTTOOLS.ovWriteFile otFileLog, optBody
		If (opvSendMail=-1) and (len(optRecipient)>0) Then
			'Envoi compte rendu par mail
			call PYSCRIPTTOOLS.PamSendMailHtml( "Pytheas Action Engine" , optRecipient, optSubject, optBody , PYPAM.ovLectureParameter("/MISC/MAIL/OTSERVERMAIL.value") , PYPAM.ovLectureParameter("/MISC/MAIL/ONLSERVERPORT.value") , "" , "" , "" )
		End if
	End if


'////////////////////////////////////////////////////////////
	objRES("Properties").RemoveAll
	objRES("Elements").RemoveAll
	objRES("Relations").RemoveAll
	objRES("OrderLocal").RemoveAll
	objRES.RemoveAll
	Set objRES = nothing
	OBJECTTRANS14("Properties").RemoveAll
	OBJECTTRANS14("Elements").RemoveAll
	OBJECTTRANS14("Relations").RemoveAll
	OBJECTTRANS14("OrderLocal").RemoveAll
	OBJECTTRANS14.RemoveAll
	Set OBJECTTRANS14 = nothing
	OBJECTTRANS12("Properties").RemoveAll
	OBJECTTRANS12("Elements").RemoveAll
	OBJECTTRANS12("Relations").RemoveAll
	OBJECTTRANS12("OrderLocal").RemoveAll
	OBJECTTRANS12.RemoveAll
	Set OBJECTTRANS12 = nothing
	Network("Properties").RemoveAll
	Network("Elements").RemoveAll
	Network("Relations").RemoveAll
	Network("OrderLocal").RemoveAll
	Network.RemoveAll
	Set Network = nothing
	Erase oGrp_RRecBoard2
	Processeur("Properties").RemoveAll
	Processeur("Elements").RemoveAll
	Processeur("Relations").RemoveAll
	Processeur("OrderLocal").RemoveAll
	Processeur.RemoveAll
	Set Processeur = nothing
	Erase oGrp_RRecProc
	Partition("Properties").RemoveAll
	Partition("Elements").RemoveAll
	Partition("Relations").RemoveAll
	Partition("OrderLocal").RemoveAll
	Partition.RemoveAll
	Set Partition = nothing
	Erase oGrp_RRecPartition
	Software("Properties").RemoveAll
	Software("Elements").RemoveAll
	Software("Relations").RemoveAll
	Software("OrderLocal").RemoveAll
	Software.RemoveAll
	Set Software = nothing
	Erase oGrp_RRecSoft
	DisqueDur("Properties").RemoveAll
	DisqueDur("Elements").RemoveAll
	DisqueDur("Relations").RemoveAll
	DisqueDur("OrderLocal").RemoveAll
	DisqueDur.RemoveAll
	Set DisqueDur = nothing
	Erase oGrp_RRecHardDisk
	Erase oGrp_RRecBios
	OBJECTTRANS10("Properties").RemoveAll
	OBJECTTRANS10("Elements").RemoveAll
	OBJECTTRANS10("Relations").RemoveAll
	OBJECTTRANS10("OrderLocal").RemoveAll
	OBJECTTRANS10.RemoveAll
	Set OBJECTTRANS10 = nothing
	Erase oGrp_REGROUPEMENT16
	UC("Properties").RemoveAll
	UC("Elements").RemoveAll
	UC("Relations").RemoveAll
	UC("OrderLocal").RemoveAll
	UC.RemoveAll
	Set UC = nothing
	ObjPoste("Properties").RemoveAll
	ObjPoste("Elements").RemoveAll
	ObjPoste("Relations").RemoveAll
	ObjPoste("OrderLocal").RemoveAll
	ObjPoste.RemoveAll
	Set ObjPoste = nothing
	OBJECTTRANS13("Properties").RemoveAll
	OBJECTTRANS13("Elements").RemoveAll
	OBJECTTRANS13("Relations").RemoveAll
	OBJECTTRANS13("OrderLocal").RemoveAll
	OBJECTTRANS13.RemoveAll
	Set OBJECTTRANS13 = nothing
	Erase oGrp_RRecMemory
	Erase oGrp_REGROUPEMENT18
	Erase oGrp_REGROUPEMENT13
	Erase oGrp_RRecPoste
	RecRegistry.Close
	Set RecRegistry = nothing
	RecTAG.Close
	Set RecTAG = nothing
	RecHardDisk.Close
	Set RecHardDisk = nothing
	RecBoard.Close
	Set RecBoard = nothing
	RecProc.Close
	Set RecProc = nothing
	RecMemory.Close
	Set RecMemory = nothing
	RecBios.Close
	Set RecBios = nothing
	RecPartition.Close
	Set RecPartition = nothing
	RecSoft.Close
	Set RecSoft = nothing
	RecPoste.Close
	Set RecPoste = nothing
	set DATASOURCE1 = nothing
	On error resume next
	'Positionnement de l'état de l'événement de la règle
	'0,Actif,1,Wait,2,Sleep,4,Terminate
	If PyEvent.onbStatus <> 2 Then
		PyEvent.onbStatus = 2
	End If
	'Restauration du mode de création des classes d'informations
	If not (PyObject is nothing) Then
		PyPam.Objects.Remove PyObject.otKey
	End If
	PyPam.Objects.ovModeStatic = SaveStateInfo

'////////////////////////////////////////////////////////////
	TRANSFORMATION1("Properties").RemoveAll
	TRANSFORMATION1("Elements").RemoveAll
	TRANSFORMATION1("Resumed").RemoveAll
	TRANSFORMATION1("OrderLocal").RemoveAll
	TRANSFORMATION1.RemoveAll
	Set TRANSFORMATION1 = nothing

'////////////////////////////////////////////////////////////
End Function

'////////////////////////////////////////////////////////////
'/////////////////////////////////////////////////////////////////////
'////////////////////////////////////////////////////////////
Function oConvertAdoVb(ByVal oType)
'##BD Converti un type ADO en Type VB
'##PD oType Type ado à convertir
	Select Case oType
		Case 7,133,135 'Date
			oConvertAdoVb = 7
		Case 2,3 'Integer
			oConvertAdoVb = 2
		Case 20 'Long
			oConvertAdoVb = 3
		Case 11 'Boolean
			oConvertAdoVb = 11
		Case 5 'Double
			oConvertAdoVb = 5
		Case 4 'Single
			oConvertAdoVb = 4
		Case 14 'Decimal
			oConvertAdoVb = 14
		Case 6 'Currency
			oConvertAdoVb = 6
		Case Else 'String
			oConvertAdoVb = vbString
	End Select
End Function

'////////////////////////////////////////////////////////////
Function IsFieldExist(ByVal oRecordset, Byval otField,Byval ovIsValue)  
'##BD Renvoi la valeur true si le champ existe dans le recordset
'##PD oRecordset enregistrement à analyser
'##PD otField nom du champ à rechercher
'##PD ovIsValue True iniduqe de tester la présence d'une valeur ou d'une chaine non vide
	Dim oni
	Dim oniStart
	Dim oniStop 

	On Error Resume Next
	
	IsFieldExist = False
	If typename(oRecordset)="oclQueriesPam" Then
		oniStart = 1
		oniStop = oRecordset.fields.count
	Else
		oniStart = 0
		oniStop = oRecordset.fields.count - 1
	End If
	For oni=oniStart To oniStop 
		If err<>0 Then Exit For
		If strcomp(oRecordset.fields(oni).name, otField, 1)=0 Then
			IsFieldExist = True
			If ovIsValue=True Then
				If (Not oRecordset.eof) And (Not oRecordset.bof) Then
					If isnull(oRecordset.fields(oni).value) Then
						IsFieldExist = False
					Else
						If len(trim(cstr(oRecordset.fields(oni).value)))=0 Then
							IsFieldExist = False
						End If
					End If
				End If    
			End If
			Exit For
		End If
		err.clear
	Next
	Err.Clear
End Function

'////////////////////////////////////////////////////////////
Function Nz(ByVal oValue,Byval oDefault)  
'##BD Renvoi la valeur oDfault si oValue est null
'##PD oValue Valeur à évaluer
'##PD oDefaut Valeur à renvoyer si null
	On Error Resume Next
	If isempty(oValue) Then
		Nz = oDefault
	Else
		If isnull(oValue) Then
			Nz = oDefault
		Else  
			Nz = oValue
		End If 
	End If
	Err.Clear
End Function

'////////////////////////////////////////////////////////////
Function otGUID()  
'##BD Génération d'un GUID
	Dim guid
	
	On Error Resume Next
	otGUID = ""
	If PYTOOLS Is nothing Then
		Set oGuid = CreateObject("scriptlet.typelib")
		If Not oGuid Is nothing Then
			otGUID = oGuid.guid
		End If
	Else
		otGUID = PYTOOLS.otGUID()
	End If	
	err.Clear
End Function

'////////////////////////////////////////////////////////////
Function Iif(ByVal oValue,Byval oTrue,Byval oFalse)
'##BD SImule l'instrcution IIF de VB
'##PD oValue valeur à évaluer
'##PD oTrue Caleur à évaluer si oValue est True
'##PD oFalse Caleur à évaluer si oValue est False	
	On Error Resume Next
	If oValue = True Then
		Iif = oTrue
	Else
		Iif = oFalse
	End If
	Err.Clear
End Function

'////////////////////////////////////////////////////////////
Function CreateLog()
'##BD Création du fichier de log
	On Error Resume Next
	Dim oFso
	Dim oFile

	Const ForReading = 1, ForWriting = 2, ForAppending = 8

	If ovLog = -1 Then
		Set oFso = CreateObject("Scripting.FileSystemObject")
		If ovCreateLog = -1 Then
			Set oFile = oFso.OpenTextFile(otFileLog, ForWriting, True)
		Else
			Set oFile = oFso.OpenTextFile(otFileLog, ForAppending, True)
		oFile.WriteBlankLines 1
		End If
		if opvFormatLog=-1 then
			oFile.WriteLine  date & " " & time & Chr(9) & otApplicationName
			oFile.WriteLine  date & " " & time & Chr(9) & otApplicationDescription
		else
			oFile.WriteLine "<table width=""100%""><tr><td class=Titre>Log de l'exécution</td></tr></table>"
			oFile.WriteLine "<P>" & date & "&nbsp" & time & "&nbsp" & otApplicationName & "</P>"
			oFile.WriteLine "<P>" & date & "&nbsp" & time & "&nbsp" & otApplicationDescription & "</P>"
		end if
		oFile.WriteBlankLines 1
		oFile.Close
		Set oFile = Nothing
		Set oFso = Nothing
	End If
	Err.Clear
End Function

'////////////////////////////////////////////////////////////
Function WriteLog(ByVal otValue)
'##BD Ecriture d'une ligne d'ans le fichier de log
'##PD otValue Ligne à écrire
	On Error Resume Next
	Dim oFso
	Dim oFile
	Const ForReading = 1, ForWriting = 2, ForAppending = 8
	If ovLog = -1 Then
		If ovTypeLog = -1 Then
			
			Set oFso = CreateObject("Scripting.FileSystemObject")
			Set oFile = oFso.OpenTextFile(otFileLog, ForAppending, True)
			if opvFormatLog=-1 then
				oFile.WriteLine date & " " & time & Chr(9) & otValue
			else
				oFile.WriteLine "<P>" & date & "&nbsp" & time & "&nbsp" & otValue & "</P>"	
			end if
			oFile.Close
			Set oFile = Nothing
			Set oFso = Nothing			
		Else
			wscript.echo Now & " " & Replace(otValue,Chr(9),Chr(47))
		End If
	End If
	Err.Clear
End Function

'////////////////////////////////////////////////////////////
Function oSelectCase(ByVal otListValue, ByVal otValue, ByVal otElse)
'##BD Recherche une valeur dans une liste"
'##PD otListValue Liste de référence. Les éléments sont séparés par une virgule. Un élément vide esy conisdéré comme valeur par défaut
' supporte les expressions régulière pour les chaines. opérateur à utiliser =
' supporte les comparaisons pour les valeurs numériques opérateur supporté =,>,<,>=,<=,<>
' supporte le maque binaire de Type And opérateur &
'##PD otValue Valeur à translater
'##PD otElse Valeur par défaut si aucune correspondance. Cette valeur est prioritaire sur la valeur vide de la chaîne

Dim oTemp
Dim oni
Dim otDefault
Dim oRegExp
Dim oCar
Dim oVal
Dim ov

On Error Resume Next

Set oRegExp = CreateObject("VBScript.RegExp")
oRegExp.IgnoreCase = True
oRegExp.Global = True
If Len(CStr(otElse)) Then
    'Utilisation de la valeur par défaut si elle est fournit
    oSelectCase = otElse
    otDefault = otElse
Else
    'La valeur de base est renvoyée si aucune correspondance
    oSelectCase = otValue
    otDefault = ""
End If
oTemp = Split(otListValue, ",") 
'cast de la virgule
For oni = 0 To UBound(oTemp)
    oTemp(oni) = Replace(oTemp(oni), "%2C", ",")

Next
If IsArray(oTemp) Then
    For oni = 0 To UBound(oTemp) Step 2
        'Vérification de la présence d'une chaine vide comme valeur par défaut
        If (Len(oTemp(oni)) = 0) And (Len(otDefault) = 0) Then
            otDefault = oTemp(oni + 1)
            'La valeur par défaut est renvoyée
            oSelectCase = otDefault
        End If
        'Recherche les caractères spéciaux
        oCar = Mid(oTemp(oni), 1, 1)
        If InStr("=<>&", oCar) > 0 Then
            Err.Clear
            ov = False
            If oCar = "=" Then
                If IsNumeric(otValue) Then
                    If CDbl(otValue) = CDbl(Trim(Mid(oTemp(oni), 2))) Then ov = True
                Else
                    'Utilisation des critères de chaine
                    oRegExp.Pattern = CStr(Trim(Mid(oTemp(oni), 2)))
                    If oRegExp.test(otValue) = True Then ov = True
                End If
            ElseIf oCar = "&" Then
                If IsNumeric(otValue) Then
                    oVal = CLng(Trim(Mid(oTemp(oni), 2)))
                    If (CLng(otValue) And oVal) = oVal Then ov = True
                End If
            ElseIf oCar = ">" Then
                If IsNumeric(otValue) Then
                    oCar = Mid(oTemp(oni), 2, 1)
                    If oCar = "=" Then
                        oVal = CDbl(Trim(Mid(oTemp(oni), 3)))
                        If CDbl(otValue) >= oVal Then ov = True
                    Else
                        oVal = CDbl(Trim(Mid(oTemp(oni), 2)))
                        If CDbl(otValue) > oVal Then ov = True
                    End If
                End If
            ElseIf oCar = "<" Then
                If IsNumeric(otValue) Then
                    oCar = Mid(oTemp(oni), 2, 1)
                    If oCar = "=" Then
                        oVal = CDbl(Trim(Mid(oTemp(oni), 3)))
                        If CDbl(otValue) <= oVal Then ov = True
                    Else
                        If oCar = ">" Then
                            oVal = CDbl(Trim(Mid(oTemp(oni), 3)))
                            If CDbl(otValue) <> oVal Then ov = True
                        Else
                            oVal = CDbl(Trim(Mid(oTemp(oni), 2)))
                            If CDbl(otValue) < oVal Then ov = True
                        End If
                    End If
                End If
            End If
            If Err = 0 Then
                If ov = True Then
                    oSelectCase = oTemp(oni + 1)
                    Exit For
                End If
            End If
        Else
            If StrComp(CStr(oTemp(oni)), CStr(otValue), 1) = 0 Then
                'Renvoi de la valeur correspondante
                oSelectCase = oTemp(oni + 1)
                Exit For
            End If
        End If
    Next
End If
Set oRegExp = Nothing
Err.Clear

End Function

'////////////////////////////////////////////////////////////
Function otObjectPAMExist(byval otWhere)
'Vérifie si un objet existe dans PAM
Dim otSql
Dim otTemp
Dim oRec
Dim otRet

On Error Resume Next

onlObjectPAMExist = 0
If PYPAM Is nothing Then Exit Function  
If len(otWhere)=0 Then Exit Function

otSql = "SELECT oidobject FROM otObject WHERE otinternalnumber='" & otlogin & "'"
Set oRec = PYPAM.executeSql(otSql)
If Not oRec.EOF Then
	otRet = oRec.GetString(2, , ",", ",")
	If right(otsql,1) = "," Then otRet = left(otsql,len(otsql)-1)
End If
Set oRec = nothing
otObjectPAMExist=otRet
err.Clear

End Function


Function PAMCreateEventEx(ByVal otAction, ByVal odDateRef, ByVal oParameters,Byval otTime)
'##BD Permet la création d'évènement avec paramètres pour un objet PAM donné
'##PD otAction Nom de l'action associé à l'évènement
'##PD odDateRef Date de référence
'##PD otTime Durées en minutes séparées par une virgule
'##PD onlTypeEvent Type d'évènements à créer toujours = 1 alarme
'##PD oParameters Liste de paramètres tableau avec nom, valeur,nom , valeur
'##RD Renvoi le nombre d'évènements créés
Dim oAction				'Action
Dim oEvent              'Evènement
Dim oIntervals         	'Intervalles de temps
Dim oInterval         	'Intervalle de temps
Dim oParameter			'Paramètre
Dim oActionParameter	'Paramètre
Dim oParameterEvent
Dim oArray
Dim odDateStart			'Date de prochaine exécution de l'évènement
Dim onlNbEvents			'Nombre d'évènements créés
Dim otDefault                
Dim onl

	'Valeur des états des actions
	'    ActifEvent = 0
	'    WaitEvent = 1
	'    SleepEvent = 2
	'    RunEvent = 3
	'    TerminateEvent = 4
	'    StartEvent = 5
	'    ErrorEvent = 6
	'    DebugEvent = 7
	'    CreateEvent = 8
	
	'Valeur des types d'évenements
	'    EventAlarmAnyObject = 0
	'    EventAlarmOneObject = 1
	'    EventImpression = 2
	'    EventMail = 3
	'    EventNone = 4
	
	On Error Resume Next
	
	If otTime = "0"  Then
		otTime = "1"
	End If	
	
	onlTypeEvent = 1
	
	onlNbEvents = 0
	PAMCreateEventEx = onlNbEvents
	
	If (Len(Trim(otAction)) = 0) Or (Not IsNumeric(onlTypeEvent)) Then
		Err.clear
		Exit Function
	End If
	 
	Err.clear
	Set oAction = PYPAM.Script.Actions(Cstr(otAction))
	If (Err=0) And (Not (oAction Is Nothing)) Then
	    'Création des événements en fonction des intervalles
	    oIntervals = Split(otTime, ",")
	    For Each oInterval In oIntervals
	        oInterval = Trim(oInterval)
	        If Len(oInterval) > 0 Then
		        'Création de l'événement en mode createevent
		        Err.clear                                                                        
		        
		        If UBound(oParameters) >= 1 Then
		        	oidObject = clng(oParameters(1))  
		        Else
		        	oidObject = 0	
		        End If
		        
		        Set oEvent = PYPAM.Script.Events.oCreateEvent(0, Clng(oAction.oidAction), 8, Clng(oidObject), Clng(onlTypeEvent), 0, "", 0)
		        If (Err=0) And (Not (oEvent Is Nothing)) Then
		        	'Ajout des paramètres à l'évènement à partir des paramètres de l'action
		        	For Each oActionParameter In oAction.Parameters
						If Not (oActionParameter Is nothing) Then
							'Valeur par défaut du paramètre
							otDefault = oActionParameter.otDefault
							'Vérification des paramètres à mettre à jour
							For onl = 0 To UBound(oParameters) Step 2
								'Seulement les paramètres valides
								If strcomp(oActionParameter.otParameter, Trim(oParameters(onl)), 1) = 0 Then
									'Mise à jour de la valeur par défaut du paramètre
									otDefault = Trim(oParameters(onl + 1))
									Exit For
								End If
							Next
							'Création du paramètre
							Set oParameterEvent = oEvent.oCreateParameter(oActionParameter.otParameter, _
										oActionParameter.onlType, _
										otDefault, _
										oActionParameter.otAlias, _
										oActionParameter.onbOrder)
						End If
		        	Next
			        oEvent.odUpdateDateDue odDateRef
			        oEvent.onbStatus = 1
		        	onlNbEvents = onlNbEvents + 1
				End If
	    	End If
	    Next             
	End If

	Err.clear
	PAMCreateEventEx = onlNbEvents
End Function



'/////////////////////////////////////////////////////////////////////
'Partie du code étendu pour les passerelles 

'////////////////////////////////////////////////////////////
Function ovIsUTF8(otInput) 
'##BD Détermine si une chaîne de caractères est encodée en UTF-8
'##PD otInput Chaîne de caractères à vérifier
'##RD Retourne True si la  chaîne de caractères est encodée en UTF-8
Dim onlAsc0, onlAsc1, onlAsc2, onlAsc3 
Dim onl 
     
    ovIsUTF8 = True 
    onl = 1 
    Do While onl <= Len(otInput) 
        onlAsc0 = Asc(Mid(otInput, onl, 1)) 
        If onl <= Len(otInput) - 1 Then 
            onlAsc1 = Asc(Mid(otInput, onl + 1, 1)) 
        Else 
            onlAsc1 = 0 
        End If 
        If onl <= Len(otInput) - 2 Then 
            onlAsc2 = Asc(Mid(otInput, onl + 2, 1)) 
        Else 
            onlAsc2 = 0 
        End If 
        If onl <= Len(otInput) - 3 Then 
            onlAsc3 = Asc(Mid(otInput, onl + 3, 1)) 
        Else 
            onlAsc3 = 0 
        End If 
         
        If (onlAsc0 And 240) = 240 Then 
            If (onlAsc1 And 128) = 128 And (onlAsc2 And 128) = 128 And (onlAsc3 And 128) = 128 Then 
                onl = onl + 4 
            Else 
                ovIsUTF8 = False 
                Exit Function 
            End If 
        ElseIf (onlAsc0 And 224) = 224 Then 
            If (onlAsc1 And 128) = 128 And (onlAsc2 And 128) = 128 Then 
                onl = onl + 3 
            Else 
                ovIsUTF8 = False 
                Exit Function 
            End If 
        ElseIf (onlAsc0 And 192) = 192 Then 
            If (onlAsc1 And 128) = 128 Then 
                onl = onl + 2 
            Else 
                ovIsUTF8 = False 
                Exit Function 
            End If 
        ElseIf (onlAsc0 And 128) = 0 Then 
            onl = onl + 1 
        Else 
            ovIsUTF8 = False 
            Exit Function 
        End If 
    Loop 
End Function 

'////////////////////////////////////////////////////////////
Function otEncodeUTF8(otInputString) 
'##BD Encode une chaîne de caractères en UTF-8
'##PD otInputString Chaîne de caractères à encoder
'##RD Retourne la chaîne de caractères encodée en UTF-8
Dim onlAsc 
Dim onl 
Dim otResult 
     
    otResult = "" 
    For onl = 1 To Len(otInputString)
        onlAsc = AscW(Mid(otInputString, onl, 1)) 
        If onlAsc < 128 Then 
            otResult = otResult + Chr(onlAsc) 
        ElseIf ((onlAsc >= 128) And (onlAsc < 2048)) Then 
            otResult = otResult + Chr(((onlAsc \ 64) Or 192)) 
            otResult = otResult + Chr(((onlAsc And 63) Or 128)) 
        ElseIf ((onlAsc >= 2048) And (onlAsc < 65536)) Then 
            otResult = otResult + Chr(((onlAsc \ 4096) Or 224)) 
            otResult = otResult + Chr((((onlAsc \ 64) And 63) Or 128)) 
            otResult = otResult + Chr(((onlAsc And 63) Or 128)) 
        Else ' onlAsc >= 65536 
            otResult = otResult + Chr(((onlAsc \ 262144) Or 240)) 
            otResult = otResult + Chr(((((onlAsc \ 4096) And 63)) Or 128)) 
            otResult = otResult + Chr((((onlAsc \ 64) And 63) Or 128)) 
            otResult = otResult + Chr(((onlAsc And 63) Or 128)) 
        End If 
	Next
    otEncodeUTF8 = otResult 
End Function 

'////////////////////////////////////////////////////////////
Function otDecodeUTF8(otInputUTF8) 
'##BD Décode  une chaîne de caractères UTF-8
'##PD otInputUTF8 Chaîne de caractères UTF-8 à décoder
'##RD Retourne la chaîne de caractères décodée
Dim onlAsc0, onlAsc1, onlAsc2, onlAsc3 
Dim onl 
Dim otResult 
     
    If ovIsUTF8(otInputUTF8) = False Then 
        otDecodeUTF8 = otInputUTF8 
        Exit Function 
    End If 
     
    otResult = "" 
    onl = 1 
    Do While onl <= Len(otInputUTF8) 
        onlAsc0 = Asc(Mid(otInputUTF8, onl, 1)) 
        If onl <= Len(otInputUTF8) - 1 Then 
            onlAsc1 = Asc(Mid(otInputUTF8, onl + 1, 1)) 
        Else 
            onlAsc1 = 0 
        End If 
        If onl <= Len(otInputUTF8) - 2 Then 
            onlAsc2 = Asc(Mid(otInputUTF8, onl + 2, 1)) 
        Else 
            onlAsc2 = 0 
        End If 
        If onl <= Len(otInputUTF8) - 3 Then 
            onlAsc3 = Asc(Mid(otInputUTF8, onl + 3, 1)) 
        Else 
            onlAsc3 = 0 
        End If 
         
        If (onlAsc0 And 240) = 240 And (onlAsc1 And 128) = 128 And (onlAsc2 And 128) = 128 And (onlAsc3 And 128) = 128 Then 
            otResult = otResult + ChrW((onlAsc0 - 240) * 65536 + (onlAsc1 - 128) * 4096) + (onlAsc2 - 128) * 64 + (onlAsc3 - 128) 
            onl = onl + 4 
        ElseIf (onlAsc0 And 224) = 224 And (onlAsc1 And 128) = 128 And (onlAsc2 And 128) = 128 Then 
            otResult = otResult + ChrW((onlAsc0 - 224) * 4096 + (onlAsc1 - 128) * 64 + (onlAsc2 - 128)) 
            onl = onl + 3 
        ElseIf (onlAsc0 And 192) = 192 And (onlAsc1 And 128) = 128 Then 
            otResult = otResult + ChrW((onlAsc0 - 192) * 64 + (onlAsc1 - 128)) 
            onl = onl + 2 
        ElseIf (onlAsc0 And 128) = 128 Then 
            otResult = otResult + ChrW(onlAsc0 And 127) 
            onl = onl + 1 
        Else ' onlAsc0 < 128 
            otResult = otResult + ChrW(onlAsc0) 
            onl = onl + 1 
        End If 
    Loop 
 
    otDecodeUTF8 = otResult 
End Function 

'////////////////////////////////////////////////////////////
Function CreateDirectory(ByVal otDirectory)
'##BD Création d'un répertoire avec l'arborescence complète
'##PD otDirectory Chemin complet vers le répertoire à créer
'##RD Renvoi le chemin vers le répertoire à créer, sinon une chaîne vide en cas d'erreurs
Dim oFso
Dim ov
Dim oni
Dim otDir
Dim otRoot
Dim otTemp

	On Error Resume Next
	
	CreateDirectory = ""

	otDir = Trim(otDirectory)
	If len(otDir) = 0 Then Exit Function
	
	err.clear
	Set oFso = CreateObject("Scripting.FileSystemObject")
	If err<>0 Then Exit Function

	CreateDirectory = otDir

	'Vérification de la présence du répertoire
	If oFso.FolderExists(otDir) Then
		'Répertoire existant.
		Set oFso = Nothing
		Exit Function
	End If

	oni = instr(1,otDir, "\\")
	If oni=1 Then
		oni = instr(3,otDir, "\")
		If oni>0 Then
			otRoot = left(otDir, oni-1)
			otDir = mid(otDir, oni+1)
		Else
			otRoot = otDir
			otDir = ""
		End If
	Else
		oni = instr(otDir, "\")
		If oni>0 Then
			otRoot = left(otDir, oni-1)
			otDir = mid(otDir, oni+1)
		Else
			otRoot = otDir
			otDir = ""
		End If
	End If
	
	If len(otDir)>0 Then
		otTemp = otRoot
		'Vérification de la présence des répertoires, sinon création de ce dernier
		For Each ov In split(otDir, "\")
			otTemp = otTemp & "\" & ov
			If Not oFso.FolderExists(otTemp) Then
				oFso.CreateFolder otTemp
				'Si on ne peut pas créer ce répertoire, rien ne sert de continuer
				If Err<>0 Then
					Set oFso = nothing
					CreateDirectory = ""
					Exit Function
				End If
			End If
		Next
	End If
	
	Set oFso = nothing
	Err.Clear
End Function

'////////////////////////////////////////////////////////////
Function CreateTemplateRoot(ByVal otRoot)
'##BD Création de la structure complète des répertoires pour le modèle
'##PD otRoot Répertoire de base pour la création des répertoirs de travail
'##RD Renvoi le chemin vers le répertoire de base, sinon une chaîne vide en cas d'erreurs
On Error Resume Next
	
	CreateTemplateRoot = ""
	
	If IsEmpty(otRoot) Then Exit Function
	
	otRoot = Trim(otRoot)
	If Len(CreateDirectory(otRoot)) = 0 Then Exit Function
	
	'Création de la structure complète des répertoires 
	'Informations
	If Len(CreateDirectory(otRoot & "\Inf"))=0 Then Exit Function
	'Log
	If Len(CreateDirectory(otRoot & "\Log"))=0 Then Exit Function
	'Txt
	If Len(CreateDirectory(otRoot & "\Txt"))=0 Then Exit Function
	'Fichiers en erreur
	If Len(CreateDirectory(otRoot & "\Txt\Error"))=0 Then Exit Function
	'Fichiers valides
	If Len(CreateDirectory(otRoot & "\Txt\Valide"))=0 Then Exit Function
	'Fichiers temporaires pour la gestion de l historique
	If Len(CreateDirectory(otRoot & "\Txt\Temp"))=0 Then Exit Function
	
	'Ok
	CreateTemplateRoot = otRoot
	Err.Clear
End Function

'////////////////////////////////////////////////////////////
Function otEscapeFileName(Byval otFileName)
'##BD Traitement des caractères spéciaux interdits pour le nom d'un fichier
'##PD otFileName Nom du fichier
'##RD Renvoi le nom du fichier sans les caractères spéciaux interdits
On Error Resume Next

    otFileName = replace(otFileName,":","_")
    otFileName = replace(otFileName,";","_")
    otFileName = replace(otFileName,"?","_")
    otFileName = replace(otFileName,"\","_")
    otFileName = replace(otFileName,"/","_")
    otFileName = replace(otFileName,"*","_")
 	otFileName = replace(otFileName,"<","_")
 	otFileName = replace(otFileName,">","_")
  	otFileName = replace(otFileName,"""","_")
  	
  	otEscapeFileName = otFileName
  	Err.clear
End Function

'////////////////////////////////////////////////////////////
Function OpenHistory(ByVal otBaseDirectory)
'##BD Ouverture de la gestion de l'historique
'##PD otBaseDirectory Répertoire où se trouve la base de données : History.mdb
'##RD Renvoi False en cas d'erreurs
Dim otConn
Dim oFso
Dim otHistoryPath
Dim otHistoryName
Dim otTemp
Dim oRec
Dim otSql

	On Error Resume Next   
	
	Set HistoryData = Nothing
	OpenHistory = False

	otBaseDirectory = Trim(otBaseDirectory)
	If Len(otBaseDirectory) = 0 Then Exit Function

	otHistoryName = "History.mdb" 
	otHistoryPath = otBaseDirectory & "\" & otHistoryName
	Set oFso = CreateObject("Scripting.FileSystemObject")
	If Err<>0 Then
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "OpenHistory"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
		Exit Function
	End If
	'Vérification de la présence de la base de données
	If IsEmpty(PYPAM) Then Set PYPAM = Nothing
	If oFso.FileExists(otHistoryPath) = False And Not (PYPAM Is Nothing) Then
		'Tentative de récupérer le fichier dans le répertoire Pam3Scan
		otTemp = ""
		otTemp = PYPAM.ovLectureParameter("InstallRoot")
		Err.clear
		If Len(otTemp) = 0 Then
			optLastError = "Impossible de déterminer le répertoire d'installation (InstallRoot)"
			optLastErrorMessage = ""
			optLastNodeError = "OpenHistory"
			If ovLog = -1 Then
				WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
			End If
			Exit Function
		End If
		If oFso.FileExists(otTemp & "\Scan\" & otHistoryName) = False Then
			optLastError = "Impossible de récupérer automatiquement le fichier " & otHistoryName
			optLastErrorMessage = ""
			optLastNodeError = "OpenHistory"
			If ovLog = -1 Then
				WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
			End If
			Exit Function
		End If
		oFso.MoveFile otTemp & "\Scan\" & otHistoryName, otHistoryPath 
	End If
	'Vérification de la présence de la base de données
	If oFso.FileExists(otHistoryPath) = False Then
		optLastError = "Le fichier " & otHistoryPath & " n'existe pas"
		optLastErrorMessage = ""
		optLastNodeError = "OpenHistory"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
		Exit Function
	End If
	
	'Connexion sur la base historique
	otConn = "Provider=Microsoft.Jet.OLEDB.4.0;"
	otConn = otConn & "Data Source=" & otHistoryPath & ";"
	otConn = otConn & "Persist Security Info=True;"
	Set HistoryData = CreateObject("ADODB.Connection")
	HistoryData.CommandTimeout = 30
	HistoryData.Open otConn
	If Err<>0 Then 
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "OpenHistory"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
	Else
		'Chargement des paramètres globaux (History)
		LoadGlobalHistory
		
		'Ajout de certains paramètres
		If TypeName(History)="Dictionary" Then		
			History("HistoryDirectory") = otBaseDirectory
			History("HistoryName") = otHistoryName
			History("HistoryPath") = otHistoryPath
		End If
			
		'Création d'une nouvelle session (HistorySession)
		NewSessionHistory
	
		'Ok
		OpenHistory = True
	End If

	Err.clear
End Function

'////////////////////////////////////////////////////////////
Sub CloseHistory()
'##BD Fermeture de la gestion de l'historique
Dim otSql
Dim oRec
Dim oKeys
Dim otKey
Dim otValue

	On Error Resume Next
	
    If TypeName(HistoryData)<>"Connection" Then Exit Sub
	
	'Sauvegarde des paramètres globaux (History)
	SaveGlobalHistory
		
	'Sauvegarde des paramètres de session (HistorySession)
	SaveSessionHistory

	'Libération
	If Not (HistoryData Is Nothing) Then
		HistoryData.Close
	End If
	Set HistoryData = Nothing
	
	Err.clear
End Sub

'////////////////////////////////////////////////////////////
Function GetLastHistoryDate()
'##BD Récupération de la date de dernière exécution du modèle
'##RD Renvoi la date de dernière exécution ou 01/01/1980.
Dim oRec
Dim otSql

	On Error Resume Next   
	
    GetLastHistoryDate = CDate("1/1/1980 00:00:00")
    
    If TypeName(HistoryData)<>"Connection" Then Exit Function
	If (HistoryData Is Nothing) Then Exit Function

	'Chargement des paramètres globaux
	otSql = "SELECT TOP 1 * FROM otSession ORDER BY odDate DESC"
	Set oRec = HistoryData.Execute(otSql)
	If Err<>0 Then
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "GetLastHistoryDate"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
		Exit Function
	End If
	If Not (oRec.EOF) Then
		If Len(Nz(oRec("odDate").Value, "")) > 0 Then
			'Récupération de la dernière date d'exécution
			GetLastHistoryDate = oRec("odDate").Value 
		End If
	End If
	If Not (oRec Is nothing) Then
		oRec.Close
		Set oRec = nothing
	End If

	Err.clear
End Function

'////////////////////////////////////////////////////////////
Sub LoadGlobalHistory()
'##BD Chargement des paramètres globaux dans le dictionnaire History
Dim oRec
Dim otSql

	On Error Resume Next   
	
    If TypeName(HistoryData)<>"Connection" Then Exit Sub
	If (HistoryData Is Nothing) Then Exit Sub
    If TypeName(History)<>"Dictionary" Then Exit Sub

	'Chargement des paramètres globaux
	otSql = "SELECT * FROM otParameters"
	Set oRec = HistoryData.Execute(otSql)
	If Err<>0 Then
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "LoadGlobalHistory"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
		Exit Sub
	End If
	If Not (oRec.EOF) Then
		oRec.MoveFirst
		Do Until oRec.EOF
			If Err<>0 Then Exit Do
			If Len(Nz(oRec("otName").Value, "")) > 0 Then
				'Ajout/Remplacement du paramètre dans le dictionnaire
				History(CStr(oRec("otName").Value)) = CStr(Nz(oRec("omValue").Value, ""))
			End If
			oRec.MoveNext
		Loop
	End If
	If Not (oRec Is nothing) Then
		oRec.Close
		Set oRec = nothing
	End If

	Err.clear
End Sub

'////////////////////////////////////////////////////////////
Sub NewSessionHistory()
'##BD Création d'une nouvelle session (HistorySession)
Dim otSql

	On Error Resume Next   
	
    If TypeName(HistoryData)<>"Connection" Then Exit Sub
	If (HistoryData Is Nothing) Then Exit Sub
    If TypeName(HistorySession)<>"Dictionary" Then Exit Sub

	'Identifiant de la session
	HistorySession("__CurrentId") = otGUID()
	'Nom du modèle
	HistorySession("__CurrentName") = Left(otApplicationName, 255)
	'Date d'exécution
	HistorySession("__CurrentDate") = Now()
	
	'Création d'une nouvelle session
	otSql = "INSERT INTO otSession(otSessionId, otName, odDate) VALUES("
	otSql = otSql & "'" & PYTOOLS.otDoubleQuote(HistorySession("__CurrentId")) & "'"
	otSql = otSql & ",'" & PYTOOLS.otDoubleQuote(HistorySession("__CurrentName")) & "'"
	otSql = otSql & "," & PYTOOLS.otDateSql(HistorySession("__CurrentDate"), True)
	otSql = otSql & ")"
	Err.clear
	HistoryData.Execute otSql
	If Err<>0 Then
		optLastError = Err.Description
		optLastErrorMessage = ""
		optLastNodeError = "NewSessionHistory"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
		'Suppression des entrées
		HistorySession.RemoveAll		
	End If

	Err.clear
End Sub

'////////////////////////////////////////////////////////////
Sub SaveGlobalHistory()
'##BD Sauvegarde des paramètres globaux contenus dans le dictionnaire History
'##		Rappel : les clefs commençant par un _ ne sont pas sauvegardées
Dim otSql
Dim oRec
Dim oKeys
Dim otKey
Dim otValue

	On Error Resume Next
	
    If TypeName(HistoryData)<>"Connection" Then Exit Sub
	If (HistoryData Is Nothing) Then Exit Sub
    If TypeName(History)<>"Dictionary" Then Exit Sub
	
    'Récupération des clefs
    oKeys = History.Keys
    If Err = 0 Then
        For oni = 0 To History.Count - 1
            otKey = Trim(oKeys(oni))
            If Left(otKey,1) <> "_" Then
            	otValue = CStr(History(otKey))
				
				'Vérification de la présence du paramètre
				err.clear
				otSql = "SELECT * FROM otParameters WHERE otName = '" & PYTOOLS.otDoubleQuote(otKey) & "'"
				Set oRec = HistoryData.Execute(otSql)
				If Err<>0 Then
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "SaveGlobalHistory"
					If ovLog = -1 Then
						WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
					End If
					Exit Sub
				End If
				
				If Not (oRec.eof) Then
					'MAJ
					otSql = "UPDATE otParameters SET omValue = '" & PYTOOLS.otDoubleQuote(otValue) & "'"
					otSql = otSql & " WHERE otName = '" & PYTOOLS.otDoubleQuote(otKey) & "'"
				Else
					'INSERTION
					otSql = "INSERT INTO otParameters(otName, omValue) VALUES("
					otSql = otSql & "'" & PYTOOLS.otDoubleQuote(otKey) & "'"
					otSql = otSql & ",'" & PYTOOLS.otDoubleQuote(otValue) & "'"
					otSql = otSql & ")"
				End If
				Err.clear
				HistoryData.Execute otSql
				If Err<>0 Then
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "SaveGlobalHistory"
					If ovLog = -1 Then
						WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
					End If
				End If
            	
				If Not (oRec Is Nothing) Then
					oRec.Close
					Set oRec = Nothing
				End If
            End If
        Next
    End If
	
	Err.clear
End Sub

'////////////////////////////////////////////////////////////
Sub SaveSessionHistory()
'##BD Sauvegarde des paramètres de session contenus dans le dictionnaire HistorySession
'##		Rappel : les clefs commençant par un _ ne sont pas sauvegardées
Dim otSql
Dim oRec
Dim oKeys
Dim otKey
Dim otValue

	On Error Resume Next
	
    If TypeName(HistoryData)<>"Connection" Then Exit Sub
	If (HistoryData Is Nothing) Then Exit Sub
    If TypeName(HistorySession)<>"Dictionary" Then Exit Sub
	If Not HistorySession.Exists("__CurrentId") Then Exit Sub
	
    'Récupération des clefs
    oKeys = HistorySession.Keys
    If Err = 0 Then
        For oni = 0 To HistorySession.Count - 1
            otKey = Trim(oKeys(oni))
            'Rappel : les clefs commençant par un _ ne sont pas sauvegardées
            If Left(otKey,1) <> "_" Then
            	otValue = CStr(HistorySession(otKey))
				
				'Vérification de la présence du paramètre
				err.clear
				otSql = "SELECT * FROM otSessionParameters"
				otSql = otSql & " WHERE otName = '" & PYTOOLS.otDoubleQuote(otKey) & "'"
				otSql = otSql & " AND otSessionId = '" & PYTOOLS.otDoubleQuote(HistorySession("__CurrentId")) & "'"
				Set oRec = HistoryData.Execute(otSql)
				If Err<>0 Then
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "SaveSessionHistory"
					If ovLog = -1 Then
						WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
					End If
					Exit Sub
				End If
				
				If Not (oRec.eof) Then
					'MAJ
					otSql = "UPDATE otSessionParameters SET omValue = '" & PYTOOLS.otDoubleQuote(otValue) & "'"
					otSql = otSql & " WHERE otName = '" & PYTOOLS.otDoubleQuote(otKey) & "'"
					otSql = otSql & " AND otSessionId = '" & PYTOOLS.otDoubleQuote(HistorySession("__CurrentId")) & "'"
				Else
					'INSERTION
					otSql = "INSERT INTO otSessionParameters(otSessionId, otName, omValue) VALUES("
					otSql = otSql & "'" & PYTOOLS.otDoubleQuote(HistorySession("__CurrentId")) & "'"
					otSql = otSql & ",'" & PYTOOLS.otDoubleQuote(otKey) & "'"
					otSql = otSql & ",'" & PYTOOLS.otDoubleQuote(otValue) & "'"
					otSql = otSql & ")"
				End If
				Err.clear
				HistoryData.Execute otSql
				If Err<>0 Then
					optLastError = Err.Description
					optLastErrorMessage = ""
					optLastNodeError = "SaveSessionHistory"
					If ovLog = -1 Then
						WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
					End If
				End If
            	
				If Not (oRec Is Nothing) Then
					oRec.Close
					Set oRec = Nothing
				End If
            End If
        Next
    End If
	
	Err.clear
End Sub

'////////////////////////////////////////////////////////////
Function IsHistoryChanged(ByVal otHistoryTable, ByVal otKey, ByVal otMd5)
'##BD Indique si les données ont changé en utilisant la gestion de l'historique
'##PD otHistoryTable Nom de la table contenant les données historisées
'##PD otKey Clef d'identification unique
'##PD otMd5 Chaîne utilisée pour effectuer la comparaison
'##RD Renvoi False si les données n'ont pas changé
Dim otTemp
Dim onl
Dim otSql
Dim oRec
Dim otMd5Calculated

	On Error Resume Next
	
	IsHistoryChanged = True
	'Vérification de la connexion sur la base historique
    If TypeName(HistoryData)<>"Connection" Then Exit Function
	If (HistoryData Is Nothing) Then Exit Function
    If TypeName(HistorySession)<>"Dictionary" Then Exit Function
	If Not HistorySession.Exists("__CurrentDate") Then Exit Function

	'Vérification de la présence de tous les paramètres
	If Len(otHistoryTable) = 0 Then Exit Function
	If Len(otKey) = 0 Then Exit Function
	If Len(otMd5) = 0 Then Exit Function

	'Vérification de la présence de la table contenant les données historisées
	Err.clear
	otSql = "SELECT TOP 1 * FROM " & otHistoryTable & " WHERE 1=0"
	
	Set oRec = HistoryData.Execute(otSql)
	If Err<>0 Then
		Err.clear
		'La table n'existe pas, on doit la créer
		otSql = "CREATE TABLE " & otHistoryTable & "("
		otSql = otSql & "otKey Text(255) CONSTRAINT PK_OTKEY PRIMARY KEY"
		otSql = otSql & ", otMd5 Text(50)"
		otSql = otSql & ", odDate DateTime"
		otSql = otSql & ", otCode Text(1)"
		otSql = otSql & ")"
		Err.clear
		HistoryData.Execute otSql
		If Err<>0 Then
			optLastError = Err.Description
			optLastErrorMessage = "CREATE TABLE " & otHistoryTable
			optLastNodeError = "IsHistoryChanged"
			If ovLog = -1 Then
				WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
			End If
		End If
		'Vérification supplémentaire de la présence de la table
		otSql = "SELECT TOP 1 * FROM " & otHistoryTable & " WHERE 1=0"
		
		Err.clear
		Set oRec = HistoryData.Execute(otSql)
		If Err<>0 Then
			optLastError = Err.Description
			optLastErrorMessage = "VERIF CREATE TABLE " & otHistoryTable
			optLastNodeError = "IsHistoryChanged"
			If ovLog = -1 Then
				WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
			End If
			If Not (oRec Is nothing) Then
				oRec.Close
				Set oRec = nothing
			End If
			Exit Function
		End If
	End If
	If Not (oRec Is nothing) Then
		oRec.Close
		Set oRec = nothing
	End If
	
	'Recherche de la clé
	Err.clear
	otSql = "SELECT otMD5 FROM " & otHistoryTable & " WHERE otKey='" & PYTOOLS.otDoubleQuote(otKey) & "'"
	Set oRec = HistoryData.Execute(otSql)
	If Err<>0 Then
		optLastError = Err.Description
		optLastErrorMessage = otHistoryTable
		optLastNodeError = "IsHistoryChanged"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
		If Not (oRec Is nothing) Then
			oRec.Close
			Set oRec = nothing
		End If
		Exit Function
	End If
	
	'Conversion Md5 avant d'effectuer la comparaison
	otMd5Calculated = PYSCRIPTTOOLS.Archive.otMd5(otMd5)
	'Vérification que la conversion a réussi car la méthode n'était pas disponible avant la 3.4.4.0
	If StrComp(otMd5, otMd5Calculated, 1) = 0 Then
		optLastError = "La clef MD5 n'a pas pu être calculée"
		optLastErrorMessage = otKey
		optLastNodeError = "IsHistoryChanged"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
		If Not (oRec Is nothing) Then
			oRec.Close
			Set oRec = nothing
		End If
		Exit Function
	End If
	otMd5 = otMd5Calculated
	
	If Not (oRec.eof) Then
		'Informations de suivi trouvées
		otSql = "UPDATE " & otHistoryTable & " SET odDate=" & PYTOOLS.otDateSql(HistorySession("__CurrentDate"), True)
		If StrComp(Nz(oRec(0).value, ""), otMd5, 1) = 0 Then
			'Pas de modifications détectées
			otSql = otSql & ", otCode='C'"
			IsHistoryChanged = False
		Else
			'Modifications détectées
			otSql = otSql & ", otMd5='" & otMd5 & "', otCode='U'"
		End If
		otSql = otSql & " WHERE otKey='" & PYTOOLS.otDoubleQuote(otKey) & "'"
	Else
		'Informations de suivi non trouvées
		otSql = "INSERT INTO " & otHistoryTable & "(otKey,otMd5,odDate,otCode)"
		otSql = otSql & " VALUES("
		otSql = otSql & "'" & PYTOOLS.otDoubleQuote(otKey) & "'"
		otSql = otSql & ",'" & otMd5 & "'"
		otSql = otSql & "," & PYTOOLS.otDateSql(HistorySession("__CurrentDate"), True)
		otSql = otSql & ",'N'"
		otSql = otSql & ")"
	End If
	Err.clear
	HistoryData.Execute otSql
	If Err<>0 Then
		optLastError = Err.Description
		optLastErrorMessage = "MAJ " & otHistoryTable & " (" & otKey & ")"
		optLastNodeError = "IsHistoryChanged"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
	End If

	If Not (oRec Is nothing) Then
		oRec.Close
		Set oRec = nothing
	End If
	
	Err.clear
End Function
'/////////////////////////////////////////////////////////////////////
Function otExtractCmd(otParameter, otDefaut)
Dim oParameters
Dim onl
Dim oTemp
Dim oRet

	On Error Resume Next
	'Gestion des paramètres només
	oRet = ""
	Set oParameters = WScript.Arguments.Named
	If err<>0 Then
		Set oParameters = WScript.Arguments
		For onl = 0 To oParameters.Count - 1
			oTemp = split(mid(oParameters(onl),2), ":",2)
			If strcomp(oTemp(0), otParameter, 1)=0 Then
				oRet = oTemp(1)
				Exit For
			End If
		Next	
	Else
		oRet = oParameters.Item(otParameter)
	End If

	If len(oret)>0 Then
		If Not PYTOOLS Is nothing Then
			oRet = PYTOOLS.otAnsiToOem(oRet)
		End If
	Else
		oRet = otDefaut
	End If
	otExtractCmd = oret
	err.clear
	
End Function

function otgetrezo(otip)
' renvoie le type de réseau en fonction de l'IP
otgetrezo = "Administratif"
if otip = "" then otgetrezo = "Hors réseau"
if left(otip, 6) = "10.206" then otgetrezo = "Opérationnel"
end function


Function odSearchDate (otFormat)
	' recherche la dernière date d'inventaire dans fichierINI
	On Error Resume Next
	' otFormat doit être d ou m selon si la date sur sqlserveur est en français ou en anglais	                                
	err.clear
	odSearchDate = PyTools.ovaPrivateIni(cstr(fichierINI),"Inventaire","Date","22/03/2011 00:00:00",True)
		
	If Err <> 0 Then   
		err.clear
		odSearchDate = "22/03/2011 00:00:00"
	End If	
	
	' odSearchDate = Pytools.otDateSqlEx(odSearchDate,True,otFormat,"SQL server")	          

End Function


' transforme une date FR en GB
Function GBdate(buf)
GBdate = mid(buf, 7, 4) & "/" & mid(buf, 4, 2) & "/" & left(buf, 2) & mid(buf, 11)
End Function

Function generereqUC(dateinv)
generereqUC = ""
dim ret
ret = "select hardware.ipaddr as ipaddr,"
ret = ret & " (select name from subnet where NETID = networks.IPSUBNET) as nom_reseau,"
ret = ret & " hardware.ID, memory, convert(convert(hardware.name using binary) using utf8) as NAME, LASTDATE, convert(convert(hardware.userid using binary) using utf8) as USERID,"
ret = ret & " convert(convert(osname using binary) using utf8) as OSNAME,convert(convert(oscomments using binary) using utf8) as OSCOMMENTS,"
ret = ret & " convert(convert(userdomain using binary) using utf8) as USERDOMAIN, convert(convert(workgroup using binary) using utf8) as WORKGROUP "
ret = ret & " from hardware, networks"
ret = ret & " where networks.HARDWARE_ID = hardware.ID and hardware.ipaddr = networks.ipaddress"
ret = ret & " and hardware.lastdate > '" & GBdate(dateinv) & "'"
ret = ret & " and hardware.name not like 'ddsis%' and hardware.name not like 'win%'"
generereqUC = ret
end Function

'/////////////////////////////////////////////////////////////////////
Function TRANSFORMATION1_Code(ByVal oObject, Byval MasterElement, Byval otOutputDirectory, Byval otTatouage, Byval oniMode, Byval otMd5Key)
'TRANSFORMATION1_oCode esr transformé lors de l'exécution en fonction du nom de la tansformation
'Exemple si le nom est TransUser le nom finale sera TransUser_Code
   Const ForReading = 1, ForWriting = 2, ForAppending = 8
   
    Dim oFso, oRep, oFile, otFile, otId, oResume
    Dim oKeys, oProp ,oniA, oTemp, oni
    Dim oColKey
    Dim otFileTmp
    Dim otMd5Checked
    
    On Error Resume Next    
    
    If TypeName(oObject)<>"Dictionary" Then Exit Function
    If len(otOutputDirectory)> 0 Then
     	If left(otOutputDirectory,1) = Chr(34) Then
     		otOutputDirectory = mid(otOutputDirectory,2)
     	End If
     	If right(otOutputDirectory,1) = Chr(34) Then
     		otOutputDirectory = mid(otOutputDirectory,1,len(otOutputDirectory) -1)	
     	End If	
    End If
    
    'Si le répertoire de sortie n'existe pas. On sort en erreur
	If Len(CreateDirectory(otOutputDirectory)) = 0 Then
		optLastError = "Le répertoire de sortie n'existe pas : " & otOutputDirectory
		optLastErrorMessage = ""
		optLastNodeError = "TRANSFORMATION1_Code"
		If ovLog = -1 Then
			WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
		End If
	    'Libération des élément de la transformation
	    oObject("Elements").RemoveAll
		Exit Function
	End If
    
    If (Not IsEmpty(MasterElement)) Then
    	If (Not MasterElement Is nothing) Then
		    If Len(otTatouage) > 0 Then
		        otId = otTatouage
		    Else
		        otId = otGuid()
		    End If
		    'Traitement des caractères spéciaux interdit dans le nom du fichier
		    otId = otEscapeFileName(otId)
		     
		    otMd5Checked = ""

	     	'Fichier de sortie final
	        otFile = otOutputDirectory & "\" & otId & ".Txt"
	        otFileTmp = otFile
	    	
	    	'Si la clé d'identification pour l historique est fournie
		    If Len(otMd5Key) > 0 Then
		    	'On s'assure que le répertoire temporaire existe
		    	CreateDirectory otOutputDirectory & "\Temp"
		    	'On passe par un fichier temporaire
		        otFileTmp = otOutputDirectory & "\Temp\" & otId & ".Tmp"
		    End If
	
		    Set oFso = CreateObject("Scripting.FileSystemObject")
		    Set oFile = oFso.OpenTextFile(otFileTmp, ForWriting, True)
		   
		    oFile.WriteLine "[TATOUAGE]"
		    oFile.WriteLine "Numero=" & otId
		    oFile.WriteBlankLines 1
		
		    oFile.WriteLine "[INVENTORY]"
		    oFile.WriteBlankLines 1
		
		    oFile.WriteLine "[OPTIONS]"
		    oFile.WriteLine "Windows = oui"
		    oFile.WriteLine "Date scan = " & Format(Now, "YYYYMMDDHHMMSS")
		    oFile.WriteLine "Modele = " & oObject("Name") 
		    oFile.WriteLine "Description = " & oObject("Description")
		    oFile.WriteLine "Mode = " & oniMode 
		    oFile.WriteBlankLines 1
		
		    oFile.WriteLine "[STATION]"
		    oFile.WriteLine "Count = 1"
		    oFile.WriteLine "instance = Resume"
		    oFile.WriteBlankLines 1
		    
		    'Traitement du master Element
		    oKeys = oObject("Elements").Keys
		    oFile.WriteLine "[Resume]"
		    oFile.WriteLine "Type = Object"
		    If Not IsEmpty(MasterElement) Then
		        oFile.WriteLine "ObjectType = " & MasterElement("Type")
		    Else
		        If oObject.Exists("Elements") Then
		            oFile.WriteLine "ObjectType = " & oObject("Elements")(oKeys(0))("Type")
		        Else
		            oFile.WriteLine "ObjectType ="
		        End If
		    End If
		    oFile.WriteLine "Antecedent = " & MasterElement("Name") & "_1"
		    oFile.WriteLine "N° Interne = " & otId
		    Err.Clear
		    If oObject.Exists("Resumed") Then
		        oKeys = oObject("Resumed").Keys
		        If Err = 0 Then
		            For oni = 0 To oObject("Resumed").Count - 1
		                'Ecriture des propriété dans le résumé
			            oProp = oObject("Resumed")(oKeys(oni))
			            If IsArray(oProp(1)) Then
			                oTemp = ""
			                For oniA = 0 To UBound(oProp(1))
			                    If Len(oTemp) > 0 Then oTemp = oTemp & ";"
			                    oTemp = oTemp & oProp(1)(oniA)
			                Next
			            Else
			                oTemp = oProp(1)
			            End If
			            oFile.WriteLine oKeys(oni) & " = " & otEncode(oTemp) 
		            Next
		        End If
		        oObject("Resumed").RemoveAll
		    End If
		      
		    Err.Clear
		    'Collection des clés des chapitres créés
		    Set oColKey = CreateObject("Scripting.Dictionary")
		    If oObject.Exists("Elements") Then
		        oKeys = oObject("Elements").Keys
		        If Err = 0 Then
		            For oni = 0 To oObject("Elements").Count - 1
	             		'Ecriture des chapitres associés aux objets de la transformation
		                WriteChapiter oObject, oKeys(oni), oObject("Elements")(oKeys(oni)), oFile, otFile, oColKey, otMd5Checked     
		            Next
		        End If
		    End If
		    'Libération des clés des chapitres
	        oColKey.RemoveAll
	        Set oColKey = Nothing
		    
		    'Fermeture du fichier Txt
		    oFile.Close

			'Gestion du différenciel
			If StrComp(otFile, otFileTmp, 1) <> 0 Then
			    'Vérification du différenciel
				If IsHistoryChanged("", otMd5Key, otMd5Checked) Then
					'Changement détecté
					'Suppression du fichier final
					If oFso.FileExists(otFile) Then oFso.DeleteFile otFile, True
					'On renomme le fichier temporaire en fichier final
					oFso.MoveFile otFileTmp, otFile
				Else
					'Pas de changement
					'Suppression du fichier temporaire
					If oFso.FileExists(otFileTmp) Then oFso.DeleteFile otFileTmp, True
				End If
			End If
		End If
	End If    
    'Libération des élément de la transformation
    oObject("Elements").RemoveAll

End Function

Function WriteChapiter(byval oObjectFather, Byval otKey, ByVal oObject, ByVal oFile, Byval otFile,oColKey, ByRef otMd5Value)

    Dim oKeys
    Dim oKey
    Dim oni
    Dim oniA
    Dim oProp
    Dim oTemp
    Dim ov
    Dim otType
    Dim otLocal
    Dim otChapiter
    Dim oColLocal
    
    On Error Resume Next
    
    'Définition de l'ordre local de sortie
    Set oColLocal = oObjectFather("OrderLocal")
    If oColLocal.exists(cstr(oObject("Type"))) Then
    	otLocal = clng(oColLocal(cstr(oObject("Type")))) + 1
    	oColLocal(cstr(oObject("Type"))) = otLocal
    Else
    	otLocal = 1 	
    	oColLocal.Add cstr(oObject("Type")), otLocal
    End If
    'Association de la clé du chappitre avec l'ordre local dans le txt
    otChapiter = oObject("Name") & "_" & oObject("Count")
    oColKey.add otKey, otChapiter
    
    oFile.WriteBlankLines 1
    oFile.WriteLine "[" & otChapiter & "]"
    
    'Dépendance du chapitre
    If strcomp(oObject("Parent"),"Néant",1)=0 Then
        oFile.WriteLine "Dependance = " & oObject("Parent")
    Else
    	oFile.WriteLine "Dependance = " & oColKey(oObject("Parent"))
    End If
    
    If len(trim(oObject("Axis")))>0 Then
    	oFile.WriteLine "Type = Axe"
    	oFile.WriteLine "Axis = " & oObject("Axis") 
    Else
    	oFile.WriteLine "Type = Object"
    End If

    oFile.WriteLine "ObjectType = " & oObject("Type")
    If instr(1, cstr(oObject("Type")), "," ,1)>0 Then 
    	otType = "*"
    Else
    	otType = oObject("Type")
    End If
    
    'Action à réaliser sur le chapitre
    oFile.WriteLine "Action = " & oObject("Action")
    
    'Analyse des clés d'itenfications
    oTemp = ""
    oTemp = oObject("Identification")	
    If len(oTemp)>0 Then
    	'Ajout des clés
    	ov = split(otemp,",")
    	For oni=0 To UBound(ov)
    		err.clear
    		oTemp = "Key" & oni & " = " & ov(oni)
    		If err=0 Then
    			oFile.WriteLine "Keys = " & oTemp		
    		Else  
    			Exit For
    		End If
    	Next	
    End If
    oFile.WriteLine "OrdreLocal = " & otLocal 

    'Relation
    Err.clear
    oTemp = ""
    oTemp = oObject("Relation")
    If len(oTemp) > 0 Then
		oFile.WriteLine "Relation = " & oTemp
      	oFile.WriteLine "Objet = " & oObject("Object")
    End If
    Err.clear    

    'Activation du chapitre
    oFile.WriteLine "Actif = Oui"
    'Vérification de la création par article
    oTemp = ""
    oTemp = oObject("CreateByArticle")
    If len(oTemp)>0 Then
    	oFile.WriteLine "CreationParArticle = " & oTemp 
    End If
        
    'Relations
    Err.Clear
    If oObject.Exists("Relations") Then
        oKeys = oObject("Relations").Keys
        If Err = 0 Then
            For oni = 0 To oObject("Relations").Count - 1
                oProp = oObject("Relations")(oKeys(oni))
                oFile.WriteLine "Relation" & oni + 1 & " = " & oProp(0) & " = " & oProp(1) & " %3D '" & oProp(2) & "' = " & oProp(3) & " = " & oProp(4)
            Next
        End If
    End If
    oObject("Relations").removeAll

    Err.Clear
    If oObject.Exists("Properties") Then
        oKeys = oObject("Properties").Keys
        If Err = 0 Then
            For oni = 0 To oObject("Properties").Count - 1
                oProp = oObject("Properties")(oKeys(oni))
                If IsArray(oProp(1)) Then
                    oTemp = ""
                    For oniA = 0 To UBound(oProp(1))
                        If Len(oTemp) > 0 Then oTemp = oTemp & ";"
                        oTemp = oTemp & oProp(1)(oniA)
                    Next
                Else
                    oTemp = oProp(1)
                End If
                If (left(oTemp,1)="{") And (right(oTemp,1)="}") Then
                	'Référence sur un chapitre
                	err.clear
                	oTemp = oColKey(oTemp)
                	If err=0 Then
                		If len(oTemp)>0 Then
                			oTemp = "[" & oTemp & "]"
                		End If
                	Else
                		oTemp = ""
                	End If
                End If
                oFile.WriteLine "Property" & oni + 1 & " = " & otType & " = " & oProp(6) & oProp(0) & " = " & otEncode(oTemp) & " = " & otEncode(oProp(2))
                'Gestion de l historique
                If UBound(oProp)>=5 Then
                	If IsNumeric(oProp(5)) Then
	                	If Abs(oProp(5)) <> 0 Then otMd5Value = otMd5Value & oTemp
                	End If
                End If      
            Next
        End If
    End If
    oObject("Properties").RemoveAll
    
    Err.Clear
    If oObject.Exists("Elements") Then
        oKeys = oObject("Elements").Keys
        If Err = 0 Then
            For oni = 0 To oObject("Elements").Count - 1
                WriteChapiter oObject, oKeys(oni), oObject("Elements")(oKeys(oni)), oFile, otFile, oColKey, otMd5Value
            Next
        End If
    End If
    oObject("Elements").RemoveAll
    
End Function

Function GetValureForType(Byval oObject, Byval onlType, Byval otProperties)
Dim oni
Dim oObj
Dim oKeys

For oni = 0 To oObject("Elements").Count - 1
	oKeys = oObject("Elements").Keys
	Set oObj = oObject("Elements")(oKeys(oni))
	If oObj("Type") = onltype Then
		'Le type d'objet a été trouvé
		GetValureForType = oObj("Properties")(otProperties)(1)
		Exit Function
	Else
		'Recherche dans les éléments
		GetValureForType = GetValureForType(oObj, onlType, otProperties)		
		If len(GetValureForType)>0 Then
			Exit Function
		End If
	End If
Next

End Function

Function otEncode(byval otValue)
Dim otTemp

otTemp = Replace(otValue, "%", "%" & Hex(Asc("%")))
otTemp = Replace(otTemp, "=", "%" & Hex(Asc("=")))
otTemp = Replace(otTemp, " ", "%" & Hex(Asc(" ")))
otTemp = Replace(otTemp, """", "%" & Hex(Asc("""")))
otTemp = Replace(otTemp, ";", "%" & Hex(Asc(";")))
otTemp = Replace(otTemp, ",", "%" & Hex(Asc(",")))
otTemp = Replace(otTemp, "#", "%" & Hex(Asc("#")))
otTemp = Replace(otTemp, "[", "%" & Hex(Asc("[")))
otTemp = Replace(otTemp, "]", "%" & Hex(Asc("]")))
otTemp = Replace(otTemp, "-", "%" & Hex(Asc("-")))
otTemp = Replace(otTemp, "/", "%" & Hex(Asc("/")))
otTemp = Replace(otTemp, "\", "%" & Hex(Asc("\")))
otTemp = Replace(otTemp, "'", "%" & Hex(Asc("'")))
otTemp = Replace(otTemp, Chr(167), "%" & Hex(167)) 
otTemp = Replace(otTemp, Chr(64), "%" & Hex(64))	
otTemp = Replace(otTemp, Chr(36), "%" & Hex(36))	
otTemp = Replace(otTemp, Chr(124), "%" & Hex(124))	
otTemp = Replace(otTemp, ":", "%" & Hex(Asc(":")))
otTemp = Replace(otTemp, vbCr, "%0" & Hex(Asc(vbCr)))
otTemp = Replace(otTemp, vbLf, "%0" & Hex(Asc(vbLf)))
otTemp = Replace(otTemp, vbTab, "%0" & Hex(Asc(vbTab)))

otEncode = otTemp

End Function

'////////////////////////////////////////////////////////////////////////////////////////////////////////
Function DeleteObjectsHistory(otTransformation, ovLog, ovFormatLog)  

Dim otListeObj
Dim otListeObjDel
Dim otListeObjErr
Dim otListeErr
Dim otListeTypeObj
Dim otSql
Dim oRec1
Dim oRec
Dim oContObj 
Dim oAction
Dim otLog
Dim ovDel
Dim onlc

ovDel = (strcomp(otTransformation("DeleteObject"),"1")=0)
MAJ_Objects_Supprimes= true	
'Liste des types de l'object		
otListeTypeObj=otTransformation("ObjectTypes")
Set oAction = PYPAM.oNewAction()
'Boucle sur les objets à supprimer	
Set oRec1=Nothing  
otListeObj=""	    
otListeObjDel=""	    
otListeObjErr=""

on error resume next
err.clear
otSql = "SELECT otKey FROM " & HistorySession(otTransformation("Name") & "_HistoryTable") & " WHERE oddate <>  format( '" & HistorySession("__CurrentDate") & "' ,'dd/mm/yyyy hh:mm:ss')" 
Set oRec1 = HistoryData.Execute(otSql)
If Err<>0 Then
	optLastError = Err.Description
	optLastErrorMessage = ""
	optLastNodeError = "MAJ_Objects_Supprimes"
	If ovLog = -1 Then
		WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
	End If
	Exit Function
End If 

Do While Not oRec1.EOF
	if err<>0 then exit do
	'Suppression des objects
    otSql="SELECT ob.oidObject, oType.otName,  ob.otModel, ob.otInternalNumber, ob.otSerialNumber, ob.otManufactor, ob.oidObjectType FROM otObject ob, otObjectType oType WHERE  ob.oidobjectType=oType.oidobjectType and  ob.oidobjectType in (" & otListeTypeObj & ") and  ob.otInternalNumber='" & oRec1(0).value & "'" 
	Set oRec=Nothing  
    Set oRec = PYPAM.ExecuteSql(otSql)
    If  Not (oRec Is Nothing) Then		     	
		if not oRec.eof Then
			err.Clear
	      	Do While Not oRec.EOF
				if err<>0 then exit do
				If ovDel = True Then
					call oAction.ovQuickDeleteobject(Clng(oRec(0).value), CLng(oRec(6).value))
				else
					if ovFormatLog=-1 then
						otListeObjDel=otListeObjDel & oRec(1).value & vbTab & vbTab & oRec(2).value & vbTab & vbTab & oRec(3).value & vbTab & vbTab & oRec(4).value & vbTab & vbTab & oRec(5).value & vbCrLf
					else
						otListeObjDel=otListeObjDel & "<TR class=LineProperty><TD></TD><TD>" & iif(len(oRec(1).value)>0,oRec(1).value,"&nbsp;") & "</TD><TD>"  & iif(len(oRec(2).value)>0,oRec(2).value,"&nbsp;") & _
						 "</TD><TD>" & iif(len(oRec(3).value)>0,oRec(3).value,"&nbsp;") & "</TD><TD>" & iif(len(oRec(4).value)>0,oRec(4).value,"&nbsp;") & "</TD><TD>" & iif(len(oRec(5).value)>0,oRec(5).value,"&nbsp;") & "</TD></TR>"
					End if
				End if
				err.Clear
	   			oRec.MoveNext
			Loop
			if ovDel = True Then
				'Vérification de la suppression
				set oRec=Nothing
				otSql="SELECT ob.oidObject, oType.otName,  ob.otModel, ob.otInternalNumber, ob.otSerialNumber, ob.otManufactor, ob.oidObjectType FROM otObject ob, otObjectType oType WHERE  ob.oidobjectType=oType.oidobjectType and  ob.oidobjectType in (" & otListeTypeObj & ") and  ob.otInternalNumber='" & oRec1(0).value & "'" 
			    Set oRec = PYPAM.ExecuteSql(otSql)
			    If  Not (oRec Is Nothing) Then
					Do While Not oRec.EOF
						if ovFormatLog=-1 then
							otListeObjErr=otListeObjErr & oRec(1).value & vbTab & vbTab & oRec(2).value & vbTab & vbTab & oRec(3).value & vbTab & vbTab & oRec(4).value & vbTab & vbTab & oRec(5).value & vbCrLf
						else
							otListeObjErr=otListeObjErr & "<TR class=LineProperty><TD>Erreur</TD><TD>" & iif(len(oRec(1).value)>0,oRec(1).value,"&nbsp;") & "</TD><TD>"  & iif(len(oRec(2).value)>0,oRec(2).value,"&nbsp;") & _
							 "</TD><TD>" & iif(len(oRec(3).value)>0,oRec(3).value,"&nbsp;") & "</TD><TD>" & iif(len(oRec(4).value)>0,oRec(4).value,"&nbsp;") & "</TD><TD>" & iif(len(oRec(5).value)>0,oRec(5).value,"&nbsp;") & "</TD></TR>"	
						end if	  
					loop
					set oRec=Nothing
				End if
			end if
		Else
			otMesErr = "L'objet n'existe pas dans PSD"
			otListeObjErr = otListeObjErr & "<TR class=LineProperty><TD>" & otMesErr & "</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>" & iif(len(oRec1(0).value)>0,oRec1(0).value,"&nbsp;") & "</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR>"	
		End if
	End If 
	err.clear
   	oRec1.MoveNext
Loop   
PYPAM.ovRemoveAction oAction.otKey

if ovLog then
	otLog = ""
	if ovFormatLog=-1 then					
		otLog =  vbCrLf & "-------------------------------------------------------------------------------------------------------" & vbCrLf & _
		"Type " & vbTab & vbTab & "Désignation" & vbTab & vbTab & "Numero interne"	& vbTab & vbTab & "Numero de série" & vbTab & vbTab & "Constructeur" & vbCrLf & _
													"-------------------------------------------------------------------------------------------------------" & vbCrLf & otListeObjDel				
	else
		otLog = "<TABLE cellSpacing=0 cellPadding=1 width=""100%"" border=1><TR class=HeaderProperty><TD>Erreur</TD><TD>Type</TD><TD>Désignation</TD><TD>Numero interne</TD><TD>Numero de série</TD><TD>Constructeur</TD></TR><BR>" & otListeObjDel
	end if
	if ovFormatLog=-1 then
		otLog = otLog & vbCrLf & "-------------------------------------------------------------------------------------------------------" & vbCrLf & otListeObjErr	
	else
		otLog = otLog & otListeObjErr & "</TABLE>"	
	end if
	if ovDel = true then
		opColLog.Add otTransformation("Name") & "DEL" ,Array("Compte rendu des suppressions par rapport à l'historique précédent", "0", otLog) 
	else
		opColLog.Add otTransformation("Name") & "DEL" ,Array("Compte rendu des éléments non identifiés par rapport à l'historique précédent", "0", otLog) 
	end if
end if

End Function

																										 
'////////////////////////////////////////////////////////////////////////////////////////////////////////
Function UpdateObjectsHistory(otTransformation, ovLog, ovFormatLog)  

Dim otListeObj
Dim otListeObjUpd
Dim otListeObjErr
Dim otListeErr
Dim otListeTypeObj
Dim otSql
Dim oRec
Dim oRecP
Dim oContObj 
Dim oAction
Dim oObj
Dim otMesErr
Dim otTitre
Dim otLog
Dim ovUp

on error resume next

otListeObjUpd=""	    
ovUp = (strcomp(otTransformation("UpdateObject"),"1")=0)
'Liste des types de l'object		
otListeTypeObj=otTransformation("ObjectTypes")
'Liste des objets à mettre à jour	
err.clear
Set oRec=Nothing  
otSql = "SELECT otKey FROM " & HistorySession(otTransformation("Name") & "_HistoryTable") & " WHERE oddate =  " & PYTOOLS.otDateSqlEx(HistorySession("__CurrentDate"), True, "", "Access97") & " AND otCode='C'" 
Set oRec = HistoryData.Execute(otSql)
If Err<>0 Then
	optLastError = Err.Description
	optLastErrorMessage = ""
	optLastNodeError = "MAJ_Objects_Check"
	If ovLog = -1 Then
		WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
	End If
	Exit Function
End If 
set oObj = nothing
err.clear
Do While Not oRec.EOF
	if err<>0 then exit do
    'Mise à jour des objets
    otSql="SELECT ob.oidObject, oType.otName,  ob.otModel, ob.otInternalNumber, ob.otSerialNumber, ob.otManufactor, ob.oidObjectType FROM otObject ob, otObjectType oType WHERE  ob.oidobjectType=oType.oidobjectType and  ob.oidobjectType in (" & otListeTypeObj & ") and  otInternalNumber ='" & oRec(0).value & "'"  
    Set oRecP = PYPAM.ExecuteSql(otSql)
	if not oRecP.eof then
		if ovUp = True Then
			if not oObj is nothing then PYPAM.ovRemoveInfoObject(oObj.otKey)
		    Set oObj = PYPAM.onlNewInfoObject(, clng(oRecP(0).value))
			if oObj.ovUpdate("Common_odDateScan;" & now) = True Then
				'Mise à jour des enfants
				otMesErr = ""
				if oObj.ObjectChilds(,,True).Actions.UpdateObject("Common_odDateScan;" & now) = False Then
					'Erreur de mise à jour des enfants
					otMesErr = "Erreur de mise à jour des enfants"
				end if
			else
				'Erreur de mise à jour de la date d'inventaire
				otMesErr = PYPAM.otLastError
			end if
		End if
		If len(otMesErr)=0 then
			if ovFormatLog=-1 then
				otListeObjUpd=otListeObjUpd & oRecP(1).value & vbTab & vbTab & oRecP(2).value & vbTab & vbTab & oRecP(3).value & vbTab & vbTab & oRecP(4).value & vbTab & vbTab & oRecP(5).value & vbCrLf
			else
				otListeObjUpd=otListeObjUpd & "<TR class=LineProperty><TD></TD><TD>" & iif(len(oRecP(1).value)>0,oRecP(1).value,"&nbsp;") & "</TD><TD>"  & iif(len(oRecP(2).value)>0,oRecP(2).value,"&nbsp;") & _
				 "</TD><TD>" & iif(len(oRecP(3).value)>0,oRecP(3).value,"&nbsp;") & "</TD><TD>" & iif(len(oRecP(4).value)>0,oRecP(4).value,"&nbsp;") & "</TD><TD>" & iif(len(oRecP(5).value)>0,oRecP(5).value,"&nbsp;") & "</TD></TR>"
			end if
		else
			if ovFormatLog=-1 then
				otListeErr=otListeErr &  "ERROR:" & otMesErr & ": " & oRec(0).value & vbTab & vbTab & "" & vbTab & vbTab & "" & vbCrLf  
			else							
				otListeObjUpd=otListeObjUpd & "<TR class=LineProperty><TD></TD><TD>" & iif(len(oRecP(1).value)>0,oRecP(1).value,"&nbsp;") & "</TD><TD>"  & iif(len(oRecP(2).value)>0,oRecP(2).value,"&nbsp;") & _
				 "</TD><TD>" & iif(len(oRecP(3).value)>0,oRecP(3).value,"&nbsp;") & "</TD><TD>" & iif(len(oRecP(4).value)>0,oRecP(4).value,"&nbsp;") & "</TD><TD>" & iif(len(oRecP(5).value)>0,oRecP(5).value,"&nbsp;") & "</TD></TR>"
			end if
		End if
	Else
		otMesErr = "L'objet n'existe pas dans PSD"
		if ovFormatLog=-1 then
			otListeErr=otListeErr &  "ERROR:" & otMesErr & ": " & oRec(0).value & vbTab & vbTab & "" & vbTab & vbTab & "" & vbCrLf  
		else
			otListeErr=otListeErr &  "<TR class=LineProperty><TD>" & otMesErr & "</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>" & iif(len(oRec(0).value)>0,oRec(0).value,"&nbsp;") & "</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR>"	  
		end if
	End if
	err.clear
   	oRec.MoveNext
Loop   
if not oObj is nothing then PYPAM.ovRemoveInfoObject(oObj.otKey)

if ovLog then
	otLog = ""
	if ovFormatLog=-1 then					
		otLog = vbCrLf & "-------------------------------------------------------------------------------------------------------" & vbCrLf & _
		"Type " & vbTab & vbTab & "Désignation" & vbTab & vbTab & "Numero interne"	& vbTab & vbTab & "Numero de série" & vbTab & vbTab & "Constructeur" & vbCrLf & _
													"-------------------------------------------------------------------------------------------------------" & vbCrLf & otListeObjUpd				
	else
		otLog = "<TABLE cellSpacing=0 cellPadding=1 width=""100%"" border=1><TR class=HeaderProperty><TD>Erreur</TD><TD>Type</TD><TD>Désignation</TD><TD>Numero interne</TD><TD>Numero de série</TD><TD>Constructeur</TD></TR><BR>" & otListeObjUpd 
	end if
	if ovFormatLog=-1 then
		otLog = otLog & vbCrLf	& "-------------------------------------------------------------------------------------------------------" & vbCrLf & otListeErr	
	else
		otLog = otLog & otListeErr & "</TABLE>"	
	end if
	if ovUp = True then
		opColLog.Add otTransformation("Name") & "UPD" ,Array("Compte rendu des mises à jour directes par rapport à l'historique précédent", "0", otLog) 
	else
		opColLog.Add otTransformation("Name") & "UPD" ,Array("Compte rendu des éléments identiques identifiés par rapport à l'historique précédent", "0", otLog) 
	end if
end if

End Function

'////////////////////////////////////////////////////////////////////////////////////////////////////////
Function GenereObjectsHistory(otTransformation, ovLog, ovFormatLog)  

Dim otListeObjGen
Dim otListeObjErr
Dim otListeErr
Dim otListeTypeObj
Dim otSql
Dim oRec
Dim oRecP
Dim oContObj 
Dim oAction
Dim oObj
Dim otMesErr
Dim otTitre
Dim otLog

on error resume next
err.clear
'Liste des types de l'object		
otListeTypeObj=otTransformation("ObjectTypes")
'Liste des objets à mettre à jour	
Set oRec=Nothing  
otListeObjGen=""	
otListeObjErr=""    
otSql = "SELECT otKey FROM " & HistorySession(otTransformation("Name") & "_HistoryTable") & " WHERE oddate =  " & PYTOOLS.otDateSqlEx(HistorySession("__CurrentDate"), True, "", "Access97") & " AND otCode='U'" 
Set oRec = HistoryData.Execute(otSql)
If Err<>0 Then
	optLastError = Err.Description
	optLastErrorMessage = ""
	optLastNodeError = "GEN_Objects_Check"
	If ovLog = -1 Then
		WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
	End If
	Exit Function
End If 
err.clear
set oObj = nothing
Do While Not oRec.EOF
	if err<>0 then exit do
    'Mise à jour des objets
    otSql="SELECT ob.oidObject, oType.otName,  ob.otModel, ob.otInternalNumber, ob.otSerialNumber, ob.otManufactor, ob.oidObjectType FROM otObject ob, otObjectType oType WHERE  ob.oidobjectType=oType.oidobjectType and  ob.oidobjectType in (" & otListeTypeObj & ") and  otInternalNumber ='" & oRec(0).value & "'"  
    Set oRecP = PYPAM.ExecuteSql(otSql)
	if not oRecP.eof then
		'Extraction de l'objet
		if ovFormatLog=-1 then
			otListeObjGen=otListeObjGen & oRecP(1).value & vbTab & vbTab & oRecP(2).value & vbTab & vbTab & oRecP(3).value & vbTab & vbTab & oRecP(4).value & vbTab & vbTab & oRecP(5).value & vbCrLf
		else
			otListeObjGen=otListeObjGen & "<TR class=LineProperty><TD></TD><TD>" & iif(len(oRecP(1).value)>0,oRecP(1).value,"&nbsp;") & "</TD><TD>"  & iif(len(oRecP(2).value)>0,oRecP(2).value,"&nbsp;") & _
			 "</TD><TD>" & iif(len(oRecP(3).value)>0,oRecP(3).value,"&nbsp;") & "</TD><TD>" & iif(len(oRecP(4).value)>0,oRecP(4).value,"&nbsp;") & "</TD><TD>" & iif(len(oRecP(5).value)>0,oRecP(5).value,"&nbsp;") & "</TD></TR>"
		end if
	Else
		otMesErr = "L'objet n'existe pas dans PSD"
		if ovFormatLog=-1 then
			otListeObjErr=otListeObjErr &  "ERROR:" & otMesErr & ": " & oRec(0).value & vbTab & vbTab & "" & vbTab & vbTab & "" & vbCrLf  
		else							
			otListeObjErr=otListeObjErr &  "<TR class=LineProperty><TD>" & otMesErr & "</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>" & iif(len(oRec(0).value)>0,oRec(0).value,"&nbsp;") & "</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR>"	  
		end if
	End if
	err.clear
   	oRec.MoveNext
Loop   

if ovLog then
	otLog = ""
	if ovFormatLog=-1 then					
		otLog = vbCrLf & "-------------------------------------------------------------------------------------------------------" & vbCrLf & _
		"Type " & vbTab & vbTab & "Désignation" & vbTab & vbTab & "Numero interne"	& vbTab & vbTab & "Numero de série" & vbTab & vbTab & "Constructeur" & vbCrLf & _
													"-------------------------------------------------------------------------------------------------------" & vbCrL & otListeObjGen				
	else
		otLog = "<TABLE cellSpacing=0 cellPadding=1 width=""100%"" border=1><TR class=HeaderProperty><TD>Erreur</TD><TD>Type</TD><TD>Désignation</TD><TD>Numero interne</TD><TD>Numero de série</TD><TD>Constructeur</TD></TR><BR>" & otListeObjGen 
	end if
	if ovFormatLog=-1 then
		otLog = otLog & vbCrLf	& "-------------------------------------------------------------------------------------------------------" & vbCrLf & otListeObjErr	
	else
		otLog = otLog & otListeObjErr & "</TABLE>"	
	end if
	opColLog.Add otTransformation("Name") & "GEN" ,Array("Compte rendu des mises à jour générées par rapport à l'historique précédent", "0", otLog) 
end if

End Function

'////////////////////////////////////////////////////////////////////////////////////////////////////////
Function CreateObjectsHistory(otTransformation, ovLog, ovFormatLog)  

Dim otListeObjNew
Dim otListeObjErr
Dim otListeErr
Dim otListeTypeObj
Dim otSql
Dim oRec
Dim oRecP
Dim oContObj 
Dim oAction
Dim oObj
Dim otMesErr
Dim otTitre
Dim otLog

on error resume next
err.Clear
'Liste des types de l'object		
otListeTypeObj=otTransformation("ObjectTypes")
'Liste des objets à mettre à jour	
Set oRec=Nothing  
otListeObjUpd=""	    
otSql = "SELECT otKey FROM " & HistorySession(otTransformation("Name") & "_HistoryTable") & " WHERE oddate =  " & PYTOOLS.otDateSqlEx(HistorySession("__CurrentDate"), True, "", "Access97") & " AND otCode='N'" 
Set oRec = HistoryData.Execute(otSql)
If Err<>0 Then
	optLastError = Err.Description
	optLastErrorMessage = ""
	optLastNodeError = "NEW_Objects_Check"
	If ovLog = -1 Then
		WriteLog optLastError & Chr(9) & optLastErrorMessage & Chr(9) & optLastNodeError
	End If
	Exit Function
End If 
set oObj = nothing
err.clear
Do While Not oRec.EOF
	if err<>0 then exit do
    'Mise à jour des objets
	if ovFormatLog=-1 then
		otListeObjNew=otListeObjNew & oRec(0).value & vbCrLf
	else
		otListeObjNew=otListeObjNew & "<TR class=LineProperty><TD>" & oRec(0).value & "</TD></TR>"
	end if
	err.clear
   	oRec.MoveNext
Loop   

if ovLog then
	otLog = ""
	if ovFormatLog=-1 then					
		otLog = vbCrLf & "-------------------------------------------------------------------------------------------------------" & vbCrLf & _
			"Type " & vbTab & vbTab &  "Numero interne"	& vbcrlf & _
			"-------------------------------------------------------------------------------------------------------" & vbCrLf & otListeObjNew				
	else
		otLog = "<TABLE cellSpacing=0 cellPadding=1 width=""100%"" border=1><TR class=HeaderProperty><TD>Numero interne</TD></TR><BR>" & otListeObjNew & "</TABLE>"
	end if
	opColLog.Add otTransformation("Name") & "NEW" ,Array("Compte rendu des créations par rapport à l'historique précédent", "0", otLog) 
end if

End Function

'/////////////////////////////////////////////////////////////////////
Function otConvertADOToClause(ByVal oType, ByVal oValue, ByVal oInfo, ByVal otField, ByVal otOperator)

Dim otADODataName
Dim otConvert
Dim ov
Dim otSep
Dim otClause
Dim otBeg
Dim otEnd
Dim otFie
Dim otOp

On Error Resume Next

Select Case oType
    Case 0 'Specifies no value (DBTYPE_EMPTY).
        otADODataName = "adEmpty"
        otConvert = "'" & PYTOOLS.otDoubleQuote(oValue) & "'"
    Case 2 'Indicates a two-byte signed integer (DBTYPE_I2).
        otADODataName = "adSmallInt"
        otConvert = otNumeric(CInt(oValue))
    Case 3 'Indicates a four-byte signed integer (DBTYPE_I4).
        otADODataName = "adInteger"
        otConvert = otNumeric(Clng(oValue))
    Case 4 'Indicates a single-precision floating-point value (DBTYPE_R4).
        otADODataName = "adSingle"
        otConvert = otNumeric(CSng(oValue))
    Case 5 'Indicates a double-precision floating-point value (DBTYPE_R8).
        otADODataName = "adDouble"
        otConvert = otNumeric(CDbl(oValue))
    Case 6 'Indicates a currency value (DBTYPE_CY). Currency is a fixed-point number with four digits to the right of the decimal point. It is stored in an eight-byte signed integer scaled by 10,000.
        otADODataName = "adCurrency"
        otConvert = otNumeric(CCur(oValue))
    Case 7 'Indicates a date value (DBTYPE_DATE). A date is stored as a double, the whole part of which is the number of days since December 30, 1899, and the fractional part of which is the fraction of a day.
        otADODataName = "adDate"
        otConvert = Format(CVDate(oValue), "\#mm\/dd\/yyyy\#")
    Case 8 'Indicates a null-terminated character string (Unicode) (DBTYPE_BSTR).
        otADODataName = "adBSTR"
        otConvert = "'" & CStr(oValue) & "'"
    Case 9 'Indicates a pointer to an IDispatch interface on a COM object (DBTYPE_IDISPATCH).Note___This data type is currently not supported by ADO. Usage may cause unpredictable results.
        otADODataName = "adIDispatch"
        otConvert = "0"
    Case 10 'Indicates a 32-bit error code (DBTYPE_ERROR).
        otADODataName = "adError"
        otConvert = "0"
    Case 11 'Indicates a boolean value (DBTYPE_BOOL).
        otADODataName = "adBoolean"
        otConvert = CStr(CInt(oValue))
    Case 12 'Indicates an Automation Variant (DBTYPE_VARIANT).Note___This data type is currently not supported by ADO. Usage may cause unpredictable results.
        otADODataName = "adVariant"
        If oInfo <> 1 Then
            If IsArray(oValue) Then
                otConvert = ""
                otSep = ""
                If VarType(oValue(0)) = vbString Then otSep = "'"
                If StrComp(Trim(otOperator), "IN", 1) = 0 Then
                    otClause = ","
                    otBeg = ""
                    otEnd = ""
                    otFie = ""
                    otOp = ""
                Else
                    otClause = " OR "
                    otBeg = "("
                    otEnd = ")"
                    otOp = otOperator
                    otFie = otField
                End If
                For Each ov In oValue
                    If Len(otConvert) > 0 Then otConvert = otConvert & otClause
                    otConvert = otConvert & otBeg & otFie & otOp & otSep & PYTOOLS.otDoubleQuote(CStr(ov)) & otSep & otEnd
                Next
                If StrComp(otOperator, "IN", 1) = 0 Then
                    otConvert = otField & "In(" & otConvert & ")"
                End If
                otField = ""
                otOperator = ""
            Else
                If VarType(oValue) = vbString Then
                    otConvert = "'" & CStr(oValue) & "'"
                ElseIf VarType(oValue) = vbDate Then
                    otConvert = Format(CVDate(oValue), "\#mm\/dd\/yyyy\#")
                Else
                    otConvert = otNumeric(CDbl(oValue))
                End If
            End If
        End If
    Case 13 'Indicates a pointer to an IUnknown interface on a COM object (DBTYPE_IUNKNOWN).Note___This data type is currently not supported by ADO. Usage may cause unpredictable results.
        otADODataName = "adIUnknown"
        otConvert = "0"
    Case 14 'Indicates an exact numeric value with a fixed precision and scale (DBTYPE_DECIMAL).
        otADODataName = "adDecimal"
        otConvert = CStr(CLng(oValue))
    Case 16 'Indicates a one-byte signed integer (DBTYPE_I1).
        otADODataName = "adTinyInt"
        otConvert = CStr(CInt(oValue))
    Case 17 'Indicates a one-byte unsigned integer (DBTYPE_UI1).
        otADODataName = "adUnsignedTinyInt"
        otConvert = CStr(CByte(oValue))
    Case 18 'Indicates a two-byte unsigned integer (DBTYPE_UI2).
        otADODataName = "adUnsignedSmallInt"
        otConvert = CStr(CLng(oValue))
    Case 19 'Indicates a four-byte unsigned integer (DBTYPE_UI4).
        otADODataName = "adUnsignedInt"
        otConvert = CStr(CLng(oValue))
    Case 20 'Indicates an eight-byte signed integer (DBTYPE_I8).
        otADODataName = "adBigInt"
        otConvert = CStr(oValue)
    Case 21 'Indicates an eight-byte unsigned integer (DBTYPE_UI8).
        otADODataName = "adUnsignedBigInt"
        otConvert = CStr(oValue)
    Case 64 'Indicates a 64-bit value representing the number of 100-nanosecond intervals since January 1, 1601 (DBTYPE_FILETIME).
        otADODataName = "adFileTime"
        otConvert = CStr(oValue)
    Case 72 'Indicates a globally unique identifier (GUID) (DBTYPE_GUID).
        otADODataName = "adGUID"
        otConvert = "'" & CStr(oValue) & "'"
    Case 128 'Indicates a binary value (DBTYPE_BYTES).
        otADODataName = "adBinary"
        otConvert = "0"
    Case 129 'Indicates a string value (DBTYPE_STR).
        otADODataName = "adChar"
        otConvert = "'" & PYTOOLS.otDoubleQuote(CStr(oValue)) & "'"
    Case 130 'Indicates a null-terminated Unicode character string (DBTYPE_WSTR).
        otADODataName = "adWChar"
        otConvert = "'" & PYTOOLS.otDoubleQuote(CStr(oValue)) & "'"
    Case 131 'Indicates an exact numeric value with a fixed precision and scale (DBTYPE_NUMERIC).
        otADODataName = "adNumeric"
        otConvert = otNumeric(oValue)
    Case 133 'Indicates a date value (yyyymmdd) (DBTYPE_DBDATE).
        otADODataName = "adDBDate"
        otConvert = CStr(oValue)
    Case 134 'Indicates a time value (hhmmss) (DBTYPE_DBTIME).
        otADODataName = "adDBTime"
        otConvert = CStr(oValue)
    Case 135 'Indicates a date/time stamp (yyyymmddhhmmss plus a fraction in billionths) (DBTYPE_DBTIMESTAMP).
        otADODataName = "adDBTimeStamp"
        otConvert = CStr(oValue)
    Case 136 'Indicates a four-byte chapter value that identifies rows in a child rowset (DBTYPE_HCHAPTER).
        otADODataName = "adChapter"
        otConvert = "0"
    Case 138 'Indicates an Automation PROPVARIANT (DBTYPE_PROP_VARIANT).
        otADODataName = "adPropVariant"
        otConvert = "0"
    Case 139 'Indicates a numeric value (Parameter object only).
        otADODataName = "adVarNumeric"
        otConvert = otNumeric(oValue)
    Case 200 'Indicates a string value (Parameter object only).
        otADODataName = "adVarChar"
        otConvert = "'" & PYTOOLS.otDoubleQuote(CStr(oValue)) & "'"
    Case 201 'Indicates a long string value (Parameter object only).
        otADODataName = "adLongVarChar"
        otConvert = "'" & PYTOOLS.otDoubleQuote(CStr(oValue)) & "'"
    Case 202 'Indicates a null-terminated Unicode character string (Parameter object only).
        otADODataName = "adVarWChar"
        otConvert = "'" & PYTOOLS.otDoubleQuote(CStr(oValue)) & "'"
    Case 203 'Indicates a long null-terminated Unicode string value (Parameter object only).
        otADODataName = "adLongVarWChar"
        otConvert = "'" & PYTOOLS.otDoubleQuote(CStr(oValue)) & "'"
    Case 205 'Indicates a long binary value (Parameter object only).
        otADODataName = "adLongVarBinary"
        otConvert = "0"
    Case 132 'Indicates a user-defined variable (DBTYPE_UDT).
        otADODataName = "adUserDefined"
        otConvert = "0"
    Case 204 'Indicates a binary value (Parameter object only).
        otADODataName = "adVarBinary"
        otConvert = "0"
    Case Else
        otADODataName = "Unknown"
        otConvert = CStr(oValue)
End Select
Select Case oInfo
    Case 0
        'Renvoi la valeur uniquement
        otConvertADOToClause = otField & Trim(otOperator) & otConvert
    Case 1
        'Renvoi le nom du type
        otConvertADOToClause = otADODataName
End Select
Err.Clear

End Function

Function otNumeric(ByVal oValue)
'Remplace le caractère décimal par un .
otNumeric = Replace(CStr(oValue), otSepDec, ".")
End Function

Function otSepDec()
'Renvoi le caractère séparateur numérique
otSepDec = Trim(Replace(CStr(3.3), "3", ""))

End Function

Sub WriteDate (odDate)
	' écrit odDate dans fichierINI
	On Error Resume Next
	err.clear                                          
	PyTools.ovaPrivateIni cstr(fichierINI),"Inventaire","Date",cstr(odDate),False
	err.clear		
End Sub
' cherche si nomsoft fait partie de la liste des exclusions de soft
' 21/11/11 fonction inutilisée, le filtre est fait dans OCS

Function bvExclusion(nomsoft)
bvExclusion=0

if left(lcase(nomsoft), 11) = "mise à jour" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 2) = "kb" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 6) = "update" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 12) = "adobe reader" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 5) = "7-zip" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 11) = "adobe flash" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 10) = "bluesoleil" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 8) = "cartohub" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 6) = "citrix" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 9) = "correctif" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 4) = "divx" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 3) = "dvd" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 5) = "epson" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 4) = "free" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 4) = "gimp" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 6) = "hotfix" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 2) = "hp" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 5) = "intel" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 10) = "intervideo" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 4) = "j2se" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 4) = "java" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 7) = "lecteur" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 14) = "microsoft .net" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 21) = "microsoft compression" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 6) = "module" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 5) = "msxml" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 6) = "nvidia" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 3) = "ocs" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 8) = "oscagent" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 10) = "pdfcreator" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 7) = "realtek" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 8) = "security" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 5) = "think" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 3) = "vc8" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 3) = "vlc" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 17) = "windows installer" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 13) = "windows media" then bvExclusion=1 : exit function
if left(lcase(nomsoft), 3) = "xnv" then bvExclusion=1 : exit function

end function
