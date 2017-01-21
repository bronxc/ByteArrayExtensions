# Byte Array Extensions

- Insert Variant 1

Inserts a sequence at the top of a byte array
Returns: Byte Array
````
Byte().Insert(<sequence>)
````

- Insert Variant 2

Inserts a sequence at a given index of a byte array
Returns: Byte Array
````
Byte().Insert(<sequence>,index)
````

- Append

Appends a sequence to the end of a byte array
Returns: Byte Array
````
Byte().Append(<sequence>)
````

[b]BeginsWith[/b]
Checks if the byte array begins with the sequence
Returns: Boolean
````
Byte().BeginsWith(<sequence>)
````

[b]EndsWith[/b]
Checks if the byte array ends with the sequence
Returns: Boolean
````
Byte().EndsWith(<sequence>)
````

[b]NibbleTop[/b]
Removes bytes starting from the top with given length
Returns: Byte Array
````
Byte().NibbleTop(<length>)
````

[b]NibbleEnd[/b]
Removes bytes starting from the end with given length
Returns: Byte Array
````
Byte().NibbleEnd(<length>)
````

[b]ToHexString[/b]
Creates a hexadecimal string format of the byte array
Returns: String
````
Byte().ToHexString()
````

[b]Randomize[/b]
Creates non-zero based bytes within a byte array
Returns: Byte Array
````
Byte().Randomize()
````

[b]Checksum[/b]
Creates a checksum from the byte array, providing a HashAlgorithm instance
Returns: Byte Array
````
Byte().Checksum(<provider>)
````

[b]ToSHA256Checksum[/b]
Creates a SHA256 checksum from the byte array
Returns: Byte Array
````
Byte().ToSHA256Checksum()
````

[b]ToSHA384Checksum[/b]
Creates a SHA384 checksum from the byte array
Returns: Byte Array
````
Byte().ToSHA384Checksum()
````

[b]ToSHA512Checksum[/b]
Creates a SHA512 checksum from the byte array
Returns: Byte Array
````
Byte().ToSHA512Checksum()
````

[b]Compress[/b]
Compresses the byte array with built-in GZip algorithm (Deflation variant)
Returns: Byte Array
````
Byte().Compress(<optional:CompressionLevel[none,fast,optimal]>)
````

[b]Decompress[/b]
Decompresses the byte array with built-in GZip algorithm (Deflation variant)
Returns: Byte Array
````
Byte().Decompress()
````
