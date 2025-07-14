# Complete Guide to JWT: Internals, Validation, Tokens, and Security Best Practices

This is a comprehensive guide tailored for professionals working with .NET, Angular, and AWS. It covers how JWT (JSON Web Token) works internally, key terminologies like claims, audience, issuer, authority, and the differences between ID, Access, and Refresh tokens. It also details token validation, storage strategies, security practices, and the use of HttpOnly cookies.


## What Is JWT (JSON Web Token)?

JWT is a compact, URL-safe token used for securely transmitting user identity and authorization information between systems. It is commonly used in SPA + API setups (e.g., Angular + .NET Web API).


## JWT Structure

```text
Header.Payload.Signature
```

Each part is Base64Url-encoded.

### 1. Header

Contains metadata and the algorithm used to sign the token.

```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

### 2. Payload (Claims)

Contains user info and metadata.

```json
{
  "sub": "user1",
  "name": "Bhanu",
  "role": "Admin",
  "aud": "api.myapp.com",
  "iss": "https://auth.myapp.com",
  "exp": 1716239022
}
```

### 3. Signature

```text
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret)
```

Ensures token integrity and authenticity. If any part of the token is altered, the signature becomes invalid.


## Token Validation: How It Works Internally

### Step-by-Step Process in the Backend (.NET, Node, etc.)

1. **Extract Token from Header**:

```http
Authorization: Bearer <JWT>
```

2. **Split Token into Parts**:

```text
[Header].[Payload].[Signature]
```

3. **Decode Header and Payload** (Base64Url decoding):

* Get algorithm (`alg`)
* Get claims like `iss`, `aud`, `exp`

4. **Recompute Signature Using the Same Algorithm**:

* Concatenate: `base64(header) + "." + base64(payload)`
* Apply algorithm (e.g., HS256) using the shared secret or public key
* Result: `computedSignature`

5. **Compare**:

* If `computedSignature === token.Signature` → ✅ Valid token
* Else → ❌ Reject token

6. **Validate Claims**:

* `exp` (expiry)
* `iss` (issuer)
* `aud` (audience)
* `nbf`, `iat` (optional)

### Sample .NET Configuration

```csharp
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://auth.myapp.com";
        options.Audience = "api.myapp.com";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://auth.myapp.com",
            ValidAudience = "api.myapp.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret"))
        };
    });
```


## Secret Key vs Public Key

### HS256 (HMAC)

* Symmetric: Same key for signing and verifying
* Simpler but requires secure key sharing

### RS256 (RSA)

* Asymmetric: Private key signs, public key verifies
* Public key can be exposed via `.well-known/jwks.json`


## What is `Bearer`?

When sending the token to the backend:

```http
Authorization: Bearer <token>
```

* `Bearer` is the HTTP authentication scheme
* Indicates the token is a bearer token (like a session)


## Token Types: ID, Access, and Refresh

| Token Type        | Purpose            | Audience    | Contains Identity Info?  | Used By                   |
| ----------------- | ------------------ | ----------- | ------------------------ | ------------------------- |
| **ID Token**      | Authentication     | Client app  | ✅ Yes (profile info)     | Angular frontend          |
| **Access Token**  | Authorization      | API         | ❌ Only metadata & claims | .NET API                  |
| **Refresh Token** | Renew Access Token | Auth Server | ❌                        | Angular (securely stored) |


## Common JWT Claims

| Claim  | Description                   |
| ------ | ----------------------------- |
| `sub`  | Subject (unique user ID)      |
| `name` | User name                     |
| `role` | User’s role                   |
| `iss`  | Issuer (who issued the token) |
| `aud`  | Audience (target API)         |
| `exp`  | Expiry timestamp              |
| `iat`  | Issued At timestamp           |
| `nbf`  | Not Before (optional)         |


## HttpOnly Cookie vs Storage

### What Is an HttpOnly Cookie?

* Not accessible via JavaScript
* Automatically included in every request
* Helps prevent **XSS** attacks

```http
Set-Cookie: access_token=eyJhbGciOiJI...; HttpOnly; Secure; SameSite=Strict
```

### Why Use It?

| Feature  | Benefit                         |
| -------- | ------------------------------- |
| HttpOnly | Prevent JavaScript access (XSS) |
| Secure   | HTTPS-only transmission         |
| SameSite | Helps prevent CSRF              |

### 🚫 Limitations:

* Can’t manually attach `Authorization` header
* Requires CSRF protection in stateful setups


## Token Storage in Frontend: Trade-Offs

| Storage             | Persistent | XSS Risk | Manual Send | Auto Send |
| ------------------- | ---------- | -------- | ----------- | --------- |
| **localStorage**    | ✅ Yes      | ❌ High   | ✅ Yes       | ❌ No      |
| **sessionStorage**  | ❌ No       | ❌ High   | ✅ Yes       | ❌ No      |
| **HttpOnly Cookie** | ✅ Yes      | ✅ Safe   | ❌ No        | ✅ Yes     |


## Token Lifecycle

1. **Login**

   * Angular calls Identity Provider (Azure AD / Cognito / IdentityServer)
   * Receives: ID Token, Access Token, Refresh Token

2. **Token Storage**

   * Store in `localStorage`, `sessionStorage`, or `HttpOnly cookie`

3. **API Calls**

   * Attach `Authorization: Bearer <AccessToken>` manually or automatically (cookie)

4. **Token Expiry**

   * On expiry, use Refresh Token to get a new Access Token

5. **Logout**

   * Remove token from storage / expire cookie


## Algorithms for Signing JWTs

| Algorithm | Type       | Usage                                            |
| --------- | ---------- | ------------------------------------------------ |
| `HS256`   | Symmetric  | Shared secret for signing/verification           |
| `RS256`   | Asymmetric | Private/public key pair (preferred for frontend) |
| `ES256`   | Asymmetric | Elliptic Curve variant                           |
| `none`    | Insecure   | Never use in production                          |


## Sample JWT Decoding Tools

* [https://jwt.io](https://jwt.io) – Online decoder/validator
* .NET: `System.IdentityModel.Tokens.Jwt`

```csharp
var handler = new JwtSecurityTokenHandler();
var token = handler.ReadJwtToken(jwt);
var name = token.Claims.First(c => c.Type == "name").Value;
```


## Summary Cheat Sheet

| Term                | Description                                 |
| ------------------- | ------------------------------------------- |
| `JWT`               | JSON Web Token for identity and access      |
| `Header`            | Contains algorithm and token type           |
| `Payload`           | Contains claims (user info)                 |
| `Signature`         | Verifies authenticity and integrity         |
| `HS256/RS256`       | Signing algorithms (symmetric/asymmetric)   |
| `ID Token`          | Used for frontend identification            |
| `Access Token`      | Used for backend authorization              |
| `Refresh Token`     | Used to renew tokens without login          |
| `Bearer`            | Authorization scheme in HTTP headers        |
| `Authority`         | Issuer’s base URL (e.g., Azure AD, Cognito) |
| `Audience`          | Target service/API name                     |
| `HttpOnly Cookie`   | Secure, JS-inaccessible token storage       |
| `exp`, `iat`, `nbf` | Time-based claims                           |


## ✅ JWT Token Validation Example in AWS


## 1. **When and Where Tokens Are Validated in AWS**

| Component                      | Token Validation Happens?  | Description                                                                 |
| ------------------------------ | -------------------------- | --------------------------------------------------------------------------- |
| **API Gateway**                | ✅ Yes (with Authorizer)    | Uses JWT authorizer to validate access token before invoking Lambda/backend |
| **AWS Lambda**                 | ❌ No (optional manually)   | Relies on API Gateway or custom code inside Lambda                          |
| **Cognito User Pool**          | ✅ Issues & verifies tokens | Acts as identity provider and verifies tokens if requested                  |
| **App Load Balancer**          | ✅ With Cognito auth        | Can enforce token validation at ALB level before forwarding to EC2/ECS      |
| **.NET Backend (EC2/Fargate)** | ✅ Validates manually       | Backend must parse and validate JWT using libraries                         |


## 2. AWS Token Validation Flow (Cognito + API Gateway + Lambda/.NET)

### Scenario:

* You have an Angular frontend.
* Using **AWS Cognito** for user authentication.
* Backend is served via **API Gateway + Lambda** or **.NET on EC2/ECS**.

### Flow:

```
1. Angular login → Cognito (Hosted UI or SDK)
2. Cognito authenticates user → returns ID Token + Access Token
3. Angular calls API Gateway with Bearer token in header
4. API Gateway validates token using JWT Authorizer or Cognito Authorizer
5. If valid → forwards request to Lambda or .NET backend
6. Backend optionally re-validates or extracts user info from token
```


## 3. Token Validation Process Internally

### Step-by-Step on AWS

#### Step 1: Token Sent

```http
Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI...
```

#### Step 2: API Gateway (with JWT Authorizer)

* API Gateway uses Cognito or a custom **JWT Authorizer**.
* It extracts:

  * Header → `alg` (e.g., RS256)
  * Payload → `iss`, `aud`, `exp`, `scope`
* Retrieves public key from **Cognito JWK endpoint**:

  ```
  https://cognito-idp.<region>.amazonaws.com/<userPoolId>/.well-known/jwks.json
  ```

#### Step 3: Recomputes Signature

* Uses the **RS256 algorithm** and the public key
* Verifies:

  * `signature`
  * `iss` == Cognito User Pool URL
  * `aud` == App client ID
  * `exp`, `nbf`, `iat`

#### Step 4: Authorizer Policy (Optional)

If custom Lambda Authorizer is used, it returns:

```json
{
  "principalId": "user123",
  "policyDocument": {
    "Statement": [{
      "Action": "execute-api:Invoke",
      "Effect": "Allow",
      "Resource": "arn:aws:execute-api:..."
    }]
  },
  "context": {
    "user": "Bhanu",
    "role": "Admin"
  }
}
```


## 4. Token Validation in .NET (on EC2, ECS, Fargate)

If API Gateway does NOT do validation, or you have internal services doing their own checks:

### Code Sample – .NET Token Validation with Cognito

```csharp
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = "https://cognito-idp.<region>.amazonaws.com/<userPoolId>";
    options.Audience = "<AppClientId>"; // from Cognito
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = $"https://cognito-idp.<region>.amazonaws.com/<userPoolId>",
        ValidateAudience = true,
        ValidAudience = "<AppClientId>",
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});
```

> .NET will **fetch and cache** the Cognito JWKs for RS256 and verify JWTs automatically.


## 5. Other Services and JWT Token Handling

### App Load Balancer (ALB)

* Can enforce authentication using Cognito
* Offloads JWT validation before reaching ECS/EC2 backend
* No code needed for token parsing in backend

### AWS Amplify (Frontend + Auth)

* Uses Cognito to manage authentication
* Handles tokens (ID/Access/Refresh) in browser automatically
* Integrates with API Gateway and Lambda


## 6. JWKs in AWS

### What is JWK?

JSON Web Key is a public key published by Cognito (or other IdPs) used to verify JWT signatures.

### AWS Cognito JWK URL Format:

```
https://cognito-idp.<region>.amazonaws.com/<userPoolId>/.well-known/jwks.json
```

> Libraries like `System.IdentityModel.Tokens.Jwt` or `jsonwebtoken` (Node.js) use this automatically when validating RS256 tokens.


## 7. Common Security Settings and Best Practices

| Feature            | Best Practice                                          |
| ------------------ | ------------------------------------------------------ |
| **Token Expiry**   | Use short-lived access tokens (15 mins)                |
| **Refresh Tokens** | Store securely, rotate regularly                       |
| **RS256**          | Always prefer asymmetric validation for public clients |
| **Cookie Storage** | Use HttpOnly + Secure + SameSite cookies               |
| **API Gateway**    | Use JWT/Cognito Authorizers to reduce load on backend  |
| **Rotate Secrets** | Regularly rotate App Client secrets / keys             |


## Summary

| Layer                | Role in JWT Validation                         |
| -------------------- | ---------------------------------------------- |
| **AWS Cognito**      | Issues and signs tokens (ID, Access, Refresh)  |
| **API Gateway**      | Validates JWT using public keys (JWKs)         |
| **Lambda / .NET**    | May parse token claims, re-validate (optional) |
| **ALB with Cognito** | Offloads token validation from backend         |
| **JWKs**             | Public keys to validate RS256-signed tokens    |


## Example Use Case

> Angular App → AWS Cognito Login → Gets Tokens → Calls API Gateway with `Bearer <AccessToken>` → API Gateway validates via Cognito → forwards to Lambda → Lambda uses `context.authorizer.claims` or token data to act accordingly.
>
> Here’s an **extended and updated explanation** that includes a detailed comparison between **symmetric vs. asymmetric algorithms** for JWT, why to choose one over the other, and how that choice affects **token generation and validation**, especially in cloud environments like **AWS**.


## JWT Signing Algorithms: Symmetric vs Asymmetric

### JWT tokens are **digitally signed**, not encrypted.

The signature ensures:

* The token has not been **tampered** with.
* It was **issued by a trusted source**.

To create and verify this signature, two main algorithm types are used:


## Symmetric (HMAC - e.g., HS256)

### How It Works:

* The same **shared secret** is used for both:

  * Signing the token (Auth Server)
  * Verifying the token (API)

### Common Algorithm:

* `HS256` (HMAC using SHA-256)

### Pros:

* Simple and fast
* Easy to implement
* Good for **internal services** or **monolithic applications**

### Cons:

* **Secret must be shared** across all services that verify tokens
* More risky in **distributed or cloud-native systems**
* If any service is compromised, the secret is compromised


## Asymmetric (Public/Private Key - e.g., RS256)

### How It Works:

* A **private key** signs the JWT (Auth Server only)
* A **public key** verifies it (any service/API)

### Common Algorithm:

* `RS256` (RSA SHA-256)
* Others: `RS384`, `RS512`, `ES256` (Elliptic Curve)

### Pros:

* More secure in distributed architectures
* **No need to share secret** across services
* Public key can be openly distributed (via JWK URL)
* Ideal for **third-party APIs** or **frontend+backend** separation

### Cons:

* Slightly more complex setup
* Slightly slower than symmetric algorithms


## When to Choose What?

| Use Case                                                   | Choose    | Why                                               |
| ---------------------------------------------------------- | --------- | ------------------------------------------------- |
| Small internal microservices, all in same network          | **HS256** | Simple, single trust boundary                     |
| Public-facing APIs, SPAs (Angular/React) + backend         | **RS256** | Public clients can't store secrets securely       |
| Federated authentication (e.g., Azure AD, Google, Cognito) | **RS256** | Trust-based public verification                   |
| External integrations or third-party clients               | **RS256** | Public verification without exposing private keys |
| Multi-tenant SaaS apps (secure boundary per tenant)        | **RS256** | Better key isolation and control                  |


## In AWS Context

### Cognito

* Uses **RS256** by default
* Exposes public keys via:

  ```
  https://cognito-idp.<region>.amazonaws.com/<userPoolId>/.well-known/jwks.json
  ```

### API Gateway

* Validates RS256 signatures using **JWKs from Cognito**
* Zero setup required on your end — just configure Cognito Authorizer

### .NET or Lambda Backends

* Can validate RS256 tokens using the **Microsoft JWT libraries**
* No need to store secret — just use the **public key from the JWK**


## Summary Comparison

| Feature      | Symmetric (HS256)      | Asymmetric (RS256)          |
| ------------ | ---------------------- | --------------------------- |
| Key Type     | Shared secret          | Private + Public key        |
| Verification | Secret required        | Only public key needed      |
| Security     | Risk if secret leaks   | Safer for distributed apps  |
| Setup        | Simpler                | Needs keypair, JWK setup    |
| Ideal For    | Internal apps          | Web/mobile apps, cloud APIs |
| Example      | In-house token service | Cognito, Azure AD, Auth0    |


### Recommendation for You (as .NET + Angular + AWS Dev):

* Use **RS256** with **AWS Cognito** or **IdentityServer**.
* Let Cognito issue tokens signed with the **private key**.
* Let **API Gateway or .NET backend** validate using the **JWK public key**.
* Avoid HS256 unless all services are tightly controlled and isolated.
