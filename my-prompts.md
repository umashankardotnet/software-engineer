## Scalability
- System Design - What is scalability, explain in detail considering all the cases. How can you achieve it? What are the AWS and Azure services which helps to develop scalable solutions. Provide some real-time use cases. What are the challenges to build scalable solutions? Consider cost and security practices as well. Where example is required consider .NET and C# as I am a .NET developer.
-  Provide architecture diagrams wherever possible.
-  how an Architect, principle engineer or lead engineer should think and provide his advices around this during brain storming sessions

## A Guide to wtite good prompt
- Prompt gives little guidance and leaves a lot to the model’s interpretation
- Prompt Engineering = developing, designing, and optimizing prompts to enhance the output of FMs for your needs
- Improved Prompting technique consists of:
  - Instructions – a task for the model to do (description, how the model should perform)
  - Context – external information to guide the model
  - Input data – the input for which you want a response
  - Output Indicator – the output type or format
### Negative Prompting
- A technique where you explicitly instruct the model on what not to include or do in its response
- Negative Prompting helps to:
  - Avoid Unwanted Content – explicitly states what not to include, reducing the chances of irrelevant or inappropriate content
  - Maintain Focus – helps the model stay on topic and not stray into areas that are not useful or desired
  - Enhance Clarity – prevents the use of complex terminology or detailed data, making the output clearer and more accessible
 
### Prompt Example
Instructions: What is scalability, explain in detail considering all the cases. How can you achieve it? What and how AWS services which helps to develop scalable solutions, provide some explanation of each service. Provide some real-time use cases. What are the challenges to build scalable solutions? Consider cost and security practices as well. how an Architect, principle engineer or lead engineer thinks about it in brainstorming session.
Context: I am a .NET background professional, and going to teach for system design concepts.
Input data: Explain scalability in system design. My target audience is Senior developers and lead engineers who are targeting for product based companies interviews.
Output Indicator: A easy, understandable guide to explain scalability in system design.
