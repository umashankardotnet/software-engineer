# mTLS (Mutual TLS) authentication
Here’s a **complete, comprehensive guide to mTLS (Mutual TLS) authentication**, covering everything you need from fundamentals to AWS/.NET implementations. This guide includes:

* What is TLS vs mTLS
* How mTLS works step-by-step
* Key components (X.509, CA, Trust Store, Keystore, Cert Chain)
* Real-world use cases
* Implementation in AWS (API Gateway, ALB, ACM, IoT)
* .NET examples
* Security best practices
* Certificate lifecycle
* Diagrams (optional on request)

---

## 🛡️ 1. What is TLS vs mTLS?

| **TLS (1-way)**                      | **mTLS (Mutual TLS)**                            |
| ------------------------------------ | ------------------------------------------------ |
| Only the **server** is authenticated | Both **client and server** are authenticated     |
| Browser trusts the server            | Server also trusts the client                    |
| Used in HTTPS                        | Used in secure B2B, APIs, IoT, internal services |

### TLS Example:

* You access `https://example.com`
* Browser checks the server’s certificate
* Data is encrypted after trust is established

### mTLS Example:

* Internal service A calls service B
* B verifies A’s certificate
* A verifies B’s certificate
* Connection only succeeds if both certs are valid and trusted

---

## 🔐 2. How mTLS Works: Step-by-Step

```
CLIENT                                     SERVER
   |                                           |
1. |----- Client Hello ---------------------> |
2. |                                           |
3. | <---- Server Hello + Server Certificate  |
4. |                                           |
5. |---- Client Certificate ----------------> |
6. |---- Encrypted Client Key + Finish ---->  |
7. | <--- Encrypted Server Key + Finish ----- |
8. |---- Secure Communication Starts -------> |
```

✅ If any certificate fails verification, the handshake is **terminated**.

---

## 📄 3. Key Components

### 🧾 a. X.509 Certificate

* Follows a standard format
* Contains identity info + public key
* Signed by CA

### 🏛️ b. Certificate Authority (CA)

* Signs certs
* Can be public (DigiCert) or private (your own CA)

### 🔒 c. Private Key

* Stays with owner
* Used to sign and decrypt

### 🔓 d. Public Key

* Shared
* Used to verify and encrypt

### 🔗 e. Certificate Chain

```
Your Cert → Intermediate CA → Root CA
```

### 🗂️ f. Trust Store

* Stores **CA certs** that are trusted
* Used to **verify** peer certs

### 📦 g. Keystore

* Contains private key + public cert
* Used by server/client to **authenticate**

---

## 🧰 4. Use Cases for mTLS

| Use Case                       | Example                                       |
| ------------------------------ | --------------------------------------------- |
| **Internal API Communication** | Microservice A ↔ B (inside VPC or Kubernetes) |
| **Banking & Payments**         | Client POS → Payment Processor                |
| **IoT Devices**                | Smart Meter authenticating to backend         |
| **Zero Trust Security**        | User Device ↔ Application Gateway             |
| **Client App Auth**            | External app authenticating without tokens    |

---

## ☁️ 5. How to Implement mTLS in AWS

### 🏗️ a. Using AWS API Gateway

* Enable **Custom Domain**
* Upload **Truststore (CA certificate)** for client cert validation
* Use **IAM or Lambda authorizer** for additional logic

#### Workflow:

```
Client → API Gateway (validates client cert) → Backend
```

### 🏗️ b. Using AWS Application Load Balancer (ALB)

* ALB supports mTLS since 2022
* Use Listener configuration:

  * TLS termination at ALB
  * Require Client Authentication
  * Upload **trusted CA cert**

#### Workflow:

```
Client → ALB (mTLS handshake) → Target EC2 / ECS / Lambda
```

### 🛠️ c. AWS IoT Core (mTLS by default)

* Each IoT device has a **unique cert and key**
* AWS verifies identity through cert

---

## 💻 6. mTLS in .NET (Example)

### Step 1: Generate Certs (using OpenSSL or PowerShell)

```bash
# Generate Root CA
openssl genrsa -out rootCA.key 2048
openssl req -x509 -new -nodes -key rootCA.key -sha256 -days 1024 -out rootCA.crt

# Generate Client Cert
openssl genrsa -out client.key 2048
openssl req -new -key client.key -out client.csr
openssl x509 -req -in client.csr -CA rootCA.crt -CAkey rootCA.key -CAcreateserial -out client.crt -days 365 -sha256
```

### Step 2: Configure Kestrel in .NET API

```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ConfigureHttpsDefaults(httpsOptions =>
                {
                    httpsOptions.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                    httpsOptions.CheckCertificateRevocation = false;
                    httpsOptions.ClientCertificateValidation = (cert, chain, errors) =>
                    {
                        // Validate cert chain or thumbprint
                        return cert.Issuer.Contains("YourRootCA");
                    };
                });
            });
            webBuilder.UseStartup<Startup>();
        });
```

---

## 🧮 7. Certificate Lifecycle Management

| Stage        | Task                             | Tools                                |
| ------------ | -------------------------------- | ------------------------------------ |
| Generation   | Create cert + key                | OpenSSL, ACM                         |
| Signing      | Sign with CA                     | Private or Public CA                 |
| Distribution | Install certs on clients/servers | S3, Parameter Store, Systems Manager |
| Rotation     | Replace expiring certs           | ACM auto-renewal, Lambda scripts     |
| Revocation   | Invalidate compromised certs     | CRL, OCSP                            |

---

## 🔐 8. Security Best Practices

* 🔁 Rotate certificates regularly
* 🔏 Store private keys securely (e.g., AWS Secrets Manager)
* 🔎 Validate entire certificate chain
* 🛑 Set short cert lifetimes for IoT and ephemeral devices
* 📜 Use proper CA hierarchy (Root → Intermediate → End-Entity)
* ✅ Always enable revocation check (CRL or OCSP)

---

## 🧠 9. Summary Table

| Concept          | Description                            |
| ---------------- | -------------------------------------- |
| **mTLS**         | Mutual TLS (both client/server auth)   |
| **X.509 Cert**   | Cert format with identity + public key |
| **CA**           | Issues/signed certs                    |
| **Keystore**     | Stores private key + public cert       |
| **Truststore**   | Stores trusted CAs                     |
| **PEM/PFX**      | File formats for certs/keys            |
| **.NET Support** | Via Kestrel config + cert validation   |
| **AWS Support**  | API Gateway, ALB, IoT Core, ACM        |

---

## 📊 Want Diagrams or Live Code Demo?

Let me know if you’d like:

* 📈 Sequence diagram of TLS/mTLS handshake
* 🎥 Full .NET sample with client + server
* ⚙️ AWS Terraform/CloudFormation to set up mTLS with ALB or API Gateway

Would you like this turned into a **PDF** or **Markdown document** for sharing or internal documentation?
