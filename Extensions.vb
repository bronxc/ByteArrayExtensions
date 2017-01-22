Imports System.IO
Imports System.IO.Compression
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Public Module TypeExt
    <System.Runtime.CompilerServices.Extension>
    Public Function QuickCast(Of T)(src As Byte()) As Object
        Dim dataPtr As IntPtr = IntPtr.Zero
        Try
            If src.Length <> 0 Then
                dataPtr = Marshal.AllocHGlobal(src.Length)
                Marshal.Copy(src, 0, dataPtr, src.Length)
                Return Marshal.PtrToStructure(dataPtr, GetType(T))
            End If
            Throw New Exception("Structure length mismatch")
        Finally
            If (dataPtr <> IntPtr.Zero) Then Marshal.FreeHGlobal(dataPtr)
        End Try
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function Insert(src As Byte(), sequence As Byte()) As Byte()
        Dim buffer() As Byte = New Byte(src.Length + sequence.Length - 1) {}
        sequence.CopyTo(buffer, 0)
        src.CopyTo(buffer, sequence.Length)
        Return buffer
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function Insert(src As Byte(), sequence As Byte(), index As Integer) As Byte()
        Dim srcbegin() As Byte = New Byte(index) {}
        Dim srcend() As Byte = New Byte(src.Length - index - 1) {}
        Dim buffer() As Byte = New Byte(src.Length + sequence.Length - 1) {}
        Array.Copy(src, 0, srcbegin, 0, index)
        Array.Copy(src, index, srcend, 0, src.Length - index)
        srcbegin.CopyTo(buffer, 0)
        sequence.CopyTo(buffer, index)
        srcend.CopyTo(buffer, index + sequence.Length)
        Return buffer
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function InsertAfter(src As Byte(), func As Func(Of Byte(), Byte()), sequence As Byte(), Optional index As Integer = 0) As Byte()
        Return src.Insert(func.Invoke(src), index).Insert(sequence, index)
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function Truncate(src As Byte(), fixedLen As Integer) As Byte()
        If (fixedLen <= src.Length) Then
            Dim buffer() As Byte = New Byte(fixedLen) {}
            Array.Copy(src, 0, buffer, 0, fixedLen)
            Return buffer
        End If
        Throw New Exception("length must be within the range of the array")
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function Peek(src As Byte(), index As Integer, len As Integer) As Byte()
        If (index <= src.Length AndAlso index + len <= src.Length) Then
            Dim buffer() As Byte = New Byte(len - 1) {}
            Array.Copy(src, index, buffer, 0, len)
            Return buffer
        End If
        Throw New Exception("index and length must be within the range of the array")
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function Append(ByRef src As Byte(), sequence As Byte()) As Byte()
        Dim buffer() As Byte = New Byte(src.Length + sequence.Length - 1) {}
        src.CopyTo(buffer, 0)
        sequence.CopyTo(buffer, src.Length)
        Return buffer
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function NibbleEnd(src As Byte(), len As Integer) As Byte()
        Dim buffer() As Byte = New Byte((src.Length - 1) - len) {}
        Array.Copy(src, buffer, src.Length - len)
        Return buffer
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function NibbleTop(src As Byte(), len As Integer) As Byte()
        Dim buffer() As Byte = New Byte((src.Length - 1) - len) {}
        Array.Copy(src, len, buffer, 0, buffer.Length)
        Return buffer
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function BeginsWith(src() As Byte, sequence As Byte()) As Boolean
        If (src.Length >= sequence.Length) Then
            Dim buffer() As Byte = New Byte(sequence.Length - 1) {}
            Array.Copy(src, 0, buffer, 0, sequence.Length)
            Return buffer.SequenceEqual(sequence)
        End If
        Return False
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function EndsWith(src() As Byte, sequence As Byte()) As Boolean
        If (src.Length >= sequence.Length) Then
            Dim buffer() As Byte = New Byte(sequence.Length - 1) {}
            Array.Copy(src, src.Length - sequence.Length, buffer, 0, sequence.Length)
            Return buffer.SequenceEqual(sequence)
        End If
        Return False
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Sub SaveAs(src As Byte(), filename As String, Optional Overwrite As Boolean = False)
        If (src IsNot Nothing) Then
            If (File.Exists(filename) AndAlso Overwrite) Then File.Delete(filename)
            Using bw As New BinaryWriter(File.Open(filename, FileMode.OpenOrCreate))
                bw.Write(src)
            End Using
        End If
    End Sub

    <Runtime.CompilerServices.Extension>
    Public Function ToHexString(src() As Byte) As String
        Return String.Join(String.Empty, Array.ConvertAll(src, Function(b) b.ToString("X2")))
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function Randomize(ByRef src() As Byte) As Byte()
        Using rng As New Security.Cryptography.RNGCryptoServiceProvider()
            rng.GetNonZeroBytes(src)
            Return src
        End Using
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function Checksum(src() As Byte, provider As HashAlgorithm) As Byte()
        If (provider IsNot Nothing) Then
            Return provider.ComputeHash(src)
        End If
        Throw New NullReferenceException()
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function ToSHA256(src() As Byte) As Byte()
        Using Cryptograph As Security.Cryptography.SHA256 = Security.Cryptography.SHA256.Create()
            Return Cryptograph.ComputeHash(src)
        End Using
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function ToSHA384(src() As Byte) As Byte()
        Using Cryptograph As Security.Cryptography.SHA384 = Security.Cryptography.SHA384.Create()
            Return Cryptograph.ComputeHash(src)
        End Using
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function ToSHA512(src() As Byte) As Byte()
        Using Cryptograph As Security.Cryptography.SHA512 = Security.Cryptography.SHA512.Create()
            Return Cryptograph.ComputeHash(src)
        End Using
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function ToCrc32(src() As Byte) As UInt32
        Dim offset As UInt32 = 0
        Dim crc As UInt32 = &HFFFFFFFFUI
        Dim values() As UInt32 = New UInt32(255) {}
        For x As UInt32 = 0 To Convert.ToUInt32(values.Length - 1)
            offset = x
            For y As Integer = 8 To 1 Step -1
                If (offset And 1) = 1 Then
                    offset = CUInt((offset >> 1) Xor &HEDB88320UI)
                Else
                    offset >>= 1
                End If
            Next
            values(Convert.ToInt32(x)) = offset
        Next
        For i As Integer = 0 To src.Length - 1
            Dim index As Byte = CByte(((crc) And &HFF) Xor src(i))
            crc = CUInt((crc >> 8) Xor values(index))
        Next
        Return Not crc
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function Compress(src() As Byte, Optional CompressionLevel As CompressionLevel = CompressionLevel.Fastest) As Byte()
        Using ms As New MemoryStream
            Using gzs As New DeflateStream(ms, CompressionLevel, True)
                gzs.Write(src, 0, src.Length)
            End Using
            Return ms.ToArray
        End Using
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function Decompress(src() As Byte) As Byte()
        Using gzs As New DeflateStream(New MemoryStream(src), CompressionMode.Decompress)
            Dim length As Integer = 0, buffer As Byte() = New Byte(&H400) {}
            Using ms As New MemoryStream
                Do
                    length = gzs.Read(buffer, 0, &H400)
                    If length > 0 Then
                        ms.Write(buffer, 0, length)
                    End If
                Loop While length > 0
                Return ms.ToArray()
            End Using
        End Using
    End Function
End Module