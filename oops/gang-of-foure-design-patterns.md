# 23 Gang of Four (GoF) Design Patterns
Let's dive into the **23 Gang of Four (GoF) Design Patterns**. These patterns were documented in the seminal book "Design Patterns: Elements of Reusable Object-Oriented Software" by Erich Gamma, Richard Helm, Ralph Johnson, and John Vlissides.

---

## The 23 Gang of Four (GoF) Design Patterns: A Complete Guide

The GoF patterns are categorized into three main types: Creational, Structural, and Behavioral.

### I. Creational Design Patterns (5 Patterns)
These patterns deal with object creation mechanisms, trying to create objects in a manner suitable for the situation while increasing flexibility and reuse.

1.  **Abstract Factory**
    * **Purpose**: Provides an interface for creating families of related or dependent objects without specifying their concrete classes.
    * **Core Idea**: A "factory of factories." You have an interface for creating a set of products, and concrete factories implement that interface to produce specific variations of those products.
    * **Structure (Simplified)**:
        * `AbstractFactory`: Declares an interface for operations that create abstract product objects.
        * `ConcreteFactoryA`, `ConcreteFactoryB`: Implement the operations to create concrete product objects.
        * `AbstractProductA`, `AbstractProductB`: Declare interfaces for a type of product object.
        * `ProductA1`, `ProductA2`, `ProductB1`, `ProductB2`: Define concrete product objects to be created by corresponding concrete factories.
        * `Client`: Uses interfaces declared by `AbstractFactory` and `AbstractProduct` classes.
    * **Pros**:
        * Isolates concrete classes from the client code.
        * Ensures consistency among products in a family.
        * Makes it easy to introduce new families of products.
    * **Cons**:
        * Adding new types of products (not new families) requires modifying the `AbstractFactory` interface and all `ConcreteFactory` implementations.
    * **Use Cases**:
        * When a system needs to be independent of how its products are created, composed, and represented.
        * When a family of related product objects is designed to be used together, and you need to enforce this constraint.
        * Cross-platform UI toolkits (e.g., creating Windows, macOS, Linux specific buttons and checkboxes).
        * Creating different database connectors (SQL Server, Oracle, MySQL) where each connector has its own set of related objects (commands, connections, readers).

2.  **Builder**
    * **Purpose**: Separates the construction of a complex object from its representation, so that the same construction process can create different representations.
    * **Core Idea**: Provides a step-by-step approach to building a complex object. The builder object constructs parts of the product, and the director object orchestrates the building process.
    * **Structure (Simplified)**:
        * `Builder`: An abstract interface for creating parts of a product object.
        * `ConcreteBuilder`: Implements the `Builder` interface, constructing and assembling parts of the product, and providing a method to retrieve the result.
        * `Director`: Constructs an object using the `Builder` interface. It's responsible for the sequence of construction steps.
        * `Product`: The complex object being built.
    * **Pros**:
        * Allows fine-grained control over the construction process.
        * Allows changing the internal representation of the product.
        * Isolates complex construction code from the client code.
        * Makes objects immutable after construction (if the client only receives the final product).
    * **Cons**:
        * Requires creating a separate `Builder` for each complex object type.
        * Can lead to more classes if the construction process is simple.
    * **Use Cases**:
        * When the process for creating a complex object is independent of the parts that make up the object.
        * When the construction process must allow for different representations of the object being constructed.
        * Constructing complex SQL queries with many optional clauses.
        * Creating a multi-part document (e.g., PDF, HTML) where different builders can produce different formats.
        * Building a complex `HttpRequest` object with various headers, body, and query parameters.

3.  **Factory Method**
    * **Purpose**: Defines an interface for creating an object, but lets subclasses alter the type of objects that will be created.
    * **Core Idea**: Delegates object creation to subclasses. A "creator" class defines a method (the factory method) for creating products, and concrete creator subclasses override this method to return specific concrete products.
    * **Structure (Simplified)**:
        * `Product`: The interface for the objects the factory method creates.
        * `ConcreteProductA`, `ConcreteProductB`: Implement the `Product` interface.
        * `Creator`: Declares the factory method, which returns an object of type `Product`. It may also define a default implementation of the factory method that returns a `ConcreteProduct` object.
        * `ConcreteCreatorA`, `ConcreteCreatorB`: Override the factory method to return an instance of a `ConcreteProduct`.
    * **Pros**:
        * Eliminates the need to bind application-specific classes into your code.
        * Provides flexibility in creating objects.
        * Promotes loose coupling between the client and the product.
    * **Cons**:
        * Requires creating a new `ConcreteCreator` for each new `ConcreteProduct` type, potentially leading to a large number of classes.
    * **Use Cases**:
        * When a class cannot anticipate the class of objects it must create.
        * When a class wants its subclasses to specify the objects it creates.
        * Frameworks that need to standardize product creation but allow application-specific types.
        * Creating different types of vehicles (Car, Bike, Truck) based on user input.
        * Parsing different file formats (XML Parser, JSON Parser) where a factory method returns the correct parser instance.

4.  **Prototype**
    * **Purpose**: Specifies the kinds of objects to create using a prototypical instance, and creates new objects by copying this prototype.
    * **Core Idea**: Instead of instantiating new objects, you clone existing ones. This is useful when object creation is expensive or when you need to create objects that are similar to existing ones.
    * **Structure (Simplified)**:
        * `Prototype`: Declares an interface for cloning itself.
        * `ConcretePrototype`: Implements the cloning operation.
        * `Client`: Creates a new object by asking a prototype to clone itself.
    * **Pros**:
        * Reduces the number of classes required (no need for dedicated factories for each product).
        * Avoids calling constructors directly.
        * Can be more efficient than creating objects from scratch (especially if many objects are similar).
    * **Cons**:
        * Implementing deep copy can be complex, especially with circular references.
        * Every class that can be a prototype must implement the clone operation.
    * **Use Cases**:
        * When the system should be independent of how its products are created, composed, and represented.
        * When the classes to instantiate are specified at runtime (e.g., by dynamic loading).
        * When a system needs to create a large number of objects quickly, and many are similar.
        * Creating multiple instances of a complex object that share many common properties (e.g., game characters with similar base stats).
        * Implementing a document editor where you can copy and paste complex elements.

5.  **Singleton**
    * **Purpose**: Ensures a class has only one instance, and provides a global point of access to it.
    * **Core Idea**: The class itself is responsible for ensuring that only one instance of itself is created. It typically involves a private constructor and a static method or property that returns the single instance.
    * **Structure (Simplified)**:
        * `Singleton`: Defines a `private` static variable to hold the single instance and a `public` static method (or property) to return that instance. The constructor is `private` to prevent external instantiation.
    * **Pros**:
        * Guarantees a single instance, useful for resources like logging, configuration, or thread pools.
        * Provides a global access point.
        * Allows lazy initialization (instance is created only when first needed).
    * **Cons**:
        * Can introduce global state, which can make testing difficult and lead to hidden dependencies.
        * Violates the Single Responsibility Principle (SRP) by being responsible for its own creation and access.
        * Can be difficult to extend or modify if the "single instance" rule needs to change.
        * Concurrency issues need to be handled carefully in multi-threaded environments (e.g., double-checked locking).
    * **Use Cases**:
        * Logging services.
        * Configuration managers.
        * Database connection pools (though often managed by DI containers now).
        * A single `PrintSpooler` instance.
        * Cache managers.

### II. Structural Design Patterns (7 Patterns)
These patterns concern how classes and objects are composed to form larger structures.

1.  **Adapter**
    * **Purpose**: Converts the interface of a class into another interface clients expect. Adapter lets classes work together that couldn't otherwise because of incompatible interfaces.
    * **Core Idea**: Acts as a "wrapper" or "translator" between two incompatible interfaces.
    * **Structure (Simplified)**:
        * `Target`: The interface that the client expects.
        * `Adapter`: Implements the `Target` interface and contains an instance of the `Adaptee`. It translates requests from the `Target` interface to the `Adaptee`'s interface.
        * `Adaptee`: The existing class with an incompatible interface that needs to be adapted.
        * `Client`: Collaborates with objects conforming to the `Target` interface.
    * **Pros**:
        * Allows classes with incompatible interfaces to work together.
        * Promotes reusability of existing code.
        * Client code is decoupled from the `Adaptee`'s specific interface.
    * **Cons**:
        * Adds a new class (the `Adapter`), increasing complexity.
        * Can become complex if many adaptations are needed.
    * **Use Cases**:
        * Integrating a legacy system with a modern one.
        * Using a third-party library that doesn't conform to your system's interfaces.
        * Converting data formats (e.g., XML to JSON).
        * Connecting different types of plugs (e.g., power adapter for electronics).

2.  **Bridge**
    * **Purpose**: Decouples an abstraction from its implementation so that the two can vary independently.
    * **Core Idea**: Separates an object's abstraction from its implementation. It's about "prefer composition over inheritance" when dealing with orthogonal dimensions of change.
    * **Structure (Simplified)**:
        * `Abstraction`: Defines the abstraction's interface and maintains a reference to an `Implementor` object.
        * `RefinedAbstraction`: Extends the `Abstraction` to define platform-specific implementation.
        * `Implementor`: Defines the interface for implementation classes. This interface doesn't have to correspond exactly to `Abstraction`'s interface.
        * `ConcreteImplementorA`, `ConcreteImplementorB`: Implement the `Implementor` interface.
    * **Pros**:
        * Avoids a permanent binding between an abstraction and its implementation.
        * Allows for independent extension of both the abstraction and the implementation.
        * Improves extensibility and maintainability.
        * Avoids a "class explosion" when dealing with multiple dimensions of variation.
    * **Cons**:
        * Adds complexity due to increased number of classes.
        * Requires careful initial design to identify the two independent dimensions.
    * **Use Cases**:
        * When you want to avoid a permanent binding between an abstraction and its implementation.
        * When both the abstraction and its implementations should be extensible by subclassing.
        * Drawing shapes on different rendering APIs (e.g., `Shape` (Abstraction) with `DrawingAPI` (Implementor) which could be `OpenGL`, `DirectX`, etc.).
        * Sending messages via different channels (`Message` (Abstraction) with `MessageSender` (Implementor) which could be `Email`, `SMS`, `PushNotification`).

3.  **Composite**
    * **Purpose**: Composes objects into tree structures to represent part-whole hierarchies. Composite lets clients treat individual objects and compositions of objects uniformly.
    * **Core Idea**: Treats individual objects and collections of objects (composites) in the same way. This is achieved by having both individual objects and composite objects share a common interface.
    * **Structure (Simplified)**:
        * `Component`: Declares the interface for objects in the composition and for accessing and managing its child components.
        * `Leaf`: Represents leaf objects in the composition (objects that have no children).
        * `Composite`: Defines behavior for components having children, stores child components, and implements child-related operations in the `Component` interface.
        * `Client`: Manipulates objects in the composition through the `Component` interface.
    * **Pros**:
        * Simplifies client code by treating individual objects and composites uniformly.
        * Makes it easy to add new types of components.
        * Promotes recursive structure.
    * **Cons**:
        * Can make it difficult to restrict the types of components that can be added to a composite.
        * Some operations that make sense for `Leaf` objects might not make sense for `Composite` objects (and vice-versa), but the common interface might force them.
    * **Use Cases**:
        * Representing file systems (files and directories).
        * UI component trees (buttons, panels, windows).
        * Organizational charts (employees and departments).
        * Mathematical expressions (numbers and operations).

4.  **Decorator**
    * **Purpose**: Attaches new responsibilities to an object dynamically. Decorators provide a flexible alternative to subclassing for extending functionality.
    * **Core Idea**: Wraps an object with another object that adds new behavior before or after delegating the call to the original object.
    * **Structure (Simplified)**:
        * `Component`: Defines the interface for objects that can have responsibilities added dynamically.
        * `ConcreteComponent`: The object to which new responsibilities can be added.
        * `Decorator`: Maintains a reference to a `Component` object and conforms to the `Component` interface.
        * `ConcreteDecoratorA`, `ConcreteDecoratorB`: Add responsibilities to the component.
    * **Pros**:
        * More flexible than inheritance for extending functionality (avoids "class explosion").
        * Allows adding responsibilities to individual objects at runtime.
        * Supports stacking multiple decorators.
        * Avoids changes to existing code.
    * **Cons**:
        * Can result in a large number of small objects if many decorators are used.
        * Makes it harder to access the original object's specific methods (if not exposed through the common interface).
        * Can make debugging more complex as calls are passed through multiple layers.
    * **Use Cases**:
        * Adding logging, security, or caching to an existing object.
        * Runtime addition of features to a GUI component (e.g., scroll bars, borders).
        * I/O streams (e.g., `BufferedStream`, `CryptoStream` wrapping a base `FileStream`).
        * Coffee shop order system where you add milk, sugar, etc., to a base coffee.

5.  **Facade**
    * **Purpose**: Provides a unified interface to a set of interfaces in a subsystem. Facade defines a higher-level interface that makes the subsystem easier to use.
    * **Core Idea**: Simplifies access to a complex subsystem by providing a single, simplified entry point.
    * **Structure (Simplified)**:
        * `Facade`: Provides a simplified interface to the `Subsystem` classes. It delegates client requests to appropriate `Subsystem` objects.
        * `Subsystem Classes`: Implement the subsystem's functionality. They handle the work assigned by the `Facade` object. They have no knowledge of the facade.
        * `Client`: Uses the `Facade` to interact with the subsystem.
    * **Pros**:
        * Simplifies the interface to a complex subsystem.
        * Decouples the client from the implementation details of the subsystem.
        * Promotes layering of subsystems.
    * **Cons**:
        * Can become a "God Object" if too many responsibilities are dumped into it.
        * Might hide some of the subsystem's flexibility.
    * **Use Cases**:
        * Providing a simple interface to a complex set of libraries.
        * Wrapping a legacy API to present a cleaner interface.
        * Simplifying complex business logic operations (e.g., `OrderProcessingFacade` that orchestrates `InventoryService`, `PaymentService`, `ShippingService`).

6.  **Flyweight**
    * **Purpose**: Uses sharing to support large numbers of fine-grained objects efficiently.
    * **Core Idea**: When you have many similar objects, store their intrinsic (immutable, shareable) state once and allow extrinsic (variable, context-dependent) state to be passed in by the client. This reduces memory footprint.
    * **Structure (Simplified)**:
        * `Flyweight`: Declares an interface through which flyweights can receive and act on extrinsic state.
        * `ConcreteFlyweight`: Implements the `Flyweight` interface and stores intrinsic state.
        * `UnsharedConcreteFlyweight`: Not all `Flyweight` subclasses need to be shared. These typically contain a reference to other flyweights.
        * `FlyweightFactory`: Creates and manages `Flyweight` objects. Ensures that flyweights are shared correctly.
        * `Client`: Maintains references to flyweights and computes or stores extrinsic state.
    * **Pros**:
        * Reduces memory consumption, especially for large numbers of similar objects.
        * Improves performance by reducing object creation overhead.
    * **Cons**:
        * Increases complexity by separating intrinsic and extrinsic state.
        * Can be difficult to implement correctly, especially with concurrent access.
    * **Use Cases**:
        * Text editors (characters can be flyweights, storing font, size intrinsically, and position extrinsically).
        * Game development (e.g., trees in a forest where tree type is intrinsic, and position/scale is extrinsic).
        * Managing connections in a connection pool (the connection object is often a flyweight).

7.  **Proxy**
    * **Purpose**: Provides a surrogate or placeholder for another object to control access to it.
    * **Core Idea**: An object (the proxy) acts as an intermediary for another object (the subject), controlling access to it, and potentially adding additional behavior like lazy loading, access control, or logging.
    * **Structure (Simplified)**:
        * `Subject`: Defines the common interface for both the `RealSubject` and the `Proxy`, allowing the `Proxy` to be used anywhere the `RealSubject` is expected.
        * `RealSubject`: The actual object that the `Proxy` represents.
        * `Proxy`: Maintains a reference to the `RealSubject`. It controls access to the `RealSubject` and may create or destroy it. It can perform additional operations before or after forwarding a request.
    * **Pros**:
        * Provides controlled access to the `RealSubject`.
        * Can add functionality (e.g., security, caching, lazy loading, logging) without modifying the `RealSubject`.
        * Allows for remote access to objects.
    * **Cons**:
        * Adds a layer of indirection, which can slightly increase complexity and overhead.
        * Can be overused for simple access control.
    * **Use Cases**:
        * **Virtual Proxy**: Lazy initialization of an expensive object.
        * **Protection Proxy**: Control access based on permissions.
        * **Remote Proxy**: Providing a local representative for an object in a different address space.
        * **Logging Proxy**: Log all method calls to an object.
        * ORM frameworks (e.g., Entity Framework lazy loading related entities).

### III. Behavioral Design Patterns (11 Patterns)
These patterns are concerned with algorithms and the assignment of responsibilities between objects. They describe how objects communicate and interact.

1.  **Chain of Responsibility**
    * **Purpose**: Avoid coupling the sender of a request to its receiver by giving more than one object a chance to handle the request. Chain the receiving objects and pass the request along the chain until an object handles it.
    * **Core Idea**: A request passes along a chain of handlers. Each handler decides either to process the request or to pass it to the next handler in the chain.
    * **Structure (Simplified)**:
        * `Handler`: Defines an interface for handling requests and optionally for accessing the next handler in the chain.
        * `ConcreteHandlerA`, `ConcreteHandlerB`: Implement the `Handler` interface. They handle requests they are responsible for; otherwise, they forward them to their successor.
        * `Client`: Sends requests to the first handler in the chain.
    * **Pros**:
        * Reduces coupling between the sender and receiver.
        * Adds flexibility in assigning responsibilities to objects.
        * Allows for dynamic configuration of the chain.
    * **Cons**:
        * A request might go unhandled if no handler in the chain processes it.
        * Can be difficult to debug due to the dynamic nature of the chain.
        * Performance might be affected if the chain is very long.
    * **Use Cases**:
        * Event processing in GUI frameworks.
        * Error handling (e.g., different loggers for different error levels).
        * Approval workflows (e.g., purchase requests needing approval from multiple levels).
        * HTTP request processing in web frameworks (e.g., ASP.NET Core Middleware).

2.  **Command**
    * **Purpose**: Encapsulates a request as an object, thereby letting you parameterize clients with different requests, queue or log requests, and support undoable operations.
    * **Core Idea**: Turns a request into a standalone object. This object contains all information about the request (the receiver, the action to perform, and any arguments).
    * **Structure (Simplified)**:
        * `Command`: Declares an interface for executing an operation.
        * `ConcreteCommand`: Implements the `Command` interface by binding a `Receiver` and an action. It executes the action on the receiver.
        * `Client`: Creates a `ConcreteCommand` object and sets its `Receiver`.
        * `Invoker`: Asks the `Command` to carry out the request. It doesn't know about the `ConcreteCommand` or `Receiver`.
        * `Receiver`: Performs the actual work when the command's `Execute` method is called.
    * **Pros**:
        * Decouples the invoker from the receiver of the request.
        * Allows for queuing, logging, and undo/redo operations.
        * Provides a clean way to add new commands without changing existing code.
    * **Cons**:
        * Increases the number of classes for each command.
        * Can become complex for simple operations.
    * **Use Cases**:
        * Undo/redo functionality in applications.
        * Macro recording.
        * Implementing a queue of tasks.
        * Remote controls for devices (each button press is a command).
        * Transactional systems (commands can be rolled back).

3.  **Interpreter**
    * **Purpose**: Given a language, define a representation for its grammar along with an interpreter that uses the representation to interpret sentences in the language.
    * **Core Idea**: Builds an abstract syntax tree (AST) for sentences in a language and uses it to interpret the sentences.
    * **Structure (Simplified)**:
        * `AbstractExpression`: Declares an abstract `Interpret` operation.
        * `TerminalExpression`: Implements an `Interpret` operation associated with terminal symbols in the grammar.
        * `NonterminalExpression`: Implements an `Interpret` operation for nonterminal symbols.
        * `Context`: Contains information global to the interpreter.
        * `Client`: Builds the abstract syntax tree and invokes the `Interpret` operation.
    * **Pros**:
        * Easy to change and extend the grammar.
        * Easy to implement the grammar.
    * **Cons**:
        * Can be complex if the grammar is large.
        * Might not be efficient for very complex grammars.
        * Grammar changes require changes to the class hierarchy.
    * **Use Cases**:
        * SQL parsers.
        * Regular expression evaluators.
        * Simple scripting languages or domain-specific languages (DSLs).
        * Converting roman numerals to integers.

4.  **Iterator**
    * **Purpose**: Provides a way to access the elements of an aggregate object sequentially without exposing its underlying representation.
    * **Core Idea**: Decouples the algorithm for traversing a collection from the collection itself.
    * **Structure (Simplified)**:
        * `Iterator`: Declares an interface for accessing and traversing elements (e.g., `HasNext()`, `Next()`, `CurrentItem()`).
        * `ConcreteIterator`: Implements the `Iterator` interface and keeps track of the current position in the traversal.
        * `Aggregate`: Declares an interface for creating an `Iterator` object.
        * `ConcreteAggregate`: Implements the `CreateIterator()` method to return an instance of `ConcreteIterator`.
    * **Pros**:
        * Supports multiple traversals of the same collection.
        * Simplifies the aggregate interface by removing traversal responsibilities.
        * Promotes loose coupling between the client and the collection.
    * **Cons**:
        * Can introduce extra classes for each type of collection.
        * In modern languages with built-in iterators (like C#'s `IEnumerable`/`IEnumerator` and `yield return`), explicitly implementing this pattern is often unnecessary.
    * **Use Cases**:
        * Traversing any collection of objects (lists, trees, graphs).
        * Custom collection classes where specific traversal logic is needed.
        * Standard library collections (lists, arrays, dictionaries).

5.  **Mediator**
    * **Purpose**: Defines an object that encapsulates how a set of objects interact. Mediator promotes loose coupling by keeping objects from referring to each other explicitly, and it lets you vary their interaction independently.
    * **Core Idea**: Centralizes communication between objects (colleagues). Instead of objects communicating directly, they communicate through the mediator.
    * **Structure (Simplified)**:
        * `Mediator`: Defines an interface for communicating with `Colleague` objects.
        * `ConcreteMediator`: Implements the `Mediator` interface and coordinates communication between `Colleague` objects. It knows and maintains its `Colleague`s.
        * `Colleague`: Defines an abstract class for objects that communicate with the `Mediator`.
        * `ConcreteColleagueA`, `ConcreteColleagueB`: Implement the `Colleague` interface and communicate with their `Mediator` when they need to interact with other `Colleague`s.
    * **Pros**:
        * Reduces coupling between colleagues.
        * Simplifies object protocols (one-to-many communication becomes one-to-one to mediator).
        * Makes it easier to change or reuse individual colleagues.
    * **Cons**:
        * The mediator can become a "God Object" if it handles too much logic.
        * Can increase complexity if the interactions are simple.
    * **Use Cases**:
        * GUI applications where controls interact with each other (e.g., a dialog box with many interacting widgets).
        * Chat rooms, where the mediator manages messages between participants.
        * Air traffic control systems, where the tower (mediator) coordinates flights (colleagues).
        * CQRS (Command Query Responsibility Segregation) implementations using libraries like MediatR.

6.  **Memento**
    * **Purpose**: Without violating encapsulation, capture and externalize an object's internal state so that the object can be restored to this state later.
    * **Core Idea**: Allows an object's state to be saved and restored without exposing its internal structure.
    * **Structure (Simplified)**:
        * `Originator`: The object whose state needs to be saved and restored. It creates a `Memento` containing a snapshot of its current state and uses a `Memento` to restore its state.
        * `Memento`: Stores the internal state of the `Originator` object. It provides a restricted interface to the `Caretaker` and a wider interface to the `Originator`.
        * `Caretaker`: Responsible for keeping the `Memento`. It never operates on or examines the contents of the `Memento`.
    * **Pros**:
        * Preserves encapsulation of the `Originator`.
        * Provides a simple mechanism for undo/redo functionality.
    * **Cons**:
        * Can be memory-intensive if `Memento` objects store a lot of state.
        * Managing the lifecycle of `Memento` objects can be tricky.
        * The `Caretaker` has no knowledge of the `Memento`'s content, which can make debugging harder.
    * **Use Cases**:
        * Undo/redo mechanisms in editors or applications.
        * Saving game states.
        * Database transactions (though often handled by the database itself).
        * Restoring an object to a previous stable state.

7.  **Observer**
    * **Purpose**: Defines a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.
    * **Core Idea**: A "subject" maintains a list of "observers" and notifies them of any state changes, usually by calling one of their methods.
    * **Structure (Simplified)**:
        * `Subject`: The object being observed. It maintains a list of `Observer`s, provides methods to attach/detach observers, and notifies them of state changes.
        * `Observer`: Defines an interface for objects that should be notified of changes in a `Subject`.
        * `ConcreteSubject`: Implements the `Subject` interface, stores state, and notifies observers when its state changes.
        * `ConcreteObserver`: Implements the `Observer` interface and registers with `ConcreteSubject`s to receive updates.
    * **Pros**:
        * Promotes loose coupling between subject and observer.
        * Supports broadcast communication (one-to-many).
        * Makes it easy to add new observers without modifying the subject.
    * **Cons**:
        * Order of notification is not guaranteed.
        * Can lead to unexpected updates if not carefully managed.
        * Potential for "notification storm" if subjects change frequently and many observers are registered.
    * **Use Cases**:
        * GUI event handling (e.g., button clicks updating a display).
        * Model-View-Controller (MVC) architecture (View observes Model).
        * RSS feeds and subscription services.
        * Stock market applications where users are notified of price changes.
        * Built-in events and delegates in C# are a direct implementation.

8.  **State**
    * **Purpose**: Allows an object to alter its behavior when its internal state changes. The object will appear to change its class.
    * **Core Idea**: Encapsulates state-dependent behavior in separate state objects. The main object delegates its behavior to the current state object.
    * **Structure (Simplified)**:
        * `Context`: The object whose behavior changes based on its state. It maintains a reference to a `ConcreteState` object and delegates state-specific requests to it.
        * `State`: Declares an interface for encapsulating the behavior associated with a particular state of the `Context`.
        * `ConcreteStateA`, `ConcreteStateB`: Implement the `State` interface. Each concrete state handles requests for its state and may transition the `Context` to another state.
    * **Pros**:
        * Organizes state-dependent behavior into separate classes, making it easier to add new states.
        * Avoids large conditional (`if-else` or `switch`) statements.
        * Improves readability and maintainability.
    * **Cons**:
        * Can lead to an increased number of classes, especially for many states.
        * State transitions can be complex to manage if there are many possible transitions.
    * **Use Cases**:
        * Implementing a TCP connection (Closed, Listening, Established, etc.).
        * Vending machine states (HasCoin, NoCoin, SoldOut).
        * Traffic light states (Red, Green, Yellow).
        * Order processing workflow (New, Pending, Shipped, Delivered, Canceled).

9.  **Strategy**
    * **Purpose**: Defines a family of algorithms, encapsulates each one, and makes them interchangeable. Strategy lets the algorithm vary independently from clients that use it.
    * **Core Idea**: Defines a common interface for a family of algorithms. A client (context) holds a reference to a concrete strategy and uses it to perform an action.
    * **Structure (Simplified)**:
        * `Context`: Maintains a reference to a `Strategy` object and uses it to execute an algorithm.
        * `Strategy`: Declares an interface common to all supported algorithms. The `Context` uses this interface to call the algorithm.
        * `ConcreteStrategyA`, `ConcreteStrategyB`: Implement the `Strategy` interface with a specific algorithm.
    * **Pros**:
        * Allows algorithms to vary independently from the clients that use them.
        * Avoids large conditional statements for selecting algorithms.
        * Enables dynamic selection of algorithms at runtime.
        * Improves extensibility by making it easy to add new algorithms.
    * **Cons**:
        * Adds extra classes for each algorithm.
        * The client must be aware of the different strategies to choose the correct one.
    * **Use Cases**:
        * Sorting algorithms (QuickSort, MergeSort, BubbleSort).
        * Payment processing methods (CreditCardPayment, PayPalPayment).
        * Tax calculation algorithms for different regions.
        * Navigation algorithms (Driving, Walking, Cycling).

10. **Template Method**
    * **Purpose**: Defines the skeleton of an algorithm in an operation, deferring some steps to subclasses. Template Method lets subclasses redefine certain steps of an algorithm without changing the algorithm's structure.
    * **Core Idea**: A base class defines a "template" method that outlines the sequence of steps for an algorithm. Some steps are concrete implementations, while others are abstract "hooks" that subclasses must implement.
    * **Structure (Simplified)**:
        * `AbstractClass`: Defines the template method, which is a final (non-overridable) method orchestrating the algorithm. It also defines abstract primitive operations that subclasses must implement and concrete operations that are shared.
        * `ConcreteClass`: Implements the abstract primitive operations to carry out specific steps of the algorithm.
    * **Pros**:
        * Enforces a consistent algorithm structure across subclasses.
        * Allows subclasses to customize specific parts of the algorithm.
        * Promotes code reuse.
    * **Cons**:
        * Subclasses can only vary behavior at predefined "hook" points.
        * Changes to the template method's structure can be difficult.
        * Can lead to tight coupling between the base class and its subclasses if not carefully designed.
    * **Use Cases**:
        * Building systems (e.g., `BuildHouse` with steps like `LayFoundation`, `BuildWalls`, `InstallRoof`, where specific materials or styles are defined by subclasses).
        * Data processing frameworks (e.g., `ProcessFile` with steps like `OpenFile`, `ReadData`, `ProcessLine`, `CloseFile`).
        * Test frameworks (setup, run test, teardown).
        * Algorithm with fixed steps but varying implementations for some steps.

11. **Visitor**
    * **Purpose**: Represents an operation to be performed on elements of an object structure. Visitor lets you define a new operation without changing the classes of the elements on which it operates.
    * **Core Idea**: Separates an algorithm from the object structure on which it operates. You "visit" each element in a structure, and the visitor object performs the operation specific to that element's type.
    * **Structure (Simplified)**:
        * `Visitor`: Declares a `Visit` operation for each concrete class of `Element` in the object structure.
        * `ConcreteVisitorA`, `ConcreteVisitorB`: Implement the `Visitor` interface. Each operation implements a fragment of the algorithm for the corresponding class of `Element`.
        * `Element`: Declares an `Accept` operation that takes a `Visitor` as an argument.
        * `ConcreteElementA`, `ConcreteElementB`: Implement the `Accept` operation.
        * `ObjectStructure`: Can enumerate its elements and provides a high-level interface to accept a visitor.
    * **Pros**:
        * Allows adding new operations to existing object structures without modifying them.
        * Centralizes operations on an object structure.
        * Works well with Composite patterns.
    * **Cons**:
        * Adding new `ConcreteElement` types requires changing every `Visitor` interface and all concrete visitors.
        * Violates encapsulation by exposing the internal structure of elements to visitors.
        * Can be overly complex for simple operations.
    * **Use Cases**:
        * Compilers and interpreters (performing type checking, code generation on an Abstract Syntax Tree).
        * Exporting different formats for a complex document structure (e.g., export to XML, JSON, PDF).
        * Performing operations on a graph or tree structure (e.g., calculating total cost of items in a shopping cart with different item types).

---

**Important Considerations When Using GoF Patterns:**

* **Not a Silver Bullet**: Patterns are solutions to common problems, not every problem. Don't force a pattern where it doesn't fit.
* **Vocabulary**: Patterns provide a common language for developers to discuss architectural and design solutions.
* **Flexibility & Maintainability**: Correctly applied patterns can lead to more flexible, modular, and maintainable code.
* **Complexity**: Introducing a pattern often means introducing more classes. Use them when the benefits (flexibility, maintainability) outweigh the increased complexity.
* **Modern Language Features**: Some patterns might be implicitly handled or simplified by modern language features (e.g., C#'s delegates/events for Observer, LINQ for Iterator/Strategy).
* **Anti-Patterns**: Be aware of anti-patterns (common solutions that are usually counterproductive) to avoid them.

This guide provides a solid foundation for understanding the 23 GoF design patterns. For each pattern, diving into specific code examples in your preferred language (like C#, Java, Python) would further solidify your understanding.
