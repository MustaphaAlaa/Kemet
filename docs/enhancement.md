
# Wrong Structure

### Problem:
The current service structure uses a pattern like
OperationCore, Operation, and OperationInternal (e.g., UpdateCore, Update, UpdateInternal).  
This approach has proven to be unnecessarily complex and leads to redundant code.
 
## Solution

back to the old structure without saving in the same method and create save method and it invokes when needed

We will refactor services to simplify this structure.  
Core operation methods (e.g., Update, Create, Delete) will handle the primary logic without automatically saving changes.  
A separate SaveAsync method will be introduced to handle persistence.  
This SaveAsync method will be called explicitly when changes need to be committed to the database.

 

so this reduce the code and make it more readable and maintainable
 

 this will done in all filles that support this wrong structure

#### When/Where to apply this structure
These changes will be introduced in an upcoming commit.  
most likely in seperate branch (enhancement).