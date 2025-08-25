# Atlassian Principle Engineer Mock Interview
## Question-1
Given your experience with microservices and distributed systems, here's your first question:

You're tasked with designing a distributed caching system for Atlassian's Jira platform that needs to handle high concurrency and maintain consistency across multiple data centers. The system should support both read and write operations with minimal latency.

Can you walk me through:

- Your high-level architecture approach
- How you would handle cache consistency
- How you would deal with network partitions
- Any potential trade-offs you'd need to consider

Please explain your reasoning for each decision.


## Questions-2
Given your experience with microservices and distributed systems, here's your first question:

You're tasked with designing a distributed event notification system that needs to handle millions of events per day across multiple services. The system needs to ensure reliable delivery, handle back pressure, and maintain message ordering when necessary. How would you approach this architecture, and what specific considerations would you keep in mind?

Please walk me through your high-level design and explain the key technical decisions you would make.

## Question-3
You're leading a team that's experiencing performance issues with a microservices-based application in production. The system is showing increased latency and occasional timeouts, particularly during peak hours. How would you approach diagnosing and resolving these issues? Please walk me through your troubleshooting methodology and the tools you would use.

### My Answer
Here's a better structured answer:

To diagnose and resolve microservice performance issues, I would:

1. Monitor Service Metrics:
- Use monitoring tools like CloudWatch to identify specific services experiencing issues
- Analyze key metrics including response times, error rates, and resource utilization

2. Code Review:
- Review critical code paths for potential bottlenecks
- Look for inefficient algorithms or resource-intensive operations
- Check for proper caching implementation

3. Database Analysis:
- Examine database performance metrics
- For relational databases:
  * Verify proper indexing
  * Review query execution plans
  * Check for slow-running queries
- For NoSQL databases:
  * Evaluate data model design
  * Check partition strategies
  * Review access patterns

This systematic approach helps identify and resolve performance bottlenecks efficiently.

### Interviewer Feedback
Good starting point with monitoring and metrics! Let me provide some feedback and then ask a follow-up question.

#### Feedback:
- Good initial focus on metrics and monitoring
- Consideration of both application code and database layer
- Logical progression from identification to investigation

#### Areas for improvement:
- Could mention specific metrics you'd look at (e.g., CPU utilization, memory usage, request/response times, error rates)
- Could discuss distributed tracing tools (like Jaeger or X-Ray) for end-to-end visibility
- Might want to mention log analysis and correlation
- Could discuss potential infrastructure-related issues

### Follow-up question:
During your investigation, you discover that one particular microservice is causing cascading failures due to timeout issues. The service uses a Circuit Breaker pattern, but it's not effectively preventing the cascade. How would you improve the resilience of this service, and what specific patterns or techniques would you implement?
### My Answer
We can use rate limiting for the API gateway and graceful degradation for failure service and can use timeouts as well.
### Interviewer Feedback
Good mention of important resilience patterns! Let me provide feedback and then move to a different area.

#### Feedback:
- Good mention of rate limiting as a preventive measure
- Understanding of graceful degradation concept
- Recognition of timeout importance

#### Areas for improvement:
- Could elaborate on specific rate limiting strategies (token bucket, leaky bucket, etc.)
- Could mention retry policies with exponential backoff
- Could discuss fallback mechanisms for graceful degradation
- Might want to mention bulkhead pattern to isolate failures
- Could discuss monitoring and alerting thresholds for these mechanisms

### Question-4:
Your team needs to design a real-time collaborative document editing system (similar to Google Docs) that supports concurrent editing by multiple users. How would you approach this design, focusing particularly on:
1. Handling concurrent updates
2. Maintaining consistency across users
3. Managing real-time synchronization

Please explain your approach and any specific algorithms or patterns you would consider.
