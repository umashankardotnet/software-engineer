# ACID in RDBMS

## Isolation
In database transactions, **Isolation** is one of the four ACID properties (Atomicity, Consistency, Isolation, Durability). It dictates the degree to which a transaction must be isolated from the data modifications made by any other concurrently running transactions.

The goal of isolation is to prevent **concurrency anomalies**, which are issues that can arise when multiple transactions access and modify the same data simultaneously. These anomalies include:

* **Dirty Read:** A transaction reads data that has been modified by another transaction but not yet committed. If the other transaction then rolls back, the first transaction has read "dirty" or never-committed data.
* **Non-Repeatable Read:** A transaction reads the same row twice, but gets different data each time because another committed transaction modified or deleted that row between the two reads.
* **Phantom Read:** A transaction reads a set of rows based on a search condition. Another *committed* transaction inserts or updates rows that now match the original search condition. If the first transaction re-executes the same query, it sees a "phantom" new row that wasn't there before.

To balance data consistency with performance and concurrency, SQL databases define several **Isolation Levels**, each allowing for a different set of these anomalies. Higher isolation levels provide stronger consistency but generally reduce concurrency (due to more locking), while lower levels prioritize performance but risk more anomalies.

Here are the four standard SQL isolation levels, from weakest (most concurrent, least consistent) to strongest (least concurrent, most consistent):

1.  **Read Uncommitted:**
    * **Allows:** Dirty Reads, Non-Repeatable Reads, Phantom Reads.
    * **Description:** The lowest isolation level. A transaction can read data that is still being modified by other transactions, even if those changes haven't been committed yet. This is very fast as it involves minimal locking, but can lead to highly inconsistent data. Rarely used for production systems where accuracy is critical.

2.  **Read Committed:**
    * **Prevents:** Dirty Reads.
    * **Allows:** Non-Repeatable Reads, Phantom Reads.
    * **Description:** The most common default isolation level in many databases. A transaction will only read data that has been committed by other transactions. It holds write locks until the end of the transaction, but read locks are typically released as soon as the read operation is complete. This prevents reading "dirty" data but doesn't guarantee that a subsequent read of the same data will yield the same result.

3.  **Repeatable Read:**
    * **Prevents:** Dirty Reads, Non-Repeatable Reads.
    * **Allows:** Phantom Reads.
    * **Description:** A transaction holds read locks on all rows it references and write locks on rows it inserts, updates, or deletes, until the transaction commits or rolls back. This ensures that if a transaction reads a row multiple times, it will always get the same value. However, new rows (phantoms) that match the query criteria can still be inserted by other committed transactions.

4.  **Serializable:**
    * **Prevents:** Dirty Reads, Non-Repeatable Reads, Phantom Reads.
    * **Description:** The highest isolation level. It guarantees that transactions execute as if they were running sequentially (serially), even when they are running concurrently. This means no concurrency anomalies are allowed. It achieves this by acquiring range locks (to prevent phantom reads) in addition to read and write locks, and holding them until the transaction completes. While providing the strongest data integrity, it comes with the highest performance overhead due to extensive locking, potentially limiting concurrency significantly.
  
## Durability
**Durability** is the **ACID** property that guarantees that once a transaction has been **committed**, its changes are permanent and will survive any subsequent system failures.

In simple terms: if the database tells you a transaction succeeded, you can trust that the data is safely stored on **non-volatile storage** (like a hard drive or SSD), even if the power goes out, the system crashes, or other hardware failures occur. When the system recovers, the committed changes will still be there.

To achieve durability, databases typically use techniques like:

* **Write-Ahead Logging (WAL):** Changes are first written to a transaction log (which is immediately flushed to disk) *before* the actual data pages on disk are updated. If a crash occurs, the log can be used to "redo" committed transactions.
* **Snapshots/Checkpoints:** Periodically, the database takes a snapshot of its state and flushes all committed changes from memory to disk.
* **Replication/Backups:** For even higher levels of durability and disaster recovery, databases often replicate data to other servers or create regular backups.

Without durability, committed data could be lost, leading to inconsistencies and a lack of trust in the database system.
