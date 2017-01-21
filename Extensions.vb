Imports System.IO
Imports System.IO.Compression
Imports System.Security.Cryptography
Public Module TypeExt
    <System.Runtime.CompilerServices.Extension>
    Public Function Insert(src As Byte(), sequence As Byte()) As Byte()
        Dim buffer() As Byte = New Byte() {}
        Array.Resize(Of Byte)(buffer, src.Length + sequence.Length)
        sequence.CopyTo(buffer, 0)
        src.CopyTo(buffer, sequence.Length)
        Return buffer
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function Insert(src As Byte(), sequence As Byte(), index As Integer) As Byte()
        Dim buffer() As Byte = New Byte() {}
        Dim srcbegin() As Byte = New Byte() {}
        Dim srcend() As Byte = New Byte() {}
        Array.Resize(Of Byte)(srcbegin, index)
        Array.Resize(Of Byte)(srcend, src.Length - index)
        Array.Resize(Of Byte)(buffer, src.Length + sequence.Length)
        Array.Copy(src, 0, srcbegin, 0, index)
        Array.Copy(src, index, srcend, 0, src.Length - index)
        srcbegin.CopyTo(buffer, 0)
        sequence.CopyTo(buffer, index)
        srcend.CopyTo(buffer, index + sequence.Length)
        Return buffer
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function Append(ByRef src As Byte(), sequence As Byte()) As Byte()
        Array.Resize(Of Byte)(src, src.Length + sequence.Length)
        sequence.CopyTo(src, src.Length - sequence.Length)
        Return src
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
    Public Function ToSHA256Checksum(src() As Byte) As Byte()
        Using Cryptograph As Security.Cryptography.SHA256 = Security.Cryptography.SHA256.Create()
            Return Cryptograph.ComputeHash(src)
        End Using
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function ToSHA384Checksum(src() As Byte) As Byte()
        Using Cryptograph As Security.Cryptography.SHA384 = Security.Cryptography.SHA384.Create()
            Return Cryptograph.ComputeHash(src)
        End Using
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function ToSHA512Checksum(src() As Byte) As Byte()
        Using Cryptograph As Security.Cryptography.SHA512 = Security.Cryptography.SHA512.Create()
            Return Cryptograph.ComputeHash(src)
        End Using
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