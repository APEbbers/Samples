﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace SheetMetal
	''' <summary>
	''' Creates a new sheetmetal document.
	''' </summary>
	Friend Class CreateSheetMetalDocument
		Private Shared Sub RunSample(ByVal breakOnStart As Boolean)
			If breakOnStart Then
				System.Diagnostics.Debugger.Break()
			End If

			Dim application As SolidEdgeFramework.Application = Nothing
			Dim documents As SolidEdgeFramework.Documents = Nothing
			Dim sheetMetalDocument As SolidEdgePart.SheetMetalDocument = Nothing

			Try
				' Register with OLE to handle concurrency issues on the current thread.
				SolidEdgeCommunity.OleMessageFilter.Register()

				' Connect to or start Solid Edge.
				application = SolidEdgeCommunity.SolidEdgeUtils.Connect(True, True)

				' Get a reference to the documents collection.
				documents = application.Documents

				' Create a new sheet metal document.
				sheetMetalDocument = DirectCast(documents.Add(SolidEdgeSDK.PROGID.SolidEdge_SheetMetalDocument), SolidEdgePart.SheetMetalDocument)
			Catch ex As System.Exception
				Console.WriteLine(ex.Message)
			Finally
				SolidEdgeCommunity.OleMessageFilter.Unregister()
			End Try
		End Sub
	End Class
End Namespace
