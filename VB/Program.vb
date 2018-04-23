Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace UpperFunction
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
		<STAThread> _
		Shared Sub Main()
			CType(New UpperFunction(), UpperFunction).Run()
		End Sub
	End Class
End Namespace