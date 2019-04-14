# Overview

This application takes you through the process of making a project easier to understand by making a series of updates. The exercise is part of the [Writing Professional Code](https://www.edx.org/course/writing-professional-code-microsoft-dev275x) course on edX. 

> The order in which you perform these updates is not important. We are trying to show a set of *code smells* that can exist in a codebase such as this one and how you can make things better. 


# Commits/ Tutorial Outline

You can check out any point of the tutorial using:

> git checkout update-?

To see the changes made between any two lessons use the git diff command:

> git diff update-?...update-?

### update-0 Initial Project
The project that is checked in is a **.NET Core** console app. To run the app, type the following at the command line. 

> `dotnet run -a`

You should then explore the code to see what other options are available, how the app is structured and what improvement can be. 

### update-1 Consistency

Coding style matters in a project because it removes one more thing to worry about as you try to read and understand the code. In this update, we'll re-position some brackets to make sure the style is consistent. We'll also make sure there is space above code comments. All of this makes the source code easier to read at a glance. 

### update-2 Guard Clauses

If you can exit early from a method, you should consider doing so. It's a good technique for handling obvious error conditions in a code path and helps the reader of your code "park" those paths in their head. In this update, we verify that the user is calling the program with the correct argument count before proceeding.

### update-3 It's all in the Name

We can convey a lot of information in a well-crafted variable, method or class name. The code we inherited here is full of one-letter names which don't convey much information. In this first naming pass, we'll remedy this situation as best we can.

### update-4 Refactor Common Code

At this point we can clearly see some repetition in our code - each conditional block is retrieving data from a stream and writing back to a file. These operations are good refactoring candidates and in this update, we'll move the repetitive code into its own methods. Since streams use system resources, we will also make sure to release those resources with the help of the `using` construct.

### update-5 Remove String Literals

Examining our **Program.cs** file in the **students-list** project, we can see lots of quotation marks. This is a potential *code smell* and can often lead to coding errors. String literals can also lead to confusion. For example, the command-line arguments `a` `r` and `?` don't mean much on first inspection. It would be much nicer to have a descriptive name for these literals greet you instead. 

In this update, we turn the string literals into constants and group them together into a Constants class. Not only does that mean I have just one location where I need to make an update, but I can also describe these entities independently of the length of the string literal itself. For example, the argument `a` now becomes Constants.ShowAll, which is a big readability gain. 

This update also resulted in us finding a bug. When we call UpdateContent to add an entry, the entry was separated by a comma with no space after it. However, the code to count entries currently counts words based on space boundaries. This leads to a wrong count. By using the same delimiter constant (`StudentEntryDelimiter`) when reading or writing to the list, we eliminate this problem. (The word counting method will get its own update further in this tutorial).

Another small, but important, change is to prepend each argument with a minus `-` sign. This is a more common syntax for command-line arguments. 

> *Code Smell* is a term of endearment in the Software Development world we give to code or code structures that indicate that something is not quite right. The example here is string literals. In this case, they give the impression that the developer wasn't quite finished making the code readable and self-describing. Smells often occur with the structure of a piece of software. If a good way of structuring or implementing something is defined as a pattern, a smell can lead to, or result, in anti-patterns. If you are interested in learning more about this topic, start with the [Code Smell Wikipedia page](https://en.wikipedia.org/wiki/Code_smell).

### update-6 Eliminate Temporary Variables

When reviewing variables, ask yourself whether each one is needed. How often is the variable used and does it add value in terms of clarification of intent? In this update, we eliminate two variables that add no real value. Instead of storing a result in a variable, we return the result directly. In another instance, it is as easy to pass DateTime.Now to a method as it is to create a variable to hold that value. 

### update-7 Eliminate Control Flow Variables

A control-flow variable is an extraneous variable that is used to influence the flow of code. Typically our code branches based on conditions or state. What we're talking about here is unnecessary use of variables as part of this control. Whenever you see a variable like this, be suspicious. It is likely that we can re-structure the flow of logic in our code  to make these type of variables go away. For example, in this example we introduce a break to exit a for loop when we have found what we're looking for. You will notice that during this re-write we also discovered a bug in our code because of missing brackets around the `if` check.

### update-8 Simplification (of count operation)

Simplification is a great way to improve the readability of your code and make it easier to maintain. When you inherit a  project, you should look for ways to simplify and refactor code as early as possible. Removing code that isn't needed or code that is not used (dead code) is a great simplification opportunity.  In this update, we will simplify the code used to count how many students are stored in **student-list.txt**.  We're not actually counting words, we're figuring out how many students are in the list. This update uses the fact that the entries are separated by design with a comma.


### update-9 Handle invalid arguments

It is never a good idea to let a user fall through your app without telling them what happened. To see this issue in action, just run the app from your command-line as follows:

> `dotnet run unknown-argument`

The user receives a silent response and that's very frustrating to the user, who must surely be  asking all kinds of questions such as:

* *"Did it work?"*

* *"What have I done?"*

* *"Is it me, or that program?"*

Ok, so perhaps we are exaggerating the crisis that ensues when the user encounters this problem. However, we as developers should always be looking for ways to build a quality relationship with the people how use our software. In this case, it means handling invalid arguments and that's our update for this lesson. 

 When you call this program with the wrong parameter, the program returns silently. This update checks for this case and shows the usage message to the user. Since showing the usage message now happens more than once, we'll refactor it into a method.

## update-10 Naming and Comment Pass

Now that we have made lots of changes and, through this work, learned more about the intent of our code, it's time to make another naming and comment pass. This is a cheap and relatively safe update to make and has the potential to make a real impact on the readability and maintainability of our code. 

By now, we realize that our program  specializes in performing operations on the very specific student-list. That's ok, there isn't anything wrong with that. Scenarios like this can often happen in Line Of Business (LOB) apps inside an organization where a department or team has a process, pipeline or experience that could benefit from automation.

In this update, we introduce names that mean more in the student list management domain.  At this point it is also important to document things we would improve if given more time. This updates prepends the common **TODO:** marker in comments to document things to do at a later time. 

Finally, one opportunity that we missed with the `Constants.cs ` class was to write proper triple-slash `///` comments. By doing this, the constants are described in full during code (dot) completion.

## update-11 One More Thing

In the preceding update, we left behind comments prepended with **TODO** markers for any updates we want to make in the future. let's tackle a couple of those just to clear a few remaining code smells. This update improves our code in the following ways:

* Remove brittle code caused by using String.Contains, when we meant String.StartsWith

* Simplify search using LINQ

* Tidy comments.

## Next Steps

We have come a long way on this journey to improve the student-list code we inherited. The code is now more robust, reads better and is set up for more improvements. The remaining TODO items will be a godo place to start.

