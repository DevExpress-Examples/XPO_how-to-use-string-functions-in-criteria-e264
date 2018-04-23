Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Data.Filtering

Namespace UpperFunction
	Public Class UpperFunction
		Public Sub Run()
			Dim dl As IDataLayer = XpoDefault.GetDataLayer(AutoCreateOption.DatabaseAndSchema)
			Dim uof As New UnitOfWork(dl)
			uof.ClearDatabase()
			uof.UpdateSchema()
			Dim obj1 As New MyObject(uof)
			obj1.MyCaseSensitiveProperty = "ab"
			Dim obj2 As New MyObject(uof)
			obj2.MyCaseSensitiveProperty = "AB"
			Dim obj3 As New MyObject(uof)
			obj3.MyCaseSensitiveProperty = "aB"
			Dim obj4 As New MyObject(uof)
			obj4.MyCaseSensitiveProperty = "BC"
			Dim obj5 As New MyObject(uof)
			obj5.MyCaseSensitiveProperty = "Cb"
			uof.CommitChanges()

			' Test UPPER
			Dim filter As CriteriaOperator = CriteriaOperator.Parse("Upper([MyCaseSensitiveProperty]) = ?", "AB")
			Dim collection As XPCollection(Of MyObject) = New XPCollection(Of MyObject)(uof, filter)

			' Test LOWER
			filter = CriteriaOperator.Parse("Lower([MyCaseSensitiveProperty]) = ?", "ab")
			collection = New XPCollection(Of MyObject)(uof, filter)

			' Test SUBSTRING
			filter = CriteriaOperator.Parse("Substring([MyCaseSensitiveProperty], 0, 1) = ?", "c")
			collection = New XPCollection(Of MyObject)(uof, filter)
		End Sub
	End Class

	Public Class UpperFunctionTest
		Inherits UpperFunction
		Public Shadows Sub Run()
			MyBase.Run()
		End Sub
	End Class

	Friend Class MyObject
		Inherits XPObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		Private myCaseSensitiveValue As String
		Public Property MyCaseSensitiveProperty() As String
			Get
				Return myCaseSensitiveValue
			End Get
			Set(ByVal value As String)
				SetPropertyValue("MyCaseSensitiveProperty", myCaseSensitiveValue, value)
			End Set
		End Property
	End Class
End Namespace
