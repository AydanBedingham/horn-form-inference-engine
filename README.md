
# Horn-form Inference Engine

## Summary
Inference engine for propositional logic in software based on the Truth Table (TT) checking, and Backward Chaining (BC) and Forward Chaining (FC) algorithms.

The inference engine will take as arguments a Horn-form Knowledge Base (KB) and a query (q) which is a proposition symbol and determine whether q can be entailed from KB. The output from the KnowledgeBase query execution will be printed to screen.

## Process

1) Commandline parameters are used to identify ReasoningMethod (Enum) and Filename (String).
2) An InferenceLoader (Class) is instantiated and uses the Filename to load the problem info. The TELL statement is identified. Individual sentences relating to facts and clauses are identified. The ASK statement (query) is identified.
3) The KnowledgeBase is then instantiated and sentences from the InferenceLoader are added as facts and clauses.
4) The KnowledgeBase executes the query from the InferenceLoader with a given ReasoningMethod.
5) The output from the KnowledgeBase query execution will be printed to screen.


## TT, FC and BC Inference Engine

Given a knowledge base KB in Horn form (with TELL) and a query q which is a proposition symbol (with ASK), the program will answer whether or not q is entailed from KB using one of the three algorithms:
- FC (Forward Chaining)
- BC (Backward Chaining)
- TT (Truth Table Checking)

File Format: The problems are stored in simple text files consisting of both the knowledge base and the query:
- The knowledge base follows the keyword TELL and consists of Horn clauses separated by semicolons.
- The query follows the keyword ASK and consists of a proposition symbol. For example, the following could be the content of one of the test files (test1.txt):

```
TELL
p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2;
ASK
d
```

## Command Line Operation

The program operates in a simple command-line form to support batch testing. This can be accomplished with a simple DOS .bat (batch) file if needed. Below is an example of how the program will be run:

- `iengine method filename` where iengine is the program, method can be either TT (for Truth Table checking), FC (for Forward Chaining), or BC (for Backward Chaining) to specify the algorithm, and filename is for the text file consisting of the problem. For Instance: `iengine FC test1.txt`.

For example, running iengine with method TT on the example test1.txt file should produce the following output: `YES: 3`

On the other hand, running iengine with method FC on the example test1.txt should produce: `YES: a, b, p2, p3, p1, d`

And running iengine with method BC on the example test file above should produce the following output: `YES: p2, p3, p1, d`

## Reasoning Methods - Forward-chaining (Deductive Reasoning)

Forward chaining is the logical process of inferring unknown truths from known data and moving forward using determined conditions and rules to find a solution. Forward chaining is analogous to Deductive Reasoning in that the truth of the premises ensures the truth of the conclusion.

In propositional logic the Forward-chaining algorithm is used to determine if a given symbol is entailed by a knowledge base of definite clauses. The available data is used in conjunction with inference rules to extract more data until a goal is reached.
Forward chaining uses a breadth-first style traversal approach in that all nodes of a given hierarchical level are examined before proceeding to the next level.

It begins by examining known facts, facts can be added to the agenda to initiate new inferences. It then uses these facts as a basis for examining clauses. When dealing with clauses, if all the conjunctions of the clause’s premises are known then the implication of (what is entailed by that clause) will be added to the set of known facts.

```
TELL:
- croaks AND eats-flies ENTAILS frog
- frog ENATAILS green

ASK:
- Fred croaks AND Fred eats-flies

FORWARD CHAIN:
- Fred croaks AND Fred eats flies
- Fred croaks AND eats-flies
- Fred ENTAILS frog
```

This program creates an agenda as a list and all facts are added to it. A loop is then used to dequeue items from the agenda, if the symbol currently being examined is the premise of a clause then the edge count for that clause is decremented by one. Once a clause has no more edges then we know all the symbols in its premise correspond to items that have already been entailed and it can be added to the agenda.

The agenda will continue to be iterated through until either there are no agenda items left (indicating that the ASK query is not entailed by facts and clauses in the TELL) or until a clause (with all symbols in premise identified) that entails the query is found. An advantage that forward-chaining offers over Backward Chaining is that new data can trigger new inferences which would make it preferable in situations where data is being added dynamically.

## Reasoning Methods - Backward Chaining (Inductive Reasoning)

Backward Chaining is the logical process of inferring unknown truths from known conclusions by moving backward from a solution to determine the initial conditions and rules. This type of chaining starts from the goal and moves backward to comprehend the steps that were taken to attain this goal. Backward Chaining is analogous to Inductive Reasoning in that a specific conclusions are used to ascertain generalised facts.

In propositional logic the Backward Chaining algorithm is used to determine if a given symbol is entailed by a knowledge base by working backwards from the goal. It works by checking if the query is true and (in the event that it isn’t) by identifying what implications in the knowledge base conclude that the query is true. The Backward Chaining reasoning method follows a depth-first style approach to examining clauses in that it fully evaluates a clause’s premises before moving on to the next clause. 

```
TELL:
- croaks AND eats flies ENAILS frog
- frog ENAILS green
- Fred croaks
- Fred eats-flies

ASK:
- who is a frog?

BACKWARD CHAIN:
- what ENTAILS frog
- what croaks AND eats flies
- what croaks AND what eats-flies
- Fred croaks AND Fred eats flies
- Fred ENTAILS Frog
```

The program implements Backward Chaining in a similar way to forward-chaining except the order items from the agenda are examined is reversed. The Backward Chaining reasoning method typically requires fewer operations and performs faster than the forward-chaining method as the algorthym only looks at the relevant facts.

## Reasoning Methods - Truth-Table

Truth-table solving involves building a truth-table for all possible combinations of values involved in the formula. However the truth-table solving method also comes with one severe drawback. Because the truth-table relies on solving a problem exhaustively, the number of rows it contains grows rapidly based on the number of variables in the formula.

This program implements truth-table checking involved using basic maths to dynamically construct a truth-table based on a given number of columns and populate it with the required Boolean combinations. The number of columns corresponds to all the symbols used in facts and clauses.

After a list of all symbols have been established and a truth-table created, the program then examines each clause/fact and removes entries from the truth-table that correspond to worlds where the clause/fact is invalid. It then removes all worlds where the query is false. The worlds remaining represent all worlds where the query is true.

The Truth-Table checking method is typically more memory intensive than forward chaining and Backward Chaining as the number of rows it contains grows rapidly in relation to the number of symbols used. Truth-table checking is conceptually simpler than the forward-chaining and Backward Chaining.

## Terminology

- Fact: A truth value of a single premise.
- Clause: A sentence with a => in it.
- Premise: A previous statement or proposition from which another is inferred or follows as a conclusion.
- Conjunction:  a truth-functional connective similar to "and" in English.
- Implication: A conclusion that can be drawn from something.
- Algorithm: A process or set of rules to be followed in calculations or other problem-solving operations.
- Knowledge base (KB): an information repository that provides a means for information to be collected, organized, shared, searched and utilized.
- Query: Ask a question about something.
- Entailed: a logically necessary consequence.
- Sentence: A claim in the horn-form format using conjunction(&) and conditional (=>).