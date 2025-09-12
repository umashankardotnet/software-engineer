# LMTS @ Salesforce
[5 Things You Really Need to Know For Your Technical Interview From a Lead Engineer](https://engineering.salesforce.com/5-things-you-really-need-to-know-for-your-technical-interview-from-a-lead-engineer-95c9f7cde3a9/)

Based on the interview reviews, here's a comprehensive guide to prepare for the Lead Member of Technical Staff (LMTS) role at Salesforce:

### Interview Process Structure
1. Initial Screening
   - Recruiter phone screen
   - Hiring Manager round (behavioral/experience discussion)

2. Technical Assessment
   - HackerRank coding test (2-3 programming questions)
   - Online coding exercise

3. On-site/Virtual Interviews (4-5 rounds)
   - 2-3 Coding rounds
   - 1-2 System Design rounds
   - Behavioral/Managerial round
   - Senior Director round (for some candidates)

### Key Areas to Prepare

1. **Coding/DSA**
   - Data structures & algorithms
   - Common problems:
     - Longest increasing subsequence
     - Tree construction (preorder/inorder)
     - Graph problems
     - String manipulation
     - Linked list operations
     - Stack problems (Stock span)
   - Focus on optimization and edge cases
   - Practice whiteboard coding and online coding platforms

2. **System Design**
   - Low-level design:
     - Parking lot system
     - Snake and ladder game
     - Coupon management system
   - High-level design:
     - Instagram-like systems
     - Shopify-like platforms
     - URL shortener
     - Workflow management
     - High-volume data ingestion
     - Caching systems

3. **Technical Concepts**
   - Java fundamentals
   - Python concepts (GIL, tuples, dictionaries)
   - C++ (friend functions)
   - Git commands (fetch, pull, reset, stash)
   - Database design (ACID properties)
   - REST APIs
   - Performance optimization
   - CI/CD and DevOps

4. **UI/Frontend (if applicable)**
   - Accessibility
   - ES6 features
   - Responsiveness
   - Web performance
   - Memory optimization

5. **Behavioral/Leadership**
   - Conflict resolution
   - Team management
   - Project prioritization
   - Communication skills
   - Problem-solving approach
   - Past experience examples

### Preparation Tips
1. Practice coding on platforms like LeetCode and HackerRank
2. Study system design patterns and scalability concepts
3. Review fundamental CS concepts
4. Prepare concrete examples from past experiences
5. Practice explaining technical solutions clearly
6. Be ready to discuss trade-offs in design decisions
7. Focus on both theoretical knowledge and practical implementation

### Interview Success Factors
- Clear communication
- Problem-solving approach
- Code quality and optimization
- System design considerations
- Leadership qualities
- Cultural fit
- Technical depth and breadth

This role requires strong technical expertise combined with leadership abilities, so prepare accordingly across all these areas.

## Good interview Discussions
- https://leetcode.com/discuss/post/4973910/salesforce-lmts-interview-by-anonymous_u-h12k/
- https://leetcode.com/discuss/post/4312326/salesforce-smtslmts-by-anonymous_user-3pd5/

## Some specific questions
### Coding:

	One question was related to Strings and Array manipulation, I would say leetcode medium, second question was related to graph, which 	was very difficult. 75 min is given to complete the test.

	Add and Search Word

	[DS-Algo] One DS algo problem which can be solved using priority queue. Never heard about the problem but it was easy one

	[DS-Algo] Complete multiple linked list operations without extra space and without swpping data. The operations were expected to 	be done by using pointers/references only

	find greatest number by swapping adjacent digits of same parity (odd-odd/even-even)
	something with marketing campaign management. (simple DP problem)
	
	print all paths from top-left corner to bottom-right corner.
	
	evaluate expression; calculator-III

	Graph based

	Round 1 (DSA): Given a matrix with positive weights and -1(block, cannot move). Find maximum weight path such that no path exists 	after the last reachable block. Movement is possible in all 8 possible directions.

### Design:(HLD + LLD)
	Discussion on current project architectire
	Design Tagging System
	Design Social Media Comments, Likes, Follow
	Design Airflow
	Design Netflix recommendations

### HLD:
	Design a URL shortening service with multiple requirements from different internal teams.

	[HLD] Design a data model for News Feed system and how can we optimise get feed API response time

	Design rule engine for banking system; like if salary is X and account is 10 years old; user should be given Z % discount.

	Round 4 (HLD): Design chat service.

### LLD:
	A usecsae to refactor and have extension on existing code base. A Solution was expected to adhere a design patter and have it open 	for extension.

	Design stackoverflow.He asked me to design schema for it. wrote code for it as well. Then he asked about search functionality. He 	wanted me to write code for how elastic search does the searching; in particular what data structure it uses [LSM tree etc.]

	Round 2 (LLD): Design Snake & Ladder Game with working solution to be run on the platform. Focus was on design patter, use of 	classes, execution.

### HM Round:
	Situational Questions
	Team Motivations
	Maintaining Escalations
	Best practices for maintaining quality
	Discussion on responsibilities, how you prioratise tasks, how you handle conflicts in the discussion, how you mentor and groom 	your subordinates.

	Round 3 (Director): LP questions with current project details on how you lead the project to completion and how you handled 	failures, mentoring etc.



