# Complete Guide to Sharding in Relational Databases (RDBMS)

## What is Sharding?

Sharding is a database architecture pattern in which data is horizontally partitioned and distributed across multiple databases (shards). Each shard contains a subset of the data, and together, they make up the full dataset.


## Why Sharding?

* **Scalability:** Split large datasets across multiple systems.
* **Performance:** Reduce query load and improve response time.
* **Fault Isolation:** Failure in one shard affects only a portion of users.
* **Compliance:** Store data regionally for legal requirements (e.g., GDPR).
* **Cost Optimization**: Scale out horizontally instead of vertically.

---

## Sharding vs Partitioning

| Aspect   | Sharding                          | Partitioning        |
| -------- | --------------------------------- | ------------------- |
| Scope    | Across multiple nodes/databases   | Within one database |
| Use Case | Scalability, multi-region         | Performance tuning  |
| Example  | Different tenants in separate DBs | Split table by date |



## Sharding Techniques

### 1. **Range-Based Sharding**

* **How:** Based on a continuous range of column values
* **Example:**

  * `CustomerID 1-10000 → Shard A`
  * `CustomerID 10001-20000 → Shard B`
* **Use Case:** Time-series data, incremental IDs
* **Risk:** Skewed traffic if recent ranges are hot

### 2. **Hash-Based Sharding**

* **How:** Apply hash function on a shard key
* **Example:** `hash(UserID) % totalShards`
* **Use Case:** Balanced workloads
* **Challenge:** Hard to scale without consistent hashing

### 3. **List-Based Sharding**

* **How:** Define a list of values that map to a shard
* **Example:** Country-wise:

  * `US, CA → Shard A`
  * `IN, PK → Shard B`
* **Use Case:** Geolocation-based routing
* **Challenge:** Rebalancing if new list items appear

### 4. **Directory-Based (Lookup/Shard Map)**

* **How:** Use a metadata table/service to look up shard for each key
* **Example:**

  ```
  TenantShardMap
  ┌──────────┬────────────┐
  │ TenantID │ Shard URL  │
  └──────────┴────────────┘
  ```
* **Use Case:** Multi-tenant SaaS
* **Challenge:** Lookup service must be highly available

### 5. **Geography/Region-Based Sharding**

* **How:** Distribute data by physical region
* **Example:**

  * `India → Shard A`
  * `EU → Shard B`
* **Use Case:** GDPR, latency optimization
* **Challenge:** Cross-region joins are costly

### 6. **Hybrid Sharding**

* **How:** Combine multiple techniques
* **Example:** Region-based then hash by user ID
* **Use Case:** Complex SaaS with compliance & scale needs

### 7. **Vertical Sharding (Functional Sharding)**

* **How:** Split tables by feature/module/domain across different databases
* **Example:**

  * `Auth tables → Shard A`
  * `Orders → Shard B`
  * `Products → Shard C`
* **Use Case:** Microservices or domain-driven architectures
* **Pros:** Clear domain boundaries
* **Cons:** Joins across databases are hard, increased complexity


## How Sharding Works in Practice

* Applications route queries based on shard key
* Middleware or gateway may determine correct shard
* Each shard is often hosted on separate infrastructure


## RDBMS Support for Sharding

* **SQL Server:** Manual or via federated databases
* **PostgreSQL:** With Citus extension
* **MySQL:** MySQL Fabric, Vitess, ProxySQL
* **Aurora/RDS:** Manual sharding using EC2/Proxy/gateway


## Sharding in AWS RDS (Relational Database Service)

| Solution               | Description                                                      |
| ---------------------- | ---------------------------------------------------------------- |
| Multiple RDS Instances | Host separate shards per tenant/region                           |
| EC2 + RDS              | App running on EC2 connects to respective RDS based on shard map |
| Route 53               | DNS-level routing based on metadata lookup                       |
| RDS Proxy              | Shared proxy across shards to manage connection pooling          |
| Multi-AZ RDS           | Fault tolerance for individual shard                             |

### Example

```text
ShardMap:
Tenant1 → rds-tenant1.us-east-1.rds.amazonaws.com
Tenant2 → rds-tenant2.eu-west-1.rds.amazonaws.com
```


## Failure, Fault Tolerance & Resilience

### What if a Shard Fails?

* Only the data in that shard is impacted
* Other shards (and tenants) remain available

### How to Improve Availability?

* **Multi-AZ RDS:** For automatic failover
* **Read Replicas:** For read scaling and backup
* **Backup & Restore:** Frequent snapshots and PITR (Point-In-Time Recovery)
* **Cross-Region Replication:** For disaster recovery
* **Service Mesh / Retry Logic:** App logic to retry on alternate endpoint

### Compliance and Isolation

* Tenant-specific encryption keys per shard
* Region-based shards for GDPR/CCPA
* Auditing and monitoring per shard



## Security Considerations

* **RBAC at Shard Level**: Ensure access control per shard
* **Row-Level Security** if multiple tenants share the same shard
* **Column-Level Encryption** for PII fields
* **TLS between services and DBs**



## Best Practices

* Use consistent hashing or lookup table for routing
* Monitor shard health independently
* Automate provisioning and migration
* Pre-warm new shards with indexes and schema
* Define shard lifecycle: creation, scaling, archiving


## Real-World Use Cases

| Company    | Use Case                                        |
| ---------- | ----------------------------------------------- |
| Salesforce | Each customer (tenant) has its own schema/shard |
| Amazon     | Region-based sharding for product/order systems |
| Netflix    | Microservice DBs (vertical sharding)            |
| Uber       | Hash-based sharding for user and trip data      |


## Vertical vs Horizontal Sharding

| Feature       | Horizontal Sharding       | Vertical Sharding            |
| ------------- | ------------------------- | ---------------------------- |
| Data Split By | Rows                      | Tables / Modules             |
| Example       | `UserID 1–1000` in one DB | Auth in DB A, Orders in DB B |
| Scalability   | Excellent                 | Medium                       |
| Complexity    | Higher                    | Moderate                     |

### Example in .NET + AWS

* Create a Shard Map table (SQL Server)
* Lookup shard DB connection string by tenantId
* Use RDS Multi-AZ deployments per shard
* Use EF Core with dynamic DB context switching

```csharp
public class ShardResolver
{
    public string ResolveShardConnection(string tenantId)
    {
        // Query ShardMap table
        return _shardMap[tenantId];
    }
}
```

## Best Practices

* Keep shard sizes balanced
* Monitor query performance across shards
* Automate provisioning using Infrastructure-as-Code
* Avoid cross-shard joins
* Use consistent shard keys
* Plan for shard rebalancing (use GUIDs or consistent hashing)


## AWS Considerations

| Feature                  | AWS Service/Practice       |
| ------------------------ | -------------------------- |
| Database instances       | RDS / Aurora               |
| Load balancing/shard map | Lambda + API Gateway       |
| Backup                   | RDS Snapshots + Lifecycle  |
| Failover                 | Multi-AZ / Aurora failover |
| Data locality            | Region-based deployment    |


## Diagram Suggestion (For Visual Inclusion)

* Request → Shard Resolver → DB Connection → RDS Shard A/B/C
* Shard Map table lookup → Route request
