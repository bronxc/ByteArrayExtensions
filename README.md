# Byte Array Extensions

- Insert (Variant 1)

Inserts a sequence at the top of a byte array

Returns: Byte Array
```
Byte().Insert(<sequence>)
```

- Insert (Variant 2)

Inserts a sequence at a given index of a byte array

Returns: Byte Array
```
Byte().Insert(<sequence>,index)
```

- Append

Appends a sequence to the end of a byte array

Returns: Byte Array
```
Byte().Append(<sequence>)
```

- BeginsWith

Checks if the byte array begins with a sequence

Returns: Boolean
```
Byte().BeginsWith(<sequence>)
```

- EndsWith

Checks if the byte array ends with a sequence

Returns: Boolean
```
Byte().EndsWith(<sequence>)
```

- NibbleTop

Removes bytes starting from the top with given length

Returns: Byte Array
```
Byte().NibbleTop(<length>)
```

- NibbleEnd

Removes bytes starting from the end with given length

Returns: Byte Array
```
Byte().NibbleEnd(<length>)
```

- ToHexString

Creates a hexadecimal string format of the byte array

Returns: String
```
Byte().ToHexString()
```

- Randomize

Creates non-zero based bytes within a byte array

Returns: Byte Array
```
Byte().Randomize()
```

- Checksum

Creates a checksum from the byte array, providing a HashAlgorithm instance

Returns: Byte Array
```
Byte().Checksum(<provider>)
```

- ToSHA256Checksum

Creates a SHA256 checksum from the byte array

Returns: Byte Array
```
Byte().ToSHA256Checksum()
```

- ToSHA384Checksum[/b]

Creates a SHA384 checksum from the byte array

Returns: Byte Array
```
Byte().ToSHA384Checksum()
```

- ToSHA512Checksum

Creates a SHA512 checksum from the byte array

Returns: Byte Array
```
Byte().ToSHA512Checksum()
```

- Compress

Compresses the byte array with built-in GZip algorithm (Deflation variant)

Returns: Byte Array
```
Byte().Compress(<optional:CompressionLevel[none,fast,optimal]>)
```

- Decompress

Decompresses the byte array with built-in GZip algorithm (Deflation variant)

Returns: Byte Array
```
Byte().Decompress()
```
