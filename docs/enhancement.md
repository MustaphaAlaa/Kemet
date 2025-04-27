
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
 

 this is done in all filles that support this wrong structure
 


 # Create GenericService class
 GenericService class is an abstract, it is created to handle shared among all services
 the methods in it are:

 - All Retrieve Methods (RetrieveAllAsync, RetrieveAllAsync(With expression), RetrieveBy ).  
 - SaveAsync The Save Method that responsible for saving changes to the database.
 - Move IMapper, ILogger, IRepository, and IUnitOfWork to the GenericService.
 - Create a TName property to hold the name of the service' Class like (order,Color,etc..). 	
- ## Facade
	- The Facade pattern is used to provide a simplified interface to a complex subsystem.
	- move all constructor arguments to the ServiceFacade to simplify the constructor and reduandt code from all services.