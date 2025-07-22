# Encryption and Decryption
Encryption and decryption are fundamental concepts in cybersecurity, used to protect data confidentiality, integrity, and authenticity. In .NET C\#, the `System.Security.Cryptography` namespace provides a rich set of classes and methods to implement various cryptographic operations.

Let's break down how encryption and decryption work in .NET C\#, covering the key types and related concepts:

### I. Core Concepts

1.  **Encryption:** The process of transforming plaintext (readable data) into ciphertext (unreadable, encoded data) using an algorithm and a key.
2.  **Decryption:** The reverse process of transforming ciphertext back into plaintext using the correct key and algorithm.
3.  **Key:** A piece of information (a string of bits) that is used by an algorithm to transform data. The security of the encryption largely depends on the strength and secrecy of the key.
4.  **Algorithm:** The mathematical procedure used for encryption and decryption.

### II. Types of Encryption

There are two main types of encryption:

#### A. Symmetric Encryption

  * **How it works:** Uses a **single, secret key** for both encryption and decryption.
  * **Analogy:** Like a strongbox with one key that both locks and unlocks it.
  * **Key Characteristics:**
      * **Speed:** Generally much faster than asymmetric encryption, making it suitable for encrypting large amounts of data (files, streams).
      * **Key Management:** The biggest challenge is securely sharing the secret key between the sender and receiver. If the key is compromised, the entire communication is compromised.
  * **Common Algorithms in .NET:**
      * **AES (Advanced Encryption Standard):** The most widely used and recommended symmetric algorithm. It supports key sizes of 128, 192, and 256 bits.
      * **DES (Data Encryption Standard) / TripleDES (3DES):** Older algorithms. DES is considered insecure for modern use. TripleDES is more secure than DES but is slower than AES and generally deprecated in favor of AES.
  * **Initialization Vector (IV):**
      * A random, non-secret value used with block ciphers (like AES) to ensure that identical plaintext blocks produce different ciphertext blocks. This prevents patterns from appearing in the ciphertext and enhances security.
      * The IV does *not* need to be kept secret but *must* be unique for each encryption operation (or at least each key-IV pair) and transmitted along with the ciphertext.

**Example: AES Encryption/Decryption in C\#**

```csharp
using System.Security.Cryptography;
using System.Text;

public class AesEncryption
{
    // Generate a new random AES key
    public static byte[] GenerateKey()
    {
        using (Aes aes = Aes.Create())
        {
            aes.GenerateKey();
            return aes.Key;
        }
    }

    // Generate a new random IV
    public static byte[] GenerateIV()
    {
        using (Aes aes = Aes.Create())
        {
            aes.GenerateIV();
            return aes.IV;
        }
    }

    public static string EncryptString(string plainText, byte[] key, byte[] iv)
    {
        if (string.IsNullOrEmpty(plainText))
            throw new ArgumentNullException(nameof(plainText));
        if (key == null || key.Length == 0)
            throw new ArgumentNullException(nameof(key));
        if (iv == null || iv.Length == 0)
            throw new ArgumentNullException(nameof(iv));

        byte[] encrypted;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        // Return the encrypted bytes as a Base64 string for easier storage/transmission
        return Convert.ToBase64String(encrypted);
    }

    public static string DecryptString(string cipherText, byte[] key, byte[] iv)
    {
        if (string.IsNullOrEmpty(cipherText))
            throw new ArgumentNullException(nameof(cipherText));
        if (key == null || key.Length == 0)
            throw new ArgumentNullException(nameof(key));
        if (iv == null || iv.Length == 0)
            throw new ArgumentNullException(nameof(iv));

        // Convert base64 string to byte array
        byte[] buffer = Convert.FromBase64String(cipherText);

        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(buffer))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        return plaintext;
    }

    public static void Main(string[] args)
    {
        string original = "Here is some sensitive data that needs to be encrypted!";

        // In a real application, store and manage keys securely (e.g., Azure Key Vault)
        // Never hardcode keys!
        byte[] key = GenerateKey();
        byte[] iv = GenerateIV(); // Generate a new IV for each encryption

        Console.WriteLine($"Original Text: {original}");
        Console.WriteLine($"Key (Base64): {Convert.ToBase64String(key)}");
        Console.WriteLine($"IV (Base64): {Convert.ToBase64String(iv)}");

        string encryptedText = EncryptString(original, key, iv);
        Console.WriteLine($"Encrypted Text (Base64): {encryptedText}");

        string decryptedText = DecryptString(encryptedText, key, iv);
        Console.WriteLine($"Decrypted Text: {decryptedText}");
    }
}
```

#### B. Asymmetric Encryption (Public-Key Cryptography)

  * **How it works:** Uses a **pair of mathematically related keys**: a **public key** and a **private key**.
      * Data encrypted with the **public key** can *only* be decrypted with the corresponding **private key**.
      * Data signed with the **private key** can be *verified* with the corresponding **public key** (used for digital signatures, not direct encryption for confidentiality).
  * **Analogy:** Like a mailbox. Anyone can put a letter in (encrypt with public key), but only the person with the specific key can open it (decrypt with private key).
  * **Key Characteristics:**
      * **Speed:** Much slower than symmetric encryption, so typically used for small amounts of data, like encrypting symmetric keys or for digital signatures.
      * **Key Management:** Solves the key distribution problem. The public key can be freely distributed, while the private key must be kept secret by its owner.
  * **Common Algorithms in .NET:**
      * **RSA (Rivest–Shamir–Adleman):** The most common asymmetric algorithm. Supports key sizes from 1024 bits up to 4096 bits (or more).
      * **ECDSA (Elliptic Curve Digital Signature Algorithm):** Used for digital signatures, often more efficient than RSA for the same level of security.
  * **Uses:**
      * **Secure Key Exchange:** Encrypting a symmetric key with the recipient's public key, then sending the encrypted symmetric key along with the symmetrically encrypted data.
      * **Digital Signatures:** Proving data authenticity and integrity (covered below).
      * **Authentication:** Verifying identity.

**Example: RSA Encryption/Decryption in C\#**

```csharp
using System.Security.Cryptography;
using System.Text;

public class RsaEncryption
{
    public static (RSAParameters publicKey, RSAParameters privateKey) GenerateKeyPair()
    {
        using (RSA rsa = RSA.Create())
        {
            // Export public key parameters
            RSAParameters publicKey = rsa.ExportParameters(false);
            // Export private key parameters (sensitive!)
            RSAParameters privateKey = rsa.ExportParameters(true);
            return (publicKey, privateKey);
        }
    }

    public static byte[] Encrypt(byte[] dataToEncrypt, RSAParameters publicKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(publicKey);
            // OAEP padding is recommended for encryption
            return rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);
        }
    }

    public static byte[] Decrypt(byte[] dataToDecrypt, RSAParameters privateKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(privateKey);
            // Use the same padding as encryption
            return rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA256);
        }
    }

    public static void Main(string[] args)
    {
        // Typically, RSA is used to encrypt a symmetric key, not large data directly.
        string originalMessage = "Secret AES Key for Session!"; // Max length limited by key size (e.g., 256 bytes for RSA 2048)
        byte[] originalBytes = Encoding.UTF8.GetBytes(originalMessage);

        (RSAParameters publicKey, RSAParameters privateKey) = GenerateKeyPair();

        Console.WriteLine("Original Message: " + originalMessage);

        // Encrypt with public key
        byte[] encryptedBytes = Encrypt(originalBytes, publicKey);
        Console.WriteLine("Encrypted Bytes (Base64): " + Convert.ToBase64String(encryptedBytes));

        // Decrypt with private key
        byte[] decryptedBytes = Decrypt(encryptedBytes, privateKey);
        string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);
        Console.WriteLine("Decrypted Message: " + decryptedMessage);
    }
}
```

### III. Hashing

Hashing is **not encryption**; it's a **one-way process** to transform data into a fixed-size string of characters (a hash or message digest). It's designed to be irreversible.

  * **Purpose:** Primarily for **data integrity verification** and **password storage**.
  * **Key Characteristics:**
      * **One-Way:** Cannot be reversed to get the original data.
      * **Fixed Size:** The output hash always has the same length, regardless of the input size.
      * **Collision Resistance:** It should be computationally infeasible to find two different inputs that produce the same hash (a "collision").
  * **Common Algorithms in .NET:**
      * **SHA256, SHA512 (Secure Hash Algorithm):** Recommended for general-purpose hashing, including password storage (when combined with salting).
      * **MD5 (Message Digest 5):** Older and cryptographically broken (susceptible to collisions), **should not be used for security-sensitive applications** like password hashing or digital signatures.
  * **Salting:** When hashing passwords, a unique, random "salt" is added to each password before hashing. This prevents "rainbow table" attacks.

**Example: SHA256 Hashing with Salting in C\#**

```csharp
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    // Generate a cryptographically strong random salt
    public static byte[] GenerateSalt(int length = 16) // 16 bytes is common for SHA256
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] salt = new byte[length];
            rng.GetBytes(salt);
            return salt;
        }
    }

    // Hash a password with a salt
    public static byte[] HashPassword(string password, byte[] salt)
    {
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
        if (salt == null || salt.Length == 0) throw new ArgumentNullException(nameof(salt));

        // Combine password bytes and salt bytes
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

        // Compute the hash
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(saltedPassword);
        }
    }

    // Verify a password against a stored hash and salt
    public static bool VerifyPassword(string enteredPassword, byte[] storedSalt, byte[] storedHash)
    {
        byte[] enteredPasswordHash = HashPassword(enteredPassword, storedSalt);
        // Use CryptographicOperations.FixedTimeEquals for constant-time comparison to prevent timing attacks
        return CryptographicOperations.FixedTimeEquals(enteredPasswordHash, storedHash);
    }

    public static void Main(string[] args)
    {
        string userPassword = "MySecretPassword123!";

        // 1. Generate a unique salt for this user
        byte[] salt = GenerateSalt();

        // 2. Hash the password with the salt
        byte[] hashedPassword = HashPassword(userPassword, salt);

        Console.WriteLine($"Original Password: {userPassword}");
        Console.WriteLine($"Generated Salt (Base64): {Convert.ToBase64String(salt)}");
        Console.WriteLine($"Hashed Password (Base64): {Convert.ToBase64String(hashedPassword)}");

        // Simulate storing salt and hash in a database

        // 3. Later, when user tries to log in:
        string loginAttemptPassword = "MySecretPassword123!"; // Correct password
        // Retrieve the stored salt and hash from the database for this user
        byte[] retrievedSalt = salt;
        byte[] retrievedHash = hashedPassword;

        bool isValid = VerifyPassword(loginAttemptPassword, retrievedSalt, retrievedHash);
        Console.WriteLine($"Login attempt with correct password: {isValid}");

        string wrongPassword = "WrongPassword!";
        isValid = VerifyPassword(wrongPassword, retrievedSalt, retrievedHash);
        Console.WriteLine($"Login attempt with wrong password: {isValid}");
    }
}
```

### IV. Digital Signatures

Digital signatures provide:

1.  **Authenticity:** Proves the sender's identity.
2.  **Integrity:** Ensures the data has not been tampered with since it was signed.
3.  **Non-repudiation:** The sender cannot deny having sent the data.

<!-- end list -->

  * **How it works:**
    1.  **Sender:** Hashes the data, then encrypts the hash with their **private key** to create the digital signature. The original data and the signature are sent to the receiver.
    2.  **Receiver:** Uses the sender's **public key** to decrypt the signature, which gives them the sender's computed hash. The receiver then independently hashes the received data.
    3.  **Verification:** If the two hashes match, the signature is valid, meaning the data came from the legitimate sender and hasn't been altered.
  * **Algorithms:** Typically use asymmetric algorithms like RSA or ECDSA.

**Example: RSA Digital Signature in C\#**

```csharp
using System.Security.Cryptography;
using System.Text;

public class DigitalSignature
{
    public static byte[] SignData(byte[] dataToSign, RSAParameters privateKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(privateKey);
            // Sign the hash of the data using the private key
            return rsa.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }

    public static bool VerifySignature(byte[] dataToVerify, byte[] signature, RSAParameters publicKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(publicKey);
            // Verify the signature using the public key
            return rsa.VerifyData(dataToVerify, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }

    public static void Main(string[] args)
    {
        string documentContent = "This is a contract agreement.";
        byte[] documentBytes = Encoding.UTF8.GetBytes(documentContent);

        // Generate RSA key pair (private key for signing, public key for verifying)
        (RSAParameters publicKey, RSAParameters privateKey) = RsaEncryption.GenerateKeyPair(); // Reusing the previous function

        Console.WriteLine($"Original Document: {documentContent}");

        // Sender signs the document
        byte[] signature = SignData(documentBytes, privateKey);
        Console.WriteLine($"Digital Signature (Base64): {Convert.ToBase64String(signature)}");

        // Receiver verifies the document
        bool isValid = VerifySignature(documentBytes, signature, publicKey);
        Console.WriteLine($"Signature is valid (original data): {isValid}");

        // Simulate tampering
        string tamperedContent = "This is a contract agreement. And I added something malicious.";
        byte[] tamperedBytes = Encoding.UTF8.GetBytes(tamperedContent);

        bool isTamperedValid = VerifySignature(tamperedBytes, signature, publicKey);
        Console.WriteLine($"Signature is valid (tampered data): {isTamperedValid}"); // Should be false!
    }
}
```

### V. Public Key Infrastructure (PKI) and X.509 Certificates

For managing asymmetric keys and digital identities in a scalable and trustworthy way, **Public Key Infrastructure (PKI)** is used.

  * **Components:**
      * **Certificate Authority (CA):** A trusted third party that issues digital certificates.
      * **X.509 Certificates:** Digital documents issued by a CA that bind a public key to an entity (person, server, organization) and verify its identity. They contain the public key, the owner's identity, the CA's signature, and validity dates.
      * **Certificate Store:** A location where certificates are stored (e.g., Windows Certificate Store).
  * **How it works in .NET:**
      * The `System.Security.Cryptography.X509Certificates` namespace allows you to work with X.509 certificates.
      * You can load certificates from files (`.pfx`, `.cer`), from the Windows Certificate Store, or create them programmatically (though self-signed for development, a CA is needed for production trust).
      * These certificates contain the public and private keys (if it's a PFX/PKCS\#12 file with the private key) which can then be used with `RSA` or `ECDsa` classes for encryption, decryption, signing, and verification.

**Example: Loading an X.509 Certificate and Using its Key**

```csharp
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

public class CertificateUsage
{
    public static void Main(string[] args)
    {
        // For demonstration, let's assume you have a .pfx file (containing private key)
        // In a real scenario, you'd load from a secure location or certificate store.
        // You can generate a self-signed PFX for testing:
        // PowerShell: New-SelfSignedCertificate -Subject "CN=MyTestCert" -CertStoreLocation "Cert:\CurrentUser\My" -KeyExportPolicy Exportable -KeySpec Signature -Provider "Microsoft Enhanced RSA and AES Cryptographic Provider" -HashAlgorithm SHA256 -NotAfter (Get-Date).AddYears(1) -KeyLength 2048
        // Then export it to a .pfx file with a password.

        string pfxFilePath = "MyTestCert.pfx"; // Path to your PFX file
        string pfxPassword = "your_password"; // Password for the PFX file

        try
        {
            // Load the certificate with its private key
            using (X509Certificate2 certificate = new X509Certificate2(pfxFilePath, pfxPassword, X509KeyStorageFlags.Exportable))
            {
                Console.WriteLine($"Certificate Subject: {certificate.Subject}");
                Console.WriteLine($"Certificate Thumbprint: {certificate.Thumbprint}");
                Console.WriteLine($"Has Private Key: {certificate.HasPrivateKey}");

                // Get the RSA private key from the certificate
                RSA privateKey = certificate.GetRSAPrivateKey();
                RSA publicKey = certificate.GetRSAPublicKey(); // Get the public key

                if (privateKey != null && publicKey != null)
                {
                    string dataToSign = "This is data signed using certificate private key.";
                    byte[] dataBytes = Encoding.UTF8.GetBytes(dataToSign);

                    // Sign data using the private key from the certificate
                    byte[] signature = privateKey.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    Console.WriteLine($"Signed data (Base64): {Convert.ToBase64String(signature)}");

                    // Verify signature using the public key from the certificate
                    bool isValid = publicKey.VerifyData(dataBytes, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    Console.WriteLine($"Signature verification result: {isValid}");

                    // Example of encrypting a small payload with the public key (e.g., a symmetric key)
                    string symmetricKey = "MyAwesomeAESKey"; // Simulate a symmetric key
                    byte[] encryptedSymmetricKey = publicKey.Encrypt(Encoding.UTF8.GetBytes(symmetricKey), RSAEncryptionPadding.OaepSHA256);
                    Console.WriteLine($"Encrypted symmetric key (Base64): {Convert.ToBase64String(encryptedSymmetricKey)}");

                    // Decrypt the payload with the private key
                    byte[] decryptedSymmetricKeyBytes = privateKey.Decrypt(encryptedSymmetricKey, RSAEncryptionPadding.OaepSHA256);
                    string decryptedSymmetricKey = Encoding.UTF8.GetString(decryptedSymmetricKeyBytes);
                    Console.WriteLine($"Decrypted symmetric key: {decryptedSymmetricKey}");
                }
                else
                {
                    Console.WriteLine("Certificate does not contain an RSA private key or public key.");
                }
            }
        }
        catch (CryptographicException ex)
        {
            Console.WriteLine($"Cryptography Error: {ex.Message}");
            Console.WriteLine("Make sure the .pfx file exists and the password is correct.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
```

### VI. Key Management Considerations

The most challenging aspect of cryptography is **secure key management**.

  * **Never hardcode keys** in your application code.
  * **Store keys securely:**
      * **Environment Variables:** For application secrets.
      * **Cloud Key Vaults (Azure Key Vault, AWS KMS, HashiCorp Vault):** Highly recommended for production environments. They provide centralized, hardware-backed secure storage for cryptographic keys, secrets, and certificates, with access control and auditing.
      * **User Secrets (for development):** A temporary store for development secrets.
      * **DPAPI (Data Protection API):** For local machine encryption of secrets (often used by ASP.NET Core Data Protection).
  * **Rotate keys regularly:** Periodically change cryptographic keys to limit the impact of a potential compromise.
  * **Protect private keys:** Treat private keys and symmetric keys as highly confidential.

By understanding these concepts and leveraging the robust `System.Security.Cryptography` namespace, .NET developers can implement strong encryption and security measures in their applications.
