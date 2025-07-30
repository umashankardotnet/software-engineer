# Concurrency Patterns
Concurrency patterns are vital in payment systems to handle a high volume of simultaneous transactions, ensure data consistency, and maintain responsiveness. The choice of pattern depends on the specific operation's requirements for atomicity, isolation, and performance.

Here are the key concurrency patterns applicable to payment systems, with a focus on their use in .NET on AWS:

### I. Database-Level Concurrency Patterns (for Strong Consistency)

These patterns are often managed by the RDBMS itself or leveraged by the application for critical, state-changing operations.

1.  **Optimistic Concurrency Control (OCC):**
    * **Concept:** Assumes conflicts are rare. Transactions proceed concurrently without explicit locks. At the commit phase, it verifies if any data read has been modified by another transaction. If a conflict is detected, the transaction rolls back and is typically retried by the application.
    * **How it works:** Often uses a version number, timestamp, or `ETag` on records. When data is read, the version is noted. When updating, the write operation includes a condition that the version must be the same as initially read.
    * **Payment System Use:**
        * Updating account balances (e.g., `SET balance = balance - X WHERE account_id = Y AND version = Z`).
        * Managing transaction statuses where race conditions could lead to incorrect states.
    * **.NET Implementation:**
        * **Entity Framework Core:** Uses `[ConcurrencyCheck]` attribute or row version columns (`IsConcurrencyToken`) to automatically implement OCC.
        * **Custom:** Implement a `version` column (e.g., `INT` or `ROWVERSION` in SQL Server, `last_updated_at` timestamp) and include it in `WHERE` clauses for `UPDATE` statements.
        * **AWS DynamoDB:** Uses `ConditionExpression` with `attribute_exists` or `attribute_not_exists` to implement optimistic locking on items.

2.  **Pessimistic Concurrency Control (Locking):**
    * **Concept:** Prevents conflicts by acquiring locks on data *before* modifying it. If a transaction needs to read or write, it requests a lock, blocking other transactions until the lock is released.
    * **How it works:** The database engine manages locks (row-level, page-level, table-level). Transactions wait in queues for locks.
    * **Payment System Use:**
        * **High-contention scenarios for critical resources:** While often avoided in highly scalable systems due to performance implications, it might be used for very specific, short-lived, absolute consistency requirements on singular resources if scaling isn't the primary concern.
        * Less common in payment systems due to its impact on throughput, but understanding it is key.
    * **.NET Implementation:**
        * **SQL Server:** `SELECT ... WITH (ROWLOCK, UPDLOCK)` or `SERIALIZABLE` isolation level (though usually avoided for performance).
        * **PostgreSQL:** `SELECT ... FOR UPDATE` or `SELECT ... FOR SHARE`.
        * Generally, try to minimize the scope and duration of explicit locks.

3.  **Database Transaction Isolation Levels:**
    * **Concept:** Defines the degree to which one transaction's uncommitted changes are visible to other concurrent transactions. Higher isolation offers more consistency but typically lower concurrency.
    * **How it works:** Configured at the transaction or session level.
    * **Payment System Use:**
        * **Read Committed:** Common default, prevents dirty reads. Acceptable for most transactional reads.
        * **Repeatable Read / Serializable:** For highly sensitive operations requiring absolute read consistency (e.g., a specific set of rows must not change during a transaction). Use sparingly as they reduce concurrency significantly.
    * **.NET Implementation:**
        * `System.Transactions.TransactionScope` (can define `IsolationLevel`).
        * `DbConnection.BeginTransaction(IsolationLevel)` in ADO.NET.
        * Entity Framework Core context options.

### II. Application-Level & Distributed Concurrency Patterns

These patterns address concurrency across multiple services or when strict database ACID transactions aren't feasible or desired for scale.

4.  **Saga Pattern:**
    * **Concept:** Coordinates a series of local, atomic transactions across multiple services to achieve eventual consistency for a larger business process. If a step fails, compensating transactions undo prior successful steps.
    * **How it works:** Either choreographed (services communicate via events) or orchestrated (a central service manages the flow).
    * **Payment System Use:**
        * **Payment Processing Workflow:** `InitiatePayment` (Service A) -> `AuthorizePayment` (Service B) -> `CapturePayment` (Service C) -> `NotifyUser` (Service D). If capture fails, a compensating transaction `VoidAuthorization` is sent.
        * Refund processing, dispute management.
    * **.NET Implementation:**
        * **Orchestration:** Use **AWS Step Functions** to orchestrate .NET Lambda functions or containers. Libraries like NServiceBus (Saga feature) or MassTransit for managing stateful sagas.
        * **Choreography:** .NET microservices publish/consume events via **Amazon SQS/SNS/EventBridge**.

5.  **Transactional Outbox Pattern:**
    * **Concept:** Ensures atomicity between a local database change and publishing an event to a message broker. Instead of two separate writes, the event is written to an "outbox" table *within the same database transaction* as the main business data. A separate process then publishes the event.
    * **How it works:** A background worker (e.g., .NET service on ECS) polls the outbox table or uses CDC (Change Data Capture) (e.g., DynamoDB Streams, Debezium for RDS) to publish events.
    * **Payment System Use:**
        * Atomically saving `TransactionCompleted` status in the database and publishing a `TransactionCompletedEvent` for other services (e.g., Notifications, Reconciliation).
    * **.NET Implementation:**
        * Custom outbox table within your relational DB (Aurora) or NoSQL DB (DynamoDB).
        * Utilize **DynamoDB Streams with Lambda** for real-time event publishing from DynamoDB writes.
        * Libraries like Brighter or NServiceBus's outbox feature.

6.  **Idempotent Operations:**
    * **Concept:** Designing operations so that performing them multiple times has the same effect as performing them once. This is crucial for retries and message processing in distributed systems.
    * **How it works:** For each operation, generate a unique `idempotency key` (e.g., a UUID for a payment request). Before processing, check if this key has already been processed. If yes, return the previous result. If not, process and then store the key and result.
    * **Payment System Use:**
        * **Payment Gateway Calls:** If a network error occurs during a payment authorization, you can safely retry the call with the same idempotency key to prevent duplicate charges.
        * **Message Consumers:** Ensure a message is processed only once even if delivered multiple times.
    * **.NET Implementation:**
        * Application layer logic: Store `idempotency_key` in database tables or a fast key-value store (e.g., **Redis/ElastiCache**, **DynamoDB**) before execution.
        * Many payment gateway SDKs (e.g., Stripe, Braintree) natively support idempotency keys.

### III. Messaging & Asynchronous Concurrency Patterns

These leverage message queues to manage concurrent workloads and decouple services.

7.  **Queue-Based Load Leveling (Asynchronous Processing):**
    * **Concept:** Using a message queue as a buffer between producers and consumers, smoothing out spikes in demand and decoupling processing.
    * **How it works:** Producers (e.g., front-end APIs) enqueue messages (payment requests, event notifications). Consumers (worker services) pull messages at their own pace.
    * **Payment System Use:**
        * Ingesting raw payment requests before processing.
        * Processing post-transaction events like notifications, reconciliation tasks, or fraud analysis asynchronously.
    * **.NET Implementation:**
        * **Amazon SQS:** Producers (ASP.NET Core APIs) send messages; Consumers (long-running .NET services on **ECS** or **Lambda** functions) poll and process messages.
        * **Amazon Kinesis:** For high-throughput streaming data like transaction logs for real-time analytics.

8.  **Competing Consumers Pattern:**
    * **Concept:** Multiple instances of a consumer service read from the same message queue. This enables horizontal scaling of message processing.
    * **How it works:** Each consumer picks up a message, ensuring only one consumer processes a particular message.
    * **Payment System Use:**
        * Scaling payment authorization processing.
        * Distributing reconciliation tasks.
        * Parallel execution of fraud detection rules.
    * **.NET Implementation:**
        * Deploy multiple instances of your .NET worker application (e.g., on **ECS** with Auto Scaling, or multiple **Lambda** functions for SQS triggers) all listening to the same **Amazon SQS** queue. SQS handles message visibility timeouts to prevent duplicate processing.

### IV. Concurrency for Account Balances (Specialized)

Managing account balances is a classic concurrency challenge.

9.  **Financial Ledger / Double-Entry Accounting:**
    * **Concept:** Instead of directly updating a single balance, every transaction is recorded as debits and credits in an immutable ledger. The balance is derived by summing entries.
    * **How it works:** Each transaction (e.g., payment) generates at least two ledger entries (e.g., one debit to customer's account, one credit to merchant's account). Balances are then derived from these entries.
    * **Payment System Use:** Fundamental for accurate and auditable financial records, crucial for reconciliation, reporting, and dispute resolution.
    * **.NET Implementation:** Design ledger tables in a relational database (**AWS Aurora**) or use an append-only NoSQL approach (**DynamoDB**) for ledger entries. Derive balances as needed. This often pairs well with Event Sourcing.

10. **Distributed Lock (Limited Use):**
    * **Concept:** A mechanism to ensure that only one process or instance can acquire a specific "lock" for a shared resource across a distributed system.
    * **How it works:** Typically implemented using a distributed key-value store with atomic operations (e.g., Redis `SETNX`, ZooKeeper, Consensus algorithms).
    * **Payment System Use:** **Generally avoided for critical path payment processing** due to performance bottlenecks and complexity, but *might* be used for very rare, non-critical, singular control points (e.g., ensuring only one instance runs a nightly batch reconciliation job).
    * **.NET Implementation:** Libraries for Redis distributed locks (e.g., RedLock.net) or custom implementations using DynamoDB's conditional writes.

### Summary for Payment Systems

* **Prioritize Idempotency:** Absolutely critical for retries and preventing duplicate charges.
* **Embrace Asynchronous Processing:** Use queues (SQS) and event streams (Kinesis, EventBridge) to decouple services and handle high throughput.
* **Leverage Sagas:** For complex, multi-service workflows to ensure eventual consistency and reliable recovery.
* **Data Consistency:** For core financial data, lean towards strong consistency (OCC in RDBMS, or carefully managed single-item atomicity in NoSQL). For other parts, eventual consistency is fine.
* **Auditability:** Financial ledger and Event Sourcing patterns are highly valuable for comprehensive auditing.
* **Scalability:** All patterns should be chosen with horizontal scaling in mind, leveraging AWS's managed services.

The most robust payment systems combine several of these patterns, choosing the right tool for the right job, balancing strong consistency where needed with high availability and scalability for the overall system.
